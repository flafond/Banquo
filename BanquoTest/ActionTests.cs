using Banquo.Extensions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using OpenQA.Selenium;

namespace BanquoTest
{
    public class ActionTests
    {
        private const string testSite = "https://demoqa.com/";
        private User user;

        private static string Url(string page = "") => $"{testSite}{page}";

        private static string PageTitleXPath(string name) => $"//div[@class='main-header' and .='{name}']";
        private static string CardLink(string name) => $"//h5[.='{name}']";



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
        public void TestClicking()
        {
            User joe = user.IsOnPage(Url());
            joe.Click(CardLink("Elements"));
            joe.SeesElementInDOM(PageTitleXPath("Elements"));

            joe.IsOnPage(Url());
            joe.Click(CardLink("Forms"), 500);
            joe.SeesElementInDOM(PageTitleXPath("Forms"));

            joe.IsOnPage(Url());
            joe.Click(By.XPath(CardLink("Widgets")));
            joe.SeesElementInDOM(PageTitleXPath("Widgets"));

            joe.IsOnPage(Url());
            joe.Click(By.XPath(CardLink("Interactions")), 500);
            joe.SeesElementInDOM(PageTitleXPath("Interactions"));
        }

        [Test]
        public void TestFields()
        {
            User mary = user.IsOnPage(Url("automation-practice-form"));
            mary.FillsField("#firstName", "Mary")
                .SeesInField("Mary")
                .ClearsField()
                .SeesInField("");

            mary.FillsField("#lastName", "Smith", 500)
                .SeesInField("Smith")
                .ClearsField()
                .SeesInField("");

            mary.FillsField(By.Id("firstName"), "Nancy")
                .SeesInField("Nancy")
                .ClearsField()
                .SeesInField("");

            mary.FillsField(By.Id("lastName"), "Drew", 500)
                .SeesInField("Drew")
                .ClearsField()
                .SeesInField("");
        }

        [Test]
        public void TestOptions()
        {
            var sports = "//input[@id='hobbies-checkbox-1']";

            User mary = user.IsOnPage(Url("automation-practice-form"));
            mary.ChecksOption(sports)
                .Selected.Should().BeTrue();

            mary.ChecksOption(By.XPath(sports))
                .Selected.Should().BeTrue();

            mary.UncheckOption(sports, 500)
                .Selected.Should().BeFalse();

            mary.ChecksOption(By.XPath(sports), 500)
                .UncheckOption()
                .Selected.Should().BeFalse();

            mary.ChecksOption(sports);
            mary.UncheckOption(sports)
                .Selected.Should().BeFalse();
        }

        [Test]
        [Ignore("Submit not working yet")]
        public void TestFormSubmit()
        {
            User joe = user.IsOnPage(Url("automation-practice-form"));
            joe.FillsField("#firstName", "Joe");
            joe.FillsField("#lastName", "Black");
            joe.FillsField("#firstName", "Joe");
            joe.FillsField("#userEmail", "joe.black@example.com");
            joe.FillsField("#userNumber", "5055551212");
            joe.FillsField("//*[@id='dateOfBirth']//input", "12/13/1970" + Keys.Enter);
            joe.FillsField("#subjectsInput", "Math");
            joe.ChecksOption("#hobbies-checkbox-1");
            joe.FillsField("#subjectsInput", "Math");
            joe.FillsField("#currentAddress", "1 Main St.");
            //joe.Click("//input[@id='react-select-3-input']").WaitMSec(100)
            //    .Type("NCR" + Keys.Tab);
            //joe.Click("//input[@id='react-select-4-input']").WaitMSec(100)
            //    .Type("Delhi" + Keys.Tab);
            joe.Type("//input[@id='react-select-3-input']", "NCR" + Keys.Tab);
            joe.Type("//input[@id='react-select-4-input']", "Delhi" + Keys.Tab);

            joe.SubmitForm("userForm");

            joe.SeesInTitle("ToolsQA");
        }
    }
}