using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LangTrace.Languages;

namespace LangTrace.Utilities
{
	struct TokenPosition
	{
		public int Line;
		public int Start;
		public int End;

		public TokenPosition(int line, int start, int end)
		{
			Line = line;
			Start = start;
			End = end;
		}
	}
}
