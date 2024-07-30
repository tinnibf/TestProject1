using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
        public void LoginToVnExpress()
        {
            try
            {
                // Mở trang chủ của VnExpress
                driver.Navigate().GoToUrl("https://tuoitre.vn/");

                // Click vào liên kết đăng nhập nếu có
                var loginButton = wait.Until(d => d.FindElement(By.Id("head_login")));
                loginButton.Click();

                // Tìm ô nhập tên đăng nhập và mật khẩu
                var usernameField = wait.Until(d => d.FindElement(By.Name("username")));
                usernameField.SendKeys("cuong.0907062600@gmail.com");

                var passwordField = driver.FindElement(By.Id("password-field-register"));
                passwordField.SendKeys("Thuong12345");

                // Tìm và nhấn nút đăng nhập
                var submitButton = driver.FindElement(By.Id("button-login"));
                submitButton.Click();

                // Chờ trang đăng nhập phản hồi
                wait.Until(d => d.Title.Contains("Expected Title After Login") || d.FindElement(By.CssSelector("txt-error-login")).Displayed);

                // Kiểm tra nếu đăng nhập thành công
                if (driver.Title.Contains("Expected Title After Login"))
                {
                    Console.WriteLine("Đăng nhập thành công!");
                    Assert.Pass("Đăng nhập thành công.");
                }
                else if (driver.FindElement(By.CssSelector("txt-error-login")).Displayed)
                {
                    Console.WriteLine("Đăng nhập không thành công.");
                    Assert.Fail("Đăng nhập không thành công.");
                }
                else
                {
                    Assert.Fail("Không rõ lý do đăng nhập không thành công.");
                }
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine($"Lỗi: {e.Message}");
                Assert.Fail("Có lỗi xảy ra trong quá trình kiểm tra.");
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