//TestCase1()
//Should be NO driver.FindElement..., put all that in the POMs
//Clean up the 15% off assertion, create a helper method to clean up the data
//Fix screenshot and make it a helper method
//
//TestCase2()
//EnterFullBilling(), pass data instead of hard code, log test data used
//
//
//ShopPage Get rid of mouse move, generic method for adding to cart
//

using OpenQA.Selenium;
using static MiniProject.Utilities.HelperLib;
using MiniProject.POMs;


namespace MiniProject.Test_Cases
{
    [TestFixture]
    internal class Tests : MiniProject.Utilities.BaseClass
    {
        [Test]
        public void TestCase1()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";

            NavigationPage NavigationPage = new NavigationPage(driver);
            MyAccountPage MyAccountPage = new MyAccountPage(driver);

            //Login to the account
            MyAccountPage.CompleteLogin("magmortar@pmail.com", "octoberComic0n!?");
            Console.WriteLine("Completed login process");

            //Enter the shop via the Shop button
            NavigationPage.GoShop();
            Console.WriteLine("Navigated to Shop page");

            ShopPage ShopPage = new ShopPage(driver);

            //Add a belt to the cart, by clicking add to cart below the item on the shop page
            ShopPage.AddItemToCart("Belt");
            Console.WriteLine("Added belt to cart");

            //View the cart
            Thread.Sleep(1000);//Aware of issue where if the cart is empty and an item is added, it may take a second to load
            NavigationPage.GoCart();
            Console.WriteLine("Naviagted to cart page");

            CartPage CartPage = new CartPage(driver);

            //Apply a coupon "edgewords"
            CartPage.KeyIntoCoupon("edgewords");
            CartPage.ClickCouponButton();
            Console.WriteLine("Applied coupon");

            //Check that the coupon is valid for 15% off
            WaitForElementPresent(driver, By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)"));
            Assert.That((Decimal.Divide((Decimal.Parse(CartPage.GetSubtotal()) - Decimal.Parse(CartPage.GetDiscount())), Decimal.Parse(CartPage.GetSubtotal()))), Is.EqualTo(0.85), "Discount is not valid for 15% off");
            TakeScreenshot(driver, "TC1A1");
            Console.WriteLine("Assertion succeeded, coupon is valid for 15% off");
            Console.WriteLine("Cart total is " + CartPage.GetSubtotal() + " and the discount amount is " + CartPage.GetDiscount());

            //Check that the total after shipping is correct
            Assert.That(Decimal.Parse(CartPage.GetSubtotal().Remove(0, 1)) -
                Decimal.Parse(CartPage.GetDiscount().Remove(0, 1))
                + Decimal.Parse(CartPage.GetShipping().Remove(0, 1)),
                Is.EqualTo(Decimal.Parse(CartPage.GetTotal().Remove(0, 1))),
                "Error, total after shipping is incorrect");
            TakeScreenshot(driver, "TC1A2");
            Console.WriteLine("The total after shipping is correct");

            //Log out
            //CartPage.ClickMyAccountLink();
            NavigationPage.GoMyAccount();
            MyAccountPage.ClickLogout();
            Console.WriteLine("Logout process completed");

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
            NavigationPage NavigationPage = new NavigationPage(driver);

            //Login to the account
            MyAccountPage.CompleteLogin("magmortar@pmail.com", "octoberComic0n!?");
            Console.WriteLine("Completed login process");

            //Enter the shop
            NavigationPage.GoShop();
            Console.WriteLine("Navigated to shop");

            //Add a clothing item to the cart
            ShopPage.AddItemToCart("Belt");
            Console.WriteLine("Added 1 belt item to cart");

            //View the cart
            NavigationPage.GoCart();
            Console.WriteLine("Navigated to cart");

            //Proceed to the checkout
            NavigationPage.GoCheckout();
            Console.WriteLine("Navigated to checkout");

            //Complete billing details, use a valid postcode
            CheckoutPage.ClearFullBilling();
            CheckoutPage.EnterFullBilling();
            Console.WriteLine("Entered billing details");

            //Select 'check payments' as a payment method
            CheckoutPage.ClickCheckPayment();
            Console.WriteLine("Selected payment method");

            //Place the order
            CheckoutPage.ClickPlaceOrder();
            Console.WriteLine("Placed order");

            //Capture the order number and write it to the results
            WaitForElementPresent(driver, By.CssSelector("li[class='woocommerce-order-overview__order order'] strong"));
            string OrderNumber = CheckoutPage.GetOrderNumber();
            Console.WriteLine("Fetched order number. It is " + OrderNumber);

            //Naviagte to 'My Account> Orders' and check the same order shows
            NavigationPage.GoMyAccount();
            MyAccountPage.ClickOrderLink();
            Assert.That(OrderNumber, Is.EqualTo(OrderPage.GetTopOrderNum()), "Order numbers do not match");
            Console.WriteLine("Confirmed order numbers match");
            TakeScreenshot(driver, "TC2A1");

            //Log out
            NavigationPage.GoMyAccount();
            MyAccountPage.ClickLogout();
            Console.WriteLine("Logout process completed");

        }
        public void TakeScreenshot(IWebDriver driver, string ssName)
        {
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\" + ssName);
        }

        [Test]
        public void TestCase3()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
        }
    }
}