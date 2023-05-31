using System.IO;

using LangTrace;
using LangTrace.Languages.Java;

JavaTracer interpreter = new JavaTracer();
interpreter.ExecuteAndTrace(new LangTrace.Languages.TracerOptions() { EntryPoint = "Main" }, File.ReadAllText("code.java"));

