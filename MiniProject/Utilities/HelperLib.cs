using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;


namespace MiniProject.Utilities
{
    public static class HelperLib
    {
        public static void WaitForElementPresent(IWebDriver driver, By theElement)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(theElement).Displayed);
        }
    }
}
