using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Laba_4
{
    public class TableScheme
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("columns")]
        public List<Column> Columns { get; }

        public TableScheme(string name, List<Column> columns)
        {
            Name = name;
            Columns = columns;
        }
    }
}