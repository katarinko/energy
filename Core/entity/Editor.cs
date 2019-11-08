using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Site.UITests.Core.entity
{
    public class Editor : User
    {
        private List<String> roles = new List<String>(); 

         public Editor(String Login, String Password) : base(Login, Password)
        {
        }

         public static Editor GetDefaultEditor()
        {
            return new Editor("editor", "1234QweR!!");
        }

        /*public List<String> GetRoles()
        {
            roles.Add(Roles.EDIT);
            return roles;
        }*/
    }
}

