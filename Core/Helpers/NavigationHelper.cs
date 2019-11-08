using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using Umbraco.Site.UITests.Tests.Data.ArticleData;

namespace Umbraco.Site.UITests.Core.Helpers
{
    public class NavigationHelper
    {

        public static void Open(String url)
        {
            WebDriverManager.GetWebDriver().Navigate().GoToUrl(url);

        }
        
        public static void Close()
        {
            WebDriverManager.CloseWebDriver();
        }

        public static void OpenNewTab(string url)
        {

            var originalTabInstance = WebDriverManager.GetWebDriver().CurrentWindowHandle;
            WebDriverManager.GetWebDriver().ExecuteJavaScript("window.open();");
            ReadOnlyCollection<String> windowHandles = WebDriverManager.GetWebDriver().WindowHandles;
            String lastTab = windowHandles[windowHandles.Count - 1];
            WebDriverManager.GetWebDriver().SwitchTo().Window(lastTab);
            WebDriverManager.GetWebDriver().Navigate().GoToUrl(url);
        }
        public static void CloseNewTab()
        {
            ReadOnlyCollection<String> windowHandles = WebDriverManager.GetWebDriver().WindowHandles;
            String lastTab = windowHandles[windowHandles.Count - 1];
            WebDriverManager.GetWebDriver().SwitchTo().Window(lastTab);

        }

    }
}