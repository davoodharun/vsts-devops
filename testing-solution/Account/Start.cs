using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using System.Collections.Generic;
using SeleniumExample.Cases;
using System.IO;
using System.Reflection;

namespace SeleniumExample
{
    [TestClass]
    public class Account
    {
        private TestContext testContextInstance;
        // Test Account Lookup
        [TestMethod]
        [TestCategory("Account")]
        public void CheckAccountLookup()
        {
            List<dynamic> sites = ReadCSV.GetSites("Cases/CheckAccountLookup/CheckAccountLookupTestData.csv");
            IWebDriver driver = StartUp();
            try
            {
                for (int j = 0; j < sites.Count; j++)
                {
                    try
                    {
                        AccountLookup.Execute(sites[j], driver);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        TestContext.WriteLine("CheckAccountLookup");
                        foreach (KeyValuePair<string, object> entry in sites[j])
                        {
                            TestContext.WriteLine(entry.Key + ": " + entry.Value);
                        }
                    }
                }
            }
            catch(System.Exception ex)
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
