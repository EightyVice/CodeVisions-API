namespace LangTrace.Languages.Java
{
	internal class Environment
	{
		private Dictionary<string, object> variables = new Dictionary<string, object>();
		private Dictionary<string, Class> classes = new Dictionary<string, Class>();
		private List<Object> objects = new List<Object>();

		public Environment()
		{
			InitBuiltIns();
		}
		public void DefineClass(Class structure)
		{
			if (classes.ContainsKey(structure.Name))
				throw new CompileErrorException("Structure already defined");
			else
				classes.Add(structure.Name, structure);
		}

		public Class GetStructure(string Name)
		{
			if (classes.ContainsKey(Name))
				return classes[Name];
			else
				throw new CompileErrorException("Class doesnt exists");
		}
		public void DefineVariable(LValue variable)
		{
			if (variables.ContainsKey(variable.Name))
			{
				throw new CompileErrorException($"{variable.Name} already defined.");
			}
			else
			{
				variables.Add(variable.Name, variable);
			}
		}

		public void DefineVariable(Variable variable)
		{
			DefineVariable(variable.Name, variable.Type, variable.Value);
		}

		public LValue GetLValue(string Name)
		{
			if (variables.ContainsKey(Name))
			{
				return (LValue)variables[Name];
			}
			else
			{
				throw new CompileErrorException($"{Name} is not defined.");
			}
		}
		public void DefineVariable(string Name, DataType type, IAtom init)
		{
			if (variables.ContainsKey(Name))
			{
				throw new CompileErrorException($"{Name} already defined.");
			}
			else
			{
				variables.Add(Name, new Variable(Name, type, init));
			}
		}

		public void SetLValue(string Name, IAtom Value)
		{
			if (variables.ContainsKey(Name))
			{
				if(variables[Name] is Variable)
				{
					((Variable)variables[Name]).Value = Value;

				}
			}
			else
			{
				throw new CompileErrorException($"{Name} is not defined.");
			}
		}

		public readonly Class NodeClass = new Class("Node", new List<(string name, Kind type)>()
		{
			("data", new Kind() { IsPrimitive = true, DataType = DataType.Int }),
			("next", new Kind() { IsReference = true, ClassName = "Node" })
		});

		public void InitBuiltIns()
		{
			// Node class
			DefineClass(NodeClass);
		}

		public Reference InitObject(Class Class)
		{
			Object obj = new Object();
			obj.ClassName = Class.Name;
			Reference reference = new Reference("<unnnamed>", Class.Name, obj);

			foreach (var member in Class.Members)
			{
				if (member.type.IsPrimitive)
				{
					if (member.type.DataType == DataType.Int)
						obj.Members.Add(member.name, new Variable(member.name, member.type.DataType, new IntLiteral(0)));

					if (member.type.DataType == DataType.Float)
						obj.Members.Add(member.name, new Variable(member.name, member.type.DataType, new FloatLiteral(0)));
				}
				else if (member.type.IsReference)
				{
					obj.Members.Add(member.name, new Reference(member.name, Class.Name, Object.NullObject) { ParentObject = objects.Count});
				}
				else
					throw new CompileErrorException("something wrong");
			}
			obj.id = objects.Count;
			objects.Add(obj);
			return reference;
		}
	}
}
