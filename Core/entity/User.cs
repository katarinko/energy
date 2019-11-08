using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Site.UITests.Core.entity
{
    public class User
    {
        private String Login;
        private String Password;

        public User(String Login, String Password)
        {
            this.Login = Login;
            this.Password = Password;
        }

        public String GetLogin()
        {
            return Login;
        }

        public String GetPassword()
        {
            return Password;
        }

       
    }
}
