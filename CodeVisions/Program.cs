#define CODE_TEST

// Un/comment for dis/enabling local testing and starting the host
#undef CODE_TEST


#if CODE_TEST
using System.Text.Json;
using LangTrace;
using LangTrace.Utilities;
using LangTrace.Languages;
using LangTrace.Languages.Java;
using LangTrace.Languages.Python;

var code = File.ReadAllText("testcode.txt");
var test = File.ReadAllText("cdl_code.txt");

Interpreter interpreter = new JavaInterpreter();
var result = interpreter.Interpret(code, test);
if (interpreter.Status == InterpretationStatus.Success)
{
	//Console.WriteLine(JsonSerializer.Serialize(result.Steps, new JsonSerializerOptions() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping}));
	StepsPrinter.Print(Console.Out, result.Steps);
}
else if (interpreter.Status == InterpretationStatus.Failed)
	Console.WriteLine(result.Errors);
else
	Console.WriteLine("OMG somoething wrong");
return;
#endif

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthorization();
app.MapControllers();

app.Run();
