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

		// Special Events
		DeclareLinkedList,
		SetLLNodeNext,
	}
	public class Event
	{
		public EventType EventID { get; set; }
		//public List<string> Arguments { get; set; } = new List<string>();
		public dynamic Data { get; set; }
		public Event(EventType id)
		{
			EventID = id;
		}

		public static Event DeclareVariable(string type, string name, string value)
		{
			return new Event(EventType.InitVariable)
			{
				Data = new
				{
					Type = type,
					Name = name,
					Value = value,
				}
			};
		}

		public static Event ValueChanged(string name, string oldValue, string newValue)
		{
			return new Event(EventType.ValueChanged)
			{
				Data = new
				{
					Name = name,
					OldValue = oldValue,
					NewValue = newValue,
				}
			};
		}

		public static Event DeclareReference(string type, string name, int initID)
		{
			return new Event(EventType.InitReference)
			{
				Data = new
				{
					Type = type,
					Name = name,
					InitID = initID,
				}
			};
		}

		public static Event PrintOutput(string text)
		{
			return new Event(EventType.PrintOutput)
			{
				Data = new
				{
					Text = text,
				}
			};
		}

		public static Event Branch(string comparison, bool result)
		{
			return new Event(EventType.Branching) {
				Data = new
				{
					Comparison = comparison,
					Result = result,
				}
			};
		}

		public static Event CallFunction(string name, params string[] arguments)
		{
			return new Event(EventType.CallFunction)
			{
				Data = new
				{
					Name = name,
					Arguments = arguments
				}
			};
		}

		public static Event ReferenceChanged(string name, int oldID, int newID)
		{
			return new Event(EventType.ReferenceChanged)
			{
				Data = new
				{
					Name = name,
					OldID = oldID,
					NewID = newID
				}
			};
		}

	}
}
