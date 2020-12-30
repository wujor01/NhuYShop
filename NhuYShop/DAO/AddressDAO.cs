using Newtonsoft.Json.Linq;
using NhuYShop.Model.Address;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NhuYShop.DAO
{
    public class AddressDAO
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
        public Task<List<PickAddress>> GetPickAddress()
        {
            var json = GetRestAPI("https://services.ghtklab.com/services/shipment/list_pick_add");
            List<PickAddress> pickAddresses = new List<PickAddress>();
            foreach (var item in json["data"])
            {
                pickAddresses.Add(item.ToObject<PickAddress>());
            }
            return Task.FromResult(pickAddresses);
        }
        public async Task<string> GetJsonFile(string link, string path)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(link);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return await Task.FromResult(responseBody);

            //var currentDirectory = Directory.GetCurrentDirectory();
            //var downloadPath = Path.Combine(currentDirectory, path);

            //if (!Directory.Exists(downloadPath))
            //{
            //    using (var client = new WebClient())
            //    {
            //        client.DownloadFile(link, path + ".json");
            //        Directory.CreateDirectory(downloadPath);
            //    }
            //}

            //StreamReader r = new StreamReader(path + ".json");
            //string responseBody = r.ReadToEnd();
            //return await Task.FromResult(responseBody);
        }
        public async Task<List<Province>> GetProvince(string responseBody)
        {
            var js = JObject.Parse(responseBody);
            IList<JToken> values = js.Properties().Select(x => x.First).ToList();
            List<Province> provinces = new List<Province>();
            foreach (var item in values)
            {
                Province pro = new Province();
                pro = item.ToObject<Province>();
                provinces.Add(pro);
            }
            return await Task.FromResult(provinces);
        }
        public async Task<List<Distrist>> GetDistrist(string responseBody, string parent_code)
        {

            var js = JObject.Parse(responseBody);
            IList<JToken> values = js.Properties().Where(x => x.First.SelectToken("parent_code").ToString() == parent_code).Select(x => x.First).ToList();
            List<Distrist> distrists = new List<Distrist>();
            foreach (var item in values)
            {
                Distrist pro = new Distrist();
                pro = item.ToObject<Distrist>();
                distrists.Add(pro);
            }
            return await Task.FromResult(distrists);
        }
        public async Task<List<Ward>> GetWard(string responseBody, string parent_code)
        {
            var js = JObject.Parse(responseBody);
            IList<JToken> values = js.Properties().Where(x => x.First.SelectToken("parent_code").ToString() == parent_code).Select(x => x.First).ToList();
            List<Ward> wards = new List<Ward>();
            foreach (var item in values)
            {
                Ward pro = new Ward();
                pro = item.ToObject<Ward>();
                wards.Add(pro);
            }
            return await Task.FromResult(wards);
        }
    }
}
