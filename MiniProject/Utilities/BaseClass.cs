//Do not rely on implicit timeouts, use alternative
//Add multiple browser options

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Utilities
{
    public class BaseClass
    {
        public IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //Dismisses the demo warning at the bottom of the page, as this is always a necessary step
            driver.FindElement(By.LinkText("Dismiss")).Click();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
