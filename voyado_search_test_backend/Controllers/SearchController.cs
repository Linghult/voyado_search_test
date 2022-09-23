using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace voyado_search_test_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        //[HttpGet(Name = "GetSearchResultCount")]
        //public IEnumerable<SearchResultCount> Get(string searchParam)
        //{
        //    return Enumerable.Range(1, 5).Select(index => new SearchResultCount
        //    {
        //        TotalCount = getResultCount(searchParam)
        //    })
        //    .ToArray();
        //}
        [Route("getResultCount")]
        [HttpGet] 
        public Int64 getResultCount(string searchParam)
        {
            string cx = "76e758613b929406e";
            string apiKey = "AIzaSyC9Q8jZYaOn8Yjsj8JjaC2ofpFLCtr9WpM";

            //var request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=AIzaSyC9Q8jZYaOn8Yjsj8JjaC2ofpFLCtr9WpM&cx=76e758613b929406e&q=Jens%20Linghult");
            var request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + apiKey + "&cx=" + cx + "&q=" + searchParam);
            request.UseDefaultCredentials = true;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);
            Int64 totalResults = (int)jsonData.SelectToken("searchInformation.totalResults");
            return totalResults;
        }

    }
}
