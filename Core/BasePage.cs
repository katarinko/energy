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
    public abstract class BasePage
    {
        public static void WaitPageLoaded(By locator, int timeout, String AssertionText)
         {
             int counter = 0;
             int step = 100;

             while (counter < timeout && !Element.IsElementDisplayed(locator))
             {
                 counter += step;
                 Commons.Sleep(step);
             }

             if (!Element.IsElementDisplayed(locator))
         {
             throw new AssertionException(AssertionText + "object was not loaded in " + timeout + " milliseconds!");
         }
       }
        
    }
}
