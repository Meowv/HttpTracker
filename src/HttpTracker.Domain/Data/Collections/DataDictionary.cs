using System;
using System.Collections.Generic;

namespace HttpTracker.Data.Collections
{
    public class DataDictionary : Dictionary<string, object>
    {
        public DataDictionary() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public DataDictionary(IEnumerable<KeyValuePair<string, object>> values) : base(StringComparer.OrdinalIgnoreCase)
        {
            foreach (var kvp in values)
            {
                Add(kvp.Key, kvp.Value);
            }
        }
    }
}