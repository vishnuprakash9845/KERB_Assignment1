using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;

namespace Kerb_Assignment
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MapNavigation()
        {
            string source = "Mantri";
            string destination = "Mysuru Junction";

            AppiumOptions option = new AppiumOptions();
            option.AddAdditionalCapability("platformName", "android");
            option.AddAdditionalCapability("deviceName", "mototrola one");
            option.AddAdditionalCapability("appActivity", "com.google.android.maps.MapsActivity");
            option.AddAdditionalCapability("appPackage", "com.google.android.apps.maps");
            option.AddAdditionalCapability("noReset", true);

            AndroidDriver<IWebElement> driver = new AndroidDriver<IWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"), option);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //Search
            driver.FindElementByXPath("//android.widget.TextView[@text='Search here']").Click();
            driver.FindElementByXPath("//android.widget.EditText[@text='Search here']").SendKeys(source);

            //Select 2nd option
            driver.FindElementByXPath($"//android.widget.TextView[contains(@text,'{source}')]").Click();

            //Direction
            driver.FindElementByXPath("//android.view.View[@text='Directions']").Click();

            //Start Location
            driver.FindElementByXPath("//android.widget.TextView[@text='Your location']").Click();

            //Enter Destination
            driver.FindElementByXPath("//android.widget.EditText[@text='Choose start location']").SendKeys(destination);

            //Select from Auto Suggestion
            driver.FindElementByXPath($"//android.widget.TextView[contains(@text,'{destination}')]").Click();

            //Reverse
            driver.FindElementByXPath("//android.widget.FrameLayout[@content-desc='Swap start and destination']").Click();

            Thread.Sleep(4000);

            //Prin the Disyance and Time
            IWebElement hours = driver.FindElementByXPath("//android.widget.FrameLayout[@resource-id='com.google.android.apps.maps:id/trip_card_primary_detail']//android.widget.TextView");
            Console.WriteLine($"Hours : {hours.Text}");

            IWebElement distance = driver.FindElementByXPath("//android.widget.FrameLayout[@resource-id='com.google.android.apps.maps:id/trip_card_trip_summary']//android.widget.FrameLayout//android.view.ViewGroup//android.widget.TextView");
            Console.WriteLine($"Distance : {distance.Text}");

            driver.Quit();

        }
    }
}
