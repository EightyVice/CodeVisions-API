using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LangTrace.Utilities;

namespace LangTrace.Languages.Java
{
	internal interface IExpressionVisitor
	{
		void Visit(Integer integerExpr);
		void Visit(Boolean booleanExpr);
		void Visit(BinaryExpression binaryExpr);
		void Visit(FunctionCall callExpr);
		void Visit(Identifier idExpr);
		void Visit(Assignment assignmentExpr);
		void Visit(ConstructorCall ctorExpr);
		void Visit(Null nullExpr);
		void Visit(FieldAccess fieldAccessExpr);
		void Visit(ArrayExpression arrayExpr);

	}
	internal interface IExpression
	{
		public void Accept(IExpressionVisitor visitor);
		public TokenPosition Position { get; }
		public byte[] ToBytes();

	}

	#region Operators
	internal enum ArithmeticOperator
	{
		Plus,
		Minus,
		Asterisk,
		Slash,

		Equal,
		NotEqual,
		Greater,
		GreaterEqual,
		Less,
		LessEqual,
	}

	internal class BinaryExpression : IExpression
	{
		public readonly IExpression Left;
		public readonly IExpression Right;
		public readonly ArithmeticOperator Operator;

		public BinaryExpression(IExpression left, IExpression right, ArithmeticOperator @operator)
		{
			Left = left;
			Right = right;
			Operator = @operator;
		}

        public TokenPosition Position { get; }

        public void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }

	#endregion

	internal class ArrayExpression : IExpression
    {
		public IExpression[] Elements { get; }

        public TokenPosition Position { get; }

        public ArrayExpression(IExpression[] elements, TokenPosition position)
        {
            Elements = elements;
            Position = position;
        }

        public void Accept(IExpressionVisitor visitor)
        {
			visitor.Visit(this);
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
	internal class Integer : IExpression
	{
		public readonly int Value;

        public TokenPosition Position { get; }

        public Integer(int value) => Value = value;


		// int op int -> int
		public static Integer operator +(Integer lhs, Integer rhs) => new Integer(lhs.Value + rhs.Value);
		public static Integer operator -(Integer lhs, Integer rhs) => new Integer(lhs.Value - rhs.Value);
		public static Integer operator *(Integer lhs, Integer rhs) => new Integer(lhs.Value * rhs.Value);
		public static Integer operator /(Integer lhs, Integer rhs) => new Integer(lhs.Value / rhs.Value);

		// int op float -> float
		//public static FloatLiteral operator +(IntLiteral lhs, FloatLiteral rhs) => new FloatLiteral(lhs.Value + rhs.Value);
		//public static FloatLiteral operator -(IntLiteral lhs, FloatLiteral rhs) => new FloatLiteral(lhs.Value - rhs.Value);
		//public static FloatLiteral operator *(IntLiteral lhs, FloatLiteral rhs) => new FloatLiteral(lhs.Value * rhs.Value);
		//public static FloatLiteral operator /(IntLiteral lhs, FloatLiteral rhs) => new FloatLiteral(lhs.Value / rhs.Value);

		public override bool Equals(object? obj)
		{
			if (obj is Integer)
				return this.Value == ((Integer)obj).Value;

			return false;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }

	internal class Boolean : IExpression
	{
		public readonly bool Value;

        public TokenPosition Position { get; }

        public Boolean(bool value) => Value = value;

		public override bool Equals(object? obj)
		{
			if (obj is Boolean)
				return this.Value == ((Boolean)obj).Value;

			return false;
		}


		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }

    internal class Null : IExpression
    {
		private Null() { }

		public static Null Reference = new Null();

        public TokenPosition Position { get; }

        public void Accept(IExpressionVisitor visitor)
        {
			visitor.Visit(this);
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
    internal class FunctionCall : IExpression
	{
		public readonly string Name;
		public readonly List<IExpression> Arguments;

		public FunctionCall(string name, List<IExpression> arguments, TokenPosition position)
		{
			Name = name;
			Arguments = arguments;
			Position = position;
		}

		public TokenPosition Position { get; }

        public void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }


	internal class ConstructorCall : IExpression
    {
		public readonly string ClassName;
		public readonly IExpression[] Arguments;

        public ConstructorCall(string className, IExpression[] arguments, TokenPosition position)
        {
            ClassName = className;
            Arguments = arguments;
			Position = position;
		}

		public TokenPosition Position { get; }

        public void Accept(IExpressionVisitor visitor)
        {
			visitor.Visit(this);
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }

	internal class FieldAccess : IExpression
    {
		public IExpression Reference { get; }
		public string Field { get; }

        public TokenPosition Position { get; }

        public FieldAccess(IExpression reference, string field)
        {
            Reference = reference;
            Field = field;
        }

        public void Accept(IExpressionVisitor visitor)
        {
			visitor.Visit(this);
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
	internal class Identifier : IExpression
	{
		public readonly string Name;

		public Identifier(string name)
		{
			Name = name;
		}

        public TokenPosition Position { get; }

        public void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
	internal class Assignment : IExpression
	{
		internal readonly IExpression Lhs;
		internal readonly IExpression Value;

		public Assignment(IExpression lhs, IExpression value, TokenPosition position)
		{
			Lhs = lhs;
			Value = value;
			Position = position;
		}

        public TokenPosition Position { get; }

        public void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }

	internal class ObjectRef
	{
		public string ClassName { get; }

		public static readonly ObjectRef NullObject = new ObjectRef("<null>");

        public ObjectRef(string className)
        {
            ClassName = className;
        }

        public override string ToString()
		{
			return $"[{ClassName}]";
		}
	}
}
