using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;

namespace TestProject1
{
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            // Khởi tạo trình duyệt Chrome
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void LoginToTuoiTre()
        {
            try
            {
                // Mở trang chủ
                driver.Navigate().GoToUrl("https://tuoitre.vn/");

                // Click vào liên kết đăng nhập nếu có
                var loginButton = wait.Until(d => d.FindElement(By.Id("head_login")));
                loginButton.Click();

                // Tìm ô nhập tên đăng nhập và mật khẩu
                var usernameField = wait.Until(d => d.FindElement(By.Name("username")));
                usernameField.SendKeys("cuong.0907062600@gmail.com");

                var passwordField = driver.FindElement(By.Name("password"));
                passwordField.SendKeys("Thuong12345");

                // Tìm và nhấn nút đăng nhập
                var submitButton = driver.FindElement(By.Id("button-login"));
                submitButton.Click();

                bool loginSuccessful = false;
                try
                {
                    // Kiểm tra tiêu đề trang hoặc phần tử cụ thể để xác nhận đăng nhập thành công
                    wait.Until(d => d.Title.Contains("Tiêu đề mong đợi sau khi đăng nhập"));
                    loginSuccessful = true;
                }
                catch (WebDriverTimeoutException)
                {
                    // Kiểm tra thông báo lỗi nếu tiêu đề không thay đổi
                    bool errorDisplayed = IsElementDisplayed(By.CssSelector("txt-error-login"));
                    if (errorDisplayed)
                    {
                        loginSuccessful = false;
                    }
                }
            }
            catch (NoSuchElementException e)
            {
                Assert.Fail($"Có lỗi xảy ra trong quá trình kiểm tra: {e.Message}");
            }
        }

        private bool IsElementDisplayed(By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Đóng trình duyệt sau khi hoàn thành test
            driver.Quit();
        }
    }
}
