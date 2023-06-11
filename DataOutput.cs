using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Laba_4
{
    class DataOutput
    {
        public static void Table(Table table, TableScheme tableScheme)
        {
            var columns = tableScheme.Columns.Select(c => c.Name).ToArray();
            var columnWidths = columns.Select(c => c.Length).ToArray();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < tableScheme.Columns.Count; j++)
                {
                    string cellValue = table.Rows[i].Data[tableScheme.Columns[j]].ToString();
                    columnWidths[j] = Math.Max(columnWidths[j], cellValue.Length);
                }
            }

            var data = new List<object>();
            foreach (var row in table.Rows)
            {
                var rowData = new Dictionary<string, object>();
                foreach (var column in tableScheme.Columns)
                {
                    rowData[column.Name] = row.Data[column];
                }
                data.Add(rowData);
            }

            var headerRow = string.Join(" ", columns.Select((c, i) => c.PadRight(columnWidths[i])));
            Console.WriteLine(headerRow);
            foreach (var row in table.Rows)
            {
                var rowData = string.Join(" ", tableScheme.Columns.Select((c, i) => row.Data[c].ToString().PadRight(columnWidths[i])));
                Console.WriteLine(rowData);
            }
        }
    }
}