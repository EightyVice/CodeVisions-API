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
}
