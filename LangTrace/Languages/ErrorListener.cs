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
		private InterpreterResult _interpreter;

		public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
		{
			_interpreter.Errors.Add($"line: {line} @ {charPositionInLine} [{offendingSymbol.Text}]: {msg}\n");
		}

		public ErrorListener(InterpreterResult interpreter)
		{
			_interpreter = interpreter;
		}
	}
}
