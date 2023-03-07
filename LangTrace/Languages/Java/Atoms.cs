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
		public Statement[] Statements { get;}
		public Declaration[] Parameters { get;}
		public LocalVariableDeclaration[] Locals { get;}

        public Method(TypeDescriptor returnType, string name, Statement[] statements, Declaration[] parameters, LocalVariableDeclaration[] locals)
        {
            ReturnType = returnType;
            Name = name;
            Statements = statements;
            Parameters = parameters;
            Locals = locals;
        }
    }

}
