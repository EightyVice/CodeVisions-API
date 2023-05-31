using LangTrace.Languages;
using LangTrace.Languages.Java;

namespace CodeVisions.Models
{
	public class CodeRequest
	{
		public string Language { get; set; }
		public string Code { get; set; }
		public string TestCode { get; set; }
		public Options Options { get; set; }
	}

	public class Options
	{
		public bool AllowLoops { get; set; } = false;
	}

	public class CodeResponse
	{
		public bool HasErrors { get; set; }
		public ICollection<Step> Steps { get; set; }
		public ICollection<string> Errors { get; set; }
		public ICollection<string> TestLogs { get; set; }
		//public ICollection<CaseResult> CaseResults { get; set; }

	}
}
