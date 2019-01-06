using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Npgsql;

namespace BackupAndRestore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get connection string
            var connString = Environment.GetEnvironmentVariable("MEALS_DB_CONN_STR");
            var tableList = new List<string>()
            {
                "ingredient",
                "unit",
                "store",
                "recipe",
                "shoppinglist",
                "ingredientstore",
                "recipeingredient",
                "shoppinglistrecipe",
            };


            Console.WriteLine("Options:\n" +
                "B: Backup Database\n" +
                "R: Restore Database\n");
            Console.WriteLine("Enter Option: ");
            var entry = Console.ReadLine();
            if(entry.ToUpper() == "B")
            {
                foreach (var table in tableList)
                {
                    BackupTable(connString, table);
                }
            }
            else if (entry.ToUpper() == "R")
            {
                foreach (var table in tableList)
                {
                    RestoreTable(connString, table);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Exiting.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void BackupTable(string connString, string tableName)
        {
            Console.WriteLine($"Backing up table {tableName}...");
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM meals.{tableName}";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dt = new DataTable();
                        dt.TableName = tableName;
                        dt.Load(reader);
                        dt.WriteCsv(new FileStream($"{tableName}.csv", FileMode.Create));
                    }
                }
            }
        }

        private static void RestoreTable(string connString, string tableName)
        {
            Console.WriteLine($"Restoring '{tableName}'...");

            var dataTable = new DataTable(tableName).ReadCsv(new FileStream($"{tableName}.csv", FileMode.Open));
            var columnNames = string.Join(',', dataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName));
            var valuesParamsList = new List<string>();
            for(var i = 0; i < dataTable.Columns.Count; i++)
            {
                valuesParamsList.Add($"@value{i}");
            }
            var valueParams = string.Join(',', valuesParamsList);
            

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                var totalRows = 0;
                foreach(DataRow row in dataTable.Rows)
                {
                    using(var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"insert into meals.{tableName} ({columnNames}) VALUES ({valueParams})";
                        for (var i = 0; i < row.ItemArray.Count(); i++)
                        {
                            cmd.Parameters.Add(new NpgsqlParameter($"@value{i}", row.ItemArray[i]));
                        }
                        totalRows += cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine($"Wrote {totalRows} rows to meals.{tableName}");
            }
        }
    }
}
