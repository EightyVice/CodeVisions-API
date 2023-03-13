using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.VirtualMachine
{
    internal class ProgramFile
    {
        public Class[] Classes { get; }
        public Function[] Functions { get; }
        public Value[] Constants { get; }
        public string[] Strings { get; }
        public ProgramFile(Class[] classes, Function[] functions, Value[] constants, string[] strings)
        {
            Classes = classes;
            Functions = functions;
            Constants = constants;
            Strings = strings;
        }

        internal class Class
        {
            public string Name { get; }
            public (string Name, Descriptor Type)[] Fields { get; }

            public Class(string name, (string Name, Descriptor Type)[] fields)
            {
                Name = name;
                Fields = fields;
            }
        }

        internal class Function
        {
            public string Name { get; }
            public Descriptor ReturnType { get; }
            public int Arity { get; }
            public (string Name, Descriptor Type)[] Locals { get; }
            public byte[] Bytecode { get; }
            public IReadOnlyDictionary<int, int> LineTable { get; }

            public Function(string name, Descriptor returnType, int arity, (string Name, Descriptor Type)[] locals, byte[] bytecode, IReadOnlyDictionary<int, int> lineTable)
            {
                Name = name;
                ReturnType = returnType;
                Arity = arity;
                Locals = locals;
                Bytecode = bytecode;
                LineTable = lineTable;
            }
        }

        // TODO Globals;
    }


    internal abstract class Descriptor { public abstract Value Default { get; } }

    internal class MethodDescriptor : Descriptor
    {
        public Descriptor[] ParametersDescriptors { get; }
        public Descriptor ReturnDescriptor { get; }

        public override Value Default => throw new NotImplementedException();

        public MethodDescriptor(Descriptor[] parametersDescriptors, Descriptor returnDescriptor)
        {
            ParametersDescriptors = parametersDescriptors;
            ReturnDescriptor = returnDescriptor;
        }
    }
    internal class ArrayDescriptor : Descriptor
    {
        public Descriptor ComponentDescriptor { get; }

        public override Value Default => throw new NotImplementedException();

        public ArrayDescriptor(Descriptor componentDescriptor)
        {
            ComponentDescriptor = componentDescriptor;
        }
    }
    internal class ClassDescriptor : Descriptor
    {
        public string ClassName { get; }

        public override Value Default => Object.NullObject;

        public ClassDescriptor(string className)
        {
            ClassName = className;
        }
    }

    internal class BaseTypeDescriptor : Descriptor
    {
        internal enum BaseType
        {
            Byte,
            Char,
            Double,
            Float,
            Int,
            Long,
            Short,
            Boolean,
            Void,
        }

        public BaseType Type { get; }
        public override Value Default { get {
                switch (Type)
                {
                    case BaseType.Byte: return new SInt8(0);
                    case BaseType.Int: return new SInt32(0);
                    case BaseType.Float: return new Float(0);
                    default : return null;
                }
            }
        }

        public BaseTypeDescriptor(BaseType type)
        {
            Type = type;
        }
    }
}
