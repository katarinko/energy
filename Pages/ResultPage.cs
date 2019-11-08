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
using Umbraco.Site.UITests.Core.Helpers;
using Umbraco.Site.UITests.Tests.Data.ArticleData;
using System.Collections.ObjectModel;

namespace Umbraco.Site.UITests.Pages
{
    public class ResultPage : BasePage
    {
        //test data
        By ListOfArticlesLocator = By.XPath("//*[@class='umb-table-body__link ng-binding']");
        By TestArticleLocatorFromSearch = By.XPath("//a[@title = 'Title - Goldman SachsArticleForAutoTest']");
        By AuthorLocator = By.XPath("//span[text()='Author(s)']");
        By AuthorOfArticle = By.XPath("//span[contains(@title, 'Author(s):')]");
        By StatusLocator = By.XPath("//span[text()='Status']");
        By StatusOfArticle = By.XPath("//span[contains(@title, 'Status:')]");
        By IsPublishedLocator = By.XPath("//span[text()='Is Published']");
        By IsPublishedOfArticle = By.XPath("//span[contains(@title, 'Is Published:')]");
        By DeleteButtonLocator = By.XPath("//div[@label-key='actions_delete']");
        By CheckBoxLocatorForDeleteArticle = By.XPath("//a[@title = 'Title - Goldman SachsArticleForAutoTest']/parent::*/parent::*//i[@class = 'umb-table-body__icon umb-table-body__fileicon icon-document']");
        By MissingArticleMessageLocator = By.XPath("//localize[@key='general_searchNoResult']");
        //By AvatarLocator = By.XPath("//img[@class='umb-avatar -xs']");
        By IconLocator = By.XPath("//i[@class='icon-list']");
        By ContentPageLocator = By.XPath(".//*[@icon='traycontent']");
        public List<string> GetArticlesList()
        {
            List<IWebElement> ArticleElements = Element.FindElements(ListOfArticlesLocator);
            List<string> ArticlesList = new List<string>();
            foreach (var a in ArticleElements)
            {
                ArticlesList.Add(a.Text);
            };
            return ArticlesList;
        }

        public string GetAuthor()
        {
            Element.FindElement(AuthorLocator);
            return Element.FindElement(AuthorOfArticle).Text;
        }

        public string GetStatus()
        {
            Element.FindElement(StatusLocator);
            return Element.FindElement(StatusOfArticle).Text;
        }

        public string GetIsPublished()
        {
            Element.FindElement(IsPublishedLocator);
            return Element.FindElement(IsPublishedOfArticle).Text;
        }

        public void ClickCheckBox()
        {
            Element.WaitUntilDisplayed(CheckBoxLocatorForDeleteArticle, 7000);
            Element.Click(CheckBoxLocatorForDeleteArticle);
        }

        public void DeleteArticle()
        {
            Element.Click(DeleteButtonLocator);
            Commons.Sleep(3000);
            Element.AcceptAlert();
        }

        public ArticlePage OpenFoundArticle()
        {
            Element.Click(TestArticleLocatorFromSearch);
            return new ArticlePage();
        }

        public string GetMissingArticleMessage()
        {
            Element.WaitUntilDisplayed(MissingArticleMessageLocator, 3000);
            return Element.FindElement(MissingArticleMessageLocator).Text;
        }
        public SiteHomePage OpenSite()
        {
            /*Element.FindElement(ContentPageLocator).SendKeys(Keys.Control + "t");
            ReadOnlyCollection<String> windowHandles = WebDriverManager.GetWebDriver().WindowHandles;
            String lastTab = windowHandles[windowHandles.Count - 1];
            WebDriverManager.GetWebDriver().SwitchTo().Window(lastTab);
            NavigationHelper.Open(ArticleData.siteURL);
            return new SiteHomePage();*/
            
           
            Element.MouseOver(ContentPageLocator);

            return new SiteHomePage();
        }

    }
}
