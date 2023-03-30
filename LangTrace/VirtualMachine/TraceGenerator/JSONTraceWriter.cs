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
        List<object> traces = new List<object>();
        List<object> functions = new List<object>();
        List<object> classes = new List<object>();

        public void SetField(int line, int objectId, string fieldName, string value, string tag = null)
        {
            traces.Add(new
            {
                line = line,
                @event = "store_field",
                event_data = new
                {
                    objectId = objectId,
                    fieldName = fieldName,
                    value = value,
                },
                tag = tag
            });
        }

        public void Assign(int line, int localID, string value, string tag = null)
        {
            traces.Add(new
            {
                line = line,
                @event = "store",
                event_data = new
                {
                    localId = localID,
                    value = value,
                },
                tag = tag
            });
        }

        public void Call(int line, int funcId, string tag, params string[] args)
        {
            traces.Add(new
            {
                line = line,
                @event = "call",
                event_data = new
                {
                    funcId = funcId,
                    args = args
                },
                tag = tag
            });
        }

        public void DefineFunction(string name, string returnType, int arity, string[] locals, string tag = null)
        {
            functions.Add(new
            {
                name = name,
                returnType = returnType,
                arity = arity,
                locals = locals,
                tag = tag
            });

        }


        public void Return(int line, object returnValue = null)
        {
            traces.Add(new
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
                    traces
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

        public void SetArrayElement(int line, int arr_id, int index, string value, string tag = null)
        {
            traces.Add(new
            {
                line = line,
                @event = "store_element",
                event_data = new
                {
                    arrayId = arr_id,
                    index = index,
                    value = value,
                },
                tag = tag
            });
        }

        private void AddTrace(int line, string eventName, object eventData, string tag = null)
        {
            traces.Add(new
            {
                line = line,
                @event = eventName,
                event_data = eventData,
                tag = tag
            });
        }
        public void NewObject(int line, int clsID, string tag = null) => AddTrace(line, "new_object", new { classID = clsID }, tag);

        public void NewArray(int line, string[] elements, string tag = null)
            => AddTrace(line, "new_array", new { elements = elements }, tag);
    }

}
