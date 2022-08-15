using Microsoft.AspNetCore.Mvc;

using LangTrace.Languages;
using LangTrace.Languages.Java;

using System.Text.Json;

namespace CodeVisions.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CodeController : ControllerBase
	{
		// GET: api/<CodeController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "C", "Java" };
		}

		// GET api/<CodeController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<CodeController>
		[HttpPost]
		public object Post([FromBody] Models.CodeRequest request)
		{
			if(request.Language.ToLower() == "java")
			{
				JavaInterpreter interpreter = new JavaInterpreter(request.Code);
				interpreter.Interpret();
				if(interpreter.Status == InterpretationStatus.Success)
				{
					return interpreter.Steps;
				}
				else if(interpreter.Status == InterpretationStatus.Failed)
				{
					return interpreter.Errors;
				}
				else
				{
					return null;
				}
			}
			return null;
		}

		// PUT api/<CodeController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<CodeController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
