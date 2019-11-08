using Umbraco.Site.UITests.Pages;

namespace Umbraco.Site.UITests.Core
{
    public class ResultAction
    {
        public void DeleteFoundArticle()
        {
            var resultPage = new ResultPage();
            resultPage.ClickCheckBox();
            resultPage.DeleteArticle();
       }

        
    }
}
