using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Site.UITests.Tests.Data.ArticleData
{
    public class ArticleData
    {
        public const string homeURL = "http://eigumbracodev-backoffice.azurewebsites.net/umbraco";
        public const string nameOfArticle = "Title - Goldman SachsArticleForAutoTest";
        public const string siteURL = "http://eigumbracodev.azurewebsites.net/#";
        public const string testTitleOfArticle = "/world-energy-opinion/title-goldman-sachsarticleforautotest/";
        public const string copyStatus = "COPY EDITING";
        public const string initialStatus = "INITIAL EDITING";
        public const string finalStatus = "FINAL EDITING";
        public const string publishStatus = "PUBLISHED";
        public const string authorJournalist = "journalist";
        public const string authorEditor = "editor";
        public const string authorCopyEditor = "copyeditor";
        public const string authorEditorJournalist = "editor, journalist";
        public const string authorCopyEditorEditor = "copyeditor, editor";
        public const string authorsAll = "copyeditor, editor, journalist";
        public const string authorsAllOnSite = "journalist, editor, copyeditor";
        public const string isPublishedFalse = "false";
        public const string isPublishedTrue = "true";
        public const string headlineOfArticle = "Headline - Goldman SachsArticleForAutoTest Projects Steady Rise in US Oil Prices.";
        public const string summaryOfArticle = "Summary - Einhorn's argument was never about prices, however.";
        public const string bodyOfArticle = "Body - It's been roughly a year since hedge fund titan David Einhorn pilloried US shale oil producers as destroyers of capital and a bad investment for anyone who cares about traditional financial metrics (EIF May13'15).OD Apr 3'17";
        public const string bodyOfArticleWithoutLink = "Body - It's been roughly a year since hedge fund titan David Einhorn pilloried US shale oil producers as destroyers of capital and a bad investment for anyone who cares about traditional financial metrics (EIF May13'15).";
        public const string fishbackLink = "OD Apr 3'17";
        public const string saveAndPublishButton = "Save and publish";
        public const string unPublishButton = "Unpublish";
        public const string returnToLIstButton = "Return to list";
        public const string previewButton = "Preview";
    }
}
