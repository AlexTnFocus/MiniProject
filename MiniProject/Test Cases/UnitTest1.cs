//Additional to do:
//Add navigation page to fix naviagting between pages
//Add reporting, for assertions and take screenshots of items used in assertions

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Interactions;

using static MiniProject.Utilities.HelperLib;
using MiniProject.POMs;
using System.Data.SqlTypes;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace MiniProject.Test_Cases
{
    [TestFixture]
    internal class Tests : MiniProject.Utilities.BaseClass
    {
        [Test]
        public void TestCase1()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            ShopPage ShopPage = new ShopPage(driver);
            CartPage CartPage = new CartPage(driver);
            NavigationPage NavigationPage = new NavigationPage(driver);

            //Dismiss the 'Demo Store' bar that appears at the bottom
            MyAccountPage.DismissDemoWarning();

            //Login to the account
            //Email = magmortar@pmail.com
            //Password = octoberComic0n!?
            MyAccountPage.FullLogin("magmortar@pmail.com", "octoberComic0n!?");

            //Enter the shop via the Shop button
            //MyAccountPage.ClickShopLink();
            NavigationPage.GoShop();

            //Add a belt to the cart, by clicking add to cart below the item on the shop page
            ShopPage.AddBeltToCart();

            //View the cart
            Thread.Sleep(1000);//Aware of issue where if the cart is empty and an item is added, it may take a second to load
            ShopPage.ClickCartLink();

            //Apply a coupon "edgewords"
            CartPage.KeyIntoCoupon("edgewords");
            CartPage.ClickCoupon();

            //Check that the coupon is valid for 15% off
            WaitForElementPresent(driver, By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)"));
            Assert.That((Decimal.Divide((Decimal.Parse(CartPage.GetSubtotal().Remove(0, 1)) - Decimal.Parse(CartPage.GetDiscount().Remove(0, 1))), Decimal.Parse(CartPage.GetSubtotal().Remove(0, 1)))), Is.EqualTo(0.85), "Discount is not valid for 15% off");

            //Check that the total after shipping is correct
            Assert.That(Decimal.Parse(CartPage.GetSubtotal().Remove(0, 1)) -
                Decimal.Parse(CartPage.GetDiscount().Remove(0, 1))
                + Decimal.Parse(CartPage.GetShipping().Remove(0, 1)),
                Is.EqualTo(Decimal.Parse(CartPage.GetTotal().Remove(0, 1))),
                "Error");

            //Log out
            CartPage.ClickMyAccountLink();
            MyAccountPage.ClickLogout();

            /*driver.FindElement(By.LinkText("Dismiss")).Click();//Dismisses the "demo store" warning which gets in the way
            //Login details for manually setup account
            //Email = magmortar@pmail.com
            //Password = octoberComic0n!?

            //Login to the account
            driver.FindElement(By.Id("username")).SendKeys("magmortar@pmail.com");
            driver.FindElement(By.Id("password")).SendKeys("octoberComic0n!?");
            driver.FindElement(By.Name("login")).Click(); //Possible to fail if button not on screen, scroll to find it?

            //Enter the shop via the Shop button
            driver.FindElement(By.LinkText("Shop")).Click();

            //Add a belt to the cart, by clicking add to cart below the item on the shop page
            var AddToCart = driver.FindElement(By.CssSelector("[href='?add-to-cart=28']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(AddToCart).Perform();
            AddToCart.Click();

            //View the cart
            driver.FindElement(By.ClassName("cart-contents")).Click();
            //Apply a coupon "edgewords"
            driver.FindElement(By.Id("coupon_code")).SendKeys("edgewords");
            driver.FindElement(By.Name("apply_coupon")).Click();
            //Check that the coupon is valid for 15% off
            Thread.Sleep(2000); //Wait is necessary, change later
            var Subtotal = driver.FindElement(By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)")).Text;
            //Console.WriteLine(Subtotal);
            var Discount = driver.FindElement(By.CssSelector("td[data-title='Coupon: edgewords'] span[class='woocommerce-Price-amount amount']")).Text;
            //Console.WriteLine(Discount);//May need to wait for price to update for new added item

            Subtotal = Subtotal.Remove(0, 1);
            Discount = Discount.Remove(0, 1);

            decimal SubMoney = Decimal.Parse(Subtotal);
            decimal DisMoney = Decimal.Parse(Discount);


            //Console.WriteLine(Decimal.Divide((SubMoney - DisMoney), SubMoney));
            Assert.That((Decimal.Divide((SubMoney - DisMoney), SubMoney)), Is.EqualTo(0.85), "Discount is not valid for 15% off");
            //Check that the total after shipping is correct
            var Shipping = driver.FindElement(By.CssSelector("tr[class='woocommerce-shipping-totals shipping'] bdi:nth-child(1)")).Text;
            Shipping = Shipping.Remove(0, 1);
            decimal ShiMoney = Decimal.Parse(Shipping);
            //Console.WriteLine(Shipping);

            var Total = driver.FindElement(By.CssSelector("tr[class='order-total'] bdi:nth-child(1)")).Text;
            Total = Total.Remove(0, 1);
            decimal TotMoney = Decimal.Parse(Total);
            //Console.WriteLine(Total);

            var SupposedTotal = SubMoney - DisMoney + ShiMoney;
            Assert.That(SupposedTotal, Is.EqualTo(TotMoney), "The supposed total and the actual do not match");

            //Log out
            driver.FindElement(By.CssSelector("li[id='menu-item-46'] a")).Click();
            driver.FindElement(By.CssSelector("li[class='woocommerce-MyAccount-navigation-link woocommerce-MyAccount-navigation-link--customer-logout'] a")).Click();
*/
        }

        [Test]
        public void TestCase2()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            ShopPage ShopPage = new ShopPage(driver);
            CartPage CartPage = new CartPage(driver);
            CheckoutPage CheckoutPage = new CheckoutPage(driver);
            OrderPage OrderPage = new OrderPage(driver);

            //Dismiss the 'Demo Store' bar that appears at the bottom
            MyAccountPage.DismissDemoWarning();

            //Login details for manually setup account
            //Email = magmortar@pmail.com
            //Password = octoberComic0n!?
            //Login to the account
            MyAccountPage.FullLogin("magmortar@pmail.com", "octoberComic0n!?");

            //Enter the shop
            MyAccountPage.ClickShopLink();

            //Add a clothing item to the cart
            ShopPage.AddBeltToCart();

            //View the cart
            ShopPage.ClickCartLink();

            //Proceed to the checkout
            CartPage.ClickCheckoutLink();

            //Complete billing details, use a valid postcode
            CheckoutPage.ClearFullBilling();
            CheckoutPage.EnterFullBilling();

            //Select 'check payments' as a payment method
            CheckoutPage.ClickCheckPayment();

            //Place the order
            CheckoutPage.ClickPlaceOrder();

            //Capture the order number and write it to the results
            WaitForElementPresent(driver, By.CssSelector("li[class='woocommerce-order-overview__order order'] strong"));
            string OrderNumber = CheckoutPage.GetOrderNumber();

            //Naviagte to 'My Account> Orders' and check the same order shows
            CheckoutPage.ClickMyAccountLink();
            MyAccountPage.ClickOrderLink();
            Assert.That(OrderNumber, Is.EqualTo(OrderPage.GetTopOrderNum()), "Order numbers do not match");

            //Log out
            OrderPage.ClickMyAccountLink();
            MyAccountPage.ClickLogout();

            /*driver.FindElement(By.LinkText("Dismiss")).Click();//Dismisses the "demo store" warning which gets in the way
            //Login details for manually setup account
            //Email = magmortar@pmail.com
            //Password = octoberComic0n!?

            //Login to the account
            driver.FindElement(By.Id("username")).SendKeys("magmortar@pmail.com");
            driver.FindElement(By.Id("password")).SendKeys("octoberComic0n!?");
            driver.FindElement(By.Name("login")).Click(); //Possible to fail if button not on screen, scroll to find it?

            //Enter the shop
            driver.FindElement(By.LinkText("Shop")).Click();

            //Add a clothing item to the cart
            var AddToCart = driver.FindElement(By.CssSelector("[href='?add-to-cart=28']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(AddToCart).Perform();
            AddToCart.Click();

            //View the cart
            driver.FindElement(By.ClassName("cart-contents")).Click();

            //Proceed to the checkout
            driver.FindElement(By.CssSelector("li[id='menu-item-45'] a")).Click();

            //Complete billing details, use a valid postcode
            driver.FindElement(By.CssSelector("#billing_first_name")).Clear();
            driver.FindElement(By.CssSelector("#billing_last_name")).Clear();
            driver.FindElement(By.CssSelector("#billing_address_1")).Clear();
            driver.FindElement(By.CssSelector("#billing_city")).Clear();
            driver.FindElement(By.CssSelector("#billing_postcode")).Clear();
            driver.FindElement(By.CssSelector("#billing_phone")).Clear();

            driver.FindElement(By.CssSelector("#billing_first_name")).SendKeys("John");
            driver.FindElement(By.CssSelector("#billing_last_name")).SendKeys("Doe");
            driver.FindElement(By.CssSelector("#billing_address_1")).SendKeys("1 Astreetname");
            driver.FindElement(By.CssSelector("#billing_city")).SendKeys("Metropolis");
            driver.FindElement(By.CssSelector("#billing_postcode")).SendKeys("SA44 4NE");
            driver.FindElement(By.CssSelector("#billing_phone")).SendKeys("01234567890");
            //Select 'check payments' as a payment method
            driver.FindElement(By.CssSelector("label[for= 'payment_method_cheque']")).Click();

            //Place the order
            driver.FindElement(By.CssSelector("#place_order")).Click();
            Thread.Sleep(2000);
            //Capture the order number and write it to the results
            var OrderNum = driver.FindElement(By.CssSelector("li[class='woocommerce-order-overview__order order'] strong")).Text;

            //Naviagte to 'My Account> Orders' and check the same order shows
            driver.FindElement(By.CssSelector("li[id='menu-item-46'] a")).Click();
            driver.FindElement(By.CssSelector("li[class='woocommerce-MyAccount-navigation-link woocommerce-MyAccount-navigation-link--orders'] a")).Click();
            var AccOrderNum = driver.FindElement(By.CssSelector("td[class='woocommerce-orders-table__cell woocommerce-orders-table__cell-order-number'] a")).Text;
            AccOrderNum = AccOrderNum.Remove(0, 1);
            Assert.That(AccOrderNum, Is.EqualTo(OrderNum), "Order numbers do not match");
            //Log out
            driver.FindElement(By.CssSelector("li[id='menu-item-46'] a")).Click();
            driver.FindElement(By.CssSelector("li[class='woocommerce-MyAccount-navigation-link woocommerce-MyAccount-navigation-link--customer-logout'] a")).Click();
*/
        }
    }
}