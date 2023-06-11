using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Laba_4
{
    class Program
    {
        static void Main()
        {
            string[,] file = new string[,] { { "Stringed_Instruments.json", "Stringed_Instruments.csv" }, 
                                             { "Computer_Games.json", "Computer_Games.csv" } };
            Console.WriteLine("Есть две базы данных:\n" + 
                              "1. Stringed_Instruments\n" + 
                              "2. Computer_Games" + 
                              "\nКакую базу хотите считать?");
            int dataBase;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 1 || input == 2)
                    {
                        dataBase = input - 1;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод: введите число 1 или 2.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод: нужно число.");
                }
            }
            
            Console.WriteLine();
            TableScheme tableScheme = JsonSerializer.Deserialize<TableScheme>(File.ReadAllText(file[dataBase, 0]));
            string way = file[dataBase, 1];
            Table table = new Table(tableScheme, way);
            DataOutput.Table(table, tableScheme);
        }
    }

    public class TableScheme////////////
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

    public class Table//////////все//////
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

    public class Column//////////все//////
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; }

        public Column(string name, string type, bool isPrimary)
        {
            Name = name;
            Type = type;
            IsPrimary = isPrimary;
        }
    }

    public class Row///////////
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

    public static class CheckingInformationInFile //////////все////////
    {
        public static bool CheckingFile(TableScheme tableScheme, string[] line, int rowNumber)
        {
            if (tableScheme.Columns.Count != line.Length)
            {
                Console.WriteLine($"Неверное количество столбцов в {rowNumber} строке");
                return false;
            }

            bool correctnessOfData = true;
            for (int i = 0; i < line.Length; i++)
            {
                if (!CheckingDataType(tableScheme.Columns[i], line[i], rowNumber, i + 1))
                {
                    correctnessOfData = false;
                }
            }
            return correctnessOfData;
        }

        private static void PrintDataError(int rowNumber, int columnNumber)
        {
            Console.WriteLine($"Тип данных неверен в {rowNumber} строке и в {columnNumber} столбце.");
        }

        private static bool CheckingDataType(Column column, string value, int rowNumber, int columnNumber)
        {
            switch (column.Type)
            {
                case "integer":
                    if (!int.TryParse(value, out var intValue))
                    {
                        PrintDataError(rowNumber, columnNumber); return false;
                    }
                    break;
                case "float":
                    if (!float.TryParse(value, out var floatValue))
                    {
                        PrintDataError(rowNumber, columnNumber); return false;
                    }
                    break;
                case "double":
                    if (!double.TryParse(value, out var doubleValue))
                    {
                        PrintDataError(rowNumber, columnNumber); return false;
                    }
                    break;
                case "boolean":
                    if (!bool.TryParse(value, out var booleanValue))
                    {
                        PrintDataError(rowNumber, columnNumber); return false;
                    }
                    break;
                case "dateTime":
                    if (!DateTime.TryParse(value, out var dateTimeValue))
                    {
                        PrintDataError(rowNumber, columnNumber); return false;
                    }
                    break;
            }
            return true;
        }
    }

    class DataOutput
    {
        public static void Table(Table table, TableScheme tablescheme)
        {
            var columns = tablescheme.Columns.Select(c => c.Name).ToArray();
            var columnWidths = columns.Select(c => c.Length).ToArray();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < tablescheme.Columns.Count; j++)
                {
                    string cellValue = table.Rows[i].Data[tablescheme.Columns[j]].ToString();
                    columnWidths[j] = Math.Max(columnWidths[j], cellValue.Length);
                }
            }

            var data = new List<object>();
            foreach (var row in table.Rows)
            {
                var rowData = new Dictionary<string, object>();
                foreach (var column in tablescheme.Columns)
                {
                    rowData[column.Name] = row.Data[column];
                }
                data.Add(rowData);
            }

            var headerRow = string.Join(" ", columns.Select((c, i) => c.PadRight(columnWidths[i])));
            Console.WriteLine(headerRow);
            foreach (var row in table.Rows)
            {
                var rowData = string.Join(" ", tablescheme.Columns.Select((c, i) => row.Data[c].ToString().PadRight(columnWidths[i])));
                Console.WriteLine(rowData);
            }
        }
    }
}
