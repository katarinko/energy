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
using System.Drawing;

namespace Umbraco.Site.UITests.Pages
{
    public class ArticlePage : BasePage
    {
        //Test data
        By EditPageLocator = By.XPath("//a[text()='Document' and @data-toggle='tab']");
        By NameLocator = By.XPath("//input[@name='headerName']");
        By HeadlineLocator = By.XPath("//div[contains(@class, 'umb-el-wrap')]/label[contains (@for,'headline')]/parent::*/descendant::input");
        By SummaryLocator = By.XPath("//div[contains(@class, 'umb-el-wrap')]/label[contains (@for,'summary')]/parent::*/descendant::input");
        By BodyLocator = By.XPath("//div[contains(@class, 'umb-el-wrap')]/label[contains (@for,'body')]//parent::*/descendant::iframe");
        By KeywordsLocator = By.Id("keywords");
        By SaveButtonLocator = By.XPath("//localize[@key='buttons_save']");
        By TinyMceLocator = By.Id("tinymce");
        By AddFishbackButtonLocator = By.XPath("//button[text()='Add Fishback']");
        By LinkFishbackLocator = By.XPath("//iframe[@tabindex]");
        By SelectPublicationLocator = By.XPath(".//*[@id='publications_chosen']//ul[@class='chosen-choices']");
        By OilDailyLinkLocator = By.XPath("//li[text()='Oil Daily']");
        string authorLocatorXpath = "//span[@title='{0}']";
        By SearchLinkButtonLocator = By.XPath("//button[text()='Search']");
        By AddSelectedBuutonLocator = By.XPath("//button[text()='Add Selected']");
        By ThumbnailLocator = By.XPath(".//span[@class='fileinput-button umb-upload-button-big']");
        By UseLikeNameLocator = By.XPath("//input[@value='Use Like Name']");
        By ContentLocator = By.XPath("//a[@href='#/content']");
        By SaveAndPublishLocator = By.XPath("//localize[@key='buttons_saveAndPublish']");
        By MetaDataLocator = By.XPath(".//a[text()='Meta-Data']");
        By AuthorLocator = By.XPath(".//label[@for='author']");
        By ListAuthorsLocator = By.XPath("//div[contains(@class, 'umb-el-wrap')]/label[contains (@for,'author')]/parent::*/descendant::div[@class='controls']");
        By SelectAthorLocator = By.XPath("//div[@class='taxdrop__bottom']/treeview");
        By WorkflowLocator = By.XPath(".//a[text()='Workflow']");
        By WorkflowStatusLocator = By.XPath("//div[./label[@for='workflowStatus']]//select");
        By StatusLocator = By.XPath("//select/*[@selected='selected']");
        By SelectedAuthorlocator = By.XPath(".//span[@class='taxdrop__tag ng-scope']/span[@class='ng-binding']");
        By MessageLocator = By.XPath(".//li[contains(@class, 'alert alert-block')]//a/strong");
        By DocumentLocator = By.XPath("//a[text()='Document']");
        By ActionsLocator = By.XPath("//localize[text()='Actions']");
        By DeleteLocator = By.XPath("//span[text()='Delete']");
        By OKButtonLocator = By.XPath("//localize[text()='OK']");
        // By SaveAndPublishLocator = By.XPath("//localize[text()='Save and publish']");
        By ListOfButtonsLocator = By.XPath("//localize[@class='ng-isolate-scope ng-scope ng-binding']");
        By ReleaseDateLocator = By.XPath(".//span[contains(@title, 'Release Date:')]");
        By ReturnToListButtinLocator = By.XPath("//localize[text()='Return to list']");
        By PreviewButtonLocator = By.XPath("//localize[text()='Preview']");
        By PropertiesLocator = By.XPath("//a[text()='Properties']");
        By LinkToDocLocator = By.XPath(".//span[@class='ng-scope ng-binding']");
        By UnpublishButtonLocator = By.XPath("//localize[@key='content_unPublish']");
        public ArticlePage()
        {
            WaitPageLoaded(EditPageLocator, 10000, "Article Page");
        }

        public ArticlePage ClickDocument()
        {
            Element.Click(DocumentLocator);
            return this;
        }
        public ArticlePage EditName(String Name)
        {

            Element.WaitUntilDisplayed(NameLocator, 7000);
            Element.ClearField(NameLocator);
            Element.InputText(NameLocator, Name);
            return this;
        }

        public ArticlePage EditHeadline(String Headline)
        {
            Element.WaitUntilDisplayed(HeadlineLocator, 5000);
            Element.ClearField(HeadlineLocator);
            Element.InputText(HeadlineLocator, Headline);
            return this;
        }
        public ArticlePage EditSummary(String Summory)
        {
            Element.WaitUntilDisplayed(HeadlineLocator, 5000);
            Element.ClearField(SummaryLocator);
            Element.InputText(SummaryLocator, Summory);
            return this;
        }
        public ArticlePage EditBody(String Body)
        {
            Element.WaitUntilDisplayed(BodyLocator, 20000);
            var tinyMce = Element.FindElement(BodyLocator);
            WebDriverManager.GetWebDriver().SwitchTo().Frame(tinyMce);
            var editorBody = Element.FindElement(TinyMceLocator);
            ((IJavaScriptExecutor)WebDriverManager.GetWebDriver()).ExecuteScript("arguments[0].innerHTML =''", editorBody);
            Commons.Sleep(1000);
            Element.ClearField(TinyMceLocator);
            Element.InputText(TinyMceLocator, Body);
            WebDriverManager.GetWebDriver().SwitchTo().DefaultContent();
            return this;
        }
        public ArticlePage ClickMetaData()
        {
            Element.Click(MetaDataLocator);
            return this;
        }
        public string GetSelectedAuthor()
        {
            return Element.FindElement(SelectedAuthorlocator).Text;
        }

        public ArticlePage EditKeywords(String Keywords)
        {
            Element.Click(MetaDataLocator);
            Element.WaitUntilDisplayed(KeywordsLocator, 5000);
            Element.ClearField(KeywordsLocator);
            Element.InputText(KeywordsLocator, Keywords);
            return this;
        }

        public ArticlePage EditThumbnail(String image)
        {
            Element.Click(MetaDataLocator);
            Element.WaitUntilDisplayed(ThumbnailLocator, 5000);
            Element.Click(ThumbnailLocator);
            Commons.Sleep(2000);
            SendKeys.SendWait(@"C:\Projects\UMBRACO_SITE\Umbraco.Site.UITests\Resources\testimage.jpg");
            Commons.Sleep(2000);
            SendKeys.SendWait(@"{Enter}");
            return this;
        }

        public ArticlePage EditAuthor(String Author)
        {
            Element.Click(MetaDataLocator);
            Element.Click(ListAuthorsLocator);
            By authorLocator = By.XPath(String.Format(authorLocatorXpath, Author));
            Element.Click(authorLocator);
            Commons.Sleep(7000);
            return this;
        }

        public ArticlePage ClickWorkflow()
        {
            Element.Click(WorkflowLocator);
            return this;
        }
        public string GetSelectedStatus()
        {
            IWebElement element = Element.FindElement(WorkflowStatusLocator);
            var selectElement = new SelectElement(element);
            return selectElement.SelectedOption.Text;
        }
        public ArticlePage ChangeStatus(String Status)
        {
            IWebElement elem = Element.FindElement(WorkflowStatusLocator);
            var select = new SelectElement(elem);
            select.SelectByText(Status);
            return this;
        }

        public List<string> GetButtonsList()
        {
            List<IWebElement> ButtonElements = Element.FindElements(ListOfButtonsLocator);
            List<string> ButtonsList = new List<string>();
            foreach (var a in ButtonElements)
            {
                ButtonsList.Add(a.Text);
            };
            return ButtonsList;
        }

        public ArticlePage ClickSaveButton()
        {
            Element.WaitUntilDisplayed(SaveButtonLocator, 6000);
            Element.Click(SaveButtonLocator);
            Commons.Sleep(3000);
            return this;
        }

        public string GetMessage()
        {
            Element.WaitUntilDisplayed(MessageLocator, 3000);
            return Element.FindElement(MessageLocator).Text;
        }

        public ArticlePage ClickPublishButton()
        {
            Element.WaitUntilDisplayed(SaveAndPublishLocator, 6000);
            Element.Click(SaveButtonLocator);
            Commons.Sleep(3000);
            return this;
        }
        public ArticlePage ClickUnpublishButton()
        {
            Element.WaitUntilDisplayed(UnpublishButtonLocator, 6000);
            Element.Click(UnpublishButtonLocator);
            Commons.Sleep(3000);
            return this;
        }

        public ArticlePage ClickUseLikeName()
        {
            Element.WaitUntilDisplayed(UseLikeNameLocator, 5000);
            Element.Click(UseLikeNameLocator);
            return this;
        }

        public ContentPage GoToContent()
        {
            Element.Click(ContentLocator);
            return new ContentPage();
        }

        public ArticlePage ClickSaveAndPublishButton()
        {
            Element.WaitUntilDisplayed(SaveAndPublishLocator, 6000);
            Element.Click(SaveAndPublishLocator);
            Commons.Sleep(5000);
            return this;
        }

        public void ClickActions()
        {
            Element.Click(ActionsLocator);
        }

        public void ClickDelete()
        {
            Element.Click(DeleteLocator);
        }
        public void ApplyDelete()
        {
            Element.Click(OKButtonLocator);
        }
        public string GetReleaseDateOfArticle()
        {
            Element.FindElement(ReleaseDateLocator);
            return Element.FindElement(ReleaseDateLocator).Text;
        }
       
        public ContentPage ClickReturnToList()
        {
            Element.WaitUntilDisplayed(ReturnToListButtinLocator,3000);
            Element.Click(ReturnToListButtinLocator);
            return new ContentPage();
        }

        public PreviewPage ClickPreview()
        {
            Element.WaitUntilDisplayed(PreviewButtonLocator, 3000);
            Element.Click(PreviewButtonLocator);
            ReadOnlyCollection<String> windowHandles = WebDriverManager.GetWebDriver().WindowHandles;
            String lastTab = windowHandles[windowHandles.Count - 1];
            WebDriverManager.GetWebDriver().SwitchTo().Window(lastTab);
            return new PreviewPage();
        }

        public PreviewPage GoToPreviewPage()
        {
            ReadOnlyCollection<String> windowHandles = WebDriverManager.GetWebDriver().WindowHandles;
            String lastTab = windowHandles[windowHandles.Count - 1];
            WebDriverManager.GetWebDriver().SwitchTo().Window(lastTab);
            return new PreviewPage();
        }


        public void ClickProperties()
        {
            Element.Click(PropertiesLocator);
        }

        public string GetLinkToDoc()
        {
            Element.FindElement(LinkToDocLocator);
            return Element.FindElement(LinkToDocLocator).Text;
        }
        public PublishedArticlePage ClickLinkToDoc()
        {
            Element.WaitUntilDisplayed(LinkToDocLocator, 3000);
            Element.Click(LinkToDocLocator);
            ReadOnlyCollection<String> windowHandles = WebDriverManager.GetWebDriver().WindowHandles;
            String lastTab = windowHandles[windowHandles.Count - 1];
            WebDriverManager.GetWebDriver().SwitchTo().Window(lastTab);
            return new PublishedArticlePage();
        }
        
    }
}
