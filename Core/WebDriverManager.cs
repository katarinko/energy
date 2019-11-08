using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium.Support.PageObjects;
using Umbraco.Site.UITests.Core;
using OpenQA.Selenium.Interactions;

namespace Umbraco.Site.UITests.Core
{
   public  class WebDriverManager
    {
       private static IWebDriver driver;
       private const string IE_DRIVER_PATH = @"F:\WorkDoc\ScreenSelenium\IEDriverServer_x64_2.53.1";

       public static void SetWebDriver()
       {
            //Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"C:\Projects\UMBRACO_SITE\Umbraco.Site\bin\");
            //ChromeOptions options = new ChromeOptions();
            //options.AddArguments("disable-infobars");
           // driver = new ChromeDriver(options);


            driver = new ChromeDriver(@"C:\Projects\UMBRACO_SITE\Umbraco.Site\bin\");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
           
            // driver = new FirefoxDriver();
            // System.Environment.SetEnvironmentVariable("InternetExplorerDriver", @"F:\WorkDoc\ScreenSelenium\IEDriverServer_x64_2.53.1\IEDriverServer.exe");

            // driver = new InternetExplorerDriver(IE_DRIVER_PATH);
            //  driver = new InternetExplorerDriver();
            //Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"C:\Projects\UMBRACO_SITE\Umbraco.Site\bin\chromedriver.exe");


        }



        public static IWebDriver GetWebDriver()
       {
          return driver;
       }

       public static void CloseWebDriver()
       {
           driver.Close();
       }
    }
 }
