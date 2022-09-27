using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace LangTrace.Languages.Java
{

	public class JavaInterpreter : Interpreter
	{
		public override InterpreterResult Interpret(string sourceCode, string testCode = null)
		{
			InterpreterResult result = new InterpreterResult();


			AntlrInputStream fs = new AntlrInputStream(sourceCode);
			JavaLexer lexer = new JavaLexer(fs);
			CommonTokenStream token = new CommonTokenStream(lexer);
			JavaParser parser = new JavaParser(token);
			parser.AddErrorListener(new ErrorListener(result));
			var tree = parser.prog();

			if (Status == InterpretationStatus.Failed)
				return null;

			//try
			//{
				JavaParserVisitor visitor = new JavaParserVisitor(result);
				tree.Accept(visitor);
			//}
			//catch (CompileErrorException ex)
			//{
				//Console.WriteLine(ex.Message);

				//Status = InterpretationStatus.Failed;
				//result.Errors.Add(ex.Message);
			//}

			Tester = new Tester();
			
			if(testCode != null) 
			{ 
				var testResults = Tester.Validate(testCode, result);
				result.TesterResult = testResults;

				foreach(var r in testResults.CaseResults)
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
