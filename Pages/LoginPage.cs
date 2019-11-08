using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium.Support.PageObjects;
using Umbraco.Site.UITests.Core;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;

namespace Umbraco.Site.UITests
{
   public class LoginPage : BasePage 
   {

    By LoginPageLocator = By.XPath("//localize[text()='Login']");
    By UserNameLocator = By.Name("username");
    By PasswordLocator = By.Name("password");
    By LoginButtonLocator = By.XPath("//localize[text()='Logink']");
    By ErrorMessageLocator = By.XPath("//div[@class='text-error ng-binding']");
    
   
    //String articleXpath = "//div[@name='%s']";
    

    public LoginPage() {
        WaitPageLoaded(LoginPageLocator, 6000, "Login Page");
    }

    public LoginPage SetLogin(String login)
    {
        Element.WaitUntilDisplayed(UserNameLocator, 5000);
        Element.ClearField(UserNameLocator);
        Commons.Sleep(3000);
        Element.InputText(UserNameLocator, login);
        return this;
    }

    public LoginPage SetPassword(String password)
    {
        Element.WaitUntilDisplayed(PasswordLocator, 5000);
        Element.ClearField(PasswordLocator);
        Element.InputText(PasswordLocator, password);
        return this;
    }

    public void Submit()
    {
        Element.WaitUntilDisplayed(LoginButtonLocator, 3000);
        Element.Click(LoginButtonLocator);
    }
         
       public string WrongPasswordForJournalist()
     {
         return Element.FindElement(ErrorMessageLocator).Text;
       }

       public string WrongUserName()
         {
           return  Element.FindElement(ErrorMessageLocator).Text;
         }
       
       public LoginPage EmptyLoginName()
       {
           Element.WaitUntilDisplayed(UserNameLocator, 5000);
           Element.ClearField(UserNameLocator);
           return this;
       }

       public LoginPage EmptyPassword()
       {
           Element.WaitUntilDisplayed(PasswordLocator, 5000);
           Element.ClearField(PasswordLocator);
           return this;
       }

       public string GetErrorMessage()
       {
           return Element.FindElement(ErrorMessageLocator).Text;
       }
     
    }
}
       
              
        
        
        
        
        
        
      
        