using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	interface RValue : IAtom
	{

	}
	interface  LValue : IAtom
	{
		public string Name { get; }

		public RValue ToRvalue();
	}
}
