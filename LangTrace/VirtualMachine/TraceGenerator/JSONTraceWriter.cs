using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LangTrace.VirtualMachine.TraceGenerator
{
    internal class JSONTraceWriter : ITraceWriter
    {
        List<object> trace = new List<object>();
        List<object> functions = new List<object>();
        List<object> classes = new List<object>();

        public void SetField(int line, int objectId, string fieldName, string oldVal, string newVal, string tag = null)
        {
            trace.Add(new
            {
                line = line,
                @event = "store_field",
                event_data = new
                {
                    objectId = objectId,
                    fieldName = fieldName,
                    oldVal = oldVal,
                    newVal = newVal,
                },
                tag = tag
            });
        }

        public void Assign(int line, string name, string oldVal, string newVal, string tag = null)
        {
            trace.Add(new
            {
                line = line,
                @event = "store",
                event_data = new
                {
                    name = name,
                    oldValue = oldVal,
                    newValue = newVal,
                },
                tag = tag
            });
        }

        public void Call(int line, string funcName, string tag, params string[] args)
        {
            trace.Add(new
            {
                line = line,
                @event = "call",
                event_data = new
                {
                    funcName = funcName,
                    args = args
                },
                tag = tag
            });
        }

        public void DefineFunction(string name, string returnType, string[] locals, string tag = null)
        {
            functions.Add(new
            {
                name = name,
                returnType = returnType,
                locals = locals,
                tag = tag
            });

        }


        public void Return(int line, object returnValue = null)
        {
            trace.Add(new
            {
                line = line,
                @event = "return",
                event_data = new
                {
                    value = returnValue ?? "void",
                }
            });
        }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(
                new
                {
                    classes,
                    functions,
                    trace
                },
                new JsonSerializerOptions() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }
            );
        }

        public void DefineClass(string name, string[] fields, string tag = null)
        {
            classes.Add(new
            {
                name = name,
                fields = fields,
                tag = tag
            });
        }

        public void SetArrayElement(int line, int arr_id, int index, string oldVal, string newVal, string tag = null)
        {
            trace.Add(new
            {
                line = line,
                @event = "store_element",
                event_data = new
                {
                    index = index,
                    oldValue = oldVal,
                    newValue = newVal
                },
                tag = tag
            });
        }

        private void AddTrace(int line, string eventName, object eventData, string tag = null)
        {
            trace.Add(new
            {
                line = line,
                @event = eventName,
                event_data = eventData,
                tag = tag
            });
        }
        public void NewObject(int line, int objID, string tag = null) => AddTrace(line, "new_object", new { objectID = objID }, tag);
    }

}
