using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.VirtualMachine
{

    internal class Object : Value
    {
        public ProgramFile.Class Class { get; set; }
        private Dictionary<string, Value> fields = new Dictionary<string, Value>();

        public static readonly Object NullObject = new Object(null);

        public Object(ProgramFile.Class @class)
        {
            Class = @class;
            if(@class != null)
            {
                foreach (var field in @class.Fields)
                    fields.Add(field.Name, field.Type.Default);
            }

        }
        public Value this[string FieldName]
        {
            get => fields[FieldName];
            set => fields[FieldName] = value;
        }

        public override string ToString()
        {
            string ret = $"{Class?.Name}{{";
            foreach(var kv in fields)
                ret += $"{kv.Key}: {kv.Value} ";
            return ret + "}";
        }
    }
}
