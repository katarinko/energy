using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace Umbraco.Site.UITests
{
    [TestFixture]
    public class Main
    {
        [Test]
        public void SetUserName()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            Thread.Sleep(4000);
            //  driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(500));
            driver.Navigate().GoToUrl("http://eigdevkiev.ciklum.net:3333/umbraco/#/loginl");
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(500));
            Thread.Sleep(4000);
            driver.FindElement(By.Name("username")).SendKeys("journalist");
            driver.FindElement(By.Name("password")).SendKeys("1234QweR!!");
            driver.FindElement(By.ClassName("btn")).Click();
        }
    }
}