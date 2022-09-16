using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	internal class Metadata
	{
		public Dictionary<string, int> FunctionsCalls = new Dictionary<string, int>();
		private Dictionary<string, int> variableAccesses = new Dictionary<string, int>();
		public int NumberOfStatementsExecuted { get; }

		public void CallFunction(string name)
		{
			if (FunctionsCalls.ContainsKey(name)) FunctionsCalls[name]++;
			else FunctionsCalls[name] = 1;
		}
		
		public void AccessLValue(string name)
		{
			if (variableAccesses.ContainsKey(name)) variableAccesses[name]++;
			else variableAccesses[name] = 1;
		}
	}
}
