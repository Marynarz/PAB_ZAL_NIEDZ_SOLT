using System;
namespace PAB_NIEDZ_SOLT
{
    public class UserClass
    {
        private bool isAdmin;
        private string userName;
        private string userSurename;
        public UserClass(string usName,string usSurnam,bool adm)
        {
            isAdmin = adm;
            userName = usName;
            userSurename = usSurnam;
        }

    }
}
