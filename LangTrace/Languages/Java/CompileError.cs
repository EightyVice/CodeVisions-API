using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace
{

	[Serializable]
	public class CompileErrorException : Exception
	{
		public CompileErrorException() { }
		public CompileErrorException(string message) : base(message) { }
		public CompileErrorException(string message, Exception inner) : base(message, inner) { }
		protected CompileErrorException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
