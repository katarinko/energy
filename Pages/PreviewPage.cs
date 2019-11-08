using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Site.UITests.Core;

namespace Umbraco.Site.UITests.Pages
{
    public class PreviewPage : BasePage
    {
        By PreviewPageLocator = By.XPath(".//div/a[text()='World Energy Opinion']");
        By HeadlineOfArticle = By.XPath("//h1[@class='ei_ipf_atricle_title']");
        public PreviewPage()
        {
            WaitPageLoaded(PreviewPageLocator, 10000, "Preview Page");
        }

        public ArticlePage BackToArticlePage()
        {
            ReadOnlyCollection<String> windowHandles = WebDriverManager.GetWebDriver().WindowHandles;
            String firstTab = (String)windowHandles[0];
            WebDriverManager.GetWebDriver().SwitchTo().Window(firstTab);
            return new ArticlePage();
        }

        public string GetHeadline()
        {
            return Element.FindElement(HeadlineOfArticle).Text;
        }
    }
}
