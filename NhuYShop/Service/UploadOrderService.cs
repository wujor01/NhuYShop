using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhuYShop.Service
{
    public class UploadOrderService
    {
        public static JObject GetRestAPI(string link)
        {
            var client = new RestClient(link);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", Common.Common.TokenGHTK);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);
            return json;
        }
        public static JObject PostUpOrderRestAPI(string link, Object obj)
        {
            var client = new RestClient(link);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Token", Common.Common.TokenGoogleSheet);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(obj);
            IRestResponse response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);
            return json;
        }
    }
}
