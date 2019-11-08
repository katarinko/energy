using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Site.UITests.Core.entity
{
    public class Journalist : User
    {
        private List<String> roles = new List<String>();    
            //{
            //    Roles.CREATE, Roles.DELETE
            //};

        public Journalist(String Login, String Password) : base(Login, Password)
        {
        }

        public static Journalist GetDefaultJournalist()
        {
            return new Journalist("journalist", "1234QweR!!");
        }

        /*public List<String> GetRoles()
        {
            roles.Add String Getroles = Roles.EDIT;
            return roles;
        }*/

        public static Journalist WrongUserName()
        {
            return new Journalist("wrongname", "1234QweR!!");
        }

        public static Journalist WrongPassword()
        {
            return new Journalist("journalist", "wrongpassword");
        }
    }
}
