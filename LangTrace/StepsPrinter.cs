using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LangTrace.Languages;

namespace LangTrace.Utilities
{
	public class StepsPrinter
	{
		public static void Print(TextWriter output, ICollection<Step> steps)
		{
			foreach(Step step in steps)
			{
				output.Write($"{step.Line},{step.Start},{step.End}: \t{step.Event.EventID.ToString().ToUpper()} ");

				switch (step.Event.EventID)
				{
					case EventType.PrintOutput: Console.WriteLine($"\"{step.Event.Data.Text}\"");
						break;
					case EventType.InitVariable:
						break;
					case EventType.InitArray:
						break;
					case EventType.InitReference:
						break;
					case EventType.ValueChanged:
						break;
					case EventType.Comparison:
						break;
					case EventType.ReferenceChanged:
						break;
					case EventType.Loop:
						break;
					case EventType.Branching:
						break;
					case EventType.CallFunction:
						break;
					case EventType.DeclareClass:
						break;
					case EventType.DeclareLinkedList:
						break;
					case EventType.SetLLNodeNext:
						break;
				}

				Console.WriteLine();
			}
		}
	}
}
