using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages
{
	public class InterpreterOptions
	{
		public bool AllowLoops { get; set; }
		public bool AllowFunctions { get; set; }
		public bool AllowDeclarations { get; set; }

		/// <summary>
		/// Specifies the maximum number of executions inside a loop
		/// </summary>
		public int LoopExecutionMax { get; set; }
		public bool AllowInfiniteLoops { get; set; }

	}
}
