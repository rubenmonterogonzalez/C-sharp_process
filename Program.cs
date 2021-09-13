using System;
using System.Data.SqlClient;
using System.IO;

namespace ImportCsvToSq {
    class Program
    {
        static void Main(string[] args) 
            {   
            
            //Put your SQL Source here:
            var lineNumber = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=Your_data_source\SQLEXPRESS; Integrated Security= true"))
            {
                conn.Open();
                //Put your file location here:
                using (StreamReader reader = new StreamReader(@"file_csv_path"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (lineNumber != 0)
                        {
                            var values = line.Split(',');

                            var sql = "INSERT INTO CSVImport.dbo.Data VALUES ('" + values[0] + "','" + values[1] + "'," + values[2] + ")";

                            var cmd = new SqlCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = conn;
                            cmd.ExecuteNonQuery();
                        }
                        lineNumber++;
                    }
                }
                conn.Close();
            }
            Console.WriteLine("Data Import Complete");
            Console.ReadLine();
        }
    }
}
