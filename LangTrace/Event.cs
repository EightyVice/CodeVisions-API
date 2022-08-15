using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages
{
	public enum EventType
	{
		InitVariable,
		InitArray,
		InitReference,
		ValueChanged,
		Comparison,
		PrintOutput,
		ReferenceChanged,
		Loop,
		Branching,
		CallFunction,
		DeclareClass,
		DeclareLinkedList,
	}
	public class Event
	{
		public EventType EventID { get; set; }
		public List<string> Arguments { get; set; } = new List<string>();
		
		public Event(EventType id)
		{
			EventID = id;
		}
	}
}
