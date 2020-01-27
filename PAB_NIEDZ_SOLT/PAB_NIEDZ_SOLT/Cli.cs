using System;
namespace PAB_NIEDZ_SOLT
{
    public class Cli
    {
        UserClass userAct;
        private bool isAdmin;
        private string userName;
        private bool isLogged;
        private SqlConnection conn;
        public Cli(UserClass usr, SqlConnection con)
        {
            isAdmin = usr.getAdminState();
            userName = usr.getName();
            isLogged = true;
            conn = con;
            userAct = usr;
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
                    case "1":
                        reserveLocker();
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
            Console.WriteLine("1 - zarezerwuj szafke");

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

        public void reserveLocker()
        {
            Random rnd = new Random();
            int lockNo = 0;
            //dane do rezerwacji
            Console.WriteLine("Miejsce (Basen, Silownia, SPA): ");
            string areaName = Console.ReadLine().ToLower();
            Console.WriteLine("YYYY-MM-DD: ");
            string date = Console.ReadLine();
            Console.WriteLine("Start: HH:MM: ");
            string startTime = Console.ReadLine();
            Console.WriteLine("End: HH:MM: ");
            string endTime = Console.ReadLine();

            //dane do query
            if(areaName = "basen")
            {
                areaName = "Basen";
                lockNo = rnd.Next(1,500);
            }
            else if(areaName = "silownia")
            {
                areaName = "Silownia";
                lockNo = rnd.Next(501,650);
            }
            else if(areaName = "spa")
            {
                areaName = "SPA";
                lockNo = rnd.Next(651,700);
            }

            //budowanie i wykoanie zapytania
            string queryDb = "insert into Reservations (userId, Area, lockerNo, startDate, endDate) values (\""+userAct.getUserId+"\",\""+areaName+"\",\""+lockNo+"\",\""+date+"T"+startTime+":00\",\""+date+"T"+endTime+"\")";
            SqlCommand command = new SqlCommand(queryDb,conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }

    }
}
