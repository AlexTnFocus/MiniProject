using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.POMs
{
    class ShopPage
    {
        private IWebDriver driver;

        public ShopPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for elements on the 'Shop' Page
        public IWebElement BeltButton => driver.FindElement(By.CssSelector("[href='?add-to-cart=28']"));
        public IWebElement CartLink => driver.FindElement(By.CssSelector("a[title='View your shopping cart']"));

        //Procedures for the 'Shop' Page
        public void AddBeltToCart()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(BeltButton).Perform();
            BeltButton.Click();
        }

        public void ClickCartLink()
        {
            CartLink.Click();
        }

    }
}
