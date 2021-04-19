using Banquo.Extensions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using OpenQA.Selenium;

namespace BanquoTest
{
    public class SeeingTests
    {
        private const string testSite = "https://demoqa.com/";
        private User user;

        private static string Url(string page = "")
        {
            return $"{testSite}{page}";
        }

        [SetUp]
        public void Setup()
        {
            user = new User(new ChromeDriver());
        }

        [TearDown]
        public void TearDown()
        {
            user.Quit();
        }

        [Test]
        public void TestTitle()
        {
            user.IsOnPage(Url())
                .SeesInTitle("Tools")
                .SeesInTitle("Tools", 500)
                .SeesTitleEquals("ToolsQA")
                .SeesTitleEquals("ToolsQA", 500)
                .DoesntSeeInTitle("Not in Title")
                .DoesntSeeInTitle("Not in Title", 500);
        }

        [Test]
        public void TestSees()
        {
            user.IsOnPage(Url("elements"))
                .Sees("Please select an item")
                .Sees("Please select an item", 500)
                .DoesntSee("Not on Page")
                .DoesntSee("Not on Page", 500);
        }

        [Test]
        public void TestSeesElement()
        {
            User joe = user.IsOnPage(Url("elements"));
            joe.SeesElement("#app");
            joe.SeesElement("#app", 500);
            joe.SeesElement(By.Id("app"));
            joe.SeesElement(By.Id("app"), 500);
        }

        [Test]
        public void TestSeesDOMElement()
        {
            User joe = user.IsOnPage(Url("elements"));
            joe.SeesElementInDOM(".main-header");
            joe.SeesElementInDOM(".main-header", 500);
            joe.SeesElementInDOM(By.ClassName("main-header"));
            joe.SeesElementInDOM(By.ClassName("main-header"), 500);
        }

        [Test]
        public void TestSeesElementCount()
        {
            User joe = user.IsOnPage(Url());
            joe.SeesNumberofElements("//div[contains(@class,'top-card')]", 6);
            joe.SeesNumberofElements("//div[contains(@class,'top-card')]", 6, 500);
            joe.SeesNumberofElements(By.XPath("//div[contains(@class,'top-card')]"), 6);
            joe.SeesNumberofElements(By.XPath("//div[contains(@class,'top-card')]"), 6, 500);
        }
    }
}