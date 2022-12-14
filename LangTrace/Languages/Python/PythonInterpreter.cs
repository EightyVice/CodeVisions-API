using Antlr4.Runtime;
using LangTrace.Languages.Java;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Python
{
	public class PythonInterpreter : Interpreter
	{
		public override InterpreterResult Interpret(string sourceCode, string testCode = null)
		{
			InterpreterResult result = new InterpreterResult();


			AntlrInputStream fs = new AntlrInputStream(sourceCode);
			PythonLexer lexer = new PythonLexer(fs);
			CommonTokenStream token = new CommonTokenStream(lexer);
			PythonParser parser = new PythonParser(token);
			parser.AddErrorListener(new ErrorListener(result));
			var tree = parser.file_input();

			if (Status == InterpretationStatus.Failed)
				return null;

			try
			{
				PythonParserVisitor visitor = new PythonParserVisitor(result);
				tree.Accept(visitor);
			}
			catch (CompileErrorException ex)
			{
				Status = InterpretationStatus.Failed;
				result.Errors.Add(ex.Message);
			}

			Tester = new Tester();

			if (testCode != null)
			{
				var testResults = Tester.Validate(testCode, result);
				result.TesterResult = testResults;

				foreach (var r in testResults.CaseResults)
				{
					Console.ForegroundColor = r.Success ? ConsoleColor.Green : ConsoleColor.Red;
					Console.WriteLine($"[Case Reuslt] {(r.Success ? "Passed" : "Failed")}: {r.Message}");
					Console.ResetColor();

				}
			}

			Status = InterpretationStatus.Success;
			return result;
		}
	}
}
