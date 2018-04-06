using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using SeleniumExample.Cases;
using System.IO;
using System.Reflection;

namespace SeleniumExample
{
    [TestClass]
    public class Admin
    {
        private TestContext testContextInstance;

        // Test Login
        [TestMethod]
        [TestCategory("Admin")]
        public void CheckAdmin()
        {
            List<dynamic> sites = ReadCSV.GetSites("Cases/BasicLogin/CheckAdminTestData.csv");
            int siteCount = sites.Count;
            IWebDriver driver = StartUp();
            
            try
            {
                for (int j = 0; j < siteCount; j++)
                {
                    try
                    {
                        BasicLogin.Execute(sites[j], driver);
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
        [TestCategory("Login")]
        public void CheckLoginAlternate()
        {
            List<dynamic> sites = ReadCSV.GetSites("Cases/BasicLogin/BasicLoginTestData.csv");
            int siteCount = sites.Count;
            IWebDriver driver = StartUp();

            try
            {
                for (int j = 0; j < siteCount; j++)
                {
                    try
                    {
                        BasicLogin.Execute(sites[j], driver);
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
