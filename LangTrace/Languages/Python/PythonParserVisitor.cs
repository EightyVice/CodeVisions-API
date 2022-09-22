using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Python
{
	internal class PythonParserVisitor : PythonBaseVisitor<object>
	{
		private Environment environment = new Environment();

		InterpreterResult _result;

		public PythonParserVisitor(InterpreterResult result)
		{
			_result = result;
		}

		#region Statements
		public override object VisitAssignment_stmt([NotNull] PythonParser.Assignment_stmtContext context)
		{
			string name = context.NAME().GetText();
			Literal expr = (Literal)Visit(context.expr());

			if (environment.variables.ContainsKey(name))
				environment.variables[name] = new Variable(name, expr);
			else
				environment.DefineVariable(new Variable(name, expr));

			return null;
		}

		public override object VisitIf_stmt([NotNull] PythonParser.If_stmtContext context)
		{
			var condexpr = Visit(context.expr());
			bool cond = IsTruthy(condexpr);
			if (cond)
			{
				Visit(context.suite(0));
			}
			else
			{
				if(context.suite(1) != null)
				{
					Visit(context.suite(1));
				}
			}
			return null;
		}

		public bool IsTruthy(object conditionExpr)
		{
			if (conditionExpr is BooleanLiteral)
				return ((BooleanLiteral)conditionExpr).Value;

			if (conditionExpr is NumberLiteral)
			{
				if (((NumberLiteral)conditionExpr).Value == 0)
					return false;
				else
					return true;
			}

			return false;
		}

		public override object VisitTermExpr([NotNull] PythonParser.TermExprContext context)
		{
			var lhs = Visit(context.expr(0));
			var rhs = Visit(context.expr(1));

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
		
		public override object VisitEqualExpr([NotNull] PythonParser.EqualExprContext context)
		{
			var lhs = Visit(context.expr(0));
			var rhs = Visit(context.expr(1));

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
		public override object VisitIdExpr([NotNull] PythonParser.IdExprContext context)
		{
			return environment.GetVariable(context.NAME().GetText());
		}
		#endregion
		#region Literals
		public override object VisitNumberExpr([NotNull] PythonParser.NumberExprContext context)
		{
			return new NumberLiteral(float.Parse(context.NUMBER().GetText()));
		}

		public override object VisitStringExpr([NotNull] PythonParser.StringExprContext context)
		{
			return new StringLiteral(context.STRING().GetText());
		}
		#endregion
	}
}
