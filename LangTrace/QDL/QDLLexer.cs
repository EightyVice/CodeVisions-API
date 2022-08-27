using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.QDL
{
	internal enum TokenType
	{
		Colon, Comma,
		Identifier,
		String, StringBlock, Number, Literal, CodeBlock,
		OpenBrace, CloseBrace, OpenParenthesis, CloseParenthesis,
		At, QuestionMark, Dot,
		Minus, Plus, Asterisk, Slash,
		NewLine,
	}

	internal class Token
	{
		public TokenType Type { get; }
		public object Literal { get; }
		public string Lexeme { get; }
		public long Position { get; }

		public Token(TokenType type, object? literal, string lexeme, long position)
		{
			Type = type;
			Literal = literal;
			Lexeme = lexeme;
			Position = position;
		}

		public override string ToString()
		{
			return $"{Type}: [{Lexeme}]";
		}
	}

	internal class QDLLexer
	{ 
		public bool HasError { get; }

		List<Token> tokens = new List<Token>();
		private int linesCount = 1;

		private int pos;
		private string source;

		private char? Peek()
		{
			if (pos < source.Length)
				return source[pos];
			else
				return null;
		}

		private char? PeekNext()
		{
			if (pos + 1 != source.Length)
				return source[pos + 1];
			else
				return null;
		}

		private char? GetChar()
		{
			if (pos < source.Length)
			{
				char ret = source[pos];
				pos++;
				return ret;
			}
			else return null;
		}

		private void Step() => pos++;
		private string ReadString(char c)
		{
			int start = pos;
			while (Peek() != c)
			{
				Step();
				if (Peek() != c && pos + 1 == source.Length)
				{
					Step(); // skip the last character
					return null;
				}
			}
			string str = source.Substring(start, pos - start);
			Step(); // skip the closing quotation
			return str;
		}
		
		
		private void addToken(TokenType tokenType, string lexeme, object? literal)
		{
			tokens.Add(new Token(tokenType, literal, lexeme, pos));
		}

		public List<Token> Scan(string code)
		{
			source = code;
			tokens.Clear();

			char? c;
			while(pos < code.Length)
			{
				c = GetChar();
				if (c == null) throw new EndOfStreamException();

				switch (c)
				{
					case '(': addToken(TokenType.OpenParenthesis, "(", null); break;
					case ')': addToken(TokenType.CloseParenthesis, ")", null); break;
					case '{': addToken(TokenType.OpenBrace, "{", null); break;
					case '}': addToken(TokenType.CloseBrace, "}", null); break;
					case ',': addToken(TokenType.Comma, ",", null); break;
					case '.': addToken(TokenType.Dot, ".", null); break;
					case '-': addToken(TokenType.Minus, "-", null); break;
					case '+': addToken(TokenType.Plus, "+", null); break;
					case '*': addToken(TokenType.Asterisk, "*", null); break;
					case ' ':
					case '\r':
					case '\t':
						break;
					case '\n': addToken(TokenType.NewLine, Environment.NewLine, null);  linesCount++; break;
				}
			}
			return tokens;
		}
	}
}
