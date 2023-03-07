using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	internal class Symbols
	{
		private readonly Symbols enclosing;
		
		public Dictionary<string, Method> Functions { get; set; } = new Dictionary<string, Method>();

		public Symbols()
		{
			enclosing = null;
		}
		public Symbols(Symbols enclosingScope)
		{
			enclosing = enclosingScope;
		}
	}
}
