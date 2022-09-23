using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace voyado_search_test_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        Int64 totalResults = 0;

        //Google
        string googleCustomSearchId = "76e758613b929406e";
        string apiKey = "AIzaSyC9Q8jZYaOn8Yjsj8JjaC2ofpFLCtr9WpM";


        //Bing
        string bingCustomSearchId = "135bc5e2-6412-4149-ae9b-7d2859feef93&mkt=sv-SE";
        string bingAccessKey = "69986a31efd147d58de1d2b2a69e7fa5";

        [Route("getResultCount")]
        [HttpGet] 
        public Int64 getResultCount(string searchParam)
        {
            string[] words = searchParam.Split(' ');
            foreach (var word in words)
            {
                GoogleWebSearch(word);
                BingWebSearch(word);
            }

            return totalResults;
        }


        /// <summary>
        /// Makes a request to the Google Search API and returns the total searches per word
        /// </summary>
        private Int64 GoogleWebSearch(string searchQuery)
        {
            string uriBase = "https://www.googleapis.com/customsearch/v1?key=" + apiKey + "&cx=" + googleCustomSearchId + "&q=" + searchQuery;
            var request = WebRequest.Create(uriBase);
            request.UseDefaultCredentials = true;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);
            totalResults += (Int64)jsonData.SelectToken("searchInformation.totalResults");
            return totalResults;
        }

        /// <summary>
        /// Makes a request to the Bing Web Search API and returns the total searches per word
        /// </summary>
        private Int64 BingWebSearch(string searchQuery)
        {
            string uriBase = "https://api.bing.microsoft.com/v7.0/custom/search?q=" + searchQuery + "&customconfig="+ bingCustomSearchId;
            
            // Construct the search request URI.
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(searchQuery);

            // Perform request and get a response.
            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = bingAccessKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            dynamic jsonData = JsonConvert.DeserializeObject(json);
            totalResults += (Int64)jsonData.SelectToken("webPages.totalEstimatedMatches");

            return totalResults;
        }
    }
}
