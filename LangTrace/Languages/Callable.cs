using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages
{
	internal class Callable
	{
		public string Name { get; set; }
		//public IAtom Invoke(JavaParser.ExprFuncCallContext context);
		public Func<object[], object> Body { get; set; }
		public int Arity { get; set; }
		public ICollection<(string paramName, Type paramType)> Parameters { get; set; } = new List<(string paramName, Type paramType)>();
	}
}
