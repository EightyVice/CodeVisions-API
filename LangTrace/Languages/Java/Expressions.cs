using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	public enum DataType
	{
		Int,
		Float,
		Pointer,
		Reference,
		Structure,
		Unknown
	}

	internal class Kind
	{
		public DataType DataType { get; set; }
		public bool IsPrimitive { get; set; }
		public bool IsPointerToPrimitive { get; set; }
		public bool IsArray { get; set; }
		public bool IsStructure { get; set; }
		public bool IsPointerToStructure { get; set; }
		public string ClassName { get; set; }
		public bool IsReference { get; set; }
	}
	internal class Variable : LValue
	{

		public Variable(string name, DataType type, IAtom value)
		{
			
			Name = name;
			Value = value;
			Type = type;
		}

		public string Name { get; set; }
		public IAtom Value { get; set; }
		public DataType Type { get; private set; }
		public int Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public bool IsAssignableTo(IAtom atom)
		{
			if (atom is Variable)
				return true;
			if (atom is FloatLiteral)
				return true;
			if (atom is IntLiteral)
				return true;
			return false;
		}

		public RValue ToRvalue()
		{
			return (RValue)Value;
		}
	}

	internal class Class
	{
		public string Name { get; set; }
	
		public List<(string name, Kind type)> Members { get; } = new List<(string name, Kind type)>();

		public Class(string name, List<(string name, Kind type)> members) {
			Name = name;
			Members = members;
		}
	}

	internal class Object
	{
		public string Name { get; set; }
		public string ClassName { get; set; }

		public Dictionary<string, LValue> Members = new Dictionary<string, LValue>();

		public static readonly Object NullRecord = new Object();

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"@{ClassName} {Name}");
			foreach(var member in Members)
			{
				if (member.Value is Variable)
					sb.AppendLine(member.Key + ": " + ((Literal)((Variable)member.Value).Value).GetLiteral().ToString());
				if (member.Value is Reference)
					sb.AppendLine(member.Key + ": " + ((Reference)member.Value).Object.ToString());
			}
			return sb.ToString();
		}
	}



	internal class Reference : LValue, IAtom
	{
		public string Name { get; set; }
		public string TypeName { get; set; }
		public Object Object { get; set; }

		public DataType Type => DataType.Pointer;

		public Reference(string name, Object @object)
		{
			Name = name;
			Object = @object;
		}

		public RValue ToRvalue()
		{
			throw new NotImplementedException();
		}

		public bool IsAssignableTo(IAtom atom)
		{
			if (atom is Reference)
			{
				if (this.Object.ClassName == ((Reference)atom).Object.ClassName)
					return true;
				return false;
			}

			if(atom is Object)
			{
				if (this.Object.ClassName == ((Object)atom).ClassName)
					return true;
				return false;
			}
			return false;
		}
		public override bool Equals(object? obj)
		{
			if(obj is Reference)
			{
				return this.Object == ((Reference)obj).Object;
			}
			return false;
		}
	}

	#region Literals


	internal class StringLiteral : Literal, IAtom, RValue
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
	internal class FloatLiteral : Literal, IAtom, RValue
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



	internal class IntLiteral : Literal, IAtom, RValue
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

	internal class ArrayVariable : LValue
	{
		public string Name { get; set; }
		public List<IAtom> Values { get; set; } = new List<IAtom>();
		public DataType Type { get; set; }
		public int Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public ArrayVariable(string name, params IAtom[] values)
		{
			Name = name;
			Values.AddRange(values);
		}

		public RValue ToRvalue()
		{
			throw new NotImplementedException();
		}

		public bool IsAssignableTo(IAtom atom)
		{
			// THINK: Should we support assignment to arrays? for me, only at declaration
			return false;
		}
	}


	#endregion
}
