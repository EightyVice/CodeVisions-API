using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using LangTrace.Languages.Java;

namespace LangTrace.Languages
{
	internal class Tester
	{
		private string testcode;
		private JavaInterpreter VM;
		public bool Success { get; set; }

		public Tester(string testCode, JavaInterpreter vm)
		{
			testcode = testCode;
			VM = vm;
		}
		
		public void Validate()
		{
			AntlrInputStream fs = new AntlrInputStream(testcode);
			JavaLexer lexer = new JavaLexer(fs);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			JavaParser parser = new JavaParser(tokens);
			var tree = parser.prog();

			TesterParseVisitor visitor = new TesterParseVisitor(VM);
			tree.Accept(visitor);

		}
	}
}
