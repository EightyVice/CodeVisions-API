using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.VirtualMachine
{

	internal abstract class Value { }

	internal class SInt8 : Value { 
		public readonly byte Value;
		public SInt8(byte value) => Value = value;
	}
	internal class SInt16 : Value {
		public readonly short Value;
		public SInt16(short value) => Value = value;
	}
	internal class SInt32 : Value {
		public readonly int Value;
		public SInt32(int value) => Value = value;

		public static SInt32 operator +(SInt32 a, SInt32 b) => new SInt32(a.Value + b.Value);
		public static SInt32 operator -(SInt32 a, SInt32 b) => new SInt32(a.Value - b.Value);
		public static SInt32 operator *(SInt32 a, SInt32 b) => new SInt32(a.Value * b.Value);
		public static SInt32 operator /(SInt32 a, SInt32 b) => new SInt32(a.Value / b.Value);
		public static SInt32 operator >(SInt32 a, SInt32 b) => new SInt32(a.Value > b.Value ? 1 : 0);
		public static SInt32 operator <(SInt32 a, SInt32 b) => new SInt32(a.Value < b.Value ? 1 : 0);
		public static SInt32 operator ==(SInt32 a, SInt32 b) => new SInt32(a.Value == b.Value ? 1 : 0);
		public static SInt32 operator !=(SInt32 a, SInt32 b) => new SInt32(a.Value > b.Value ? 1 : 0);
		public static SInt32 operator >=(SInt32 a, SInt32 b) => new SInt32(a.Value >= b.Value ? 1 : 0);
		public static SInt32 operator <=(SInt32 a, SInt32 b) => new SInt32(a.Value <= b.Value ? 1 : 0);


		public override string ToString()
        {
			return Value.ToString();
        }
    }
	internal class Float : Value {
		public readonly float Value;
		public Float(float value) => Value = value;
	}
	internal class Double : Value {
		public readonly double Value;
		public Double(double value) => Value = value;
	}
	internal class Char : Value {
		public readonly char Value;
		public Char(char value) => Value = value;
	}

}
