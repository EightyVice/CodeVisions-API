using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using LangTrace.Languages.Java;
using LangTrace.Languages.CDL;

namespace LangTrace.Languages
{
	public class CaseResult
	{
		public readonly bool Success;
		public readonly string Message;

		public CaseResult(bool success, string message = null)
		{
			Success = success;
			Message = message;
		}
	}

	public class TesterResult
	{
		public ICollection<string> Logs { get; set; } = new List<string>();
		public ICollection<CaseResult> CaseResults { get; set; } = new List<CaseResult>();
	}
	public class Tester
	{
		public bool Success { get; set; }



		public TesterResult Validate(string testCode, InterpreterResult interpreterResult)
		{
			TesterResult result = new TesterResult();

			AntlrInputStream fs = new AntlrInputStream(testCode);
			CDLLexer lexer = new CDLLexer(fs);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			CDLParser parser = new CDLParser(tokens);
			parser.AddErrorListener(new ErrorListener(interpreterResult));
			var tree = parser.program();

			CDLTreeVisitor visitor = new CDLTreeVisitor(result, interpreterResult);
			tree.Accept(visitor);

			return result;
		}
	}
}
