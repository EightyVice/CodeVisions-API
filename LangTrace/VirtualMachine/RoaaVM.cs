using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.VirtualMachine
{
	internal class RoaaVM
	{
        internal class Frame
        {
            public readonly Value[] Locals = new Value[64];
        }

        internal class Heap
        {
            List<Object> objects = new List<Object>();

            public Object CreateObject(ProgramFile.Class @class)
            {
                Object obj = new Object(@class);
                return obj;
            }
        }

		public readonly Stack<Value> OperandStack = new Stack<Value>();
        public readonly Stack<Frame> Frames = new Stack<Frame>();

        private Frame CurrentFrame { get => Frames.Peek(); }
        private Heap ObjectsHeap { get; } = new Heap();

        private void Push(Value value) => OperandStack.Push(value);
        private void Pop() => OperandStack.Pop();


		ProgramFile _program;
		public RoaaVM(ProgramFile program)
		{
			_program = program;
			
		}

		public void Call(string functionName, params Value[] arguments)
        {
			var func = _program.Functions.First(f => f.Name == functionName);

			if (arguments.Length != func.Arity)
				throw new ArgumentException("Different number of arguments");

            Frames.Push(new Frame());

            for (int i = 0; i < arguments.Length; i++)
            {
				CurrentFrame.Locals[i] = arguments[i];
            }

			Execute(func.Bytecode);
        }

		void printVars()
		{
			Debug.Write("VARS: ");
			foreach (var i in CurrentFrame.Locals)
				Debug.Write($"[0x{i:X}] ");
			Debug.WriteLine("");
		}
		void printStack()
		{
			Debug.Write("STACK: ");
			foreach(var s in OperandStack.Reverse())
				Debug.Write($"[{s}] ");


			Debug.WriteLine("");
		}
		
		private void Execute(byte[] bytecode)
        {
			BinaryReader reader = new BinaryReader(new MemoryStream(bytecode));
			
			while(reader.BaseStream.Position < reader.BaseStream.Length)
            {
				Opcode opcode = (Opcode)reader.ReadByte();

				Debug.Write($"{reader.BaseStream.Position:X4}: {opcode}\t");

                switch (opcode)
                {
                    case Opcode.NOP:
                        break;

                    #region Stack Operations
                    case Opcode.PUSHI: OperandStack.Push(new SInt32(reader.ReadInt32())); break;

                    case Opcode.PSHI0: OperandStack.Push(new SInt32(0)); break;
                    case Opcode.PSHI1: OperandStack.Push(new SInt32(1)); break;
                    case Opcode.PSHI2: OperandStack.Push(new SInt32(2)); break;
                    case Opcode.PSHI3: OperandStack.Push(new SInt32(3)); break;
                    case Opcode.PSHI4: OperandStack.Push(new SInt32(4)); break;
                    case Opcode.PSHI5: OperandStack.Push(new SInt32(5)); break;

                    case Opcode.PSHD0: OperandStack.Push(new Double(0.0f)); break;
                    case Opcode.PSHD1: OperandStack.Push(new Double(1.0f)); break;
                    case Opcode.PSHD2: OperandStack.Push(new Double(2.0f)); break;
                    case Opcode.PSHD3: OperandStack.Push(new Double(3.0f)); break;
                    case Opcode.PSHD4: OperandStack.Push(new Double(4.0f)); break;
                    case Opcode.PSHD5: OperandStack.Push(new Double(5.0f)); break;

                    case Opcode.PSHF0: OperandStack.Push(new Float(0.0f)); break;
                    case Opcode.PSHF1: OperandStack.Push(new Float(1.0f)); break;
                    case Opcode.PSHF2: OperandStack.Push(new Float(2.0f)); break;
                    case Opcode.PSHF3: OperandStack.Push(new Float(3.0f)); break;
                    case Opcode.PSHF4: OperandStack.Push(new Float(4.0f)); break;
                    case Opcode.PSHF5: OperandStack.Push(new Float(5.0f)); break;

                    case Opcode.PNULL: OperandStack.Push(Object.NullObject); break;

                    case Opcode.POP: OperandStack.Pop(); break;
                    #endregion

                    #region Arithmetics
                    case Opcode.ADDI: { 
                            SInt32 b = (SInt32)OperandStack.Pop(); SInt32 a = (SInt32)OperandStack.Pop();
                            OperandStack.Push(a + b);
                        }
                        break;
                    #endregion

                    #region Comparison

                    #endregion

                    #region Control Flow
                    case Opcode.RET:

                        break;
                    #endregion


                    // Variables
                    case Opcode.STORE:
                        {
                            int index = reader.ReadByte();
                            Debug.Write($"({index})");
                            CurrentFrame.Locals[index] = OperandStack.Pop();
                        }
                        break;
                    case Opcode.LOAD:
                        { 
                            int index = reader.ReadByte();
                            Debug.Write($"({index})");
                            OperandStack.Push(CurrentFrame.Locals[index]);
                        }
                        break;


                    case Opcode.NEW:
                        {
                            int index = reader.ReadByte();
                            Debug.Write($"({index})");
                            Push(ObjectsHeap.CreateObject(_program.Classes[index]));
                        }
                        break;
                    // IO
                    case Opcode.PRNT:
                        Console.WriteLine(OperandStack.Pop().ToString());
                        break;
                    case Opcode.READ:
                        break;
                    case Opcode.SIG_VARDEC:
                        break;
                    case Opcode.SIG_PRINT:
                        break;
                    case Opcode.SIG_IF:
                        break;
                    case Opcode.SIG_LOOP:
                        break;
                }
                Debug.WriteLine("");
            }
        }

	}
}
