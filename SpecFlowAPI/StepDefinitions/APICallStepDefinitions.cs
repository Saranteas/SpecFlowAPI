using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Security.Policy;
using TechTalk.SpecFlow;

namespace SpecFlowAPI.StepDefinitions
{
    [Binding]
    public class APICallStepDefinitions
     
    {
        public static string message;
        public static string URL;

        [Given(@"we pick a random API")]
        public void GivenWePickARandomAPI()
        {
            Random random = new Random();
            string[] URLarray = new string[] { "https://www.boredapi.com/api/activity", "https://catfact.ninja/fact" };
            URL = URLarray[random.Next(0, 2)];
        }

        [When(@"the API is called")]
        public void WhenTheAPIIsCalled()
        {
            Task.Run(async () => await GetCatFact()).Wait();
        }

        [Then(@"the API responds with a fact")]
        public void ThenTheAPIRespondsWithAFact()
        {
            Console.Write(message);
        } 

         private static async Task GetCatFact()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                     message = await httpClient.GetStringAsync(URL);                  
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }
    }
}
