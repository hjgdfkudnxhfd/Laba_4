using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Laba_4
{
    public class Row
    {
        public Dictionary<Column, object> Data { get; } = new Dictionary<Column, object>();

        public Row(TableScheme tableScheme, string lineFromFile)
        {
            string[] columnValues = lineFromFile.Split(';');

            for (int i = 0; i < columnValues.Length; i++)
            {
                Column column = tableScheme.Columns[i];
                object value = columnValues[i];
                Data.Add(column, value);
            }
        }
    }
}