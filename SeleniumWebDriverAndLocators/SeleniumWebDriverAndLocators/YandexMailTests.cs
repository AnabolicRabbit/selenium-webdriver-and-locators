using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace SeleniumWebDriverAndLocators
{
    [TestFixture]
    class YandexMailTests
    {
        private IWebDriver driver;
        private string baseUrl;

        [SetUp]
        public void TestInitialize()
        {
            var service = FirefoxDriverService.CreateDefaultService();
            this.driver = new FirefoxDriver(service);
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //baseUrl for gmail account creation is added
            //this.baseUrl = "https://www.google.com/intl/ru/gmail/about/";
            this.baseUrl = "https://yandex.by/";

            this.driver.Navigate().GoToUrl(this.baseUrl);
            this.driver.Manage().Window.Maximize();

            //Steps for gmail account creation are added
            //IsElementVisible(createAccountButton);
            //this.driver.FindElement(createAccountButton).Click();

            //var currentTab = driver.WindowHandles.Last();
            //this.driver.SwitchTo().Window(currentTab);

            //IsElementVisible(confirmationField);
            //this.driver.FindElement(By.Name("firstName")).SendKeys("Selenium");
            //this.driver.FindElement(By.Name("lastName")).SendKeys("WebDriver");
            //this.driver.FindElement(By.Name("Username")).Clear();
            //this.driver.FindElement(By.Name("Username")).SendKeys("Selenium2Web2Driver");
            //this.driver.FindElement(By.Name("Passwd")).SendKeys("$elenium789");
            //this.driver.FindElement(confirmationField).SendKeys("$elenium789");
            //this.driver.FindElement(By.XPath("//span[text()='Далее']")).Click();

            //IsElementVisible(furtherButton);
            //this.driver.FindElement(furtherButton).Click();

            //IsElementVisible(phoneNumberField);
            //this.driver.FindElement(phoneNumberField).SendKeys("+*** *********");
            //this.driver.FindElement(By.XPath("//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc qIypjc TrZEUc lw1w4b']")).Click();

            //IsElementVisible(codeField);
            //this.driver.FindElement(codeField).SendKeys("******");
            //this.driver.FindElement(By.XPath("//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc qIypjc TrZEUc lw1w4b']")).Click();

            //IsElementVisible(furtherButton);
            //this.driver.FindElement(By.Id("day")).SendKeys("1");
            //this.driver.FindElement(By.Id("month")).Click();
            //this.driver.FindElement(By.XPath("//select/option[text()='Январь']")).Click();
            //this.driver.FindElement(By.Id("year")).SendKeys("2000");
            //this.driver.FindElement(By.XPath("//select/option[text()='Женский']")).Click();
            //this.driver.FindElement(furtherButton).Click();

            //IsElementVisible(skipButton);
            //this.driver.FindElement(skipButton).Click();

            //IsElementVisible(acceptButton);
            //this.driver.FindElement(acceptButton).Click();

            //IsElementVisible(furtherButton);
            //this.driver.FindElement(acceptButton).Click();

            //Thread.Sleep(15000);

            //IsElementVisible(furtherButton);
            //this.driver.FindElement(furtherButton).Click();

            //IsElementVisible(furtherButton);
            //this.driver.FindElement(furtherButton).Click();

            //IsElementVisible(checkTextField);
            //Thread.Sleep(10000);
            //this.driver.FindElement(furtherButton).Click();
        }

        [Test]
        public void GetLoggedInUser()
        {
            //Arrange
            LoginToMailBox();

            string expectedUserName = "Selenium1Web1Driver";

            //Act
            var actualUserName = this.driver.
                FindElement(By.XPath("//a[@class='user-account user-account_left-name user-account_has-ticker_yes user-account_has-accent-letter_yes count-me legouser__current-account legouser__current-account i-bem']/span[1]")).
                Text;

            //Assert
            Assert.AreEqual(expectedUserName, actualUserName, "The actual logged in user differs from the expected.");
        }

        [Test]
        public void IsPresentInDraftEmail()
        {
            //Arrange
            LoginToMailBox();
            CreateDraftEmail();
            this.driver.FindElement(By.XPath("//a[@data-title='Черновики']")).Click();

            string expectedSubject = "Greeting";

            //Act
            var actualSubject = this.driver.
                FindElement(By.XPath("(//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_subject']/span)[1]")).
                Text;

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject, "The actual number of draft emails differs from the expected.");
        }

        [Test]
        public void GetAddresseeSubjectBodyFromDraftEmail()
        {
            //Arrange
            LoginToMailBox();
            CreateDraftEmail();
            this.driver.FindElement(By.XPath("//a[@data-title='Черновики']")).Click();

            string expectedAddressee = "Selenium WebDriver";
            string expectedSubject = "Greeting";
            string expectedBody = "Hi, Selenium!";

            //Act
            var actualAddressee = this.driver.
                FindElement(By.XPath("(//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_sender js-message-snippet-sender']/span)[1]")).
                Text;
            var actualSubject = this.driver.
                FindElement(By.XPath("(//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_subject']/span)[1]")).
                Text;
            var actualBody = this.driver.
                FindElement(By.XPath("(//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_firstline js-message-snippet-firstline']/span)[1]")).
                Text;

            //Assert
            Assert.AreEqual(expectedAddressee, actualAddressee, "The actual addressee differs from the expected.");
            Assert.AreEqual(expectedSubject, actualSubject, "The actual subject differs from the expected.");
            Assert.AreEqual(expectedBody, actualBody, "The actual body differs from the expected.");
        }

        [Test]
        public void IsNotPresentInDraftEmail()
        {
            //Arrange
            LoginToMailBox();
            CreateDraftEmail();
            var initialNumberOfDraftEmails = int.Parse(this.driver.FindElement(By.XPath("//a[@href='#draft']/div/span")).Text);
            SendDraftEmail();
            this.driver.FindElement(By.XPath("//a[@class='control link link_theme_normal ComposeDoneScreen-Link']")).Click();
            this.driver.FindElement(By.XPath("//a[@data-title='Черновики']")).Click();

            //Act
            int actualNumberOfDraftEmails;
            try
            {
                actualNumberOfDraftEmails = int.Parse(this.driver.FindElement(By.XPath("//a[@href='#draft']/div/span")).Text);
            }
            catch (Exception ex)
            {
                actualNumberOfDraftEmails = 0;
            }

            //Assert
            Assert.AreEqual(1, initialNumberOfDraftEmails - actualNumberOfDraftEmails, "The email is still in the draft folder.");
        }

        [Test]
        public void IsPresentInSentEmail()
        {
            //Arrange
            LoginToMailBox();
            CreateDraftEmail();
            SendDraftEmail();
            this.driver.FindElement(By.XPath("//a[@class='control link link_theme_normal ComposeDoneScreen-Link']")).Click();
            this.driver.FindElement(By.XPath("//a[@data-title='Отправленные']")).Click();

            IsElementVisible(By.XPath("//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_subject']"));
            string expectedSubject = "Greeting";

            //Act
            var actualSubject = this.driver.
                FindElement(By.XPath("(//span[@class='mail-MessageSnippet-Item mail-MessageSnippet-Item_subject']/span)[1]")).
                Text;

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject, "The email is not in the sent folder.");
        }

        [Test]
        public void GetLoggedOutUser()
        {
            //Arrange
            LoginToMailBox();
            CreateDraftEmail();
            SendDraftEmail();
            this.driver.FindElement(By.XPath("//a[@class='control link link_theme_normal ComposeDoneScreen-Link']")).Click();
            this.driver.FindElement(By.XPath("//a[@data-title='Отправленные']")).Click();
            this.driver.FindElement(By.XPath("(//div/img[@class='user-pic__image'])[1]")).Click();
            this.driver.FindElement(By.XPath("//span[text()='Выйти из сервисов Яндекса']")).Click();

            IsElementVisible(By.XPath("//div[@class='passp-auth-screen passp-welcome-page']/h1/span"));
            string expectedLoginMessage = "Войдите с Яндекс ID, чтобы перейти к Почте";

            //Act
            var actualLoginMessage = this.driver.FindElement(By.XPath("//div[@class='passp-auth-screen passp-welcome-page']/h1/span")).Text;

            //Assert
            Assert.AreEqual(expectedLoginMessage, actualLoginMessage, "Log Off failed.");
        }

        public void IsElementVisible(By element, int timeoutSecs = 10)
        {
            new WebDriverWait(this.driver, TimeSpan.FromSeconds(timeoutSecs)).Until(ExpectedConditions.ElementIsVisible(element));
        }

        public void LoginToMailBox()
        {
            By signInButton = By.XPath("//a[@class='home-link desk-notif-card__login-new-item desk-notif-card__login-new-item_enter home-link_black_yes home-link_hover_inherit']");
            IsElementVisible(signInButton);
            this.driver.FindElement(signInButton).Click();

            By emailField = By.Id("passp-field-login");
            IsElementVisible(emailField);
            this.driver.FindElement(emailField).SendKeys("Selenium1Web1Driver@yandex.by");
            this.driver.
                FindElement(By.XPath("//button[@class='Button2 Button2_size_l Button2_view_action Button2_width_max Button2_type_submit']")).
                Click();

            By passwordField = By.Id("passp-field-passwd");
            IsElementVisible(passwordField);
            this.driver.FindElement(passwordField).SendKeys("$elenium789");
            this.driver.
                FindElement(By.XPath("//button[@class='Button2 Button2_size_l Button2_view_action Button2_width_max Button2_type_submit']")).
                Click();

            By emailButton = By.XPath("//a[@class='home-link desk-notif-card__domik-mail-line home-link_black_yes']");
            IsElementVisible(emailButton);
            this.driver.FindElement(emailButton).Click();

            var currentTab = driver.WindowHandles.Last();
            this.driver.SwitchTo().Window(currentTab);
        }

        public void CreateDraftEmail()
        {
            this.driver.FindElement(By.XPath("//a[@class='mail-ComposeButton js-main-action-compose']")).Click();

            this.driver.FindElement(By.ClassName("composeYabbles")).Click();
            this.driver.FindElement(By.ClassName("ContactsSuggestItemDesktop-Email")).Click();
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).SendKeys("Greeting");
            this.driver.
                FindElement(By.XPath("//div[@class='cke_wysiwyg_div cke_reset cke_enable_context_menu cke_editable cke_editable_themed cke_contents_ltr cke_htmlplaceholder']")).
                SendKeys("Hi, Selenium!");

            this.driver.FindElement(By.XPath("//a[@data-title='Черновики']")).Click();
        }

        public void SendDraftEmail()
        {
            this.driver.FindElement(By.XPath("//span[@class='mail-MessageSnippet-FromText']")).Click();
            this.driver.
                FindElement(By.XPath("//button[@class='control button2 button2_view_default button2_tone_default button2_size_l button2_theme_action button2_pin_circle-circle ComposeControlPanelButton-Button ComposeControlPanelButton-Button_action']")).
                Click();
        }

        [TearDown]
        public void TestClean()
        {
            this.driver.Close();
            this.driver.Quit();
        }
    }
}