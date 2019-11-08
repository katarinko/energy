using System;
using Umbraco.Site.UITests.Core.entity;
using Umbraco.Site.UITests.Pages;

namespace Umbraco.Site.UITests.Core
{
    public class ArticleAction
    {
            
        public void ChangeStatusOfArticle(String Status)
        {
            var articlePage = new ArticlePage();
            articlePage.ClickWorkflow();
            articlePage.ChangeStatus(Status);

        }
     
        public ArticlePage ClickMetaDataTab()
        {
            var articlePage = new ArticlePage();
            articlePage.ClickMetaData();
            return new ArticlePage();
        }

        public void ClickWorkflowTab()
        {
            var articlePage = new ArticlePage();
            articlePage.ClickWorkflow();
        }

        public void DeleteAction()
        {
            var articlePage = new ArticlePage();
            articlePage.ClickActions();
            articlePage.ClickDelete();
            articlePage.ApplyDelete();
        }
        
        public void DefaultArticleForJournalist(StructureOfArticle article)
        {
            new ArticlePage()
                .ClickDocument()
                .EditName(article.GetName())
                .EditHeadline(article.GetHeadline())
                .EditSummary(article.GetSummury())
                .EditBody(article.GetBody());
                new FishbackLinkPage()
                .AddFishbackLink(article.GetFishbackLink());
                new ArticlePage()
                .ClickWorkflow()
                .ChangeStatus(article.GetStatus());

         }

        public void DefaultArticleWithoutLinkForEditor(StructureOfArticle article)

         {
            new ArticlePage()
            .ClickDocument()
            .EditName(article.GetName())
            .EditHeadline(article.GetHeadline())
            .EditSummary(article.GetSummury())
            .EditBody(article.GetBody())
            .ClickMetaData()
            .EditThumbnail(article.GetThumbnail())
            .EditKeywords(article.GetKeywords())
            .ClickWorkflow()
            .ChangeStatus(article.GetStatus()); 
        }
        
        public void DefaultArticleForCopyEditor(StructureOfArticle article)
        {
            new ArticlePage()
            .ClickDocument()
            .EditName(article.GetName())
            .EditHeadline(article.GetHeadline())
            .EditSummary(article.GetSummury())
            .EditBody(article.GetBody());
            new FishbackLinkPage()
            .AddFishbackLink(article.GetFishbackLink());
            new ArticlePage()
            .ClickMetaData()
            .EditThumbnail(article.GetThumbnail())
            .EditKeywords(article.GetKeywords())
            .ClickWorkflow()
            .ChangeStatus(article.GetStatus());
        }

        public void DefaultArticleWithoutLinkForCopyEditor(StructureOfArticle article)
        {
            new ArticlePage()
            .ClickDocument()
            .EditName(article.GetName())
            .EditHeadline(article.GetHeadline())
            .EditSummary(article.GetSummury())
            .EditBody(article.GetBody())
            .ClickMetaData()
            .EditThumbnail(article.GetThumbnail())
            .EditKeywords(article.GetKeywords())
            .ClickWorkflow()
            .ChangeStatus(article.GetStatus());
        }

        public void DefaultArticleForEditor(StructureOfArticle article)
        {
            new ArticlePage()
            .ClickDocument()
            .EditName(article.GetName())
            .EditHeadline(article.GetHeadline())
            .EditSummary(article.GetSummury())
            .EditBody(article.GetBody());
            new FishbackLinkPage()
            .AddFishbackLink(article.GetFishbackLink());
            new ArticlePage()
            .ClickMetaData()
            .EditThumbnail(article.GetThumbnail())
            .EditKeywords(article.GetKeywords())
            .ClickWorkflow()
            .ChangeStatus(article.GetStatus());
        }

        public void EditArticleAsEditor(StructureOfArticle article)
        {
            new ArticlePage()
           .EditThumbnail(article.GetThumbnail())
           .EditKeywords(article.GetKeywords())
           .EditAuthor(article.GetAuthor())
           .ClickWorkflow()
           .ChangeStatus(article.GetStatus());
        }

        public void EditArticleAsCopyEditor(StructureOfArticle article)
        {
            new ArticlePage()
           .EditKeywords(article.GetKeywords())
           .EditAuthor(article.GetAuthor())
           .ClickWorkflow()
           .ChangeStatus(article.GetStatus());
        }

        
    }
}
