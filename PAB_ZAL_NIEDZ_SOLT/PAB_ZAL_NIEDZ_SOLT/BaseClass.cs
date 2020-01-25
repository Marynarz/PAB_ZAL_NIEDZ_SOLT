using System;
using System.Data.SqlClient;

namespace PAB_ZAL_NIEDZ_SOLT
{
    public class BaseClass
    {
        public BaseClass()
        {
            Console.WriteLine("Poczatek");
            SqlConnection newConnection = new SqlConnection();
        }
    }
}
