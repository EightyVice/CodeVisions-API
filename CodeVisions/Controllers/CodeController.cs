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
			Models.CodeResponse response = new Models.CodeResponse();

			Interpreter interpreter = null;
			if (request.Language.ToLower() == "java")	
				interpreter = new JavaInterpreter();

			var result = interpreter.Interpret(request.Code, request.TestCode);

			if(interpreter.Status == InterpretationStatus.Success)
			{
				response.HasErrors = false;
				response.Steps = result.Steps;

				response.TestLogs = result.TesterResult.Logs;
				response.CaseResults = result.TesterResult.CaseResults;

			}
			else
			{
				response.HasErrors = true;
				response.Errors = result.Errors;
			}




			//if(request.Language.ToLower() == "java")
			//{
			//	JavaInterpreter interpreter = new JavaInterpreter(request.Code, request.TestCode);
			//	interpreter.Interpret();


			//	if(interpreter.Status == InterpretationStatus.Success)
			//	{
			//		response.HasErrors = false;
			//		response.Steps = interpreter.Steps;

			//		if (interpreter.TesterResult)
			//			response.TestSuccess = true;
			//		else
			//			response.TestSuccess = false;

			//	}
			//	else if(interpreter.Status == InterpretationStatus.Failed)
			//	{
			//		response.HasErrors = true;
			//		response.Errors = interpreter.Errors;
			//		response.TestSuccess = false;
			//	}
			//	else
			//	{
			//		return null;
			//	}
			//}
			return response;
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
