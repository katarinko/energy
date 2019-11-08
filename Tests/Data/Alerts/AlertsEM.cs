using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Site.UITests.Tests.Data.Alerts
{
    //AlertsExpectedMessages
    public class AlertsEM
    {
        public const string errorEmptyMessage = "Username or password cannot be empty";
        public const string errorWrongMessage = "Login failed for user wrongname";
        public const string errorWrongPassword = "Login failed for user journalist";
        public const string saveMessage = "Content saved:";
        public const string publishMessage = "Content published:";
        public const string missingArticleMessage = "Sorry, we can not find what you are looking for";
        public const string unPublishMessage = "Unpublish:";

    }
}
