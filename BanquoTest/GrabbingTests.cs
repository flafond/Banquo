using Banquo.Extensions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using OpenQA.Selenium;
using System.Linq;

namespace BanquoTest
{
    public class GrabbingTests
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
        public void TestGrabAttributeFrom()
        {
            user.IsOnPage(Url());
            user.GrabAttributeFrom("//div[@class='home-banner']//a", "target")
                .Should().Be("_blank");

            user.GrabAttributeFrom("//img[@alt='New Live Session']", "src", 500)
                .Should().Contain("/images/WB.svg");

            var list = user.IsOnPage(Url("date-picker"))
                .GrabAttributeFromAll("//input", "type");

            list.Should().HaveCount(2);
            list[0].Should().Be("text");
            list[1].Should().Be("text");
        }

        public void TestGrabCssPropertyFrom()
        {
            user.IsOnPage(Url("auto-complete"));
            user.GrabCssPropertyFrom("#autoCompleteContainer", "font-size")
                .Should().Be("1rem");
        }
    }
}