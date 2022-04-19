using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ScrapperSelenium
{
    class Program
    {
        private static IWebDriver driver;

        static void Main(string[] args)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://trading.bitfinex.com/t/CLO:USD?type=exchange");

            var buy = FindElements(By.ClassName("book__row"));
            var sell = FindElements(By.ClassName("book__row--reversed"));

            foreach (var collection in buy) Console.WriteLine("Buy " + collection.Text);
            foreach (var collection in sell) Console.WriteLine("Sell " + collection.Text);
        }

        static IReadOnlyCollection<IWebElement> FindElements(By by)
        {
            Stopwatch w = Stopwatch.StartNew();

            while (w.ElapsedMilliseconds < 5 * 1000)
            {
                var elements = driver.FindElements(by);
                if (elements.Count > 0) return elements;
                Thread.Sleep(10);
            }
            return null;
        }
    }
}