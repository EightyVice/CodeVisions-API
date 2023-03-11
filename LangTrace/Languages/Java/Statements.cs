using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LangTrace.Utilities;

namespace LangTrace.Languages.Java
{

	internal interface IStatementVisitor
	{
		void Visit(BlockStatement blockStmt);
		void Visit(IfStatement ifStmt);
		void Visit(ExpressionStatement exprStmt);
		void Visit(WhileStatement whileStmt);

	}

	internal interface IStatement
	{
		public void Accept(IStatementVisitor visitor);
		public TokenPosition Position { get; }
	}

	
	internal class BlockStatement : IStatement
	{
		public readonly List<IStatement> InnerStatements;
        public TokenPosition Position { get; }

		public BlockStatement(List<IStatement> innerStatements, TokenPosition position)
		{
			InnerStatements = innerStatements;
			Position = position;
		}


        public void Accept(IStatementVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	internal class ExpressionStatement : IStatement
	{
		public readonly IExpression expression;

		public ExpressionStatement(IExpression expression, TokenPosition position)
		{
			this.expression = expression;
			Position = position;
		}

        public TokenPosition Position { get; }

        public void Accept(IStatementVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
	internal class IfStatement : IStatement
	{
		public readonly IExpression Condition;
		public readonly IStatement ThenBranch;
		public readonly IStatement ElseBranch;

		public IfStatement(IExpression condition, IStatement thenBranch, IStatement elseBranch, TokenPosition position)
		{
			Condition = condition;
			ThenBranch = thenBranch;
			ElseBranch = elseBranch;
			Position = position;
		}

        public TokenPosition Position { get; }

        public void Accept(IStatementVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	internal class WhileStatement : IStatement
	{
		public readonly IExpression Condition;
		public readonly IStatement Body;

		public WhileStatement(IExpression condition, IStatement body, TokenPosition position)
        {
            Condition = condition;
            Body = body;
            Position = position;
        }

        public TokenPosition Position { get; }

        public void Accept(IStatementVisitor visitor)
		{
			visitor.Visit(this);
		}
	}



	internal class Definition 
	{
		public readonly Declaration Declaration;
		public readonly IExpression Initializer;

		public Definition(Declaration declaration, IExpression initializer)
		{
			Declaration = declaration;
			Initializer = initializer;
		}
	}


	internal class LocalVariable
	{
		public readonly string Name;
		public readonly TypeDescriptor Type;
		public readonly IExpression InitialValue;

		public LocalVariable(string name, TypeDescriptor type, IExpression initialValue)
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
