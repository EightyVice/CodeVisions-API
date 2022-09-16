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

		internal Metadata Metadata { get; private set; } = new Metadata();
		internal Tester Tester { get; private set; }

		public bool TesterResult { get => Tester.Success; }
		public JavaInterpreter(string SourceCode)
		{
			sourceCode = SourceCode;
			Environment.InitBuiltIns();
		}
		public JavaInterpreter(string SourceCode, string TestCode)
		{
			sourceCode = SourceCode;
			Environment.InitBuiltIns();
			Tester = new Tester(TestCode, this);
		}
		public InterpretationStatus Status { get; internal set; } = InterpretationStatus.NotYet;


		public void Interpret()
		{
			AntlrInputStream fs = new AntlrInputStream(sourceCode);
			JavaLexer lexer = new JavaLexer(fs);
			CommonTokenStream token = new CommonTokenStream(lexer);
			JavaParser parser = new JavaParser(token);
			parser.AddErrorListener(new ErrorListener(this));
			var tree = parser.prog();
			if (Status == InterpretationStatus.Failed)
				return;

			ParserVisitor visitor = new ParserVisitor(this);
			tree.Accept(visitor);
			
			if(Status == InterpretationStatus.NotYet)
				Status = InterpretationStatus.Success; // else it will be a failure...

			//Tester.Validate();
			//Console.WriteLine(Tester.Success);
		}
	}
}
