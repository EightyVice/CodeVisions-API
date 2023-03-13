using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LangTrace.Utilities;
using LangTrace.VirtualMachine.TraceGenerator;

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
            List<Value> objects = new List<Value>();

            public Object CreateObject(ProgramFile.Class @class)
            {
                Object obj = new Object(@class);
                objects.Add(obj);
                return obj;
            }

            public ArrayObject CreateArray(Value[] values)
            {
                ArrayObject arr = new ArrayObject(values);
                objects.Add(arr);
                return arr;
            }

            public int FindObject(Value obj) => objects.IndexOf(obj);

        }

		public readonly Stack<Value> OperandStack = new Stack<Value>();
        public readonly Stack<Frame> Frames = new Stack<Frame>();

        private Frame CurrentFrame { get => Frames.Peek(); }
        private Heap ObjectsHeap { get; } = new Heap();

        private void Push(Value value) => OperandStack.Push(value);
        private Value Pop() => OperandStack.Pop();


		ProgramFile _program;
        ITraceWriter _tracer;

		public RoaaVM(ProgramFile program, ITraceWriter traceWriter = null)
		{
			_program = program;
            _tracer = traceWriter;

            // Generate tracer metadata

            foreach (var cls in _program.Classes)
                _tracer.DefineClass(cls.Name, cls.Fields.Select(f => f.Name).ToArray());

            foreach(var method in _program.Functions)
                _tracer.DefineFunction(method.Name, "todo", method.Locals.Select(f => f.Name).ToArray());
            
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

			Execute(func);
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
		
        
        private string StrVal(Value value)
        {
            if (value is Object)
                return $"obj:{ObjectsHeap.FindObject(value)}";

            if (value is ArrayObject)
                return $"arr:{ObjectsHeap.FindObject(value)}";

            return value.ToString();
        }
		private void Execute(ProgramFile.Function function)
        {
            var linetable = function.LineTable;
			BinaryReader reader = new BinaryReader(new MemoryStream(function.Bytecode));

            int Line() => linetable[(int)reader.BaseStream.Position];

            // Local Helper:
            TokenPosition GetPos() => new TokenPosition(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
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

                            _tracer.Assign(Line(), function.Locals[index].Name, StrVal(CurrentFrame.Locals[index]));
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
                            int index = reader.ReadByte(); Debug.Write($"({index})");
                            Push(ObjectsHeap.CreateObject(_program.Classes[index]));
                            _tracer.NewObject(Line(), index);
                        }
                        break;
                    case Opcode.ARRAY:
                        {
                            int length = reader.ReadByte();
                            Value[] array = new Value[length];
                            for (int i = length - 1; i >= 0; i--)
                                array[i] = Pop();
                            Push(ObjectsHeap.CreateArray(array));
                            _tracer.NewArray(Line(), array.Select(e => e.ToString()).ToArray());
                        }
                        break;
                    case Opcode.FLOAD:
                        {
                            int index = reader.ReadByte(); Debug.Write($"({index})");
                            var obj = (Object)Pop();
                            var field_name = _program.Strings[index];
                            Debug.Write($" {index}:\"{field_name}\" ");
                            Push(obj[field_name]);
                            
                        }
                        break;

                    case Opcode.FSTOR:
                        {
                            int index = reader.ReadByte(); Debug.Write($"({index})");
                            var obj = (Object)Pop();
                            var val = Pop();
                            var field_name = _program.Strings[index];
                            Debug.Write($" {index}:\"{field_name}\" ");
                            obj[field_name] = val;
                            _tracer.SetField(Line(), ObjectsHeap.FindObject(obj), field_name, StrVal(val));
                        }
                        break;
                    // IO
                    case Opcode.PRNT:
                        Console.WriteLine(OperandStack.Pop().ToString());
                        break;
                    case Opcode.READ:
                        break;


                    case Opcode.IMPDP:

                        break;
                }
                foreach (var s in OperandStack.Reverse())
                {
                    Debug.Write($"[{s}]");
                }
                Debug.WriteLine("");
            }
        }

    }
}
