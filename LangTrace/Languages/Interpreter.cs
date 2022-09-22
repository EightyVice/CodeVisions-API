using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages
{
	public enum InterpretationStatus
	{
		NotYet,
		Success,
		Failed,
	}

	public class InterpreterResult
	{
		public ICollection<Step> Steps { get; set; } = new List<Step>();
		public ICollection<string> Errors { get; set; } = new List<string>();
		public Metadata Metadata { get; set; } = new Metadata();
		public TesterResult TesterResult { get; set; }
	}

	public abstract class Interpreter
	{
		public Tester Tester { get; set; }
		public InterpretationStatus Status { get; set; }

		public abstract InterpreterResult Interpret(string sourceCode, string testCode);
	}
}
