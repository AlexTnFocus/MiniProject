using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.POMs
{
    class CartPage
    {
        private IWebDriver driver;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for elements on the 'Cart' Page
        public IWebElement CouponField => driver.FindElement(By.Id("coupon_code"));
        public IWebElement CouponButton => driver.FindElement(By.Name("apply_coupon"));
        //public IWebElement SubtotalValue => driver.FindElement(By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)"));
        //public IWebElement DiscountValue => driver.FindElement(By.CssSelector("td[data-title='Coupon: edgewords'] span[class='woocommerce-Price-amount amount']"));
        //public IWebElement ShippingValue => driver.FindElement(By.CssSelector("tr[class='woocommerce-shipping-totals shipping'] bdi:nth-child(1)"));
        //public IWebElement TotalValue => driver.FindElement(By.CssSelector("tr[class='order-total'] bdi:nth-child(1)"));
        public IWebElement MyAccountLink => driver.FindElement(By.CssSelector("li[id='menu-item-46'] a"));

        public IWebElement CheckoutLink => driver.FindElement(By.CssSelector("li[id='menu-item-45'] a"));

        //Procedures for the 'Shop' Page
        public void KeyIntoCoupon(string couponCode)
        {
            CouponField.Clear();
            CouponField.SendKeys(couponCode);
        }
        public void ClickCoupon()
        {
            CouponButton.Click();
        }
        public void ClickMyAccountLink()
        {
            MyAccountLink.Click();
        }
        public string GetSubtotal()
        {
            return driver.FindElement(By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)")).Text;
        }
        public string GetDiscount()
        {
            return driver.FindElement(By.CssSelector("td[data-title='Coupon: edgewords'] span[class='woocommerce-Price-amount amount']")).Text;
        }
        public string GetShipping()
        {
            return driver.FindElement(By.CssSelector("tr[class='woocommerce-shipping-totals shipping'] bdi:nth-child(1)")).Text;
        }
        public string GetTotal()
        {
            return driver.FindElement(By.CssSelector("tr[class='order-total'] bdi:nth-child(1)")).Text;
        }
        public void ClickCheckoutLink()
        {
            CheckoutLink.Click();
        }
    }
}
