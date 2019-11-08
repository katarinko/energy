using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Site.UITests.Core.entity
{
    public class CopyEditor : User
    {
        private List<String> roles = new List<String>();

        public CopyEditor(String Login, String Password)
            : base(Login, Password)
        {
        }

        public static Editor GetDefaultCopyEditor()
        {
            return new Editor("copyeditor", "1234QweR!!");
        }

        /*public List<String> GetRoles()
        {
            roles.Add(Roles.EDIT);
            return roles;
        }*/
    }
}