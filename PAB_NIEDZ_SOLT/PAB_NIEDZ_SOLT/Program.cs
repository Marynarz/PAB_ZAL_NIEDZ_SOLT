using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace PAB_NIEDZ_SOLT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=localhost;Database=Silownia;User Id = sa; Password=dupA123456.";
            try
            {
                conn.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Connection cannot be established");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection cannot be established");
                return;
            };

            SqlCommand command = new SqlCommand("SELECT * FROM Areas", conn);

            SqlDataReader reader = command.ExecuteReader();
            // while there is another record present
            while (reader.Read())
            {
                // write the data on to the screen
                Console.WriteLine(String.Format("{0} \t | {1} \t",
                // call the objects from their index
                reader[0], reader[1]));
            }

            //zamykanie polaczenia!
            conn.Close();
        }
    }
}
