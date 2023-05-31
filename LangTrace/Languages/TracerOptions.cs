using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages
{
	public class TracerOptions
	{
		public string EntryPoint { get; set; } = "Class.Main";
		public string LinkedListName { get; set; } = "LinkedList";
		public string LinkedListNodeName { get; set; } = "ListNode";

	}
}
