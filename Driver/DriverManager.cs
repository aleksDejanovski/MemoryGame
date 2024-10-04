using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Driver
{
    public class DriverManager
    {
        public ChromeOptions Options { get; }

        public DriverManager()
        {
            Options = new ChromeOptions();
            Options.AddArgument("--incognito");
            Options.AddArgument("--disable-search-engine-choice-screen");
            Options.AddArgument("--start-maximized");
        }

        /// <summary>
        /// Creates a new IWebDriver instance.
        /// </summary>
        /// <returns></returns>
        public IWebDriver Create()
        {
            return new ChromeDriver(Options);
        }
    }
}
