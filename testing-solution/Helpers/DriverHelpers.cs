using OpenQA.Selenium;
using System;
namespace SeleniumExample
{
    public class DriverHelpers
    {
        public static Func<IWebDriver, IWebElement> ElementIsClickable(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator);
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }

        public static void SendKeys(IWebDriver driver, By locator, string value)
        {
            string[] str = value.Split();
            foreach (string character in str)
            {
                driver.FindElement(locator).SendKeys(character);
            }
        }
    }

}
