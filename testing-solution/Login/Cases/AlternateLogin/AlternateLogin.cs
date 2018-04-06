using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace SeleniumExample.Cases
{
    public class AlternateLogin
    {

        public static void Execute(dynamic site, IWebDriver driver)
        {
            


            try
            {
                // Navigate to login page
                driver.Navigate().GoToUrl(site.Url + "pages/login.aspx");
                var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                webDriverWait.Until(ExpectedConditions.UrlMatches(site.Url + "pages/login.aspx"));
                webDriverWait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                // check title
                Assert.AreEqual(site.Title, driver.Title);

                // TEST SUCCESSFUL LOGIN

                // click top sign in button
                var signinButtonLocator = By.XPath("//button[contains(@class, 'btn btn-accent exc-sign-in-btn')]");
                webDriverWait.Until(DriverHelpers.ElementIsClickable(signinButtonLocator));
                driver.FindElement(signinButtonLocator).Click();

                // wait for elements to become visible (there are 2 sets of 2 elements that share the same id, so wait for one of them to become visible)
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("Username")));
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password")));

                var usernameElements = driver.FindElements(By.Id("Username"));
                var passwordElements = driver.FindElements(By.Id("Password"));
                // enter username
                do
                {
                    usernameElements = driver.FindElements(By.Id("Username"));
                    passwordElements = driver.FindElements(By.Id("Password"));
                }
                while (usernameElements.Count < 2 && passwordElements.Count < 2);

                driver.FindElements(By.Id("Username"))[1].SendKeys(site.User);

                //enter password
                driver.FindElements(By.Id("Password"))[1].SendKeys(site.Password);

                //click sign in button depending on which one is available
                driver.FindElement(By.XPath("//button[contains(@class, 'btn btn btn-primary exc-corner-btn')]")).Click();
                webDriverWait.Until(ExpectedConditions.UrlMatches(site.Url + "MyAccount/MyBillUsage/pages/secure/MyBillUsage.aspx"));
                Assert.AreEqual(site.Url + "MyAccount/MyBillUsage/pages/secure/MyBillUsage.aspx", driver.Url);
            }
            catch {

            }
        }

    }
}
