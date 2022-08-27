using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	internal class Metadata
	{
		private Dictionary<string, int> functionCalls = new Dictionary<string, int>();
		private Dictionary<string, int> variableAccesses = new Dictionary<string, int>();
		private int NumberOfStatementsExecuted { get; }
	}
}
