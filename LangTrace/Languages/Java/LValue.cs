using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	interface RValue
	{

	}
	interface  LValue
	{
		public string Name { get; }

		public RValue ToRvalue();
	}
}
