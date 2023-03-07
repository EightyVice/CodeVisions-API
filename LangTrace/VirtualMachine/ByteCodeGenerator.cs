using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.VirtualMachine
{

	internal enum Opcode
	{
		// No Operations
		NOP,

		// Stack
		PUSHI,
		PUSHD,
		PUSHF,
		PUSHS,
		PUSHB,
		PUSHC,
		PNULL,

		PSHI0,
		PSHI1,
		PSHI2,
		PSHI3,
		PSHI4,
		PSHI5,

		PSHD0,
		PSHD1,
		PSHD2,
		PSHD3,
		PSHD4,
		PSHD5,

		PSHF0,
		PSHF1,
		PSHF2,
		PSHF3,
		PSHF4,
		PSHF5,

		PIP,
		PFF,
		CPUSH,
		POP,

		// Arithmetics
		ADDI,
		SUBI,
		MULI,
		DIVI,
		MODI,

		// Comparison
		EQUL,
		NEQU,
		LESS,
		LEQU,
		MORE,
		MEQU,

		// Control Flow
		JMP,
		JMPF,
		JEQZ,
		JNEZ,
		CALL,
		RET,

		// Data Flow
		STORE,
		LOAD,


		// References
		NEW,
		LOADF,

		// IO
		PRNT,
		READ,

		// Trace Signaler
		SIG_VARDEC,
		SIG_PRINT,
		SIG_IF,
		SIG_LOOP,
	}

	/// <summary>
	/// A bytecode generator for RoaaVM
	/// </summary>
	internal class ByteCodeGenerator
	{
		List<byte> _buffer = new List<byte>();

		public int Length { get => _buffer.Count; }
		void addByte(byte b) => _buffer.Add(b);
		void addByte(Opcode opcode) => _buffer.Add((byte)opcode);
		public void AddBytes(params byte[] bytes) => _buffer.AddRange(bytes);
		byte[] getintBytes(int i) => BitConverter.GetBytes(i);
		

		public void PUSHI(int imm) {
			// Hardcoded Immediates for the most used integral values so it saves space
			if(imm >= 0 && imm  <= 5)
			{
				addByte(Opcode.PSHI0 + imm);
				return;
			}
			addByte(Opcode.PUSHI); 
			AddBytes(getintBytes(imm)); 
		}

		public void EmitOpcode(Opcode opcode) => addByte(opcode);

		public void POP() => addByte(Opcode.POP);

		public void STORE(byte id) { addByte(Opcode.STORE); addByte(id); }
		public void LOAD(byte id) { addByte(Opcode.LOAD); addByte(id); }

		public void JMP(byte offset) { addByte(Opcode.JMP); addByte(offset); }
		public void JEQZ(byte offset) { addByte(Opcode.JEQZ); addByte(offset); }
		public void JNEZ(byte offset) { addByte(Opcode.JNEZ); addByte(offset); }

		public void CPUSH(byte index) { addByte(Opcode.CPUSH); addByte(index); }

		public void NEW(byte index) { addByte(Opcode.NEW); addByte(index); }
		public void LOADF(byte index) { addByte(Opcode.LOADF); addByte(index); }

		public void PRINT() => addByte(Opcode.PRNT);
		public void CALL() => addByte(Opcode.CALL);
		public void PIP() => addByte(Opcode.PIP);
		public void RET() => addByte(Opcode.RET);
		public byte[] GetByteCode()
		{
			return _buffer.ToArray();
		}

		
	}
}
