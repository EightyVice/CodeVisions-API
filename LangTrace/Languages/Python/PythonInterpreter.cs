using Antlr4.Runtime;
using LangTrace.Languages.Java;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Python
{
	public class PythonInterpreter : Tracer
	{
		public override TracerResult ExecuteAndTrace(TracerOptions options, string sourceCode)
		{
			TracerResult result = new TracerResult();


			AntlrInputStream fs = new AntlrInputStream(sourceCode);
			PythonLexer lexer = new PythonLexer(fs);
			CommonTokenStream token = new CommonTokenStream(lexer);
			PythonParser parser = new PythonParser(token);
			parser.AddErrorListener(new ErrorListener(result));
			var tree = parser.file_input();

			if (Status == TracerStatus.Failed)
				return null;

			try
			{
				PythonParserVisitor visitor = new PythonParserVisitor(result);
				tree.Accept(visitor);
			}
			catch (CompileErrorException ex)
			{
				Status = TracerStatus.Failed;
				//result.Errors.Add(ex.Message);
			}

			Status = TracerStatus.Success;
			return result;
		}
	}
}
