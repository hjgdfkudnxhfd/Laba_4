using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Laba_4
{
    public static class CheckingInformationInFile
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
}