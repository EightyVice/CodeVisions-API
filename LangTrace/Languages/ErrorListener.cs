using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace LangTrace.Languages.Java
{
	internal class ErrorListener : BaseErrorListener
	{
		private JavaInterpreter _interpreter;

		public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
		{
			_interpreter.Status = InterpretationStatus.Failed;
			_interpreter.Errors += $"line: {line} @ {charPositionInLine} [{offendingSymbol.Text}]: {msg}\n";
		}

		public ErrorListener(JavaInterpreter interpreter)
		{
			_interpreter = interpreter;
		}
	}
}
