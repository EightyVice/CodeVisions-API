using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LangTrace.Utilities;
using Antlr4.Runtime.Tree;

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
			List<LocalVariable> locals = new List<LocalVariable>();
			List<Declaration> funcParameters = new List<Declaration>();


			bool is_static = false;

			Antlr4.Runtime.RuleContext class_decl = context;
			while (class_decl is not JavaParser.ClassDeclarationContext)
				class_decl = class_decl.Parent;

			var class_name = ((JavaParser.ClassDeclarationContext)class_decl).identifier().GetText();


			var class_body_decl = (JavaParser.ClassBodyDeclarationContext)context.Parent.Parent;
			if ((bool)class_body_decl.modifier()?.Any(m => m.GetText() == "static"))
				is_static = true;

			JavaMethodListener methodExtractor = new JavaMethodListener();
			ParseTreeWalker.Default.Walk(methodExtractor, context);

			// Parameters
			funcParameters = methodExtractor.Parameters.ToList();

			// Body
			if (context.methodBody != null)
			{

				locals = methodExtractor.Locals.ToList();
				
				foreach (var stmt_ctx in context.methodBody().block().blockStatement())
				{
					object stmt = null;
					if (stmt_ctx.statement() != null)
						stmt = Visit(stmt_ctx.statement());
					else
						stmt = Visit(stmt_ctx.localVariableDeclaration());


					if (stmt is IStatement[])
						bodyStmts.AddRange((IStatement[])stmt);
					else if (stmt is IStatement)
						bodyStmts.Add((IStatement)stmt);
					else
						throw new Exception();

				}
			}

			return new Method(returnType, methodName, bodyStmts.ToArray(), funcParameters.ToArray(), locals.ToArray(), is_static, class_name);
		}
		public override IStatement[] VisitLocalVariableDeclaration([NotNull] JavaParser.LocalVariableDeclarationContext context)
		{
			var assignment_stmt = new List<ExpressionStatement>();
			// Declaration Type
			foreach (var vardecl in context.variableDeclarators().variableDeclarator())
			{
				string decName = vardecl.variableDeclaratorId().GetText();

				IExpression init = null;
				// check initializer value
				if (vardecl.variableInitializer() != null)
				{
					if (vardecl.variableInitializer().expression() != null)
						init = (IExpression)Visit(vardecl.variableInitializer().expression());
					else
						init = (IExpression)Visit(vardecl.variableInitializer().arrayInitializer());
				}
				assignment_stmt.Add(
					new ExpressionStatement(
						new Assignment(
							new Identifier(decName), 
							init,
							GetPosition(context)
						),
						GetPosition(context)
					)
				);
			}

			return assignment_stmt.ToArray();
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
										null,
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

			//return new Method(null, $"ctor:{methodName}", bodyStmts.ToArray(), funcParameters.ToArray(), locals.ToArray(), false, methodName);
			return null;
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

        #region Statement
        public override object VisitReturnStatement([NotNull] JavaParser.ReturnStatementContext context)
        {
			return new ReturnStatement((IExpression)Visit(context.expression()), GetPosition(context));
        }

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

        public override object VisitForStatement([NotNull] JavaParser.ForStatementContext context)
        {
			// Syntactic-Sugarize it into While loop
			//	for(int i = c; cond; pstmt) stmt;
			//	->
			//  i = c; while(cond) {stmt; pstmt;}

			var forcontrol_ctx = context.forControl();
			var condition = forcontrol_ctx.expression();
			var update_ctx = forcontrol_ctx.forUpdate.expression(0);
			var init_ctx = forcontrol_ctx.forInit();

			var assignment = new ExpressionStatement(
				new Assignment(
					new Identifier(init_ctx.localVariableDeclaration().variableDeclarators().variableDeclarator(0).variableDeclaratorId().identifier().GetText()),
					(IExpression)Visit(init_ctx.localVariableDeclaration().variableDeclarators().variableDeclarator(0).variableInitializer().expression()),
					GetPosition(init_ctx)
				),
				GetPosition(init_ctx)
			);

			var update_stmt = new ExpressionStatement((IExpression)Visit(update_ctx), GetPosition(update_ctx));
			var body_ctx = (IStatement)Visit(context.statement());
			IStatement body = new BlockStatement(new[] { body_ctx, update_stmt }, GetPosition(update_ctx));

			return new IStatement[] { assignment, new WhileStatement((IExpression)Visit(condition), body, GetPosition(context)) };
        }
        public override object VisitWhileStatement([NotNull] JavaParser.WhileStatementContext context)
		{
			var condExpr = (IExpression)Visit(context.parExpression().expression());
			var body = (IStatement)Visit(context.statement());

			return new WhileStatement(condExpr, body, new TokenPosition(context.Start.Line, context.Start.StartIndex, context.Stop.StopIndex));
	
		}
		public override object VisitStatement([NotNull] JavaParser.StatementContext context)
		{			
			return VisitChildren(context);
		}
        public override object VisitBlockStatement([NotNull] JavaParser.BlockStatementContext context)
        {
			if (context.localVariableDeclaration() != null)
				return Visit(context.localVariableDeclaration());
			else
				return Visit(context.statement());
        }
        #endregion

        #region Expression

        public override object VisitPostExpr([NotNull] JavaParser.PostExprContext context)
        {
			var expr = Visit(context.expression());
			if (context.postfix.Text == "++")
				return new PostPreExpresion((IExpression)expr, PostPreOpeartor.PlusPlus, false, GetPosition(context));
			else
				return new PostPreExpresion((IExpression)expr, PostPreOpeartor.MinusMinus, false, GetPosition(context));
        }

        public override object VisitPreExpr([NotNull] JavaParser.PreExprContext context)
        {
			var expr = Visit(context.expression());
			if (context.prefix.Text == "++")
				return new PostPreExpresion((IExpression)expr, PostPreOpeartor.PlusPlus, true, GetPosition(context));
			else
				return new PostPreExpresion((IExpression)expr, PostPreOpeartor.MinusMinus, true, GetPosition(context));
		}
        public override object VisitSubscriptExpr([NotNull] JavaParser.SubscriptExprContext context)
        {
			return new ArrayAccess(
				(IExpression)Visit(context.expression(0)),
				(IExpression)Visit(context.expression(1)),
				GetPosition(context)
			);
        }
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
        public override object VisitEqualExpr([NotNull] JavaParser.EqualExprContext context)
        {
			IExpression lhs = (IExpression)Visit(context.expression(0));
			IExpression rhs = (IExpression)Visit(context.expression(1));
			string op = context.bop.Text;
			if (op == "==")
				return new BinaryExpression(lhs, rhs, ArithmeticOperator.Equal);
			else
				return new BinaryExpression(lhs, rhs, ArithmeticOperator.NotEqual);
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

            switch (context.bop.Text)
            {
				case "=": return new Assignment(lhs, rhs, GetPosition(context));
				case "+=": return new Assignment(lhs, new BinaryExpression(lhs, rhs, ArithmeticOperator.Plus), GetPosition(context));
				case "-=": return new Assignment(lhs, new BinaryExpression(lhs, rhs, ArithmeticOperator.Minus), GetPosition(context));
				case "*=": return new Assignment(lhs, new BinaryExpression(lhs, rhs, ArithmeticOperator.Asterisk), GetPosition(context));
				case "/=": return new Assignment(lhs, new BinaryExpression(lhs, rhs, ArithmeticOperator.Slash), GetPosition(context));
			}

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

        public override object VisitGroupingPrimary([NotNull] JavaParser.GroupingPrimaryContext context)
        {
			return Visit(context.expression());
        }
        #endregion
    }
}
