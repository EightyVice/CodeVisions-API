using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	internal interface IAtom
	{
		public DataType Type { get; }
	}
}
