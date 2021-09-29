using System;
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace CreateTable
{
    class Program
    {
        static void Main(string[] args)
        {
            string file_name;
            Console.Write("Enter file name: ");
            file_name = Console.ReadLine();
            var cs = @"Server=localhost\SQLEXPRESS;Database=testdb;Trusted_Connection=True;";

            using var Con = new SqlConnection(cs);
            Con.Open();

            using var Cmd = new SqlCommand();
            Cmd.Connection = Con;

            Cmd.CommandText = "DROP TABLE IF EXISTS inventory";
            Cmd.ExecuteNonQuery();

            Cmd.CommandText = @"CREATE TABLE inventory(
                id int identity(1,1) NOT NULL PRIMARY KEY,
                name VARCHAR(255) NOT NULL,
                price INT
            )";
            using (FileStream fs = File.Open(file_name, FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }
            
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ComputerShop;Integrated Security=True");
            string query =
            @"CREATE TABLE dbo.Products
                (
                    ID int IDENTITY(1,1) NOT NULL,
                    Name nvarchar(50) NULL,
                    Price nvarchar(50) NULL,
                    Date datetime NULL,
                    CONSTRAINT pk_id PRIMARY KEY (ID)
                );";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table Created Successfully");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                Con.Close();
                Console.ReadKey();
            }
        }
    }
}