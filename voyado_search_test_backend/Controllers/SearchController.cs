using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace voyado_search_test_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        Int64 totalResults = 0;

        //Google credentials
        string googleCustomSearchId = "76e758613b929406e";
        string googleApiKey = "AIzaSyC9Q8jZYaOn8Yjsj8JjaC2ofpFLCtr9WpM";


        //Bing credentials
        string bingCustomSearchId = "135bc5e2-6412-4149-ae9b-7d2859feef93&mkt=sv-SE";
        string bingAccessKey = "69986a31efd147d58de1d2b2a69e7fa5";

        [Route("getResultCount")]
        [HttpGet] 
        public async Task<Int64> getResultCount(string searchParam)
        {
            string[] words = searchParam.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                totalResults += await GoogleWebSearch(words[i]);
                totalResults += await BingWebSearch(words[i]);

                //Adds a 1 second delay after each Bing search (unless its the last search) since free plan only allows 1 each second.
                if (i < words.Length -1)
                {
                    Thread.Sleep(1000);
                }
            }

            return totalResults;
        }


        /// <summary>
        /// Makes a request to the Google Search API and returns the total searches for that word
        /// </summary>
        private async Task<Int64> GoogleWebSearch(string searchQuery)
        {
            string uriBase = "https://www.googleapis.com/customsearch/v1?key=" + googleApiKey + "&cx=" + googleCustomSearchId + "&q=" + searchQuery;

            var request = WebRequest.Create(uriBase);
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(json);

            var searchResults = (Int64)jsonData.SelectToken("searchInformation.totalResults");
            return searchResults;
        }

        /// <summary>
        /// Makes a request to the Bing Web Search API and returns the total for that word
        /// </summary>
        private async Task<Int64> BingWebSearch(string searchQuery)
        {
            string uriBase = "https://api.bing.microsoft.com/v7.0/custom/search?q=" + searchQuery + "&customconfig=" + bingCustomSearchId;
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(searchQuery);

            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = bingAccessKey;

            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(json);

            var searchResults = (Int64)jsonData.SelectToken("webPages.totalEstimatedMatches");
            return searchResults;
        }
    }
}
