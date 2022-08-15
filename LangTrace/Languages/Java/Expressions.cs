﻿using System;
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

		public RValue ToRvalue()
		{
			return (RValue)Value;
		}
	}

	internal class Class
	{
		public string Name { get; set; }
		public DataType Type => DataType.Structure;

		public List<(string name, Kind type)> Members { get; } = new List<(string name, Kind type)>();

		public Class(string name, List<(string name, Kind type)> members) {
			Name = name;
			Members = members;
		}
	}

	internal class Object : LValue
	{
		public string Name { get; set; }
		public string StructureName { get; set; }
		public DataType Type => DataType.Structure;

		public Dictionary<string, LValue> Members = new Dictionary<string, LValue>();

		public static readonly Object NullRecord = new Object();

		public RValue ToRvalue()
		{
			throw new NotImplementedException();
		}
	}



	internal class Reference : LValue
	{
		public string Name { get; set; }

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
	}

	#region Literals



	internal class FloatLiteral : Literal, IAtom, RValue
	{
		public readonly float Value;

		public FloatLiteral(float value)
		{
			Value = value;
		}

		public DataType Type => DataType.Float;
		
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

		public DataType Type => DataType.Int;

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
	}

	internal class LinkedList
	{
		public string Name { get; set; }
		public int Count { get; set; }
		private Node head;
		public Kind NodesKind { get; set; }
		public void Add(IAtom data)
		{
			if (head == null)
			{
				head = new Node();

				head.Value = data;
				head.Next = null;
			}
			else
			{
				Node toAdd = new Node();
				toAdd.Value = data;

				Node current = head;
				while (current.Next != null)
				{
					current = current.Next;
				}

				current.Next = toAdd;
			}
		}
	}

	internal class Node
	{
		public IAtom Value { get; set; }
		public Node Next { get; set; }
	}
	#endregion
}