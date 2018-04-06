using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Cases
{
    public class BasicOutages
    {
        public static void Execute(dynamic site, IWebDriver driver)
        {
            //await Task.Delay(1000);
            var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            // TEST SUCCESSFUL LOGIN
            // enter username
           
            try
            {
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("excNavLeft")));
                driver.FindElements(By.ClassName("exc-nav-left-div-inactive"))[0].FindElement(By.ClassName("exc-nav-expand")).Click();
                webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), 'Pay with Bank Account')]")));
                driver.FindElement(By.XPath("//a[contains(text(), 'Pay with Bank Account')]")).Click();
                webDriverWait.Until(ExpectedConditions.UrlMatches(site.Url + "MyAccount/MyBillUsage/PayMyBill/Pages/secure/PayByECheck.aspx"));
                Assert.AreEqual(site.Url + "MyAccount/MyBillUsage/PayMyBill/Pages/secure/PayByECheck.aspx", driver.Url);
            }
            catch {
            }
            

        }
    }
}
