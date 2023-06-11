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
            int dataBaseNumber;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 1 || input == 2)
                    {
                        dataBaseNumber = input - 1;
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
            TableScheme tableScheme = JsonSerializer.Deserialize<TableScheme>(File.ReadAllText(file[dataBaseNumber, 0]));
            string way = file[dataBaseNumber, 1];
            Table table = new Table(tableScheme, way);
            DataOutput.Table(table, tableScheme);
        }
    }
}
