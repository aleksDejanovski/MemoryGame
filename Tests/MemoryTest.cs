using MemoryGame.Driver;
using MemoryGame.Pages;
using OpenQA.Selenium;
using Xunit.Abstractions;

namespace MemoryGame.Tests
{
    public class MemoryTest : IDisposable
    {
        public IWebDriver Driver { get; private set; }
        public MemoryPage MemoryPage { get; private set; }
        public DateTime StartTime { get; }
        public ITestOutputHelper Output { get; private set; }

        public MemoryTest(ITestOutputHelper output)
        {
            // Setup for the test
            Output = output;
            Driver = new DriverManager().Create();
            MemoryPage = new MemoryPage { Driver = Driver };
            Driver.Navigate().GoToUrl("https://codepen.io/eliortabeka/pen/WwzEEg");
            StartTime = DateTime.Now;
        }

        public void Dispose()
        {

            Output.WriteLine(">>> RESULTS <<<");
            Output.WriteLine($">>> Solved the game in {Math.Round((DateTime.Now - StartTime).TotalSeconds, 2)} seconds.");
            Output.WriteLine($">>> {MemoryPage.GetWinText()}");
            Driver.Quit();
        }

        [Fact]
        public void SolveTheGame()
        {
            MemoryPage.NavigateToIframe();
            var allValues = new List<string>();
            for (int i = 0; i < 16; i++)
            {
                allValues.Add(MemoryPage.GetImage(i));
            }

            foreach (string value in allValues.Distinct())
            {
                MemoryPage.IntelligentClickTile(value);
                MemoryPage.IntelligentClickTile(value);
            }
        }

        [Fact]
        public void Solution()
        {
            MemoryPage.NavigateToIframe();
            MemoryPage.ClickFirstTimeWait(Constants.Constants.Cube);
            MemoryPage.ClickSecondTimeWait(Constants.Constants.Cube);

            List<string> Consts = new List<string> { Constants.Constants.Anchor, Constants.Constants.Bolt, Constants.Constants.Bomb, Constants.Constants.Plane, Constants.Constants.Leaf, Constants.Constants.Diamond, Constants.Constants.Bicycle };
            for (int i = 0; i <= 6; i++)
            {
                MemoryPage.ClickFirstTimeAfterMatch(Consts[i]);
                MemoryPage.ClickSecondTimeWait(Consts[i]);
            }
        }
    }
}
