using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LangTrace.VirtualMachine;

namespace LangTrace.Languages.Java
{

	internal class JavaCompiler : IExpressionVisitor, IStatementVisitor
	{
		private CompilationUnit _program;

		private ByteCodeGenerator _emitter = new ByteCodeGenerator(new MemoryStream());

		private List<string> _localIDs = new List<string>();
		private List<string> _roIDs = new List<string>();
		private List<string> _strings = new List<string>();
		private Dictionary<int, int> _lines = new Dictionary<int, int>();

		private List<string> _methodsID = new List<string>();

		public JavaCompiler(CompilationUnit program)
		{
			_program = program;
		}

		// needs to be global since emittion generates constants
		List<Value> _constants = new List<Value>();
		public ProgramFile Compile()
		{			
			// Generate Definitions in ProgramFile
			var classes = new List<ProgramFile.Class>();
			var methods = new List<ProgramFile.Function>();
			foreach(var cls in _program.Classes)
            {

				var fields = new List<(string Name, Descriptor Type)>();

				foreach(var f in cls.Fields)
                {
					Descriptor desc = null;

                    if (f.Type.IsPrimitive)
                    {
                        switch (f.Type.DataType)
                        {
							case DataType.Int: desc = new BaseTypeDescriptor(BaseTypeDescriptor.BaseType.Int); break;
							case DataType.Float: desc = new BaseTypeDescriptor(BaseTypeDescriptor.BaseType.Float); break;
						}
                    }
					else if (f.Type.IsReference)
                    {
						desc = new ClassDescriptor(f.Type.ClassName);
                    }

					fields.Add((f.Name, desc));
                }

				classes.Add(new ProgramFile.Class(cls.Name, fields.ToArray()));
				
				foreach(var method in cls.Methods) // Methods indeces needs to be known first before compilation
					_methodsID.Add(method.Name);


				foreach (var method in cls.Methods)
                {
					// Compile methods
					var bytecode = CompileMethod(method);

					// Parameters are considered locals in RVM
					var parameters = method.Parameters;
                    if (!method.IsStatic)
                    {
						var l = method.Parameters.ToList();
						l.Insert(0, new Declaration(new TypeDescriptor() { IsReference = true, ClassName = method.ClassName }, "this"));
						parameters = l.ToArray();
					}

					var locals = new List<(string Name, Descriptor Type)>();

					foreach(var param in parameters) 
						locals.Add((param.Name, null));

					foreach(var local in method.Locals)
						foreach(var l in local.Variables) locals.Add((l.Name, null)); 
            

					// Methods metadata
					methods.Add(new ProgramFile.Function(method.Name, null, parameters.Length, locals.ToArray(), bytecode, _lines));
                }
            }

			return new ProgramFile(classes.ToArray(), methods.ToArray(), _constants.ToArray(), _roIDs.ToArray());
		}

		private byte LocalID(string id)
		{
			if (!_localIDs.Contains(id))
			{
				_localIDs.Add(id);
				return (byte)(_localIDs.Count - 1);
			}
			
			return (byte)_localIDs.IndexOf(id);	
		}

		private byte StringsID(string id)
        {
            if (!_roIDs.Contains(id))
            {
				_roIDs.Add(id);
				return (byte)(_roIDs.Count - 1);
            }

			return (byte)(_roIDs.IndexOf(id));
		}

		private byte[] CompileMethod(Method method)
        {
			_emitter = new ByteCodeGenerator(new MemoryStream());
			_localIDs = new List<string>();
			_lines = new Dictionary<int, int>();

			foreach(var stmt in method.Statements){
				Emit(stmt);
            }

			_emitter.RET();

			return _emitter.GetByteCode();
        }

		#region Statement
		public void Visit(BlockStatement blockStmt)
		{
			throw new NotImplementedException();
		}

		public void Visit(IfStatement ifStmt)
		{
			// Emit Condition
			Emit(ifStmt.Condition);

			// Get Then Branch bytecode
			var then_byte_code = EmitNoAppend(ifStmt.ThenBranch);



			if (ifStmt.ElseBranch != null) // There's an Else branch
			{
				// Emit Jump if false (Jump if equal to zero)
				_emitter.JEQZ((byte)(then_byte_code.Length + 2));
				_emitter.AddBytes(then_byte_code);

				var else_byte_code = EmitNoAppend(ifStmt.ElseBranch);
				_emitter.JMP((byte)else_byte_code.Length);
				//_emitter.JNEZ((byte)else_byte_code.Length);

				_emitter.AddBytes(else_byte_code);
            }
            else
            {
				// Emit Jump if false (Jump if equal to zero)
				_emitter.JEQZ((byte)then_byte_code.Length);
				_emitter.AddBytes(then_byte_code);
			}

		}

		public void Visit(WhileStatement whileStmt)
		{
			// Emit Condition
			Emit(whileStmt.Condition);

			// Get the body bytecode
			var body_byte_code = EmitNoAppend(whileStmt.Body);

			var backward = _emitter.Length;

			_emitter.JEQZ((byte)body_byte_code.Length);
			_emitter.AddBytes(body_byte_code);

			// Push offset( -5 for PUSH (NUMBER))
			_emitter.PUSHI(backward - _emitter.Length - 6);
			_emitter.EmitOpcode(Opcode.JMP);

			// Pop Condition
			_emitter.EmitOpcode(Opcode.POP);
		}
		public void Visit(ExpressionStatement exprStmt)
		{
			Emit(exprStmt.expression);
		}

		void addlineTable(int line, int start, int end)
		{
			for (int pc = start; pc <= end; pc++)
			{
				if(!_lines.ContainsKey(pc))
					_lines.Add(pc, line);
			}
		}
		void Emit(IExpression expr)
		{
			int start_pc = _emitter.Length;
			expr.Accept(this);
			int end_pc = _emitter.Length;
			addlineTable(expr.Position.Line, start_pc, end_pc);
		}

		void Emit(IStatement stmt)
		{
			int start_pc = _emitter.Length;
			stmt.Accept(this);
			int end_pc = _emitter.Length;
			addlineTable(stmt.Position.Line, start_pc, end_pc);
		}

		byte[] EmitNoAppend(IExpression expr)
		{
			var main_gen = _emitter;
			_emitter = new ByteCodeGenerator(new MemoryStream());
			Emit(expr);

			var bytes = _emitter.GetByteCode();

			_emitter = main_gen;
			return bytes;
		}
		byte[] EmitNoAppend(IStatement stmt)
		{
			var main_gen = _emitter;
			_emitter = new ByteCodeGenerator(new MemoryStream());
			Emit(stmt);

			var bytes = _emitter.GetByteCode();

			_emitter = main_gen;
			return bytes;
		}
		#endregion

		#region Expression
		public void Visit(ArrayExpression arrayExpr)
        {
			// ARRAY <type> <length> <elements...>
			int arr_length = arrayExpr.Elements.Length;

			if (arr_length == 0)
				return;

			// push 
			foreach (var expr in arrayExpr.Elements)
				Emit(expr);
            
			_emitter.ARRAY((byte)arr_length);
        }
		public void Visit(Integer integerExpr)
		{
			if(integerExpr.Value >= 0 & integerExpr.Value < 6) _emitter.PUSHI(integerExpr.Value);
			else
            {
				var index = _constants.FindIndex(i => i is SInt32 && ((SInt32)i).Value == integerExpr.Value);
				_emitter.CPUSH((byte)index);
            }
		}

		public void Visit(Null nullExpr)
        {
			if (nullExpr == Null.Reference)
				_emitter.EmitOpcode(Opcode.PNULL);
        }

		public void Visit(Boolean booleanExpr)
		{
			throw new NotImplementedException();
		}

		public void Visit(BinaryExpression binaryExpr)
		{
			// Emit Operands
			Emit(binaryExpr.Left);
			Emit(binaryExpr.Right);

			switch (binaryExpr.Operator)
			{
				case ArithmeticOperator.Plus:			_emitter.EmitOpcode(Opcode.ADDI); break;
				case ArithmeticOperator.Minus:			_emitter.EmitOpcode(Opcode.SUBI); break;
				case ArithmeticOperator.Asterisk:		_emitter.EmitOpcode(Opcode.MULI); break;
				case ArithmeticOperator.Slash:			_emitter.EmitOpcode(Opcode.DIVI); break;
				case ArithmeticOperator.Equal:			_emitter.EmitOpcode(Opcode.EQUL); break;
				case ArithmeticOperator.NotEqual:		_emitter.EmitOpcode(Opcode.NEQU); break;
				case ArithmeticOperator.Greater:		_emitter.EmitOpcode(Opcode.MORE); break;
				case ArithmeticOperator.GreaterEqual:	_emitter.EmitOpcode(Opcode.MEQU); break;
				case ArithmeticOperator.Less:			_emitter.EmitOpcode(Opcode.LESS); break;
				case ArithmeticOperator.LessEqual:		_emitter.EmitOpcode(Opcode.LEQU); break;
			}

		}

		public void Visit(FunctionCall callExpr)
		{
			
			if (callExpr.Name == "print")
			{
				// Emit arguments in REVERSE order
				foreach (var arg in callExpr.Arguments.Reverse<IExpression>())
					Emit(arg);

				_emitter.PRINT();
				return;
			}

			// Emit arguments
			foreach (var arg in callExpr.Arguments)
				Emit(arg);

			// Push Address
			_emitter.CALL((byte)_methodsID.IndexOf(callExpr.Name));

		}

		public void Visit(Identifier idExpr)
		{
			_emitter.LOAD((byte)LocalID(idExpr.Name));
		}

		public void Visit(Assignment assignmentExpr)
		{
			// Right-Hand Side
			Emit(assignmentExpr.Value);

			// Left-Hand Side
			if(assignmentExpr.Lhs is FieldAccess) // Is field
            {
				var fa = (FieldAccess)assignmentExpr.Lhs;

				// Push Reference
				Emit(fa.Reference);

				// Push Field Name
				_emitter.FSTOR(StringsID(fa.Field));
            }
            else // Is Local variable
            {
				_emitter.STORE(LocalID(((Identifier)assignmentExpr.Lhs).Name));
            }
		}

		public void Visit(FieldAccess fieldAccess)
        {
			// Push Accessee
			Emit(fieldAccess.Reference);

			// Push Field
			_emitter.FLOAD(StringsID(fieldAccess.Field));
        }
        public void Visit(ConstructorCall ctorExpr)
        {
			// Emit Arguments
			if(ctorExpr.Arguments != null)
            {
				foreach (var arg in ctorExpr.Arguments)
					Emit(arg);
            }

			// Push NEW $CLASS_ID
			byte index = (byte)Array.FindIndex(_program.Classes, c => c.Name == ctorExpr.ClassName);
			_emitter.NEW(index);


        }
        #endregion


    }
}
