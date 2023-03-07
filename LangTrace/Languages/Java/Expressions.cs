using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	}
	internal abstract class Expression
	{
		public abstract void Accept(IExpressionVisitor visitor);
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

	internal class BinaryExpression : Expression
	{
		public readonly Expression Left;
		public readonly Expression Right;
		public readonly ArithmeticOperator Operator;

		public BinaryExpression(Expression left, Expression right, ArithmeticOperator @operator)
		{
			Left = left;
			Right = right;
			Operator = @operator;
		}

		public override void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	#endregion
	internal class Integer : Expression
	{
		public readonly int Value;
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

		public override void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	internal class Boolean : Expression
	{
		public readonly bool Value;
		public Boolean(bool value) => Value = value;

		public override bool Equals(object? obj)
		{
			if (obj is Boolean)
				return this.Value == ((Boolean)obj).Value;

			return false;
		}


		public override void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}


	}

    internal class Null : Expression
    {
		private Null() { }

		public static Null Reference = new Null();

        public override void Accept(IExpressionVisitor visitor)
        {
			visitor.Visit(this);
        }
    }
    internal class FunctionCall : Expression
	{
		public readonly string Name;
		public readonly List<Expression> Arguments;

		public FunctionCall(string name, List<Expression> arguments)
		{
			Name = name;
			Arguments = arguments;
		}

		public override void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}


	internal class ConstructorCall : Expression
    {
		public readonly string ClassName;
		public readonly Expression[] Arguments;

        public ConstructorCall(string className, Expression[] arguments)
        {
            ClassName = className;
            Arguments = arguments;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
			visitor.Visit(this);
        }
    }

	internal class FieldAccess : Expression
    {
		public Expression Reference { get; }
		public string Field { get; }

        public FieldAccess(Expression reference, string field)
        {
            Reference = reference;
            Field = field;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
			visitor.Visit(this);
        }
    }
	internal class Identifier : Expression
	{
		public readonly string Name;

		public Identifier(string name)
		{
			Name = name;
		}

		public override void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
	internal class Assignment : Expression
	{
		internal readonly Expression Lhs;
		internal readonly Expression Value;

		public Assignment(Expression lhs, Expression value)
		{
			Lhs = lhs;
			Value = value;
		}

		public override void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
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
