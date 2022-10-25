using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime.Misc;

namespace LangTrace.Languages.Java
{
	internal class JavaParserVisitor : JavaBaseVisitor<IAtom>
	{
		private Environment environment = new Environment();

		InterpreterResult _result;

		private List<string> attributes = null;

		public JavaParserVisitor(InterpreterResult result)
		{
			_result = result;
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

		public override IAtom VisitUnit([NotNull] JavaParser.UnitContext context)
		{
			if(context.identifier().Length > 0)
			{
				attributes = new List<string>();

				foreach (var attr in context.identifier())
					attributes.Add(attr.GetText());

			}
			else
			{
				attributes = null;
			}

			return VisitChildren(context);
		}
		#region Declaration

		public override IAtom VisitDeclClass([NotNull] JavaParser.DeclClassContext context)
		{
			Step step = new Step();
			step.GetFromParsingContext(context, attributes);
			step.Event = new Event(EventType.DeclareClass);

			// class name
			string name = context.classDec().identifier().GetText();
			step.Event.Data = new
			{
				Name = name,
				Members = new List<dynamic>()
			};

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
						environment.GetStructure(memType);

					kind.IsReference = true;
					kind.ClassName = memType;
				}

				members.Add((memName, kind));
				step.Event.Data.Members.Add(new {Type = memType, Name = memName});

			}

			environment.DefineClass(new Class(name, members));
			_result.Metadata.Classes.Add(name, members);
			_result.Steps.Add(step);
			return null;
		}

		public override IAtom VisitDeclReference([NotNull] JavaParser.DeclReferenceContext context)
		{
			Step step = null;
			string structName = context.identifier().GetText();

			if (environment.GetStructure(structName) != null)
			{
				foreach (var declarator in context.declarators().declarator())
				{

					string name = declarator.identifier().GetText();

					IAtom init = null;
					Object rec = null;

					step = new Step();
					step.GetFromParsingContext(context, attributes);
					step.Event = new Event(EventType.InitReference);

					string objid = null;
					if (declarator.initializer() != null)
					{
						init = Visit(declarator.initializer().expression());

						if (init is Reference)
							rec = ((Reference)init).Object;
						else
							throw new CompileErrorException("Can't assign ");

						objid = rec.id.ToString();
					}
					else
					{
						rec = Object.NullObject;
						objid = "null";
					}

					step.Event.Data = new
					{
						Class = structName,
						Name = name,
						Init = objid
					};
					
					Reference reference = new Reference(name,structName, rec);
					environment.DefineVariable(reference);
					_result.Steps.Add(step);
				}
			}

			return null;
		}


		public override IAtom VisitExprConstructor([NotNull] JavaParser.ExprConstructorContext context)
		{
			string className = context.identifier().GetText();
			Class classDef = environment.GetStructure(className);
			return environment.InitObject(classDef);
		}

		public override IAtom VisitDeclPrimitiveVar([NotNull] JavaParser.DeclPrimitiveVarContext context)
		{
			Step step = new Step();
			step.GetFromParsingContext(context, attributes);

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


					List<Variable> inits = new List<Variable>();
					List<string> values = new List<string>();

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

						values.Add(((Literal)initVal).GetLiteral().ToString());
						primitive = new ArrayVariable(_name, inits.ToArray());
					}
					step.Event.Data = new
					{
						Type = typeName,
						Name = _name,
						Values = values.ToArray()
					};
				}
				else
				{
					step.Event = new Event(EventType.InitVariable);
					IAtom val = Visit(initializer.expression());
					if(val is Variable)
						val = ((Variable)val).Value;

					if (val is FloatLiteral || val is IntLiteral)
						primitive = new Variable(_name, _type, val);
					else
						throw new CompileErrorException("Can't assign!");
					string value = (((Literal)val).GetLiteral().ToString());

					step.Event.Data = new
					{
						Type = typeName,
						Name = _name,
						Values = value
					};
				}

				environment.DefineVariable(primitive);
				_result.Steps.Add(step);
			}

			return null;
		}
#endregion


#region Statements
		private bool IsTruthy(IAtom conditionExpr)
		{

			if(conditionExpr is Reference)
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
		public override IAtom VisitStmtWhile([NotNull] JavaParser.StmtWhileContext context)
		{
			for (int i = 0; IsTruthy(Visit(context.exprpar().expression())) && i < 10; i++) // Limit while loops to 50 times
			{
				Visit(context.statement());				
			}
			_result.Metadata.LoopsUsed = true;
			return null;
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
			{
				if(members[member] is Reference)
					((Reference)members[member]).ParentObject = ((Reference)_ref).Object.id;
	
				return members[member];
			}
			else
				throw new CompileErrorException($"record {((Object)_ref).id} doesn't have member '{member}'");
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

			string condtext = context.exprpar().expression().GetText(); // condition

			IAtom conditionExpr = Visit(context.exprpar().expression());
			bool condition = IsTruthy(conditionExpr);

			if (condition)
			{
				Visit(context.statement(0));
			}
			else
			{ 
				if(context.statement(1) != null)
					Visit(context.statement(1));
			}

			Step step = new Step();
			step.GetFromParsingContext(context, attributes);
			step.Event = Event.Branch(condtext, condition);
			_result.Steps.Add(step);
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
		public override IAtom VisitExprRightAssociation([NotNull] JavaParser.ExprRightAssociationContext context)
		{
			IAtom lhs = Visit(context.expression(0));
			IAtom rhs = Visit(context.expression(1));
			string lname = context.expression(0).GetText();
			string rname = context.expression(1).GetText();

			if (lhs is LValue == false)
				throw new CompileErrorException("The left hand side has to be a lvalue to be assigned to");


			string op = context.bop.Text;
			switch (op)
			{
				case "=":
					if (lhs is Reference)
					{
						Step step = new Step();
						step.GetFromParsingContext(context, attributes);
						step.Event = new Event(EventType.ReferenceChanged);

						if (rhs is Reference)
						{
							Reference left = (Reference)lhs;
							Reference right = (Reference)rhs;
							Object oldObj = left.Object;

							/*
							 * Annonated Cases
							 */

							

							// accept nulls
							if (right.Object == Object.NullObject)
								left.Object = right.Object;

							if (left.TypeName == right.TypeName)
							{
								
								// Hardoding Node.next 
								if (left.TypeName == "Node" && left.Name == "next")
								{
									step.Event.EventID = EventType.SetLLNodeNext;
									step.Event.Data = new
									{
										LhsName = lname,
										OldToId = oldObj.id,
										ToId = right.Object.id,
										FromId = left.ParentObject,
										RhsName = rname
									};
								}
								else
								{
									step.Event.Data = new
									{
										LhsName = lname,
										OldId = oldObj.id,
										NewId = right.Object.id,
										ParentObject = left.ParentObject,
										RhsName = rname
									};
								}

								left.Object = right.Object;
							}
							else
								throw new CompileErrorException("Only can assign same reference types");



							_result.Metadata.Assign(lname, rname);

							_result.Steps.Add(step);
							return right;
						}
						else
							throw new CompileErrorException("Only can assign reference types");
					}
					if (lhs is Variable)
					{
						Step step = new Step();
						step.GetFromParsingContext(context, attributes);
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

						step.Event.Data = new
						{
							Lhs = context.expression(0).GetText(),
							OldValue = oldVal,
							NewValue = ((Literal)rhs).GetLiteral()
						};


						_result.Steps.Add(step);
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
			return new FloatLiteral(float.Parse(context.GetText().Replace("f", "")));
		}

		public override IAtom VisitExprFuncCall([NotNull] JavaParser.ExprFuncCallContext context)
		{

			string funcName = context.expression().GetText();
			int? arity = context.expressionList()?.expression().Length;
			if (funcName == "typeof")
			{
				object obj = Visit(context.expressionList().expression(0));
				Console.WriteLine($"{context.expressionList().expression(0).GetText()} is {obj.ToString().Split('.')[^1]}");
				return null;
			}

			if(funcName == "valueof") // Dummy print
			{
				object obj = Visit(context.expressionList().expression(0));
				if (obj is Variable)
					Console.WriteLine(((Literal)((Variable)obj).Value).GetLiteral().ToString());

				if (obj is Reference)
					Console.WriteLine(((Reference)obj).Object.ToString());

				return null; 
			}

			// Thanks to Roaa "vjns" Emad
			if(funcName == "CreateList")
			{
				Step _step = new Step();
				_step.GetFromParsingContext(context, attributes);
				_step.Event = new Event(EventType.DeclareLinkedList);

				if (arity == 0)
					throw new CompileErrorException("Can't create list with zero elements");
		
				Reference head = environment.InitObject(environment.NodeClass);		// Node head = new Node();
				Reference curr = head;      // curr = head;
				var arguments = context.expressionList().expression();
				List<string> items = new List<string>();

				for (int i = 0; i < arguments.Length; i++)
				{
					var arg = Visit(arguments[i]);
					if (arg is Variable)
						arg = ((Variable)arg).Value;

					if (arg is Reference)
						throw new CompileErrorException("Can't have lists of references");


					if (arg is IntLiteral)
					{
						items.Add(((IntLiteral)arg).Value.ToString());
						if (i == 0)
						{
							head.Object.Members["data"] = new Variable("<head.data>", DataType.Int, arg);
							continue;
						}

						Reference n = environment.InitObject(environment.NodeClass);                    // Node n = new Node();
						n.Name = "next";
						n.TypeName = "Node";
						n.Object.Members["data"] = new Variable("<node.data>", DataType.Int, arg);          // n.data = arg;
						curr.Object.Members["next"] = n;                                                    // curr.next = n;
						curr = n;                                                                           // curr = n;
					}
				}
				_step.Event.Data = items;
				_result.Steps.Add(_step);
				return head;																				// head
			}

			_result.Metadata.CallFunction(funcName);

			Step step = new Step();
			step.GetFromParsingContext(context, attributes);
			step.Event = new Event(EventType.CallFunction);

			step.Event.Data = new
			{
				Name = funcName,
				CallsCount = _result.Metadata.FunctionsCalls[funcName]
			};

			_result.Steps.Add(step);
			return null;

		}
		public override IAtom VisitExprGroupedExpression([NotNull] JavaParser.ExprGroupedExpressionContext context)
		{
			return Visit(context.expression());
		}
		public override IAtom VisitPrimaryIdentifier([NotNull] JavaParser.PrimaryIdentifierContext context)
		{
			if(context.identifier().GetText() == "null") return new Reference("null", null, Object.NullObject);
			
			return environment.GetLValue(context.identifier().GetText());
		}

#endregion

	}
}
