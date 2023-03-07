using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

using LangTrace.VirtualMachine;

namespace LangTrace.Languages.Java
{

	public class JavaInterpreter : Interpreter
	{
		public override InterpreterResult Interpret(string sourceCode, string entryPoint = null)
		{
			InterpreterResult result = new InterpreterResult();


			AntlrInputStream fs = new AntlrInputStream(sourceCode);
			JavaLexer lexer = new JavaLexer(fs);
			CommonTokenStream token = new CommonTokenStream(lexer);
			JavaParser parser = new JavaParser(token);
			parser.AddErrorListener(new ErrorListener(result));
			
			// Build Parse Tree
			var parseTree = parser.compilationUnit();

			if (Status == InterpretationStatus.Failed)
				return null;

			try
			{

				// Build Abstract Syntax Tree
				JavaParserVisitor ptreeVisitor = new JavaParserVisitor();
				CompilationUnit ast = (CompilationUnit)parseTree.Accept(ptreeVisitor);

				// Compile AST and Generate VM Code
				JavaCompiler compiler = new JavaCompiler(ast);
				var compiler_result = compiler.Compile();

				// Execute machine Code
				RoaaVM vm = new RoaaVM(compiler_result);
				
				Console.WriteLine("Running...\n");
				vm.Call(entryPoint);


			}
			catch (CompileErrorException ex)
			{
				Console.WriteLine(ex.Message);
				Status = InterpretationStatus.Failed;
				result.Errors.Add(ex.Message);
			}
			Status = InterpretationStatus.Success;
			return result;
		}
	}
}
