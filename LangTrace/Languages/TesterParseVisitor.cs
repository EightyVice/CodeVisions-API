using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime.Misc;
using LangTrace.Languages.Java;
using Object = LangTrace.Languages.Java.Object;

namespace LangTrace.Languages
{
	/*
	 * A very minimal C-like syntax interpreter based on the Java parser
	 * No declarations or definitions just basic statements with access to the VM's Environment and Metadata
	 */

	internal class TesterParseVisitor : JavaBaseVisitor<IAtom> 
	{
		private JavaInterpreter VM;

		Dictionary<string, Callable> Functions;
		public TesterParseVisitor(JavaInterpreter vm)
		{
			VM = vm;
			Functions = new Dictionary<string, Callable>()
			{
				{ "pass", new Callable(){
					Name = "pass",
					Arity = 0,
					Body = (args) => {
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Correct!");
						Console.ResetColor();
						VM.Tester.Success = true;
						return null;
					}
				}},
				{ "fail", new Callable(){
					Name = "fail",
					Arity = 0,
					Body = (args) => {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Wrong!");
						Console.ResetColor();
						VM.Tester.Success = false;
						return null;
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

		void Output(string text)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Tester Output: " + text);
			Console.ResetColor();
		}

		#region Statements
		private bool IsTruthy(IAtom conditionExpr)
		{

			if (conditionExpr is Reference)
			{
				if (((Reference)conditionExpr).Object == Object.NullObject)
					return false;
				else
					return true;
			}

			if (conditionExpr is Variable)
				conditionExpr = ((Variable)conditionExpr).Value;

			if (conditionExpr is IntLiteral)
			{
				if (((IntLiteral)conditionExpr).Value == 0)
					return false;
				else
					return true;
			}
			return false;
		}

		public override IAtom VisitStmtIf([NotNull] JavaParser.StmtIfContext context)
		{
			IAtom conditionExpr = Visit(context.exprpar().expression());
			bool condition = IsTruthy(conditionExpr);

			if (condition)
			{
				Visit(context.statement(0));
			}
			else
			{
				if (context.statement(1) != null)
					Visit(context.statement(1));
			}
			return null;
		}
		#endregion
		#region Expressions
		public override IAtom VisitExprRightAssociation([NotNull] JavaParser.ExprRightAssociationContext context)
		{
			IAtom lhs = Visit(context.expression(0));
			IAtom rhs = Visit(context.expression(1));

			if (lhs is LValue == false)
				throw new CompileErrorException("The left hand side has to be a lvalue to be assigned to");


			string op = context.bop.Text;
			switch (op)
			{
				case "=":
					if (lhs is Reference)
					{

						if (rhs is Reference)
						{
							Reference left = (Reference)lhs;
							Reference right = (Reference)rhs;
							if (left.Object.ClassName == right.Object.ClassName)
								left.Object = right.Object;
							else
								throw new CompileErrorException("Only can assign same reference types");
							return right;
						}
						else
							throw new CompileErrorException("Only can assign reference types");
					}
					if (lhs is Variable)
					{

						object oldVal = null, newVal = null;
						var left = ((Variable)lhs).Value;

						if (rhs is Variable)
							rhs = ((Variable)rhs).Value;

						if (left is FloatLiteral)
						{
							oldVal = ((FloatLiteral)left).Value;

							if (rhs is FloatLiteral)
								((Variable)lhs).Value = ((FloatLiteral)rhs);

							if (rhs is IntLiteral)
								((Variable)lhs).Value = new FloatLiteral(((IntLiteral)rhs).Value);

						}
						else if (left is IntLiteral)
						{
							oldVal = ((IntLiteral)left).Value;

							if (rhs is FloatLiteral)
								((Variable)lhs).Value = new IntLiteral((int)((FloatLiteral)rhs).Value);

							if (rhs is IntLiteral)
								((Variable)lhs).Value = ((IntLiteral)rhs);
						}
						return rhs;

					}
					break;
				default:

					if (lhs is Reference || rhs is Reference)
						throw new CompileErrorException("Ref op Ref is bad");
					if (lhs is Variable)
					{
						Variable assignee = (Variable)lhs;
						IAtom left = ((Variable)lhs).Value;

						if (rhs is Variable)
							rhs = ((Variable)rhs).Value;

						IAtom right = rhs;
						//assignee.Value = ApplyOperator(left, right, op[0]);
						return assignee;
					}
					break;
			}
			return null;

		}

		public override IAtom VisitExprEquality([NotNull] JavaParser.ExprEqualityContext context)
		{
			IAtom lhs = Visit(context.expression(0));
			IAtom rhs = Visit(context.expression(1));

			if (lhs is Variable)
				lhs = ((Variable)lhs).Value;

			if (rhs is Variable)
				rhs = ((Variable)rhs).Value;


			if (context.bop.Text == "==")
			{
				if (lhs.Equals(rhs)) return new IntLiteral(1);
			}
			else
			{
				if (!lhs.Equals(rhs)) return new IntLiteral(1);
			}
			return new IntLiteral(0);
		}
		public override IAtom VisitExprMemberAcess([NotNull] JavaParser.ExprMemberAcessContext context)
		{
			IAtom _ref = Visit(context.expression());
			if (_ref is Reference == false)
				throw new CompileErrorException("Only can access references");

			if (((Reference)_ref).Object == Object.NullObject)
				throw new CompileErrorException("NullReferenceException");

			string member = context.identifier().GetText();

			var members = ((Reference)_ref).Object.Members;

			if (members.ContainsKey(member))
				return members[member];
			else
				throw new CompileErrorException($"record {((Object)_ref).id} doesn't have member '{member}'");
		}

		public override IAtom VisitExprFuncCall([NotNull] JavaParser.ExprFuncCallContext context)
		{

			string funcName = context.expression().GetText();
			int arity = 0;

			if(context.expressionList() != null)
				arity = context.expressionList().expression().Length;

			List<IAtom> args = new List<IAtom>();
			for (int i = 0; i < arity; i++) args.Add(Visit(context.expressionList().expression(i)));

			if (Functions.ContainsKey(funcName))
			{
				Callable func = Functions[funcName];
				if (func.Arity == arity)
				{
					List<IAtom> arguments = new List<IAtom>();
					for (int i = 0; i < arity; i++) arguments.Add(Visit(context.expressionList().expression(i)));
					func.Body.Invoke(arguments.ToArray());
				}
				else 
					throw new CompileErrorException("Different number of arguments");
			}


			if (funcName == "valueof") // Dummy print
			{
				var obj = Visit(context.expressionList().expression(0));

				if (obj is Variable)
					Output(((Literal)((Variable)obj).Value).GetLiteral().ToString());

				if (obj is Reference)
					Output(((Reference)obj).Object.ToString());
				return null;
			}

			Console.ResetColor();
			return null;
		}

		#endregion

		#region Literals
		public override IAtom VisitIntegerLiteral([NotNull] JavaParser.IntegerLiteralContext context)
		{
			return new IntLiteral(int.Parse(context.GetText()));
		}

		public override IAtom VisitFloatLiteral([NotNull] JavaParser.FloatLiteralContext context)
		{
			return new FloatLiteral(float.Parse(context.GetText().Replace("f", "")));
		}

		public override IAtom VisitExprGroupedExpression([NotNull] JavaParser.ExprGroupedExpressionContext context)
		{
			return Visit(context.expression());
		}
		public override IAtom VisitPrimaryIdentifier([NotNull] JavaParser.PrimaryIdentifierContext context)
		{
			return VM.Environment.GetLValue(context.identifier().GetText());
		}

		#endregion

	}
}
