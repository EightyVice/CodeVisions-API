using System.IO;

using LangTrace;
using LangTrace.Languages.Java;

JavaInterpreter interpreter = new JavaInterpreter();
interpreter.Interpret(File.ReadAllText("code.java"), "Main");

