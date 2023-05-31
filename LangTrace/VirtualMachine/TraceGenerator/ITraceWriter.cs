using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.VirtualMachine.TraceGenerator
{
    internal interface ITraceWriter
    {

        /* Metadata */
        public void DefineFunction(string name, string returnType, int arity, string[] locals, string tag = null);
        public void DefineClass(string name, string[] fields, string tag = null);

        /* Events */
        public void NewArray(int line, string[] elements, string tag = null);
        public void SetArrayElement(int line, int arr_id, int index, string value, string tag = null);
        public void SetField(int line, int object_id, int class_id, string fieldName, string value, string tag = null);
        public void Assign(int line, int local_id, string value, string tag = null);
        public void Call(int line, int funcId, string tag, params string[] args);
        public void Return(int line, object returnValue = null);
        public void NewObject(int line, int objID, string tag = null);

    }
}
