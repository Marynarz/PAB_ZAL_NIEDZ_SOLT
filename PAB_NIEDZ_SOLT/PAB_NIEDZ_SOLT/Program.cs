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

            //zestawianie polaczenia z baza dnaych
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=localhost;Database=Silownia;User Id = sa; Password=dupA123456.";
            try
            {
                conn.Open();
            }
            catch(SqlException e)
            {
                Console.WriteLine("Connection cannot be established");
                return;
            };

            //logowanie
            Console.WriteLine("Login: ");
            string userLogin = Console.ReadLine();
            Console.WriteLine("Haslo: ");
            string userPwd = Console.ReadLine();

            SqlCommand command = new SqlCommand("SELECT Passwd FROM UsersLogins WHERE Login='" + userLogin+"'", conn);
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                if (reader[0].ToString() == userPwd)
                {
                    Console.WriteLine("Login ok!");
                    loggedIn = true;

                }
                else
                    Console.WriteLine("Wrong passwd");
            }
            else
            {
                Console.WriteLine("wrong login");
            }
            reader.Close();
            if(loggedIn)
            {
                SqlCommand command1 = new SqlCommand("SELECT UserId FROM UsersLogins WHERE Login='" + userLogin + "'");
                reader = command1.ExecuteReader();
                string uId = reader[0].ToString();
                reader.Close();
                SqlCommand command2 = new SqlCommand("SELECT * FROM UsersCreds WHERE UserId='" + uId + "'");
                reader = command2.ExecuteReader();

                //TODO: userclass
            }

            while(loggedIn)
            {
                Console.WriteLine(userLogin + "#: ");
                string userCommand = Console.ReadLine();
                switch(userCommand)
                {
                    case "0":
                        loggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Command!");
                        break;
                }
            }

            //zamykanie polaczenia!
            conn.Close();
        }
    }
}
