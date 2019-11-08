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
using Umbraco.Site.UITests.Core.Helpers;
using OpenQA.Selenium.Interactions;
using Umbraco.Site.UITests.Core.entity;
using Umbraco.Site.UITests.Pages;
using System.IO;
using Umbraco.Site.UITests.Tests.Data.Alerts;
using Umbraco.Site.UITests.Tests.Data.ArticleData;
using OpenQA.Selenium.Support.Extensions;
using System.Collections.ObjectModel;

namespace Umbraco.Site.UITests
{
    [TestFixture]
    class PassingTests : BaseTest
    {
        private Journalist journalist = Journalist.GetDefaultJournalist();
        private Journalist wrongUserName = Journalist.WrongUserName();
        private Journalist wrongPassword = Journalist.WrongPassword();
        private Editor editor = Editor.GetDefaultEditor();
        private Editor copyeditor = CopyEditor.GetDefaultCopyEditor();
        private TestArticleForUsers defaultArticleForJournalist = TestArticleForUsers.GetDefaultArticleForjournalist();
        private TestArticleForUsers defaultArticleForEditor = TestArticleForUsers.GetDefaultArticleForEditor();
        private TestArticleForUsers defaultArticleForCopyEditor = TestArticleForUsers.GetDefaultArticleForCopyEditor();
        
       /*[OneTimeSetUp]
        public void SetUp()
        {
            NavigationHelper.Open(ArticleData.homeURL);
        }*/

        [Test, Order(1)]
        public void TestLoginAndLogoutAsJournalist()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(journalist);
            loginActions.Logout();
        }

        [Test, Order(2)]
        public void TestLoginAndLogoutAsEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(editor);
            var contentPage = new ContentPage();
            contentPage.ClickSearchArticle();
            loginActions.Logout();
        }

        [Test, Order(3)]
        public void TestLoginAndLogoutAsCopyEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(copyeditor);
            loginActions.Logout();
        }

        [Test, Order(4)]
        public void TestLoginEmptyName()
        {
            //test steps
            var loginPage = new LoginPage();
            var loginActions = new LoginActions();
            loginActions.LoginWithoutName(journalist);
            Commons.Sleep(1000);
            Assert.AreEqual(AlertsEM.errorEmptyMessage, loginPage.GetErrorMessage(), "Message is not correct");
        }

        [Test, Order(5)]
        public void TestLoginEmptyPassword()
        {
            //test steps
            var loginPage = new LoginPage();
            var loginActions = new LoginActions();
            loginActions.LoginWithoutPassword(journalist);
            Commons.Sleep(1000);
            Assert.AreEqual(AlertsEM.errorEmptyMessage, loginPage.GetErrorMessage(), "Message is not correct");
        }
        [Test, Order(6)]
        public void TestWrongUserName()
        {
            //test steps
            var loginPage = new LoginPage();
            var loginActions = new LoginActions();
            loginActions.LoginWrongName(wrongUserName);
            Commons.Sleep(1000);
            Assert.AreEqual(AlertsEM.errorWrongMessage, loginPage.GetErrorMessage(), "Message is not correct");
        }

        [Test, Order(7)]
        public void TestWrongpassword()
        {
            //test steps
            var loginPage = new LoginPage();
            var loginActions = new LoginActions();
            loginActions.LoginWrongPassword(wrongPassword);
            Commons.Sleep(1000);
            Assert.AreEqual(AlertsEM.errorWrongPassword, loginPage.GetErrorMessage(), "Message is not correct");
        }

        [Test, Order(8)]
        public void TestCreateAndDeleteArticleAsEditor()
        {
            //test steps
            var LoginPage = new LoginPage();
            var loginActions = new LoginActions();
            loginActions.LoginAs(editor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForEditor(defaultArticleForEditor);
            articleAction.ClickMetaDataTab();
            var articlePage = new ArticlePage();
            Assert.AreEqual(ArticleData.authorEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articleAction.ClickWorkflowTab();
            articleAction.ChangeStatusOfArticle(ArticleData.initialStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorEditor, resultPage.GetAuthor(),
                "Author is not " + ArticleData.authorEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.initialStatus, resultPage.GetStatus(),
               "Status is not " + ArticleData.initialStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }

        [Test, Order(9)]
        public void TestCreateAndDeleteArticleAsCopyEditor()
        {
            //test steps
            var LoginPage = new LoginPage();
            var loginActions = new LoginActions();
            loginActions.LoginAs(copyeditor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForCopyEditor(defaultArticleForCopyEditor);
            articleAction.ClickMetaDataTab();
            var articlePage = new ArticlePage();
            Assert.AreEqual(ArticleData.authorCopyEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articleAction.ClickWorkflowTab();
            articleAction.ChangeStatusOfArticle(ArticleData.copyStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorCopyEditor, resultPage.GetAuthor(),
                "Author is not " + ArticleData.authorCopyEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.copyStatus, resultPage.GetStatus(),
               "Status is not " + ArticleData.copyStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }

        [Test, Order(10)]
        public void TestCreateAndDeleteArticleWithoutLinkAsEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(editor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleWithoutLinkForEditor(defaultArticleForEditor);
            articleAction.ClickMetaDataTab();
            var articlePage = new ArticlePage();
            Assert.AreEqual(ArticleData.authorEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articleAction.ClickWorkflowTab();
            articleAction.ChangeStatusOfArticle(ArticleData.initialStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            Assert.IsTrue(articlePage.GetButtonsList().Contains(ArticleData.returnToLIstButton), ArticleData.returnToLIstButton + "button is missing");
            articlePage.ClickReturnToList();
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorEditor, resultPage.GetAuthor(),
                "Author is not " + ArticleData.authorEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.initialStatus, resultPage.GetStatus(),
               "Status is not " + ArticleData.initialStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }

        [Test, Order(11)]
        public void TestCPDArticleAsEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(editor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForEditor(defaultArticleForEditor);
            articleAction.ClickMetaDataTab();
            var articlePage = new ArticlePage();
            Assert.AreEqual(ArticleData.authorEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articleAction.ClickWorkflowTab();
            articlePage.ChangeStatus(ArticleData.finalStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorEditor, resultPage.GetAuthor(),
                "Author is not " + ArticleData.authorEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
               "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }

        [Test, Order(12)]
        public void TestCPDArticleAsCopyEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(copyeditor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForCopyEditor(defaultArticleForCopyEditor);
            articleAction.ClickMetaDataTab();
            var articlePage = new ArticlePage();
            Assert.AreEqual(ArticleData.authorCopyEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articleAction.ClickWorkflowTab();
            articlePage.ChangeStatus(ArticleData.finalStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorCopyEditor, resultPage.GetAuthor(),
                "Author is not " + ArticleData.authorCopyEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
               "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }

        [Test, Order(13)]
        public void TestCPDArticleWithoutLinksAsCopyEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(copyeditor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleWithoutLinkForCopyEditor(defaultArticleForCopyEditor);
            var articlePage = new ArticlePage();
            articleAction.ClickMetaDataTab();
            Assert.AreEqual(ArticleData.authorCopyEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticleWithoutLink, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticleWithoutLink);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorCopyEditor, resultPage.GetAuthor(),
               "Author is not " + ArticleData.authorCopyEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }
        
        [Test, Order(14)]
        public void TestCPUDArticleAsEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(editor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForEditor(defaultArticleForEditor);
            var articlePage = new ArticlePage();
            articleAction.ClickMetaDataTab();
            Assert.AreEqual(ArticleData.authorEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articleAction.ClickWorkflowTab();
            articlePage.ChangeStatus(ArticleData.finalStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorEditor, resultPage.GetAuthor(),
               "Author is not " + ArticleData.authorEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.ClickWorkflowTab();
            Assert.AreEqual(ArticleData.publishStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            articlePage.ClickUnpublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.unPublishMessage, articlePage.GetMessage(), "Wrong message");
            Assert.AreEqual(ArticleData.finalStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            NavigationHelper.Open(ArticleData.siteURL);
            Assert.IsFalse(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.finalStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.finalStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
         }

        [Test, Order(15)]
        public void TestCPUDArticleAsCopyEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(copyeditor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForCopyEditor(defaultArticleForCopyEditor);
            var articlePage = new ArticlePage();
            articleAction.ClickMetaDataTab();
            Assert.AreEqual(ArticleData.authorCopyEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            //articleAction.ClickWorkflowTab();
            //articlePage.ChangeStatus(ArticleData.finalStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorCopyEditor, resultPage.GetAuthor(),
               "Author is not " + ArticleData.authorCopyEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.ClickWorkflowTab();
            Assert.AreEqual(ArticleData.publishStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            articlePage.ClickUnpublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.unPublishMessage, articlePage.GetMessage(), "Wrong message");
            Assert.AreEqual(ArticleData.finalStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            NavigationHelper.Open(ArticleData.siteURL);
            Assert.IsFalse(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.finalStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.finalStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }

        [Test, Order(16)]
        public void TestCPUPDArticleAsEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(editor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForEditor(defaultArticleForEditor);
            var articlePage = new ArticlePage();
            articleAction.ClickMetaDataTab();
            Assert.AreEqual(ArticleData.authorEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articleAction.ClickWorkflowTab();
            articlePage.ChangeStatus(ArticleData.finalStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorEditor, resultPage.GetAuthor(),
               "Author is not " + ArticleData.authorEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.ClickWorkflowTab();
            Assert.AreEqual(ArticleData.publishStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            articlePage.ClickUnpublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.unPublishMessage, articlePage.GetMessage(), "Wrong message");
            Assert.AreEqual(ArticleData.finalStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            NavigationHelper.Open(ArticleData.siteURL);
            Assert.IsFalse(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.finalStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.finalStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorEditor, resultPage.GetAuthor(),
               "Author is not " + ArticleData.authorEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }

        [Test, Order(17)]
        public void TestCPUPDArticleAsCopyEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(copyeditor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForCopyEditor(defaultArticleForCopyEditor);
            var articlePage = new ArticlePage();
            articleAction.ClickMetaDataTab();
            Assert.AreEqual(ArticleData.authorCopyEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorCopyEditor, resultPage.GetAuthor(),
               "Author is not " + ArticleData.authorCopyEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.ClickWorkflowTab();
            Assert.AreEqual(ArticleData.publishStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            articlePage.ClickUnpublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.unPublishMessage, articlePage.GetMessage(), "Wrong message");
            Assert.AreEqual(ArticleData.finalStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            NavigationHelper.Open(ArticleData.siteURL);
            Assert.IsFalse(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.finalStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.finalStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(2000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorCopyEditor, resultPage.GetAuthor(),
               "Author is not " + ArticleData.authorCopyEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }

        [Test, Order(18)]
        public void TestCreateEditPuiblishDeleteArticle()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(journalist);
            var contentAction = new ContentAction();
            // create weo article
            contentAction.CreateNewArticle();
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleForJournalist(defaultArticleForJournalist);
            articleAction.ClickMetaDataTab();
            var articlePage = new ArticlePage();
            Assert.AreEqual("journalist", articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorJournalist, resultPage.GetAuthor(),
                "Author is not " + ArticleData.authorJournalist + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.initialStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.initialStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            loginActions.Logout();
            loginActions.LoginAs(editor);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            resultPage.OpenFoundArticle();
            articleAction.EditArticleAsEditor(defaultArticleForEditor);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.copyStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.copyStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            loginActions.Logout();
            loginActions.LoginAs(copyeditor);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            resultPage.OpenFoundArticle();
            articleAction.ClickWorkflowTab();
            articlePage.ChangeStatus(ArticleData.finalStatus);
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticle, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticle);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            loginActions.Logout();
            loginActions.LoginAs(editor);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();

        }

        [Test, Order(21)]
        public void TestCPUDArticleWithoutLinksAsCopyEditor()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(copyeditor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleWithoutLinkForCopyEditor(defaultArticleForCopyEditor);
            var articlePage = new ArticlePage();
            articleAction.ClickMetaDataTab();
            Assert.AreEqual(ArticleData.authorCopyEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            NavigationHelper.Open(ArticleData.siteURL);
            var siteHomePage = new SiteHomePage();
            Assert.IsTrue(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            Assert.IsTrue(siteHomePage.GetSummaryList().Contains(ArticleData.summaryOfArticle), ArticleData.summaryOfArticle + " summary is missing!");
            siteHomePage.OpenPublishedArticle(ArticleData.testTitleOfArticle);
            var publishedArticlePage = new PublishedArticlePage();
            Assert.AreEqual(ArticleData.headlineOfArticle, publishedArticlePage.GetHeadline(),
                "Headline is not " + ArticleData.headlineOfArticle);
            Assert.AreEqual(ArticleData.bodyOfArticleWithoutLink, publishedArticlePage.GetBody(),
                 "Body is not " + ArticleData.bodyOfArticleWithoutLink);
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.authorCopyEditor, resultPage.GetAuthor(),
               "Author is not " + ArticleData.authorCopyEditor + ". Or check sorting, should be Author(s)");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            loginActions.Logout();
            loginActions.LoginAs(editor);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            resultPage.OpenFoundArticle();
            articleAction.ClickWorkflowTab();
            Assert.AreEqual(ArticleData.publishStatus, articlePage.GetSelectedStatus(), "Status field is wrong or empty");
            articlePage.ClickUnpublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.unPublishMessage, articlePage.GetMessage(), "Wrong message");
            Assert.IsTrue(articlePage.GetButtonsList().Contains(ArticleData.returnToLIstButton), ArticleData.returnToLIstButton + "button is missing");
            articlePage.ClickReturnToList();
            var contentPage = new ContentPage();
            NavigationHelper.Open(ArticleData.siteURL);
            Assert.IsFalse(siteHomePage.GetArticlesList().Contains(ArticleData.headlineOfArticle), ArticleData.headlineOfArticle + " article is missing!");
            NavigationHelper.Open(ArticleData.homeURL);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Commons.Sleep(3000);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.finalStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.finalStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedFalse, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedFalse + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }


        [Test, Order(34)]
        public void TestAccessForPublishAndUnpublishasJournaist()
        {
            //test steps
            var loginActions = new LoginActions();
            loginActions.LoginAs(copyeditor);
            var contentAction = new ContentAction();
            contentAction.CreateNewArticle(); // create weo article
            var articleAction = new ArticleAction();
            articleAction.DefaultArticleWithoutLinkForCopyEditor(defaultArticleForCopyEditor);
            var articlePage = new ArticlePage();
            articleAction.ClickMetaDataTab();
            Assert.AreEqual(ArticleData.authorCopyEditor, articlePage.GetSelectedAuthor(), "Author field is wrong or empty");
            articlePage.ClickSaveButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.saveMessage, articlePage.GetMessage(), "Wrong message");
            loginActions.Logout();
            loginActions.LoginAs(journalist);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            var resultPage = new ResultPage();
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.finalStatus, resultPage.GetStatus(),
                "Status is not " + ArticleData.finalStatus + ". Or check sorting, should be Status");
            resultPage.OpenFoundArticle();
            Commons.Sleep(3000);
            Assert.IsFalse(articlePage.GetButtonsList().Contains(ArticleData.saveAndPublishButton), ArticleData.saveAndPublishButton + "button is available");
            loginActions.Logout();
            loginActions.LoginAs(copyeditor);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            resultPage.OpenFoundArticle();
            articlePage.ClickSaveAndPublishButton();
            Commons.Sleep(3000);
            Assert.AreEqual(AlertsEM.publishMessage, articlePage.GetMessage(), "Wrong message");
            loginActions.Logout();
            loginActions.LoginAs(journalist);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
               "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            Commons.Sleep(3000);
            Assert.IsFalse(articlePage.GetButtonsList().Contains(ArticleData.unPublishButton), ArticleData.unPublishButton + "button is available");
            loginActions.Logout();
            loginActions.LoginAs(copyeditor);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetArticlesList().Contains(ArticleData.nameOfArticle), ArticleData.nameOfArticle + " article is missing!");
            Assert.AreEqual(ArticleData.publishStatus, resultPage.GetStatus(),
              "Status is not " + ArticleData.publishStatus + ". Or check sorting, should be Status");
            Assert.AreEqual(ArticleData.isPublishedTrue, resultPage.GetIsPublished(),
                "Is published is not " + ArticleData.isPublishedTrue + ". Or check sorting, should be is published");
            resultPage.OpenFoundArticle();
            articleAction.DeleteAction();
            Commons.Sleep(3000);
            contentAction.SearchArticle(ArticleData.nameOfArticle);
            Assert.IsTrue(resultPage.GetMissingArticleMessage().Contains(AlertsEM.missingArticleMessage), AlertsEM.missingArticleMessage + " message is missing! or wrong message or Article is not missing");
            loginActions.Logout();
        }



    }

}
 