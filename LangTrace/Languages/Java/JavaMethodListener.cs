using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	/// <summary>
	/// Extracts methods metadata
	/// </summary>
    internal class JavaMethodListener : JavaParserBaseListener
    {
		List<LocalVariable> _locals =  new List<LocalVariable>();
        public LocalVariable[] Locals { get => _locals.ToArray(); }

		List<Declaration> _params = new List<Declaration>();
		public Declaration[] Parameters { get => _params.ToArray(); }


		DataType DataTypeFromString(string str)
		{
			switch (str)
			{
				case "byte": return DataType.Byte;
				case "short": return DataType.Short;
				case "int": return DataType.Int;
				case "long": return DataType.Long;
				case "float": return DataType.Float;
				case "double": return DataType.Double;
				case "bool": return DataType.Boolean;
				default: return DataType.Void;
			}


		}
        public override void EnterMethodDeclaration([NotNull] JavaParser.MethodDeclarationContext context)
        {
			if (context.formalParameters().formalParameterList() != null)
			{
				var parameters = context.formalParameters().formalParameterList().formalParameter();
				foreach (var param in parameters)
				{
					string paramType = param.typeType().GetText();
					string paramName = param.variableDeclaratorId().GetText();

					_params.Add(new Declaration(TypeDescriptor.FromString(paramType), paramName));
				}
			}
		}
        public override void ExitLocalVariableDeclaration([NotNull] JavaParser.LocalVariableDeclarationContext context)
        {
		
			TypeDescriptor decType = new TypeDescriptor();

			if (context.typeType().classOrInterfaceType() != null)
			{
				decType.IsReference = true;
				decType.ClassName = context.typeType().classOrInterfaceType().identifier(0).GetText();
			}
			else if (context.typeType().primitiveType() != null)
			{
				decType.IsPrimitive = true;
				decType.DataType = DataTypeFromString(context.typeType().GetText());
			}

			List<LocalVariable> localvars = new List<LocalVariable>();
			// Declaration Type
			foreach (var vardecl in context.variableDeclarators().variableDeclarator())
			{
				string decName = vardecl.variableDeclaratorId().GetText();
				localvars.Add(new LocalVariable(decName, decType));
			}

			_locals.AddRange(localvars);
		}
    }
}
