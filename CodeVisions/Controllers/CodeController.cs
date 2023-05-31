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
			return new string[] { "python", "java" };
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
			if(request.Language == "java")
            {

				JavaTracer code_tracer = new JavaTracer();
				TracerOptions options = new TracerOptions()
				{
					EntryPoint = "Main"
				};

				return code_tracer.ExecuteAndTrace(options, request.Code);
				
            }
			return "WRONG";
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
