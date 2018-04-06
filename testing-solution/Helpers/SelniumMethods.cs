using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SeleniumExample
{
    public static class SeleniumExtensions
    {
        public static void SendKeysAsArray(this IWebElement c, string value)
        {
            string[] chars = value.Split();
            foreach (string character in chars)
            {
                c.SendKeys(character);
            }
        }
    }

}
