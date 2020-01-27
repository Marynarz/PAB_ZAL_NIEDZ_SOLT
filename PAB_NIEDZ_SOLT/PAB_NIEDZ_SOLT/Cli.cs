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
        public void commandLine()
        {
            while(isLogged)
            {
                Console.WriteLine(userLogin + "#: ");
                string userCommand = Console.ReadLine();
                switch(userCommand)
                {
                    case "0":
                        isLogged = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Command!");
                        break;
                }
            }
        }

    }
}
