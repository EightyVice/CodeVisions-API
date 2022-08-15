using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime.Misc;

namespace LangTrace.Languages.Java
{
	internal class ParserVisitor : JavaBaseVisitor<IAtom>
	{
		public JavaInterpreter VM { get; }

		public ParserVisitor(JavaInterpreter vm)
		{
			VM = vm;
		}

		#region Helpers
		private IAtom ApplyOperator(IAtom lhs, IAtom rhs, char op)
		{
			if (op == '+')
			{
				if (lhs is IntLiteral && rhs is IntLiteral)
					return ((IntLiteral)lhs) + ((IntLiteral)rhs);

				if (lhs is IntLiteral && rhs is FloatLiteral)
					return ((IntLiteral)lhs) + ((FloatLiteral)rhs);

				if (lhs is FloatLiteral && rhs is IntLiteral)
					return ((FloatLiteral)lhs) + ((IntLiteral)rhs);

				if (lhs is FloatLiteral && rhs is FloatLiteral)
					return ((FloatLiteral)lhs) + ((FloatLiteral)rhs);
			}
			else if (op == '-')
			{
				if (lhs is IntLiteral && rhs is IntLiteral)
					return ((IntLiteral)lhs) - ((IntLiteral)rhs);

				if (lhs is IntLiteral && rhs is FloatLiteral)
					return ((IntLiteral)lhs) - ((FloatLiteral)rhs);

				if (lhs is FloatLiteral && rhs is IntLiteral)
					return ((FloatLiteral)lhs) - ((IntLiteral)rhs);

				if (lhs is FloatLiteral && rhs is FloatLiteral)
					return ((FloatLiteral)lhs) - ((FloatLiteral)rhs);
			}
			return null;
		}
		private bool IsAssignableTo(IAtom destination, IAtom source)
		{
			if (
				destination is IntLiteral && source is IntLiteral ||
				destination is IntLiteral && source is FloatLiteral ||
				destination is FloatLiteral && source is IntLiteral ||
				destination is FloatLiteral && source is FloatLiteral
			) return true;

			return false;
		}
		private bool IsAssignableToType(DataType destination, IAtom source)
		{
			if (
				destination == DataType.Int && source is IntLiteral ||
				destination == DataType.Int && source is FloatLiteral ||
				destination == DataType.Float && source is IntLiteral ||
				destination == DataType.Float && source is FloatLiteral
			) return true;

			return false;
		}
		private bool IsKindLikeLiteral(Kind kind, IAtom literal)
		{
			if (kind.IsPrimitive)
			{
				if (literal is Variable)
					literal = ((Variable)literal).Value;

				if (literal is FloatLiteral) return true;
				if (literal is IntLiteral) return true;
				return false;
			}
			return true;
		}
		private DataType GetTypeFromString(string type)
		{
			switch (type)
			{
				case "int": return DataType.Int;
				case "float": return DataType.Float;
				default: return DataType.Unknown;
			}
		}

		private bool IsTypeFromStringPrimitive(string type)
		{
			if (type == "int") return true;
			if (type == "float") return true;
			else return false;
		}

		#endregion

		#region Declaration
		public override IAtom VisitDeclList([NotNull] JavaParser.DeclListContext context)
		{
			Step step = new Step();
			step.GetFromParsingContext(context);
			step.Event = new Event(EventType.DeclareLinkedList);

			string name = context.linkedList().identifier().GetText();
			string type = context.linkedList().type().GetText();
			step.Event.Arguments.Add(type);
			step.Event.Arguments.Add(name);
			LinkedList list = new LinkedList();
			list.Name = name;
			Kind kind = new Kind() { IsPrimitive = true };
			list.NodesKind = kind;
			if (context.linkedList().arrayInit() != null)
			{
				var inits = context.linkedList().arrayInit().initializer();
				foreach (var init in inits)
				{
					IAtom i = Visit(init);
					if (IsKindLikeLiteral(kind, i))
					{
						list.Add(new Variable("<unnamed>", DataType.Int, i));
						step.Event.Arguments.Add(((Literal)i).GetLiteral().ToString());
					}
				}

			}
			VM.Steps.Add(step);
			return null;
		}
		public override IAtom VisitDeclClass([NotNull] JavaParser.DeclClassContext context)
		{
			Step step = new Step();
			step.GetFromParsingContext(context);
			step.Event = new Event(EventType.DeclareClass);

			// class name
			string name = context.classDec().identifier().GetText();
			step.Event.Arguments.Add(name);

			List<(string name, Kind type)> members = new List<(string name, Kind type)>();

			foreach (var memberdecl in context.classDec().memberDecl())
			{
				Kind kind = new Kind();
				string memName = null;
				string memType = null;

				if (memberdecl.type() != null)
				{
					memType = memberdecl.type().GetText();
					memName = memberdecl.identifier(0).GetText();
				}
				else
				{
					memType = memberdecl.identifier(0).GetText();
					memName = memberdecl.identifier(1).GetText();
				}


				if (IsTypeFromStringPrimitive(memType))
				{
					kind.IsPrimitive = true;
					kind.DataType = GetTypeFromString(memType);
				}
				else
				{
					if (memType != name)
						VM.Environment.GetStructure(memType);

					kind.IsReference = true;
					kind.ClassName = memType;
				}

				members.Add((memName, kind));
				step.Event.Arguments.Add($"{memType} {memName}");
			}

			VM.Environment.DefineStructure(new Class(name, members));
			VM.Steps.Add(step);
			return null;
		}

		public override IAtom VisitDeclReference([NotNull] JavaParser.DeclReferenceContext context)
		{
			Step step = null;
			string structName = context.identifier().GetText();

			if (VM.Environment.GetStructure(structName) != null)
			{
				foreach (var declarator in context.declarators().declarator())
				{

					string name = declarator.identifier().GetText();

					IAtom init = null;
					Object rec = null;

					step = new Step();
					step.GetFromParsingContext(context);
					step.Event = new Event(EventType.InitReference);
					step.Event.Arguments.Add(structName);
					step.Event.Arguments.Add(name);
					if (declarator.initializer() != null)
					{
						init = Visit(declarator.initializer().expression());

						if (init is Reference)
							rec = ((Reference)init).Object;
						else
							throw new CompileErrorException("Can't assign ");

						step.Event.Arguments.Add("<new>");
					}
					else
					{
						rec = Object.NullRecord;
						step.Event.Arguments.Add("null");
					}

					Reference reference = new Reference(name, rec);

					VM.Environment.DefineVariable(reference);
				}
			}

			VM.Steps.Add(step);
			return null;
		}


		public override IAtom VisitExprConstructor([NotNull] JavaParser.ExprConstructorContext context)
		{
			Object obj = new Object();
			obj.Name = "<unnamed>";
			string className = context.identifier().GetText();

			Reference reference = new Reference(className, obj);
			reference.Name = "<unnamed>";

			Class strct = VM.Environment.GetStructure(className);
			foreach (var member in strct.Members)
			{
				if (member.type.IsPrimitive)
				{
					if (member.type.DataType == DataType.Int)
						obj.Members.Add(member.name, new Variable(member.name, member.type.DataType, new IntLiteral(0)));

					if (member.type.DataType == DataType.Float)
						obj.Members.Add(member.name, new Variable(member.name, member.type.DataType, new FloatLiteral(0)));
				}
				else if (member.type.IsReference)
				{
					obj.Members.Add(member.name, new Reference(member.name, Object.NullRecord));
				}
				else
					throw new CompileErrorException("something wrong");
			}

			return reference;
		}

		public override IAtom VisitDeclPrimitiveVar([NotNull] JavaParser.DeclPrimitiveVarContext context)
		{
			Step step = new Step();
			step.GetFromParsingContext(context);

			var typeName = context.type().GetText();
			var _type = GetTypeFromString(typeName);

			foreach (var declarator in context.declarators().declarator())
			{
				LValue primitive = null;
				string _name = declarator.identifier().GetText();
				var initializer = declarator.initializer();
				
				if (initializer.arrayInit() != null)
				{
					step.Event = new Event(EventType.InitArray);
					step.Event.Arguments.Add(typeName);
					step.Event.Arguments.Add(_name);

					List<Variable> inits = new List<Variable>();
					// For now, first-dimension arrays only
					foreach (var init in initializer.arrayInit().initializer())
					{
						IAtom initVal = Visit(init);
						if (initVal is Variable)
							initVal = ((Variable)initVal).Value;

						// todo: Check if same types
						if (initVal is FloatLiteral || initVal is IntLiteral)
							inits.Add(new Variable($"{_name}[]", _type, initVal));
						else
							throw new CompileErrorException("Different types");

						step.Event.Arguments.Add(((Literal)initVal).GetLiteral().ToString());
						primitive = new ArrayVariable(_name, inits.ToArray());
					}
				}
				else
				{
					step.Event = new Event(EventType.InitVariable);
					step.Event.Arguments.Add(typeName);
					step.Event.Arguments.Add(_name);
					IAtom val = Visit(initializer.expression());
					if(val is Variable)
						val = ((Variable)val).Value;

					if (val is FloatLiteral || val is IntLiteral)
						primitive = new Variable(_name, _type, val);
					else
						throw new CompileErrorException("Can't assign!");
					step.Event.Arguments.Add(((Literal)val).GetLiteral().ToString());
				}

				VM.Environment.DefineVariable(primitive);
				VM.Steps.Add(step);
			}

			return null;
		}
		#endregion


		#region Statements
		private bool IsTruthy(IAtom conditionExpr)
		{
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
		public override IAtom VisitExprMemberAcess([NotNull] JavaParser.ExprMemberAcessContext context)
		{
			IAtom _ref = Visit(context.expression());
			if (_ref is Reference == false)
				throw new CompileErrorException("Only can access references");

			if (((Reference)_ref).Object == Object.NullRecord)
				throw new CompileErrorException("NullReferenceException");

			string member = context.identifier().GetText();

			var members = ((Reference)_ref).Object.Members;

			if (members.ContainsKey(member))
				return members[member];
			else
				throw new CompileErrorException($"record {((Object)_ref).Name} doesn't have member '{member}'");
		}
		public override IAtom VisitExprArraySubscription([NotNull] JavaParser.ExprArraySubscriptionContext context)
		{
			IAtom arrExpr = Visit(context.expression(0));
			IAtom index = Visit(context.expression(1));
			if(arrExpr is ArrayVariable)
			{
				ArrayVariable array = ((ArrayVariable)arrExpr);
				if(index is IntLiteral || index is FloatLiteral)
				{
					int i = Convert.ToInt32(((Literal)index).GetLiteral());
					if (i < 0) throw new CompileErrorException("An index can't be a negative number");
					if (i >= array.Values.Count) throw new CompileErrorException("Index beyond size exception");

					return array.Values[i];
				}
			}
			else
			{
				throw new CompileErrorException("Only can subscript arrays");
			}
			return null;
		}
		public override IAtom VisitStmtIf([NotNull] JavaParser.StmtIfContext context)
		{
			IAtom conditionExpr = Visit(context.exprpar().expression());
			bool condition = IsTruthy(conditionExpr);

			if (condition)
				Visit(context.statement(0));
			else
				Visit(context.statement(1));

			return null;
		}


		#endregion

		#region Expressions
		public override IAtom VisitExprAS([NotNull] JavaParser.ExprASContext context)
		{
			IAtom lhs = Visit(context.expression(0));
			IAtom rhs = Visit(context.expression(1));
			string op = context.bop.Text;

			if (lhs is Variable)
				lhs = ((Variable)lhs).Value;

			if (rhs is Variable)
				rhs = ((Variable)rhs).Value;

			return ApplyOperator(lhs, rhs, op[0]);

			return null;
		}

		public override IAtom VisitExprRightAssociation([NotNull] JavaParser.ExprRightAssociationContext context)
		{
			IAtom lhs = Visit(context.expression(0));
			IAtom rhs = Visit(context.expression(1));

			if (lhs is LValue == false)
				throw new CompileErrorException("The left hand side has to be a lhs to be assigned to");


			string op = context.bop.Text;
			switch (op)
			{
				case "=":
					if (lhs is Reference)
					{
						Step step = new Step();
						step.GetFromParsingContext(context);
						step.Event = new Event(EventType.ReferenceChanged);

						if (rhs is Reference)
						{
							Reference left = (Reference)lhs;
							Reference right = (Reference)rhs;
							if (left.Object.ClassName == right.Object.ClassName)
								left.Object = right.Object;
							else
								throw new CompileErrorException("Only can assign same reference types");

							step.Event.Arguments.Add(left.Name);
							step.Event.Arguments.Add(left.Object.Name);
							step.Event.Arguments.Add(right.Name);

							VM.Steps.Add(step);
							return right;
						}
						else
							throw new CompileErrorException("Only can assign reference types");
					}
					if (lhs is Variable)
					{
						Step step = new Step();
						step.GetFromParsingContext(context);
						step.Event = new Event(EventType.ValueChanged);

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

						step.Event.Arguments.Add(context.expression(0).GetText());
						step.Event.Arguments.Add(oldVal.ToString());
						step.Event.Arguments.Add(((Literal)rhs).GetLiteral().ToString());

						VM.Steps.Add(step);
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
						assignee.Value = ApplyOperator(left, right, op[0]);
						return assignee;
					}
					break;
			}
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
			return new FloatLiteral(float.Parse(context.GetText()));
		}

		public override IAtom VisitFuncCall([NotNull] JavaParser.FuncCallContext context)
		{
			object obj = Visit(context.expressionList().expression(0));

			if (context.identifier().GetText() == "typeof")
			{
				Console.WriteLine($"{context.expressionList().expression(0).GetText()} is {obj.ToString().Split('.')[^1]}");
				return null;
			}

			// RValue-ize the parameter 
			if (obj is Variable)
				obj = ((Variable)obj).Value;

			else if (obj is IntLiteral)
				obj = ((IntLiteral)obj).Value;

			Console.WriteLine(obj);
			return null;
			string name = context.identifier().GetText();
			List<object> args = new List<object>();
			foreach (var arg in context.expressionList().expression())
			{
				args.Add(Visit(arg));
			}

			//Statements.Add(new FunctionCall(name, args));
			return null;
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
