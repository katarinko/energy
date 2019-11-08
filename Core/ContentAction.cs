using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Site.UITests.Core.entity;
using Umbraco.Site.UITests.Pages;

namespace Umbraco.Site.UITests.Core
{
   public  class ContentAction
    {
      
        public ArticlePage CreateNewArticle()
       {
           var contentPage = new ContentPage();
           contentPage.ClickContent();
           contentPage.ClickWEO();
           contentPage.CreateWeoArticle();
           return new ArticlePage();
       }
       
       public void OpenListOfArticles()
        {
           var contentPage = new ContentPage();
           contentPage.ClickContent();
           contentPage.ClickWEO();
        }

       public ResultPage SearchArticle(String searchdata)
       {
          
           var contentPage = new ContentPage();
           contentPage.ClickContent();
           contentPage.ClickWEO();
           contentPage.SearchResult(searchdata);
           return new ResultPage();
        }

        public string SearchDeletedArticle(String searchdata)
        {
            var contentPage = new ContentPage();
            contentPage.ClickContent();
            contentPage.ClickWEO();
            contentPage.SearchResult(searchdata);
            var resultPage = new ResultPage();
            return resultPage.GetMissingArticleMessage();
        }

       

    }
}
