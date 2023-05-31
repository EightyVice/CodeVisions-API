using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages
{
	public enum TracerStatus
	{
		NotYet,
		Success,
		Failed,
	}

	public class TracerResult
	{ 
		public ICollection<object> Classes { get; set; } = new List<object>();
		public ICollection<object> Functions { get; set; } = new List<object>();
		public ICollection<object> Traces { get; set; } = new List<object>();

	}

	public abstract class Tracer
	{
		public TracerStatus Status { get; set; }

		public abstract TracerResult ExecuteAndTrace(TracerOptions options, string sourceCode);
	}
}
