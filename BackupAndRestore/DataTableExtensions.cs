using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BackupAndRestore
{
    public static class DataTableExtensions
    {
        private const char DELIMITER = '|';
        public static void WriteCsv(this DataTable dataTable, Stream stream)
        {
            using(var sw = new StreamWriter(stream))
            {
                var columnNames = string.Join(DELIMITER, dataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName));
                sw.WriteLine(columnNames);

                var columnDataTypes = string.Join(DELIMITER, dataTable.Columns.Cast<DataColumn>().Select(x => x.DataType.AssemblyQualifiedName));
                sw.WriteLine(columnDataTypes);

                foreach(DataRow row in dataTable.Rows)
                {
                    sw.WriteLine(string.Join(DELIMITER, row.ItemArray.Select(x => x.ToString())));
                }
            }
        }

        public static DataTable ReadCsv(this DataTable dataTable, Stream stream)
        {
            using(var sr = new StreamReader(stream))
            {
                var columns = sr.ReadLine().Split(DELIMITER);
                var columnTypes = sr.ReadLine().Split(DELIMITER).Select( t => Type.GetType(t)).ToList();

                for(var i = 0; i < columns.Count(); i++)
                {
                    dataTable.Columns.Add(columns[i], columnTypes[i]);
                }

                var line = sr.ReadLine().Split(DELIMITER);
                while(line != null)
                {
                    var parsedValues = line.Select((x, i) => ConvertToType(x, columnTypes[i])).ToArray();

                    dataTable.Rows.Add(parsedValues);
                    line = sr.ReadLine()?.Split(DELIMITER);
                }

            }
            return dataTable;
        }

        private static IConvertible ConvertToType(string val, Type t)
        {
            if(t == typeof(Int32))
            {
                if (string.IsNullOrEmpty(val))
                {
                    return 0;
                }
                else
                {
                    Int32.TryParse(val, out var result);
                    return result;
                }
            }
            else if(t == typeof(DateTime))
            {
                DateTime.TryParse(val, out var result);
                return result;
            }
            else if(t == typeof(Single))
            {
                float.TryParse(val, out var result);
                return result;
            }
            else if(t == typeof(string)){
                return val;
            }
            throw new InvalidOperationException($"Could not convert the input to the type {t}");
        }
    }
}
