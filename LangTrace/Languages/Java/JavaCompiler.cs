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

		private ByteCodeGenerator _emitter = new ByteCodeGenerator();

		private List<string> _localIDs = new List<string>();
		private List<string> _roIDs = new List<string>();
		private List<string> _strings = new List<string>();

		private Dictionary<string, int> _methods = new Dictionary<string, int>();

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
				
				
				foreach(var method in cls.Methods)
                {
					// Compile methods
					var bytecode = CompileMethod(method);

					// Methods metadata
					methods.Add(new ProgramFile.Function(method.Name, null, method.Parameters.Length, null, bytecode));
                }
            }

			return new ProgramFile(classes.ToArray(), methods.ToArray(), _constants.ToArray());
		}

		private int LocalID(string id)
		{
			if (!_localIDs.Contains(id))
			{
				_localIDs.Add(id);
				return _localIDs.Count - 1;
			}
			
			return _localIDs.IndexOf(id);	
		}

		private int StringsID(string id)
        {
            if (!_roIDs.Contains(id))
            {
				_roIDs.Add(id);
				return _roIDs.Count - 1;
            }

			return _roIDs.IndexOf(id);
		}

		private byte[] CompileMethod(Method method)
        {
			_emitter = new ByteCodeGenerator();

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

			// Emit Jump if false (Jump if equal to zero)
			_emitter.JEQZ((byte)then_byte_code.Length);
			_emitter.AddBytes(then_byte_code);

			if (ifStmt.ElseBranch != null) // There's an Else branch
			{
				var else_byte_code = EmitNoAppend(ifStmt.ElseBranch);
				_emitter.JNEZ((byte)else_byte_code.Length);

				_emitter.AddBytes(else_byte_code);
			}

			_emitter.POP();
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

		void Emit(Expression expr) => expr.Accept(this);
	

		void Emit(Statement stmt) => stmt.Accept(this);

		byte[] EmitNoAppend(Expression expr)
		{
			var main_gen = _emitter;
			_emitter = new ByteCodeGenerator();
			Emit(expr);

			var bytes = _emitter.GetByteCode();

			_emitter = main_gen;
			return bytes;
		}
		byte[] EmitNoAppend(Statement stmt)
		{
			var main_gen = _emitter;
			_emitter = new ByteCodeGenerator();
			Emit(stmt);

			var bytes = _emitter.GetByteCode();

			_emitter = main_gen;
			return bytes;
		}
		#endregion

		#region Expression
		
		public void Visit(Integer integerExpr)
		{
			if(integerExpr.Value >= 1 & integerExpr.Value < 6) _emitter.PUSHI(integerExpr.Value);
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
				foreach (var arg in callExpr.Arguments.Reverse<Expression>())
					Emit(arg);

				_emitter.PRINT();
				return;
			}

			// Emit arguments
			foreach (var arg in callExpr.Arguments)
				Emit(arg);

			// Push Address
			_emitter.PUSHI(_methods[callExpr.Name]);
			
			_emitter.CALL();

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
			byte lhs = (byte)LocalID(assignmentExpr.Lhs.Name);
			_emitter.STORE(lhs);
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
			_emitter.NEW((byte)Array.FindIndex(_program.Classes, c => c.Name == ctorExpr.ClassName));

        }

		public void Visit(FieldAccess fieldExpr)
        {
			// Push object reference
			Emit(fieldExpr.Reference);

			// LOADF <s8: fieldNameID>
			_emitter.LOADF((byte)StringsID(fieldExpr.Field));
        }
        #endregion


    }
}
