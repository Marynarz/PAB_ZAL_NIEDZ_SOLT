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
            bool loggedIn = false;

            Console.WriteLine("PAB - projekt System rezerwacji szafek BASEN SPA SILOWNIA");
            Console.WriteLine("Autorzy:");
            Console.WriteLine("Niedzielski - 206074");
            Console.WriteLine("Soltysiak - 206082");
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
            Console.WriteLine("Login: ");
            string userLogin = Console.ReadLine();
            Console.WriteLine("Haslo: ");
            string userPwd = Console.ReadLine();

            SqlCommand command = new SqlCommand("SELECT Passwd FROM UsersLogins WHERE Login='" + userLogin+"'", conn);
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                if (reader[0].ToString() == userPwd)
                    Console.WriteLine("Login ok!");
                else
                    Console.WriteLine("Wrong passwd");
            }
            else
            {
                Console.WriteLine("wrong login");
            }
            reader.Close();

            //zamykanie polaczenia!
            conn.Close();
        }
    }
}
