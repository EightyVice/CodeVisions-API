using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	public enum DataType
	{
		Byte,
		Short,
		Int,
		Long,
		Float,
		Double,
		Boolean,
		Void,
	}

	internal class TypeDescriptor
	{
		public DataType DataType { get; set; }
		public bool IsPrimitive { get; set; }
		public bool IsArray { get; set; }
		public string ClassName { get; set; }
		public bool IsReference { get; set; }

		public readonly static TypeDescriptor Int = new TypeDescriptor()
		{
			DataType = DataType.Int,
			IsPrimitive = true,
		};

		public readonly static TypeDescriptor Bool = new TypeDescriptor()
		{
			DataType = DataType.Boolean,
			IsPrimitive = true,
		};

		public readonly static TypeDescriptor Void = new TypeDescriptor()
		{
			DataType = DataType.Void,
			IsPrimitive = false,
		};
		internal static TypeDescriptor FromString(string typeStr)
		{
			switch (typeStr)
			{
				case "int": return Int;
				case "bool": return Bool;
				default: return null;
			}
		}
	}







	#region Literals



	internal class StringLiteral : Literal, RValue
	{
		public readonly string Value;
		public StringLiteral(string value)
		{
			Value = value;
		}

		public static StringLiteral operator +(StringLiteral lhs, StringLiteral rhs) => new StringLiteral(lhs.Value + rhs.Value);

		public override object GetLiteral()
		{
			return Value;
		}
	}
	internal class FloatLiteral : Literal, RValue
	{
		public readonly float Value;

		public FloatLiteral(float value)
		{
			Value = value;
		}

		
		
		// float op float -> float
		public static FloatLiteral operator+(FloatLiteral lhs, FloatLiteral rhs) =>  new FloatLiteral(lhs.Value + rhs.Value);
		public static FloatLiteral operator-(FloatLiteral lhs, FloatLiteral rhs) =>  new FloatLiteral(lhs.Value - rhs.Value);
		public static FloatLiteral operator*(FloatLiteral lhs, FloatLiteral rhs) =>  new FloatLiteral(lhs.Value * rhs.Value);
		public static FloatLiteral operator/(FloatLiteral lhs, FloatLiteral rhs) =>  new FloatLiteral(lhs.Value / rhs.Value);

		// float op int -> float
		public static FloatLiteral operator +(FloatLiteral lhs, IntLiteral rhs) => new FloatLiteral(lhs.Value + rhs.Value);
		public static FloatLiteral operator -(FloatLiteral lhs, IntLiteral rhs) => new FloatLiteral(lhs.Value - rhs.Value);
		public static FloatLiteral operator *(FloatLiteral lhs, IntLiteral rhs) => new FloatLiteral(lhs.Value * rhs.Value);
		public static FloatLiteral operator /(FloatLiteral lhs, IntLiteral rhs) => new FloatLiteral(lhs.Value / rhs.Value);

		public override object GetLiteral()
		{
			return Value;
		}
	}



	internal class IntLiteral : Literal, RValue
	{ 
		public readonly int Value;
		
		public IntLiteral(int value)
		{
			Value = value;
		}


		// int op int -> int
		public static IntLiteral operator +(IntLiteral lhs, IntLiteral rhs) => new IntLiteral(lhs.Value + rhs.Value);
		public static IntLiteral operator -(IntLiteral lhs, IntLiteral rhs) => new IntLiteral(lhs.Value - rhs.Value);
		public static IntLiteral operator *(IntLiteral lhs, IntLiteral rhs) => new IntLiteral(lhs.Value * rhs.Value);
		public static IntLiteral operator /(IntLiteral lhs, IntLiteral rhs) => new IntLiteral(lhs.Value / rhs.Value);

		// int op float -> float
		public static FloatLiteral operator +(IntLiteral lhs, FloatLiteral rhs) => new FloatLiteral(lhs.Value + rhs.Value);
		public static FloatLiteral operator -(IntLiteral lhs, FloatLiteral rhs) => new FloatLiteral(lhs.Value - rhs.Value);
		public static FloatLiteral operator *(IntLiteral lhs, FloatLiteral rhs) => new FloatLiteral(lhs.Value * rhs.Value);
		public static FloatLiteral operator /(IntLiteral lhs, FloatLiteral rhs) => new FloatLiteral(lhs.Value / rhs.Value);

		public override object GetLiteral()
		{
			return Value;
		}

		public override bool Equals(object? obj)
		{
			if(obj is IntLiteral)
				return this.Value == ((IntLiteral)obj).Value;

			if (obj is FloatLiteral)
				return this.Value == ((FloatLiteral)obj).Value;

			return false;
		}

	}


	}


	#endregion