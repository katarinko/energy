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
using Umbraco.Site.UITests.Core.Helpers;
using Umbraco.Site.UITests.Tests.Data.ArticleData;

namespace Umbraco.Site.UITests.Core
{
    public abstract class BaseTest 
    {

    [SetUp]
    public void setUp() {
        WebDriverManager.SetWebDriver();
        NavigationHelper.Open(ArticleData.homeURL);

        }

    [TearDown]
    public void tearDown() {
        WebDriverManager.CloseWebDriver();
    }
  }
}
