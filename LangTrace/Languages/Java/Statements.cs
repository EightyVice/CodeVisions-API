using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{

	internal interface IStatementVisitor
	{
		void Visit(BlockStatement blockStmt);
		void Visit(IfStatement ifStmt);
		void Visit(ExpressionStatement exprStmt);
		void Visit(WhileStatement whileStmt);

	}
	internal abstract class Statement
	{
		public abstract void Accept(IStatementVisitor visitor);
	}

	
	internal class BlockStatement : Statement
	{
		public readonly List<Statement> InnerStatements;

		public BlockStatement(List<Statement> innerStatements)
		{
			InnerStatements = innerStatements;
		}

		public override void Accept(IStatementVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	internal class ExpressionStatement : Statement
	{
		public readonly Expression expression;

		public ExpressionStatement(Expression expression)
		{
			this.expression = expression;
		}

		public override void Accept(IStatementVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
	internal class IfStatement : Statement
	{
		public readonly Expression Condition;
		public readonly Statement ThenBranch;
		public readonly Statement ElseBranch;

		public IfStatement(Expression condition, Statement thenBranch, Statement elseBranch)
		{
			Condition = condition;
			ThenBranch = thenBranch;
			ElseBranch = elseBranch;
		}

		public override void Accept(IStatementVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	internal class WhileStatement : Statement
	{
		public readonly Expression Condition;
		public readonly Statement Body;

		public WhileStatement(Expression condition, Statement body)
		{
			Condition = condition;
			Body = body;
		}

		public override void Accept(IStatementVisitor visitor)
		{
			visitor.Visit(this);
		}
	}



	internal class Definition 
	{
		public readonly Declaration Declaration;
		public readonly Expression Initializer;

		public Definition(Declaration declaration, Expression initializer)
		{
			Declaration = declaration;
			Initializer = initializer;
		}
	}


	internal class LocalVariable
	{
		public readonly string Name;
		public readonly TypeDescriptor Type;
		public readonly Expression InitialValue;

		public LocalVariable(string name, TypeDescriptor type, Expression initialValue)
		{
			Name = name;
			Type = type;
			InitialValue = initialValue;
		}
	}

	internal class LocalVariableDeclaration
	{
		public readonly LocalVariable[] Variables;

		public LocalVariableDeclaration(LocalVariable[] variables)
		{
			Variables = variables;
		}
	}
}
