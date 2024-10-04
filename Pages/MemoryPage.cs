using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Pages
{
    public class MemoryPage
    {

        private readonly By tableBoxes = By.CssSelector(".deck li i");
        private readonly By lblResult = By.CssSelector(".swal2-content");
        private readonly By lblRandomElement = By.CssSelector(".fa-anchor");
        private readonly By lblMatchElements = By.CssSelector(".card.match");
        private readonly By lblOpenFirstElement = By.CssSelector(".open");


        public IWebDriver? Driver { get; set; }
        Actions Action => new Actions(Driver);
        WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(10));



        public string GetWinText()
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(lblResult));
            return Driver.FindElement(lblResult).Text;
        }

        //method to navigate to the iframe
        public void NavigateToIframe() => Driver.SwitchTo().Frame("result");

        public void ClickTile(string classElement, int index)
        {
            Thread.Sleep(1500);
            var elementsInTable = Driver.FindElements(tableBoxes).ToList();
            var element = elementsInTable.Where(c => c.GetAttribute("class") == $"{classElement}").ToList()[index];
            Action.MoveToElement(element).Click().Perform();

        }

        public string GetImage(int index)
        {
            return Driver.FindElements(By.CssSelector("li.card i"))[index].GetAttribute("class");
        }

        public void ClickFirstTimeWait(string elementValue)
        {
            Wait.Until(Driver => Driver.FindElement(lblRandomElement).Enabled);
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(lblRandomElement));
            var elementsInTable = Driver.FindElements(tableBoxes).ToList();
            var element = elementsInTable.Where(c => c.GetAttribute("class") == $"{elementValue}").FirstOrDefault();
            Action.MoveToElement(element).Click().Perform();
        }

        public void ClickSecondTimeWait(string elementValue)
        {
            Wait.Until(Driver => Driver.FindElement(lblOpenFirstElement).Enabled);
            var elementsInTable = Driver.FindElements(tableBoxes).ToList();
            var element = elementsInTable.Where(c => c.GetAttribute("class") == $"{elementValue}").LastOrDefault();
            Action.MoveToElement(element).Click().Perform();
            Wait.Until(Driver => Driver.FindElement(lblMatchElements).Displayed);
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(lblOpenFirstElement));
        }

        public void ClickFirstTimeAfterMatch(string elementValue)
        {
            Wait.Until(Driver => Driver.FindElement(lblMatchElements).Displayed);
            var elementsInTable = Driver.FindElements(tableBoxes).ToList();
            var element = elementsInTable.Where(c => c.GetAttribute("class") == $"{elementValue}").FirstOrDefault();
            Action.MoveToElement(element).Click().Perform();
        }

        public void IntelligentClickTile(string className)
        {
            // Wait until there are no animations
            Wait.Until(Driver => Driver.FindElements(By.CssSelector("li.card.animated")).Count == 0);

            Wait.Until(Driver => Driver.FindElement(By.XPath($"//i[@class='{className}']//parent::li[@class='card']")).Displayed);

            Driver.FindElement(By.XPath($"//i[@class='{className}']//parent::li[@class='card']")).Click();



        }
    }
}
