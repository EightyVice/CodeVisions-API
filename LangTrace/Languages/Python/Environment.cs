namespace LangTrace.Languages.Python
{
	internal class Environment
	{
		public Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

		public void DefineVariable(Variable variable)
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


		public Variable GetVariable(string Name)
		{
			if (variables.ContainsKey(Name))
			{
				return variables[Name];
			}
			else
			{
				throw new CompileErrorException($"{Name} is not defined.");
			}
		}

		public void SetLValue(string Name, Literal Value)
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
	}
}
