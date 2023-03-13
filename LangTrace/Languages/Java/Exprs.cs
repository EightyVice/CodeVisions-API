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
		public int ArrayDegree { get; set; }
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
}