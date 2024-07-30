using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;

namespace TestProject1
{
    public class UnitTest2
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
        public void SearchFunctionalityTest()
        {
            try
            {
                // Mở trang chủ
                driver.Navigate().GoToUrl("https://tuoitre.vn/");

                // Tìm ô nhập từ khóa tìm kiếm
                var searchField = wait.Until(d => d.FindElement(By.ClassName("header__search")));
                searchField.SendKeys("Tin tức mới");

                // Nhấn Enter để gửi yêu cầu tìm kiếm
                searchField.SendKeys(Keys.Enter);

              
                System.Threading.Thread.Sleep(5000); //5s

                // Chờ và kiểm tra tiêu đề trang thay đổi để xác nhận tìm kiếm
                wait.Until(d => d.Title.Contains("Tin tức mới"));

                // Hoặc chờ và kiểm tra nội dung của các phần tử kết quả tìm kiếm
                bool resultsDisplayed = IsElementDisplayed(By.ClassName("total-search"));
            }
            catch (NoSuchElementException e)
            {
                Assert.Fail($"Có lỗi xảy ra trong quá trình kiểm tra: {e.Message}");
            }
            catch (WebDriverTimeoutException e)
            {
                Assert.Fail($"Quá thời gian chờ đợi: {e.Message}");
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
