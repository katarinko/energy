using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Site.UITests.Core.entity
{
    public class StructureOfArticle
    {
        private String Name;
        private String Headline;
        private String Summary;
        private String Body;
        private String FishbackLink;
        private String Keywords;
        private String Thumbnail;
        private String Author;
        private String Status;
               
        public StructureOfArticle(String Name, String Headline, String Summary, String Body, string FishbackLink, String Keywords, String Thumbnail, String Author, String Status)
        {
            this.Name = Name;
            this.Headline = Headline;
            this.Summary = Summary;
            this.Body = Body;
            this.FishbackLink = FishbackLink;
            this.Keywords = Keywords;
            this.Thumbnail = Thumbnail;
            this.Status = Status;
            this.Author = Author;


        }
        public String GetName()
        {
            return Name;
        }

        public String GetHeadline()
        {
            return Headline;
        }
        public String GetSummury()
        {
            return Summary;
        }

        public String GetBody()
        {
            return Body;
        }
        public String GetFishbackLink()
        {
            return FishbackLink;
        }
        public String GetKeywords()
        {
            return Keywords;
        }
         public String GetThumbnail()
        {
            return Thumbnail;
        }
        public String GetAuthor()
        {
            return Author;
        }
        public String GetStatus()
        {
            return Status;
        }

        
    }
}
