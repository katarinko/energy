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
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Umbraco.Site.UITests.Pages
{
    public class FishbackLinkPage : BasePage
    {
        By AddFishbackButtonLocator = By.XPath("//button[text()='Add Fishback']");
        By LinkFishbackLocator = By.XPath("//iframe[@tabindex]");
        By SelectPublicationLocator = By.XPath(".//*[@id='publications_chosen']//ul[@class='chosen-choices']");
        By OilDailyLinkLocator = By.XPath("//li[text()='Oil Daily']");
        By SearchLinkButtonLocator = By.XPath("//button[text()='Search']");
        By OgpLocator = By.XPath("//div[text() [contains(.,'Oil and Gas Prices')]]");
        By AddSelectedBuutonLocator = By.XPath("//button[text()='Add Selected']");
        By TitleForLinkLocator = By.XPath("//input[@id='searchText']");

        public ArticlePage AddFishbackLink(String FishbackLink)
        {
            Element.WaitUntilDisplayed(AddFishbackButtonLocator, 10000);
            Element.Click(AddFishbackButtonLocator);
            Commons.Sleep(3000);
            var linkFishback = Element.FindElement(LinkFishbackLocator);
            WebDriverManager.GetWebDriver().SwitchTo().Frame(linkFishback);
            Element.WaitUntilDisplayed(SearchLinkButtonLocator, 10000);
            Element.Click(SelectPublicationLocator);
            Element.Click(OilDailyLinkLocator);
            Element.InputText(TitleForLinkLocator, FishbackLink);
            Element.Click(SearchLinkButtonLocator);
            Commons.Sleep(3000);
            Element.Click(OgpLocator);
            Element.Click(AddSelectedBuutonLocator);
            WebDriverManager.GetWebDriver().SwitchTo().DefaultContent();
            return new ArticlePage();
        }
    }
}
