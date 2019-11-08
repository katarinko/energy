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
using Umbraco.Site.UITests.Core;
using OpenQA.Selenium.Interactions;
using Umbraco.Site.UITests.Pages;

namespace Umbraco.Site.UITests
{
    public class ContentPage : BasePage
    {
        //test data
        By ContentPageLocator = By.XPath(".//*[@icon='traycontent']");
        By AvatarLocator = By.XPath("//img[@class='umb-avatar -xs']");
        By AvatarLocatorCopyEditor = By.XPath("//a[@title='copyeditor']");
        By AvatarLocatorEditor = By.XPath("//a[@title='editor']");
        By LogoutButtonLocator = By.XPath("//localize[@key='general_logout']");
        //By WEOLocator = By.XPath("//a[text()='WeoHomePage']");
        By Actionslocator = By.XPath("//localize[@key='general_actions']");
        By Createlocator = By.XPath("//localize[text()='Create']");
        By CreateDropDownLocator = By.XPath(".//a[@class='btn dropdown-toggle']");
        By CreateWEOLocator = By.XPath("//a[@class = 'ng-binding' and text()[contains(., 'WEO')]]");
        By ContentLocator = By.XPath("//a[@href='#/content']");
        //label[@class='control-label ng-binding' and contins(text(), 'Headline')]
        By WEOLocator = By.XPath("//a[text()='World Energy Opinion']");
        By SearchFieldLocator = By.XPath("//*[@class='form-control search-input ng-pristine ng-valid']");
        By SearchTestArticleLocator = By.XPath("//a[@title = 'Title - Goldman SachsArticleForAutoTest']");
        By NameLocator = By.XPath("//[@key='general_name']");
        By CreatedByLocator = By.XPath("//span[text()='Created by']");
        By CreatedByOfArticle = By.XPath("//span[contains(@title, 'Created by:')]");

        public ContentPage()
        {
            WaitPageLoaded(ContentPageLocator, 3000, "Content Page");
        }

        public LoginPage ClickLogout()
        {
            Element.WaitUntilDisplayed(AvatarLocator, 2000);
            Element.Click(AvatarLocator);
            Element.WaitUntilDisplayed(LogoutButtonLocator, 2000);
            Element.Click(LogoutButtonLocator);
            return new LoginPage();
        }

        public void ClickActions()
        {
            Element.WaitUntilDisplayed(ContentLocator, 1000);
            //Element.Click(ContentLocator);
            Element.MouseOver(ContentLocator);
            Element.Click(ContentLocator);
            Commons.Sleep(1000);
            Element.WaitUntilDisplayed(WEOLocator, 7000);
            Element.Click(WEOLocator);
            Element.WaitUntilDisplayed(Actionslocator, 7000);
            Element.Click(Actionslocator);
        }

        public ArticlePage CreateWeoArticle()
        {
            Element.WaitUntilDisplayed(Createlocator, 3000);
            Element.Click(Createlocator);
            Element.WaitUntilDisplayed(CreateWEOLocator, 2000);
            Element.Click(CreateWEOLocator);
            return new ArticlePage();
        }

        public void ClickContent()
        {
            Element.WaitUntilDisplayed(ContentLocator, 1000);
            Element.MouseOver(ContentLocator);
            Element.Click(ContentLocator);
            Commons.Sleep(1000);
        }

        public void ClickWEO()
        {
            Element.WaitUntilDisplayed(WEOLocator, 7000);
            Element.Click(WEOLocator);
        }

        public ResultPage SearchResult(String searchdata)
        {
            Element.WaitUntilDisplayed(NameLocator, 5000);
            Element.InputText(SearchFieldLocator, searchdata);
            return new ResultPage();
        }

        public ArticlePage ClickSearchArticle()
        {
            Element.WaitUntilDisplayed(SearchTestArticleLocator, 7000);
            Element.Click(SearchTestArticleLocator);
            Commons.Sleep(6000);
            return new ArticlePage();
        }
        
    }

}
