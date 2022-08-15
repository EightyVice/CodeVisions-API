using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace LangTrace.Languages.Java
{
	public enum InterpretationStatus
	{
		NotYet,
		Success,
		Failed,
	}

	public class JavaInterpreter
	{
		private string sourceCode;
		public string Errors { get; set; }
		public List<Step> Steps { get; private set; } = new List<Step>();
		internal Environment Environment { get; private set; } = new Environment();

		public JavaInterpreter(string SourceCode)
		{
			sourceCode = SourceCode;
		}
		public InterpretationStatus Status { get; internal set; } = InterpretationStatus.NotYet;


		public void Interpret()
		{
			AntlrInputStream fs = new AntlrInputStream(sourceCode);
			JavaLexer lexer = new JavaLexer(fs);
			CommonTokenStream token = new CommonTokenStream(lexer);
			JavaParser parser = new JavaParser(token);
			var tree = parser.prog();
			ParserVisitor visitor = new ParserVisitor(this);
			parser.AddErrorListener(new ErrorListener(this));
			tree.Accept(visitor);
			
			if(Status == InterpretationStatus.NotYet)
				Status = InterpretationStatus.Success; // else it will be a failure...
		}
	}
}
