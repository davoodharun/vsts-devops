using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using System.Collections.Generic;
using Payment.Cases;
using SeleniumExample.Cases;
using System.IO;
using System.Reflection;

namespace SeleniumExample
{
    [TestClass]
    public class Payment
    {
        private TestContext testContextInstance;

        [TestMethod]
        [TestCategory("Payment")]
        public void CheckPayWithBankAccount()
        {
            List<dynamic> sites = ReadCSV.GetSites("Cases/PayWithBankAccount/CheckPayWithBankAccountTestData.csv");
            IWebDriver driver = StartUp();
            try
            {

                for (int j = 0; j < sites.Count; j++)
                {
                    try
                    {
                        BasicLogin.Execute(sites[j], driver);
                        PayWithBankAccount.Execute(sites[j], driver);
                    }

                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        foreach (KeyValuePair<string, object> entry in sites[j])
                        {
                            TestContext.WriteLine(entry.Key + ": " + entry.Value);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                driver.Close();
                driver.Dispose();
            }
            
        }

        [TestMethod]
        [TestCategory("Payment")]
        public void CheckRecurringPayment()
        {
            List<dynamic> sites = ReadCSV.GetSites("Cases/RecurringPayment/CheckRecurringPaymentTestData.csv");
            IWebDriver driver = StartUp();
            try
            {

                for (int j = 0; j < sites.Count; j++)
                {
                    try
                    {
                        BasicLogin.Execute(sites[j], driver);
                        RecurringPayment.Execute(sites[j], driver);
                    }

                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        foreach (KeyValuePair<string, object> entry in sites[j])
                        {
                            TestContext.WriteLine(entry.Key + ": " + entry.Value);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                driver.Close();
                driver.Dispose();
            }

        }

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // Start Up
        public static IWebDriver StartUp()
        {
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            return driver;
        }
    }
}
