// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoadAlertUWP.MLStudioPrediction.Model;
using RoadAlertUWP.Models;

namespace RoadAlertUWP.MLStudioPrediction.Prediction
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public  class AzureMlPrediction
    {
        private static AzureMlFatality fatl;
        public  AzureMlPrediction(AzureMlFatality fatality)
        {
            fatl = fatality;
        }

       public  async Task<string> InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"speed", "airbag", "seatbelt", "frontal", "sex", "age", "vehYear", "Deploy"},
                                Values = new string[,] {  { $"{fatl.Speed}", $"{fatl.Airbag}", $"{fatl.Seatbelt}", $"{fatl.Frontal}", $"{fatl.Gender}", $"{fatl.Age}", $"{fatl.Year}", $"{fatl.Deploy}" }  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "UCn/WXgGq5ZE9QkL6XI6LllMGhpdeJJajoi2g5Ih6uMHtb22nk5lqAR8+j6kHoh9nTsMewOswWDN8a+N1y9ZPw=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/abb1a05cef2d49f0814fca947699fe6f/services/c43c55db182744009af60dc8dbe8e7cb/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                    var srt = JsonConvert.DeserializeObject<Rootobject>(result);
                    
                    return srt.Results.output1.value.Values[0][8].ToString();
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return responseContent;
                }
            }
        }
    }
}
