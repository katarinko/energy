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
using Umbraco.Site.UITests.Pages;

namespace Umbraco.Site.UITests
{
    public class SiteHomePage : BasePage
    {
        By SiteLocator = By.XPath("//h1[text()='World Energy Opinion']");
        By ListOfArticlesLocator = By.XPath("//a[@class='headline']");
        By ListOfSummaryLocator = By.XPath("//p");
        string headlineLocatorXpath = "//a[@class='linkOverlay'][@href='{0}']";
        By Favorites = By.XPath("//a[text()='View Favorites']");

        //By headline = By.XPath("//a[@class='linkOverlay'][@href='/world-energy-opinion/title-goldman-sachsarticleforautotest/']");
        //("//article[contains(@class, 'topStory')]//a[contains (@class,'linkOverlay')]/parent::*/descendant::a[text()='Headline - Goldman SachsArticleForAutoTest Projects Steady Rise in US Oil Prices.']");

        public SiteHomePage()
        {
            WaitPageLoaded(SiteLocator, 5000, "Site Home Page");
        }

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
        public List<string> GetSummaryList()
        {
            List<IWebElement> ArticleElements = Element.FindElements(ListOfSummaryLocator);
            List<string> SummaryList = new List<string>();
            foreach (var a in ArticleElements)
            {
                SummaryList.Add(a.Text);
            };
            return SummaryList;
        }
        public PublishedArticlePage OpenPublishedArticle(string headline)
        {
            By headlineLocator = By.XPath(String.Format(headlineLocatorXpath, headline));
            Element.Click(headlineLocator);
            return new PublishedArticlePage();
        }
        public void ClickWEO()
        {
            Element.FindElement(Favorites).Click();
        }
    }
}
