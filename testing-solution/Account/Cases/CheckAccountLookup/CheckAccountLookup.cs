using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumExample.Cases
{
    public class AccountLookup
    {
        public static void Execute(dynamic site, IWebDriver driver)
        {
            //click search

            try
            {
                driver.Navigate().GoToUrl(site.Url + "MyAccount/MyService/pages/MoveService.aspx");

                var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                // find and click Account Lookup Tool link
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), 'Account Lookup Tool')]")));
                driver.FindElement(By.XPath("//a[contains(text(), 'Account Lookup Tool')]")).Click();
                webDriverWait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                //Wait until modal is visible
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("Ssn")));

                //enter ssn and phone number
                DriverHelpers.SendKeys(driver, By.Id("Ssn"), site.Ssn);

                DriverHelpers.SendKeys(driver, By.Id("PhoneNumber"), site.Phone);
                var searchButtonLocator = By.XPath("//button[contains(text(), 'Search')]");
                webDriverWait.Until(DriverHelpers.ElementIsClickable(searchButtonLocator));
                try
                {
                    driver.FindElement(searchButtonLocator).Click();
                    DriverHelpers.SendKeys(driver, By.Id("Ssn"), site.Ssn);
                    driver.FindElement(searchButtonLocator).Click();
                    // wait until account number is visible
                    webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'" + site.AccountNumber + "')]")));
                }
                catch
                {
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
               
            
        }
    }
}
