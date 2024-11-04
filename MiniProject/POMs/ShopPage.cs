//ShopPage Get rid of mouse move, generic method for adding to cart

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



        //Procedures for the 'Shop' Page
        public void AddItemToCart(string itemName)
        {
            driver.FindElement(By.LinkText(itemName)).Click();
        }


    }
}
