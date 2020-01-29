using System;
using System.Data.SqlClient;
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
                    case "2":
                        addUser();
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
            Console.WriteLine("2 - dodawanie uzytkownika");

            if(isAdmin)
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
            if(areaName == "basen")
            {
                areaName = "1";
                lockNo = rnd.Next(1,500);
            }
            else if(areaName == "silownia")
            {
                areaName = "2";
                lockNo = rnd.Next(501,650);
            }
            else if(areaName == "spa")
            {
                areaName = "3";
                lockNo = rnd.Next(651,700);
            }

            //budowanie i wykoanie zapytania
            string queryDb = "insert into Silownia.dbo.Reservations (userId, Area, lockerNo, startDate, endDate) values ('"+userAct.getUserId()+"','"+areaName+"','"+lockNo+"','"+date+"T"+startTime+":00','"+date+"T"+endTime+":00')";
            SqlCommand command = new SqlCommand(queryDb,conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
            Console.WriteLine("ok");
        }
        public void addUser()
        {
            Console.WriteLine("Imie: ");
            string userNam = Console.ReadLine();
            Console.WriteLine("Nazwisko: ");
            string userSur = Console.ReadLine();

            Console.WriteLine("Login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Haslo: ");
            string passwd = Console.ReadLine();

            string adm = "0";
            if(isAdmin)
            {
                Console.WriteLine("Admin? (T/N): ");
                string choose = Console.ReadLine().ToLower();
                if (choose == "t")
                    adm = "1";
            }

            string query = "insert into Silownia.dbo.UsersCreds (UserName, UserSurname, is_admin) values ('" + userNam + "','" + userSur + "','" + adm + "')";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader1 = command.ExecuteReader();
            reader1.Close();

            string query2 = "select UserId from Silownia.dbo.UsersCreds where UserName='" + userNam + "' and UserSurname='" + userSur + "'";
            SqlCommand com2 = new SqlCommand(query2, conn);
            SqlDataReader read2 = com2.ExecuteReader();
            read2.Read();
            string uId = read2[0].ToString();
            read2.Close();

            string query3 = "insert into Silownia.dbo.UsersLogins (UserId, Login, Passwd) values ('" + uId + "','" + login + "','" + passwd + "')";
            SqlCommand com3 = new SqlCommand(query3, conn);
            SqlDataReader reader3 = com3.ExecuteReader();
            reader3.Close();

            Console.WriteLine("ok");
        }

    }
}
