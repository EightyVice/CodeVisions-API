using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages
{
	public class Metadata
	{
		public Dictionary<string, int> FunctionsCalls = new Dictionary<string, int>();
		private Dictionary<string, int> variableAccesses = new Dictionary<string, int>();
		//public Dictionary<string, List<(string, Java.TypeDescriptor)>> Classes = new Dictionary<string, List<(string, Java.TypeDescriptor)>>();
		public int NumberOfStatementsExecuted { get; }

		public Dictionary<string, Stack<string>> Assignments = new Dictionary<string, Stack<string>>();

		public bool LoopsUsed { get; set; }
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

		public void Assign(string lhs, string rhs)
		{
			if(Assignments.ContainsKey(lhs))
				Assignments[lhs].Push(rhs);
			else { Assignments[lhs] = new Stack<string>(); Assignments[lhs].Push(rhs);}
		}
	}
}
