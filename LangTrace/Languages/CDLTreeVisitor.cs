using Antlr4.Runtime.Misc;
using LangTrace.Languages.Java;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.CDL
{
	internal class CDLTreeVisitor : CDLBaseVisitor<object>
	{
		private TesterResult _testResult;
		private InterpreterResult _interpreterResult;

		Dictionary<string, Callable> Functions;

		public CDLTreeVisitor(TesterResult result, InterpreterResult interpreterResult)
		{
			_testResult = result;
			_interpreterResult = interpreterResult;

			Functions = new Dictionary<string, Callable>()
			{
				{ "pass", new Callable(){
					Name = "pass",
					Arity = 0,
					Body = (args) => {
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Correct!");
						Console.ResetColor();
						return null;
					}
				}},
				{ "callsof", new Callable(){
					Name = "callsof",
					Arity = 1,
					Parameters = {("functionName", typeof(StringLiteral))},  
					Body = (args) => { 
						string fn = ((StringLiteral)args[0]).Value;
						int count = _interpreterResult.Metadata.FunctionsCalls[fn];
						QueryLog($"calls count of \"{fn}\" = {count} times.");
						return new NumberLiteral(count);
					}
				}},
				{ "lastassign", new Callable(){
					Name = "lastassign",
					Arity = 2,
					Parameters = {("lhs", typeof(StringLiteral)), ("rhs", typeof(StringLiteral))},
					Body = (args) => {
						string lhs = ((StringLiteral)args[0]).Value;
						string rhs = ((StringLiteral)args[1]).Value;
						bool result = false;
						if(_interpreterResult.Metadata.Assignments[lhs].Peek() == rhs)
							result = true;
						QueryLog($"last assignment of \"{lhs}\" was \"{rhs}\"");
						return new BooleanLiteral(result);
					}
				}},
				{ "obj", new Callable(){
					Name = "obj",
					Arity = 1,
					Parameters = {("objectId", typeof(NumberLiteral))},
					Body = (args) => {

						//QueryLog($"last assignment of \"{lhs}\" was \"{rhs}\"");
						return new BooleanLiteral(false);
					}
				}},
				{ "typeof", new Callable(){
					Name = "typeof",
					Arity = 1,
					Body = (args) => {
						Console.WriteLine($"{args[0]} is {args[1].ToString().Split('.')[^1]}");
						return null;
					}
				}}
			};

		}

		private void QueryLog(string text)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("[Tester Query] " + text);
			Console.ResetColor();
		}


		public bool IsTruthy(object conditionExpr)
		{
			if (conditionExpr is BooleanLiteral)
				return ((BooleanLiteral)conditionExpr).Value;

			if(conditionExpr is NumberLiteral)
			{
				if (((NumberLiteral)conditionExpr).Value == 0)
					return false;
				else
					return true;
			}

			return false;
		}

		public override object VisitCasestruct([NotNull] CDLParser.CasestructContext context)
		{
			bool caseResult = ((BooleanLiteral)Visit(context.casecond())).Value;
			string passMsg = null;
			string failMsg = null;

			if(context.expression() != null)
			{
				var pm = Visit(context.expression(0));
				var fm = Visit(context.expression(1));
				if (pm is StringLiteral && fm is StringLiteral)
				{
					passMsg = ((StringLiteral)pm).Value;
					failMsg = ((StringLiteral)fm).Value;
				}
				else throw new CompileErrorException("Case results has to be strings!!");
			}


			CaseResult result;
			if (caseResult)
				result = new CaseResult(caseResult, passMsg);
			else
				result = new CaseResult(caseResult, failMsg);

			_testResult.CaseResults.Add(result);
			return null;
		}

		public override object VisitCaseExpression([NotNull] CDLParser.CaseExpressionContext context)
		{
			var caseCond = Visit(context.expression());
			return new BooleanLiteral(IsTruthy(caseCond));
		}
		#region Expressions
		public override object VisitPrimary([NotNull] CDLParser.PrimaryContext context)
		{
			if(context.NUMBER() != null)
				return new NumberLiteral(float.Parse(context.NUMBER().GetText()));
			else if(context.STRING() != null)
				return new StringLiteral(context.STRING().GetText().Replace("\"", ""));
			else
				return new BooleanLiteral(Convert.ToBoolean(context.GetText()));
		}

		public override object VisitComparisonExpression([NotNull] CDLParser.ComparisonExpressionContext context)
		{
			var lhs = Visit(context.expression(0));
			var rhs = Visit(context.expression(1));

			string op = context.bop.Text;
			if(op == ">")
			{
				if(lhs is NumberLiteral && rhs is NumberLiteral)
					return ((NumberLiteral)lhs) > (NumberLiteral)rhs;
			}else if(op == "<")
			{
				if (lhs is NumberLiteral && rhs is NumberLiteral)
					return ((NumberLiteral)lhs) < (NumberLiteral)rhs;
			}

			return null;
		}

		public override object VisitEqualityExpression([NotNull] CDLParser.EqualityExpressionContext context)
		{
			var lhs = Visit(context.expression(0));
			var rhs = Visit(context.expression(1));

			string op = context.bop.Text;
			if (op == "==")
			{
				if (lhs is NumberLiteral && rhs is NumberLiteral)
					return ((NumberLiteral)lhs) == ((NumberLiteral)rhs);
			}
			else if (op == "!=")
			{
				if (lhs is NumberLiteral && rhs is NumberLiteral)
					return ((NumberLiteral)lhs) != (NumberLiteral)rhs;
			}

			return null;
		}

		public override object VisitTermExpression([NotNull] CDLParser.TermExpressionContext context)
		{
			var lhs = Visit(context.expression(0));
			var rhs = Visit(context.expression(1));

			string op = context.bop.Text;
			if (op == "+")
			{
				if (lhs is NumberLiteral && rhs is NumberLiteral)
					return ((NumberLiteral)lhs) + ((NumberLiteral)rhs);

				if (lhs is StringLiteral && rhs is StringLiteral)
					return ((StringLiteral)lhs) + ((StringLiteral)rhs);

				if (lhs is StringLiteral && rhs is NumberLiteral)
					return ((StringLiteral)lhs) + ((NumberLiteral)rhs);

				if (lhs is NumberLiteral && rhs is StringLiteral)
					return ((NumberLiteral)lhs) + ((StringLiteral)rhs);
			}
			else if (op == "-")
			{
				if (lhs is NumberLiteral && rhs is NumberLiteral)
					return ((NumberLiteral)lhs) != (NumberLiteral)rhs;
			}

			return null;
		}
		public override object VisitFunctionExpression([NotNull] CDLParser.FunctionExpressionContext context)
		{
			string funcName = context.identifier().GetText();

			int arity = 0;

			if (context.expressionList() != null)
				arity = context.expressionList().expression().Length;

			if (Functions.ContainsKey(funcName))
			{
				Callable func = Functions[funcName];

				if (func.Arity == arity)
				{
					List<object> arguments = new List<object>();
					for (int i = 0; i < arity; i++) arguments.Add(Visit(context.expressionList().expression(i)));
					return func.Body.Invoke(arguments.ToArray());
				}
				else
					throw new CompileErrorException("Different number of arguments");
			}

			return null;
		}
		#endregion
	}
}
