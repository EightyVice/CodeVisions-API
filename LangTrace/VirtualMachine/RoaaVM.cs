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
        private Value Peek() => OperandStack.Peek();
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
                _tracer.DefineFunction(method.Name, method.ReturnType?.ToString(), method.Arity, method.Locals.Select(f => f.Name).ToArray());
            
		}

		public void Call(string functionName, params Value[] arguments)
        {
            var func = _program.Functions.First(f => f.Name == functionName);
            _tracer.Call(-1, _program.Functions.ToList().IndexOf(func), null, arguments.Select(a => a.ToString()).ToArray());
            Call(func, arguments);       
        }

        private void Call(ProgramFile.Function function, params Value[] arguments)
        {
            if (arguments.Length != function.Arity)
                throw new ArgumentException("Different number of arguments");

            Frames.Push(new Frame());

            for (int i = 0; i < arguments.Length; i++)
            {
                CurrentFrame.Locals[i] = arguments[i];
            }

            Execute(function);
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

            if (value is LinkedListNodeObject)
                return $"lnode:{ObjectsHeap.FindObject(value)}";

            return value.ToString();
        }
		private void Execute(ProgramFile.Function function)
        {
            Debug.WriteLine($"\n=== Executing {function.Name} ===");
            var linetable = function.LineTable;
			BinaryReader reader = new BinaryReader(new MemoryStream(function.Bytecode));

            int Line()
            {
                if (linetable.ContainsKey((int)reader.BaseStream.Position))
                    return linetable[(int)reader.BaseStream.Position];
                else
                    return -1;
            }
            

            // Local Helper:
            TokenPosition GetPos() => new TokenPosition(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
            int GetPC() => (int)reader.BaseStream.Position;
            void OffsetPC(int offset) => reader.BaseStream.Position += offset;

			while(reader.BaseStream.Position < reader.BaseStream.Length)
            {
				Opcode opcode = (Opcode)reader.ReadByte();

				Debug.Write($"\t${function.Name}.{reader.BaseStream.Position:X4}: {opcode}\t");

                switch (opcode)
                {
                    case Opcode.NOP:
                        break;

                    #region Stack Operations
                    case Opcode.PUSHI: OperandStack.Push(new SInt32(reader.ReadInt32())); break;

                    case Opcode.PSHI0: Push(new SInt32(0)); break;
                    case Opcode.PSHI1: Push(new SInt32(1)); break;
                    case Opcode.PSHI2: Push(new SInt32(2)); break;
                    case Opcode.PSHI3: Push(new SInt32(3)); break;
                    case Opcode.PSHI4: Push(new SInt32(4)); break;
                    case Opcode.PSHI5: Push(new SInt32(5)); break;

                    case Opcode.PSHD0: Push(new Double(0.0f)); break;
                    case Opcode.PSHD1: Push(new Double(1.0f)); break;
                    case Opcode.PSHD2: Push(new Double(2.0f)); break;
                    case Opcode.PSHD3: Push(new Double(3.0f)); break;
                    case Opcode.PSHD4: Push(new Double(4.0f)); break;
                    case Opcode.PSHD5: Push(new Double(5.0f)); break;

                    case Opcode.PSHF0: Push(new Float(0.0f)); break;
                    case Opcode.PSHF1: Push(new Float(1.0f)); break;
                    case Opcode.PSHF2: Push(new Float(2.0f)); break;
                    case Opcode.PSHF3: Push(new Float(3.0f)); break;
                    case Opcode.PSHF4: Push(new Float(4.0f)); break;
                    case Opcode.PSHF5: Push(new Float(5.0f)); break;

                    case Opcode.CPUSH: Push(_program.Constants[reader.ReadByte()]); break;
                    case Opcode.PNULL: OperandStack.Push(Object.NullObject); break;

                    case Opcode.POP: OperandStack.Pop(); break;

                    case Opcode.DUPLI: OperandStack.Push(OperandStack.Peek()); break;
                    #endregion

                    #region Arithmetics
                    case Opcode.ADDI: { 
                            SInt32 b = (SInt32)OperandStack.Pop(); SInt32 a = (SInt32)OperandStack.Pop();
                            OperandStack.Push(a + b);
                        }
                        break;
                    case Opcode.SUBI:
                        {
                            SInt32 b = (SInt32)OperandStack.Pop(); SInt32 a = (SInt32)OperandStack.Pop();
                            OperandStack.Push(a - b);
                        }
                        break;
                    case Opcode.DIVI:
                        {
                            SInt32 b = (SInt32)OperandStack.Pop(); SInt32 a = (SInt32)OperandStack.Pop();
                            OperandStack.Push(a / b);
                        }
                        break;
                    case Opcode.MULI:
                        {
                            SInt32 b = (SInt32)OperandStack.Pop(); SInt32 a = (SInt32)OperandStack.Pop();
                            OperandStack.Push(a * b);
                        }
                        break;
                    #endregion

                    #region Comparison
                    case Opcode.EQUL: { SInt32 b = (SInt32)Pop(); SInt32 a = (SInt32)Pop(); Push(a == b); } break;
                    case Opcode.NEQU: { SInt32 b = (SInt32)Pop(); SInt32 a = (SInt32)Pop(); Push(a != b); } break;
                    case Opcode.MORE: { SInt32 b = (SInt32)Pop(); SInt32 a = (SInt32)Pop(); Push(a > b); } break;
                    case Opcode.MEQU: { SInt32 b = (SInt32)Pop(); SInt32 a = (SInt32)Pop(); Push(a >= b); } break;
                    case Opcode.LESS: { SInt32 b = (SInt32)Pop(); SInt32 a = (SInt32)Pop(); Push(a < b); } break;
                    case Opcode.LEQU: { SInt32 b = (SInt32)Pop(); SInt32 a = (SInt32)Pop(); Push(a <= b); }  break;

                    #endregion

                    #region Control Flow
                    case Opcode.JMP:  { int offset = reader.ReadSByte(); OffsetPC(offset); Debug.Write($"+({offset})"); } break;
                    case Opcode.JMPF:  { int offset = reader.ReadSByte(); OffsetPC(offset); } break;
                    case Opcode.JEQZ:  { int offset = reader.ReadSByte(); if (((SInt32)Pop()).Value == 0) { OffsetPC(offset); Debug.Write($"+({offset})");} } break;
                    case Opcode.JNEZ:  { int offset = reader.ReadSByte(); if (((SInt32)Pop()).Value != 0) { OffsetPC(offset); Debug.Write($"+({offset})"); } } break;
                    case Opcode.RET:
                        Frames.Pop();
                        _tracer.Return(Line());
                        return;
                    case Opcode.RTRNV:
                        Value retVal = Peek();
                        Frames.Pop();
                        _tracer.Return(Line(), StrVal(retVal));
                        return;
                    case Opcode.CALL:
                        {
                            int index = reader.ReadByte();
                            Debug.Write($"({index})\t");
                            var callee = _program.Functions[index];
                            var args = new Value[callee.Arity];
                            for (int i = 0; i < args.Length; i++)
                                args[i] = Pop();

                            _tracer.Call(Line(), index, null, args.Select(a => StrVal(a)).ToArray());
                            Call(callee, args);
                        }
                        break;
                    #endregion

                       
                    // Variables
                    case Opcode.STORE:
                        {
                            int index = reader.ReadByte();
                            Debug.Write($"({index})\t");
                            CurrentFrame.Locals[index] = OperandStack.Pop();

                            _tracer.Assign(Line(), index, StrVal(CurrentFrame.Locals[index]));
                        }
                        break;
                    case Opcode.LOAD:
                        { 
                            int index = reader.ReadByte();
                            Debug.Write($"({index})\t");
                            OperandStack.Push(CurrentFrame.Locals[index]);
                        }
                        break;


                    case Opcode.NEW:
                        {
                            int index = reader.ReadByte(); Debug.Write($"({index})\t");
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
                            var obj = Pop();
                            // Handle Array's length
                            var field_name = _program.Strings[index];
                            Debug.Write($" {index}:\"{field_name}\" ");
                            if(obj is ArrayObject)
                            {
                                if(field_name == "length")
                                    Push(new SInt32(((ArrayObject)obj).Length));
                            }
                            else
                            {
                                Push(((Object)obj)[field_name]);
                            }
                            
                        }
                        break;

                    case Opcode.FSTOR:
                        {
                            int index = reader.ReadByte(); Debug.Write($"({index})");
                            var obj = Pop();
                            var val = Pop();
                            var field_name = _program.Strings[index];
                            Debug.Write($" {index}:\"{field_name}\" ");

                            if(obj is LinkedListNodeObject)
                            {
                                var node = (LinkedListNodeObject)obj;
                                switch (field_name)
                                {
                                    case "next": node.NextNode = (LinkedListNodeObject)val; break;
                                    case "data": node.Value = val; break;
                                    default:
                                        throw new Exception();
                                }
                            }
                            else
                            {
                                ((Object)obj)[field_name] = val;
                                _tracer.SetField(Line(), ObjectsHeap.FindObject(obj),_program.Classes.ToList().IndexOf(((Object)obj).Class), field_name, StrVal(val));
                            }
                        }
                        break;
                    case Opcode.ASTOR:
                        {
                            int index = ((SInt32)Pop()).Value;
                            var arr = (ArrayObject)Pop();
                            int arrID = ObjectsHeap.FindObject(arr);
                            Debug.Write($" arr{arrID}[{index}]");
                            arr[index] = Pop();
                            _tracer.SetArrayElement(Line(), arrID, index, null, StrVal(arr[arrID]));
                        }
                        break;

                    case Opcode.ALOAD:
                        {
                            int index = ((SInt32)Pop()).Value;
                            var arr = (ArrayObject)Pop();
                            int arrID = ObjectsHeap.FindObject(arr);
                            Debug.Write($" arr{arrID}<{index}>");
                            Push(arr[index]);
                        }
                        break;
                    // IO
                    case Opcode.PRNT:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(">>> " + OperandStack.Pop().ToString());
                        Console.ResetColor();
                        break;
                    case Opcode.READ:
                        break;

                    case Opcode.NEWLINK:
                        ObjectsHeap.CreateObject(
                            new ProgramFile.Class(
                                "LinkNode", 
                                new (string, Descriptor)[]{
                                    ("next", new ClassDescriptor("LinkNode")),
                                    ("data", new ClassDescriptor("Object"))
                                }
                            )
                        );

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
