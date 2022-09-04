using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	internal class Callable
	{
		public string Name { get; set; }
		//public IAtom Invoke(JavaParser.ExprFuncCallContext context);
		public Func<IAtom[], IAtom> Body { get; set; }
		public int Arity { get; set; }
	}
}
