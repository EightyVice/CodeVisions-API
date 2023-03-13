using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LangTrace.Utilities;

namespace LangTrace.Languages.Java
{
	internal class CompilationUnit
	{
		public Class[] Classes { get; }

        public CompilationUnit(Class[] classes)
        {
            Classes = classes;
        }
    }

	internal class JavaParserVisitor : JavaParserBaseVisitor<object>
	{
		
		DataType DataTypeFromString(string str)
        {
			switch (str)
			{
				case "byte":	return  DataType.Byte; 
				case "short":	return  DataType.Short; 
				case "int":		return  DataType.Int; 
				case "long":	return  DataType.Long; 
				case "float":	return  DataType.Float; 
				case "double":	return  DataType.Double; 
				case "bool":	return  DataType.Boolean;
				default:		return DataType.Void;
			}
			
		}

		TokenPosition GetPosition(Antlr4.Runtime.ParserRuleContext context) => new TokenPosition(context.Start.Line, context.Start.StartIndex, context.Stop.StopIndex);
		TypeDescriptor DescriptorFromTypeContext(JavaParser.TypeTypeContext type_ctx)
        {
			TypeDescriptor desc = new TypeDescriptor();

			// Is Primitive?
			if (type_ctx.GetText().Contains("[]"))
				desc.IsArray = true; // TODO: Array depth
            


			if (type_ctx.primitiveType() != null)
			{
				desc.IsPrimitive = true;
				desc.DataType = DataTypeFromString(type_ctx.primitiveType().GetText());
            }
            else
            {
				desc.IsReference = true;
				desc.ClassName = type_ctx.classOrInterfaceType().identifier(0).GetText();
            }

			return desc;
        }

		TypeDescriptor DescriptorFromTypeContext(JavaParser.TypeTypeOrVoidContext type_ctx)
        {
			if (type_ctx.VOID != null)
				return TypeDescriptor.Void;
		
			return DescriptorFromTypeContext(type_ctx.typeType());
		}
		public override CompilationUnit VisitCompilationUnit([NotNull] JavaParser.CompilationUnitContext context)
		{
			var classes = new List<Class>();
			foreach (var dec in context.typeDeclaration())
				classes.Add((Class)Visit(dec));

			return new CompilationUnit(classes.ToArray());
		}





        #region Declaration
		public override Method VisitMethodDeclaration([NotNull] JavaParser.MethodDeclarationContext context)
		{

			string methodName = context.identifier().GetText();
			var returnType = DescriptorFromTypeContext(context.typeTypeOrVoid());
			List<IStatement> bodyStmts = new List<IStatement>();
			List<LocalVariableDeclaration> locals = new List<LocalVariableDeclaration>();
			List<Declaration> funcParameters = new List<Declaration>();


			bool is_static = false;

			Antlr4.Runtime.RuleContext class_decl = context;
			while (class_decl is not JavaParser.ClassDeclarationContext)
				class_decl = class_decl.Parent;

			var class_name = ((JavaParser.ClassDeclarationContext)class_decl).identifier().GetText();


			var class_body_decl = (JavaParser.ClassBodyDeclarationContext)context.Parent.Parent;
			if ((bool)class_body_decl.modifier()?.Any(m => m.GetText() == "static"))
				is_static = true;

			// Parameters
			if (context.formalParameters().formalParameterList() != null)
			{
				var parameters = context.formalParameters().formalParameterList().formalParameter();
				funcParameters = new List<Declaration>();

				foreach (var param in parameters)
				{
					string paramType = param.typeType().GetText();
					string paramName = param.variableDeclaratorId().GetText();

					funcParameters.Add(new Declaration(TypeDescriptor.FromString(paramType), paramName));
				}
			}

			// Body
			if (context.methodBody != null)
			{
				locals = new List<LocalVariableDeclaration>();
				foreach (var stmt in context.methodBody().block().blockStatement())
				{
					if (stmt.localVariableDeclaration() != null)
					{
						var vardecl = (LocalVariableDeclaration)Visit(stmt.localVariableDeclaration());
						locals.Add(vardecl);

						foreach (var v in vardecl.Variables)
						{
							bodyStmts.Add(
								new ExpressionStatement(
									new Assignment(
										new Identifier(v.Name),
										v.InitialValue,
										GetPosition(stmt)
									), GetPosition(stmt) //TODO: assignment vs lhs start/end index
								)
							) ;
						}
					}
					else
					{
						object body_stmt = Visit(stmt.statement());
						bodyStmts.Add((IStatement)body_stmt);
					}
				}
			}

			return new Method(returnType, methodName, bodyStmts.ToArray(), funcParameters.ToArray(), locals.ToArray(), is_static, class_name);
		}
        public override Declaration[] VisitFieldDeclaration([NotNull] JavaParser.FieldDeclarationContext context)
        {
			var type_desc = DescriptorFromTypeContext(context.typeType());
			var fields = new List<Declaration>();
			foreach(var field in context.variableDeclarators().variableDeclarator())
            {
				var name = field.variableDeclaratorId().identifier().GetText();
				fields.Add(new Declaration(type_desc, name));
            }

			return fields.ToArray();
			
        }
        public override LocalVariableDeclaration VisitLocalVariableDeclaration([NotNull] JavaParser.LocalVariableDeclarationContext context)
		{
			TypeDescriptor decType = new TypeDescriptor();

			if(context.typeType().classOrInterfaceType() != null)
            {
				decType.IsReference = true;
				decType.ClassName = context.typeType().classOrInterfaceType().identifier(0).GetText();
            }else if(context.typeType().primitiveType() != null)
            {
				decType.IsPrimitive = true;
				decType.DataType = DataTypeFromString(context.typeType().GetText());
            }

			List<LocalVariable> localvars = new List<LocalVariable>();
			// Declaration Type
			foreach(var vardecl in context.variableDeclarators().variableDeclarator())
			{
				string decName = vardecl.variableDeclaratorId().GetText();

				IExpression init = null;
				// check initializer value
				if(vardecl.variableInitializer() != null)
				{
					if (vardecl.variableInitializer().expression() != null)
						init = (IExpression)Visit(vardecl.variableInitializer().expression());
					else
						init = (IExpression)Visit(vardecl.variableInitializer().arrayInitializer());
				}
				localvars.Add(new LocalVariable(decName, decType, init));
			}

			return new LocalVariableDeclaration(localvars.ToArray());
		}

        public override object VisitConstructorDeclaration([NotNull] JavaParser.ConstructorDeclarationContext context)
        {
			string methodName = context.identifier().GetText();
			TypeDescriptor returnType = null;
			var  bodyStmts = new List<IStatement>();
			var locals = new List<LocalVariableDeclaration>();
			var funcParameters = new List<Declaration>();


			// Parameters
			if (context.formalParameters().formalParameterList() != null)
			{
				var parameters = context.formalParameters().formalParameterList().formalParameter();
				funcParameters = new List<Declaration>();

				foreach (var param in parameters)
				{
					string paramType = param.typeType().GetText();
					string paramName = param.variableDeclaratorId().GetText();

					funcParameters.Add(new Declaration(TypeDescriptor.FromString(paramType), paramName));
				}
			}

			// Body
			if (context.block() != null)
			{
				locals = new List<LocalVariableDeclaration>();
				foreach (var stmt in context.block().blockStatement())
				{
					if (stmt.localVariableDeclaration() != null)
					{
						var vardecl = (LocalVariableDeclaration)Visit(stmt.localVariableDeclaration());
						locals.Add(vardecl);

						foreach (var v in vardecl.Variables)
						{
							bodyStmts.Add(
								new ExpressionStatement(
									new Assignment(
										new Identifier(v.Name),
										v.InitialValue,
										GetPosition(stmt)
									), GetPosition(stmt)
								)
							) ;
						}
					}
					else
					{
						object body_stmt = Visit(stmt.statement());
						bodyStmts.Add((IStatement)body_stmt);
					}
				}
			}

			return new Method(null, $"ctor:{methodName}", bodyStmts.ToArray(), funcParameters.ToArray(), locals.ToArray(), false, methodName);
		}
        public override Class VisitClassDeclaration([NotNull] JavaParser.ClassDeclarationContext context)
        {
			var name = context.identifier().GetText();
			var methods = new List<Method>();
			var fields = new List<Declaration>();
			foreach(var stmt_ctx in context.classBody().classBodyDeclaration())
            {
				
				var stmt = Visit(stmt_ctx);
				// Field
				if(stmt is Declaration[]) fields.AddRange((Declaration[])stmt);

				// Method
				if (stmt is Method) methods.Add((Method)stmt);

            }

			return new Class(name, fields.ToArray(), methods.ToArray());
        }
		#endregion

		#region Statements
		public override object VisitExpressionStatement([NotNull] JavaParser.ExpressionStatementContext context)
		{
			return new ExpressionStatement((IExpression)Visit(context.expression()), new TokenPosition(context.Start.Line, context.Start.StartIndex, context.Stop.StopIndex));
		}

		public override object VisitIfStatment([NotNull] JavaParser.IfStatmentContext context)
		{
			var condExpr = (IExpression)Visit(context.parExpression().expression());
			var true_stmt = (IStatement)Visit(context.statement(0));
			IStatement else_stmt = null;

			if (context.ELSE() != null) else_stmt = (IStatement)Visit(context.statement(1));

			return new IfStatement(condExpr, true_stmt, else_stmt, new TokenPosition(context.Start.Line, context.Start.StartIndex, context.Stop.StopIndex));
		}

		public override object VisitWhileStatement([NotNull] JavaParser.WhileStatementContext context)
		{
			var condExpr = (IExpression)Visit(context.parExpression().expression());
			var body = (IStatement)Visit(context.statement());

			return new WhileStatement(condExpr, body, new TokenPosition(context.Start.Line, context.Start.StartIndex, context.Stop.StopIndex));
	
		}
		#endregion
		public override object VisitStatement([NotNull] JavaParser.StatementContext context)
		{			
			return VisitChildren(context);
		}

        #region Expression
        public override object VisitArrayInitializer([NotNull] JavaParser.ArrayInitializerContext context)
        {
			var exprs = new List<IExpression>();
			foreach (var exp in context.variableInitializer())
				exprs.Add((IExpression)Visit(exp));

			return new ArrayExpression(exprs.ToArray(), GetPosition(context)); 
		}
        public override object VisitTermExpr([NotNull] JavaParser.TermExprContext context)
		{
			IExpression lhs = (IExpression)Visit(context.expression(0));
			IExpression rhs = (IExpression)Visit(context.expression(1));
			string op = context.bop.Text;
			if (op == "+")
				return new BinaryExpression(lhs, rhs, ArithmeticOperator.Plus);
			else
				return new BinaryExpression(lhs, rhs, ArithmeticOperator.Minus);
		}

		public override object VisitFactorExpr([NotNull] JavaParser.FactorExprContext context)
		{
			IExpression lhs = (IExpression)Visit(context.expression(0));
			IExpression rhs = (IExpression)Visit(context.expression(1));
			string op = context.bop.Text;
			if (op == "*")
				return new BinaryExpression(lhs, rhs, ArithmeticOperator.Asterisk);
			else
				return new BinaryExpression(lhs, rhs, ArithmeticOperator.Slash);
		}

		public override object VisitCompareExpr([NotNull] JavaParser.CompareExprContext context)
		{
			IExpression lhs = (IExpression)Visit(context.expression(0));
			IExpression rhs = (IExpression)Visit(context.expression(1));
			string op = context.bop.Text;

			switch (op)
			{
				case ">": return new BinaryExpression(lhs, rhs, ArithmeticOperator.Greater); break;
				case ">=": return new BinaryExpression(lhs, rhs, ArithmeticOperator.GreaterEqual); break;
				case "<": return new BinaryExpression(lhs, rhs, ArithmeticOperator.Less); break;
				case "<=": default: return new BinaryExpression(lhs, rhs, ArithmeticOperator.LessEqual); break;
			}

		}
		public override IExpression VisitMethodCall([NotNull] JavaParser.MethodCallContext context)
		{
			string name = context.identifier().GetText();
			List<IExpression> args = new List<IExpression>();

			if(context.expressionList() != null) { 
				foreach (var arg in context.expressionList().expression()) args.Add((IExpression)Visit(arg));
			}
			return new FunctionCall(name, args, GetPosition(context));

		}
		public override IExpression VisitIntegerLiteral([NotNull] JavaParser.IntegerLiteralContext context)
		{
			return new Integer(int.Parse(context.GetText()));
		}
        public override object VisitLiteral([NotNull] JavaParser.LiteralContext context)
        {
			if(context.BOOL_LITERAL() != null)
				return new Boolean(Convert.ToBoolean(context.BOOL_LITERAL().GetText()));

			if (context.NULL_LITERAL() != null)
				return Null.Reference;

			return VisitChildren(context);
        }
        public override object VisitIdentifier([NotNull] JavaParser.IdentifierContext context)
		{
			return new Identifier(context.IDENTIFIER().GetText());
		}
		public override object VisitAssignExpr([NotNull] JavaParser.AssignExprContext context)
		{
			
			IExpression lhs = (IExpression)Visit(context.expression(0));
			IExpression rhs = (IExpression)Visit(context.expression(1));

			if (context.bop.Text == "=")
				return new Assignment(lhs, rhs, GetPosition(context));

			return null;
		}
		
        public override object VisitNewExpr([NotNull] JavaParser.NewExprContext context)
        {
			// Class Constructor
			if(context.creator().classCreatorRest() != null)
            {
				var class_name = context.creator().createdName().identifier(0).GetText();
				var ctor_ctx = context.creator().classCreatorRest();
				return new ConstructorCall(
					class_name,
					ctor_ctx.arguments().expressionList()?.expression().Select(arg => (IExpression)Visit(arg)).ToArray(),
					GetPosition(context));
            }

			// Array Constructor
			if(context.creator().arrayCreatorRest() != null)
            {

            }
			return null;
        }

        public override object VisitDotExpr([NotNull] JavaParser.DotExprContext context)
        {
			if (context.identifier() != null) 
				return new FieldAccess((IExpression)Visit(context.expression()), context.identifier().GetText());
			return null;
        }
        #endregion
    }
}
