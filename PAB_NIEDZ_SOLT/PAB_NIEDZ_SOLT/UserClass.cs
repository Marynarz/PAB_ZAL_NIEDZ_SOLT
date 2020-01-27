using System;
namespace PAB_NIEDZ_SOLT
{
    public class UserClass
    {
        private bool isAdmin;
        private string userName;
        private string userSurename;
        private string userId;
        public UserClass(string usId, string usName,string usSurnam,bool adm)
        {
            userId = usId;
            isAdmin = adm;
            userName = usName;
            userSurename = usSurnam;
        }
        public bool getAdminState()
        {
            return isAdmin;
        }
        public string getName()
        {
            return userName;
        }
        public string getUserId()
        {
            return userId;
        }

    }
}
