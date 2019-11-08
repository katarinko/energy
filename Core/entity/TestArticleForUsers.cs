using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Site.UITests.Core.entity
{
    public class TestArticleForUsers : StructureOfArticle
    {
        public TestArticleForUsers(String Name, String Headline, String Summary, String Body, String FishbackLink, String Keywords, String Thumbnail, String Author, String Status)
            : base(Name, Headline, Summary, Body, FishbackLink, Keywords, Thumbnail, Author, Status)
        {
        }

         public static TestArticleForUsers GetDefaultArticleForjournalist()
        {
            return new TestArticleForUsers("Title - Goldman SachsArticleForAutoTest", 
                "Headline - Goldman SachsArticleForAutoTest Projects Steady Rise in US Oil Prices.",
                "Summary - Einhorn's argument was never about prices, however.",
                "Body - It's been roughly a year since hedge fund titan David Einhorn pilloried US shale oil producers as destroyers of capital and a bad investment for anyone who cares about traditional financial metrics (EIF May13'15).",
                "Oil and Gas Prices, Mar. 31, 2017",
                "Test",
                "testimage",
                "jornalist",
                "INITIAL EDITING");
        }

        public static TestArticleForUsers GetDefaultArticleForEditor()
        {
            return new TestArticleForUsers("Title - Goldman SachsArticleForAutoTest",
                "Headline - Goldman SachsArticleForAutoTest Projects Steady Rise in US Oil Prices.",
                "Summary - Einhorn's argument was never about prices, however.",
                "Body - It's been roughly a year since hedge fund titan David Einhorn pilloried US shale oil producers as destroyers of capital and a bad investment for anyone who cares about traditional financial metrics (EIF May13'15).",
                "Oil and Gas Prices",
                "Test",
                "testimage",
                "editor",
                "COPY EDITING");
        }

        public static TestArticleForUsers GetDefaultArticleForCopyEditor()
        {
            return new TestArticleForUsers("Title - Goldman SachsArticleForAutoTest",
                "Headline - Goldman SachsArticleForAutoTest Projects Steady Rise in US Oil Prices.",
                "Summary - Einhorn's argument was never about prices, however.",
                "Body - It's been roughly a year since hedge fund titan David Einhorn pilloried US shale oil producers as destroyers of capital and a bad investment for anyone who cares about traditional financial metrics (EIF May13'15).",
                "Oil and Gas Prices",
                "Test",
                "testimage",
                "copyeditor",
                "FINAL EDITING");
        }

    }
}
