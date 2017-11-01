using System;

namespace Modular.Shared.Data
{
    public class DataMapItem
    {
        public string PropertyName { get; set; }
        public string EntryRefName { get; set; }
        public bool IsIgnore { get; set; }
        public bool IsWriteDb { get; set; }
        public bool IsRef { get; set; }
        public bool IsList { get; set; }
        public bool IsNested { get; set; }
        public Type ClassType { get; set; }
        public Type PropertyType { get; set; }
    }
}
