using System;
namespace PAB_NIEDZ_SOLT
{
    public class Cli
    {
        private bool isAdmin;
        private string userName;
        private bool isLogged;
        public Cli(UserClass usr)
        {
            isAdmin = usr.getAdminState();
            userName = usr.getName();
            isLogged = true;
        }
        //CLI
        public void commandLine()
        {
            while(isLogged)
            {
                Console.WriteLine(userName + "#: ");
                string userCommand = Console.ReadLine();
                switch(userCommand)
                {
                    case "0":
                        //wlogowanie i zakonczenie sesji
                        isLogged = false;
                        break;
                    case "h":
                        //wyswietlenie menu
                        helpMenu();
                        break;
                    case "q":
                        //mozliwosc napisanie zapytania do bazy danych, tylko dla admina
                        if(isAdmin)
                            queryToDb();
                        else
                            Console.WriteLine("Access denied");
                        break;
                    default:
                        Console.WriteLine("Wrong Command!");
                        break;
                }
            }
        }
        //help
        public void helpMenu()
        {
            Console.WriteLine("PAB - projekt System rezerwacji szafek BASEN SPA SILOWNIA");
            Console.WriteLine("Autorzy:");
            Console.WriteLine("Niedzielski - 206074");
            Console.WriteLine("Soltysiak - 206082");
            Console.WriteLine("------");
            Console.WriteLine("0 - log out");

            if(isLogged)
            {
                Console.WriteLine("Admin commands:");
                Console.WriteLine("q - sql query to database");
            }
            Console.WriteLine("h - show this help menu");
        }
        //jesli admin, to moze wydac zapytanie sql bezposrednio do bazy danych
        public void queryToDb()
        {
            Console.WriteLine("Type your SQL query to database: ");
            string queryDb = Console.ReadLine();
            SqlCommand command = new SqlCommand(queryDb, conn);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine(reader);
            }
            reader.Close();
        }

    }
}
