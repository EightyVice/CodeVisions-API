using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages
{
	public class Step
	{
		public string Text { get; set; }
		public int Start { get; set; }
		public int End { get; set; }
		public int Line { get; set; }
		public Event Event { get; set; }

		public ICollection<string> Attributes { get; set; }
		public void GetFromParsingContext(ParserRuleContext context, List<string> attributes)
		{
			Start = context.Start.StartIndex;
			End = context.Stop.StopIndex;
			Line = context.start.Line;
			Text = context.GetText();
			Attributes = attributes;
		}
	}
}
