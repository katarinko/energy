using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Site.UITests.Core;
using Umbraco.Site.UITests.Core.Helpers;

namespace Umbraco.Site.UITests.Pages
{
    public class PublishedArticlePage : BasePage
    {
        By AuthorOfArticle = By.XPath("//span[@class='author']");
        By HeadlineOfArticle = By.XPath("//h1[@class='ei_ipf_atricle_title']");
        By BodyOfArticle = By.CssSelector("p");
        By FishbackLink = By.XPath(".//a[contains(@href, 'http://energyintel.com/pages')]");
        By PublishedArticlePageLocator = By.XPath(".//div/a[text()='World Energy Opinion']");
        By AuthorLocator = By.XPath("//span[text()='Author(s)']");

        public PublishedArticlePage()
        {
            WaitPageLoaded(PublishedArticlePageLocator, 10000, "PublishedArticle Page");
        }
        public string GetHeadline()
        {
            return Element.FindElement(HeadlineOfArticle).Text;
        }

        public string GetAuthor()
        {
            return Element.FindElement(AuthorOfArticle).Text;
        }
        public string GetRealAuthor()
        {
            NavigationHelper.CloseNewTab();
            Element.FindElement(AuthorLocator);
            return Element.FindElement(AuthorOfArticle).Text;
        }

        public string GetBody()
        {
            return Element.FindElement(BodyOfArticle).Text;
        }

        public string GetFishbackLink()
        {
            return Element.FindElement(FishbackLink).Text;
        }
        public ArticlePage BackToArticlePage()
        {
            ReadOnlyCollection<String> windowHandles = WebDriverManager.GetWebDriver().WindowHandles;
            String firstTab = (String)windowHandles[0];
            WebDriverManager.GetWebDriver().SwitchTo().Window(firstTab);
            return new ArticlePage();
        }

    }
}
