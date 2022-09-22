using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.CDL
{
	internal class Literal
	{
		public virtual object GetValue() { return null; }
	}

	internal class StringLiteral : Literal
	{
		public readonly string Value;
		public StringLiteral(string value)
		{
			Value = value;
		}

		public override object GetValue() => Value;

		// Appending
		public static StringLiteral operator+(StringLiteral lhs, StringLiteral rhs) => new StringLiteral(lhs.Value + rhs.Value);
		public static StringLiteral operator+(StringLiteral lhs, NumberLiteral rhs) => new StringLiteral(lhs.Value + rhs.Value);
		public static StringLiteral operator +(NumberLiteral lhs, StringLiteral rhs) => new StringLiteral(lhs.Value + rhs.Value);
	}

	internal class BooleanLiteral : Literal
	{
		public readonly bool Value;

		public BooleanLiteral(bool value)
		{
			Value = value;
		}

		public override object GetValue() => Value;

	}

	internal class NumberLiteral : Literal
	{
		public readonly float Value;

		public NumberLiteral(float value)
		{
			Value = value;
		}

		public override Object GetValue() => Value;

		// Arithmetic Operators
		public static NumberLiteral operator +(NumberLiteral lhs, NumberLiteral rhs) => new NumberLiteral(lhs.Value + rhs.Value);
		public static NumberLiteral operator -(NumberLiteral lhs, NumberLiteral rhs) => new NumberLiteral(lhs.Value - rhs.Value);
		public static NumberLiteral operator *(NumberLiteral lhs, NumberLiteral rhs) => new NumberLiteral(lhs.Value * rhs.Value);
		public static NumberLiteral operator /(NumberLiteral lhs, NumberLiteral rhs) => new NumberLiteral(lhs.Value / rhs.Value);

		// Comparison Operators
		public static BooleanLiteral operator >(NumberLiteral lhs, NumberLiteral rhs) => new BooleanLiteral(lhs.Value > rhs.Value);
		public static BooleanLiteral operator <(NumberLiteral lhs, NumberLiteral rhs) => new BooleanLiteral(lhs.Value < rhs.Value);
		public static BooleanLiteral operator ==(NumberLiteral lhs, NumberLiteral rhs) => new BooleanLiteral(lhs.Value == rhs.Value);
		public static BooleanLiteral operator !=(NumberLiteral lhs, NumberLiteral rhs) => new BooleanLiteral(lhs.Value != rhs.Value);
	}
}
