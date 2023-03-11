﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LangTrace.Utilities;

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
		FLOAD,
		FSTOR,

		// IO
		PRNT,
		READ,

        // Breakpoint
        BRKPNT,

		// Trace Signaler (IMPDP)
		IMPDP,
	}

    // Trace Signaler Opcodes
    internal enum TracerOpcode 
    {
        VARDEC,
        PRINT,
        STORE_VAR,
        STORE_FIELD,
        NEW_OBJ,
    }
	/// <summary>
	/// A bytecode generator for RoaaVM
	/// </summary>
	internal class ByteCodeGenerator
	{
		List<byte> _buffer = new List<byte>();

		public int Length { get => _buffer.Count; }
		void addByte(byte b) => _buffer.Add(b);
        void addShort(short s) => _buffer.AddRange(BitConverter.GetBytes(s));
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
		public void FLOAD(byte index) { addByte(Opcode.FLOAD); addByte(index); }
		public void FSTOR(byte index) { addByte(Opcode.FSTOR); addByte(index); }
		
		public void PRINT() => addByte(Opcode.PRNT);
		public void CALL() => addByte(Opcode.CALL);
		public void PIP() => addByte(Opcode.PIP);
		public void RET() => addByte(Opcode.RET);

        // Tracer
        private void TRACE(TracerOpcode opcode, TokenPosition position)
        {
            addByte(Opcode.IMPDP);
            addByte((byte)opcode); // Tracer Opcode
            addShort((short)position.Line);
            addShort((short)position.Start);
            addShort((short)position.End);
        }
        public void TRACE_STORE_FIELD(TokenPosition position, byte objID, byte fieldStringID) { 
            TRACE(TracerOpcode.STORE_FIELD, position);
            addByte(objID);
            addByte(fieldStringID);
        }
        public void TRACE_STORE_VAR(TokenPosition position) { }
        public void TRACE_NEW(TokenPosition position, byte classID) { TRACE(TracerOpcode.NEW_OBJ, position); addByte(classID); }
		public byte[] GetByteCode()
		{
			return _buffer.ToArray();
		}

		public static void Disassemble(byte[] byteCode, TextWriter output)
        {
			BinaryReader reader = new BinaryReader(new MemoryStream(byteCode));

			while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
				Opcode opcode = (Opcode)reader.ReadByte();
                output.Write($"{reader.BaseStream.Position:X4}: {opcode}\t");

                switch (opcode)
                {
                    case Opcode.NOP:
                        break;
                    case Opcode.PUSHI:
                        break;
                    case Opcode.PUSHD:
                        break;
                    case Opcode.PUSHF:
                        break;
                    case Opcode.PUSHS:
                        break;
                    case Opcode.PUSHB:
                        break;
                    case Opcode.PUSHC:
                        break;
                    case Opcode.PNULL:
                        break;
                    case Opcode.PSHI0:
                        break;
                    case Opcode.PSHI1:
                        break;
                    case Opcode.PSHI2:
                        break;
                    case Opcode.PSHI3:
                        break;
                    case Opcode.PSHI4:
                        break;
                    case Opcode.PSHI5:
                        break;
                    case Opcode.PSHD0:
                        break;
                    case Opcode.PSHD1:
                        break;
                    case Opcode.PSHD2:
                        break;
                    case Opcode.PSHD3:
                        break;
                    case Opcode.PSHD4:
                        break;
                    case Opcode.PSHD5:
                        break;
                    case Opcode.PSHF0:
                        break;
                    case Opcode.PSHF1:
                        break;
                    case Opcode.PSHF2:
                        break;
                    case Opcode.PSHF3:
                        break;
                    case Opcode.PSHF4:
                        break;
                    case Opcode.PSHF5:
                        break;
                    case Opcode.PIP:
                        break;
                    case Opcode.PFF:
                        break;
                    case Opcode.CPUSH:
                        break;
                    case Opcode.POP:
                        break;
                    case Opcode.ADDI:
                        break;
                    case Opcode.SUBI:
                        break;
                    case Opcode.MULI:
                        break;
                    case Opcode.DIVI:
                        break;
                    case Opcode.MODI:
                        break;
                    case Opcode.EQUL:
                        break;
                    case Opcode.NEQU:
                        break;
                    case Opcode.LESS:
                        break;
                    case Opcode.LEQU:
                        break;
                    case Opcode.MORE:
                        break;
                    case Opcode.MEQU:
                        break;
                    case Opcode.JMP:
                        break;
                    case Opcode.JMPF:
                        break;
                    case Opcode.JEQZ:
                        break;
                    case Opcode.JNEZ:
                        break;
                    case Opcode.CALL:
                        break;
                    case Opcode.RET:
                        break;
                    case Opcode.STORE:
                        break;
                    case Opcode.LOAD:
                        break;
                    case Opcode.NEW:
                        break;
                    case Opcode.FLOAD:
                        break;
                    case Opcode.FSTOR:
                        break;
                    case Opcode.PRNT:
                        break;
                    case Opcode.READ:
                        break;

                    default:
                        break;
                }
            }


        }
    }
}
