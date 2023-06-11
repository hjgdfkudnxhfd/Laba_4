using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Laba_4
{
    public class Table
    {
        public List<Row> Rows { get; }
        private TableScheme TableScheme { get; }

        public Table(TableScheme tableScheme, string way)
        {
            TableScheme = tableScheme;
            Rows = ReadRows(way);
        }

        private List<Row> ReadRows(string way)
        {
            string[] linesFromFile = File.ReadAllLines(way);
            List<Row> rows = new List<Row>();

            for (int i = 1; i < linesFromFile.Length; i++)
            {
                if (CheckingInformationInFile.CheckingFile(TableScheme, linesFromFile[i].Split(';'), i))
                {
                    rows.Add(new Row(TableScheme, linesFromFile[i]));
                }
            }

            return rows;
        }
    }
}