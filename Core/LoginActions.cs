using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Site.UITests.Core.entity;

namespace Umbraco.Site.UITests
{
    public class LoginActions
    {
        public ContentPage LoginAs(User user)
        {
            new LoginPage()
            .SetLogin(user.GetLogin())
            .SetPassword(user.GetPassword())
            .Submit();
            return new ContentPage();
        }

        public  LoginPage Logout()
        {

            return new ContentPage()
            .ClickLogout();
            
        }

        public  string LoginWithoutName(User user)
        {
           var loginPage = new LoginPage();
           loginPage.EmptyLoginName();
           loginPage.SetPassword(user.GetPassword());
           loginPage.Submit();
           return loginPage.GetErrorMessage();
        }

        
        public string LoginWithoutPassword(User user)
        {
           var loginPage = new LoginPage();
           loginPage.SetLogin(user.GetLogin());
           loginPage.EmptyPassword();
           loginPage.Submit();
           return loginPage.GetErrorMessage();
        }

        public string LoginWrongName(User user)
        {
            var loginPage = new LoginPage();
            loginPage.SetLogin(user.GetLogin());
            loginPage.SetPassword(user.GetPassword());
            loginPage.Submit();
            return loginPage.GetErrorMessage();
         }

        public string LoginWrongPassword(User user)
        {

           var loginPage = new LoginPage(); 
           loginPage.SetLogin(user.GetLogin());
           loginPage.SetPassword(user.GetPassword());
           loginPage.Submit();
           return loginPage.GetErrorMessage();
        }
    }
}