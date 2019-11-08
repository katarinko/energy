using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.ObjectModel;
using Umbraco.Site.UITests.Core;
using OpenQA.Selenium.Interactions;



namespace Umbraco.Site.UITests.Core
{
    public static class Element
    {
        public static IWebElement FindElement(By locator)
        {
            return WebDriverManager.GetWebDriver().FindElement(locator);
        }
        
        public static List<IWebElement> FindElements(By locator)
        {
            return WebDriverManager.GetWebDriver().FindElements(locator).ToList();
        }
        
        public static void ClearField(By locator)
        {
            FindElement(locator).Clear();
        }
                
        public static void InputText(By locator, String text)
        {
            FindElement(locator).SendKeys(text);
        }
        
        public static void Click(By locator)
        {
             FindElement(locator).Click();
        }

        public static void ErrorMessageView(By locator)
        {
            FindElement(locator);
           
        }

        /*public static IWebDriver SwitctToWindow()
        {
            return IWebDriverManager.GetWebDriver();
        }*/
        
        public static void WaitUntilDisplayed(By locator, int timeout)
        {
            int counter = 0;
            int step = 100;
            while (counter < timeout && !IsElementDisplayed(locator))
            {
                counter += step;
                Commons.Sleep(step);
            }
        }
       
        public static Boolean IsElementDisplayed(By locator)
        {
            try
            {
                return FindElement(locator).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }

       
        
        public static void MouseOver(By locator)
        {
           /* Actions action = new Actions(IWebDriverManager.GetWebDriver());
            IWebElement element = FindElement(locator);
            action.ClickAndHold(element).Perform();*/
           
            IWebElement element = FindElement(locator);
            Actions builder = new Actions(WebDriverManager.GetWebDriver());
            builder.MoveToElement(element).Click();
        }
        

        public static void AcceptAlert()
        {

           //Element.FindElement(By.XPath("//div[class='modal-footer']/button[text()='OK']")).Click();
            

           IAlert a = WebDriverManager.GetWebDriver().SwitchTo().Alert();
           a.Accept();
          /* if (a.Text. Equals("Are you sure you want delete?"))
           {
               a.Accept();
           }
           else
           {
               a.Dismiss();
           }*/

         
}

        
    }

}
