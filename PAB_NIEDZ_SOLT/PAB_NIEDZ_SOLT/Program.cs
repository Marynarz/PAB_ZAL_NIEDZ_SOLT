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

            //krotkie wprowadzenie co to jest.
            Console.WriteLine("PAB - projekt System rezerwacji szafek BASEN SPA SILOWNIA");
            Console.WriteLine("Autorzy:");
            Console.WriteLine("Niedzielski - 206074");
            Console.WriteLine("Soltysiak - 206082");
            
            //haslo do serwera sql
            Console.WriteLine("Haslo bazy danych: ");
            string dbPass = Console.ReadLine();

            //zestawianie polaczenia z baza dnaych
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=localhost;Database=Silownia;User Id = sa; Password="+dbPass;
            try
            {
                conn.Open();
            }
            catch(SqlException e)
            {
                Console.WriteLine("Connection cannot be established");
                return;
            };

            //logowanie do systemu
            Console.WriteLine("Login: ");
            string userLogin = Console.ReadLine();
            Console.WriteLine("Haslo: ");
            string userPwd = Console.ReadLine();

            SqlCommand command = new SqlCommand("SELECT Passwd FROM Silownia.dbo.UsersLogins WHERE Login='" + userLogin+"'", conn);
            SqlDataReader reader = command.ExecuteReader();
            //logowanie wykonanie
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
            
            //jeśli udalo sie zalogowac to ustawiamy kilka zmiennych
            if(loggedIn)
            {
                SqlCommand command1 = new SqlCommand("SELECT userId FROM Silownia.dbo.UsersLogins WHERE Login='" + userLogin + "'",conn);
                SqlDataReader reader1 = command1.ExecuteReader();
                string uId = "";
                if(reader1.Read())
                    uId = reader1[0].ToString();
                reader1.Close();
                SqlCommand command2 = new SqlCommand("SELECT * FROM Silownia.dbo.UsersCreds WHERE UserId='" + uId + "'",conn);
                SqlDataReader reader2 = command2.ExecuteReader();
                reader2.Read();
                Cli comLine = new Cli(new UserClass(reader2[0].ToString(),reader2[1].ToString(),reader2[2].ToString(),(bool)reader2[4]),conn);
                reader2.Close();

                //odpalanie lini komend
                comLine.commandLine();
            }
            //zamykanie polaczenia!
            conn.Close();
        }
    }
}
