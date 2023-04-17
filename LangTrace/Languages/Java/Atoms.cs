using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{ 

	internal class Class
	{
		public string Name { get;}

		public Declaration[] Fields { get; }
		
		public Method[] Methods { get; }

        public Class(string name, Declaration[] fields, Method[] methods)
        {
            Name = name;
            Fields = fields;
            Methods = methods;
        }
    }

	internal class Declaration
	{
		public readonly TypeDescriptor Type;
		public readonly string Name;

		public Declaration(TypeDescriptor type, string name)
		{
			Type = type;
			Name = name;
		}
	}

	internal class Method
	{
		public TypeDescriptor ReturnType { get;}
		public string Name { get;}
		public IStatement[] Statements { get;}
		public Declaration[] Parameters { get;}
		public LocalVariable[] Locals { get;}
		public bool IsStatic { get; set; }
		public string ClassName { get; set; }

        public Method(TypeDescriptor returnType, string name, IStatement[] statements, Declaration[] parameters, LocalVariable[] locals, bool isStatic, string className)
        {
            ReturnType = returnType;
            Name = name;
            Statements = statements;
            Parameters = parameters;
            Locals = locals;
            IsStatic = isStatic;
            ClassName = className;
        }
    }

}
