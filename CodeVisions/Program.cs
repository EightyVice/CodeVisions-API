#define CODE_TEST


//#undef CODE_TEST


#if CODE_TEST
using System.Text.Json;
using LangTrace;
using LangTrace.Languages.Java;

var inputs = File.ReadAllText("testcode.txt").Split("[START TEST CODE]");

JavaInterpreter interpreter = new JavaInterpreter(inputs[0], inputs[1]);
interpreter.Interpret();
if (interpreter.Status == InterpretationStatus.Success) ;
	//Console.WriteLine(JsonSerializer.Serialize(interpreter.Steps, new JsonSerializerOptions() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping}));
else if (interpreter.Status == InterpretationStatus.Failed)
	Console.WriteLine(interpreter.Errors);
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
