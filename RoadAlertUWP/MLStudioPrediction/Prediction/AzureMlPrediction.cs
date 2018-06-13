using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RoadAlertUWP.MLStudioPrediction.Prediction
{
    public class AzureMlPrediction
    {
        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }

        public static async Task<string> InvokeRequestResponseService()
        {
            string responseContent;
            using (var client=new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>()
                    {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[]
                                {
                                    "speed", "airbag", "seatbelt", "frontal", "sex", "age", "vehYear", "Deploy"
                                },
                                Values = new string[,]
                                {
                                    {
                                        "value", "value", "value", "0", "value", "0", "value", "0"
                                    },
                                    {
                                        "value", "value", "value", "0", "value", "0", "value", "0"
                                    },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "UCn/WXgGq5ZE9QkL6XI6LllMGhpdeJJajoi2g5Ih6uMHtb22nk5lqAR8+j6kHoh9nTsMewOswWDN8a+N1y9ZPw=="; 
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
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    responseContent = await response.Content.ReadAsStringAsync();
                }
            }
            return responseContent;
        }

    }
}
