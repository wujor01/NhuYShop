using Newtonsoft.Json.Linq;
using NhuYShop.Model.Address;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public Task<List<Province>> GetProvince()
        {
            using (StreamReader r = File.OpenText("Data/json/tinh_tp.json"))
            {
                //Get values tinh_tp
                string json = r.ReadToEnd();
                var js = JObject.Parse(json);
                IList<JToken> values = js.Properties().Select(x => x.First).ToList();
                List<Province> provinces = new List<Province>();
                foreach (var item in values)
                {
                    Province pro = new Province();
                    pro = item.ToObject<Province>();
                    provinces.Add(pro);
                }
                return Task.FromResult(provinces);
            }
        }
        public Task<List<Distrist>> GetDistrist(string parent_code)
        {
            using (StreamReader r = File.OpenText("Data/json/quan_huyen.json"))
            {
                //Get values tinh_tp
                string json = r.ReadToEnd();
                var js = JObject.Parse(json);
                IList<JToken> values = js.Properties().Where(x => x.First.SelectToken("parent_code").ToString() == parent_code).Select(x => x.First).ToList();
                List<Distrist> distrists = new List<Distrist>();
                foreach (var item in values)
                {
                    Distrist pro = new Distrist();
                    pro = item.ToObject<Distrist>();
                    distrists.Add(pro);
                }
                return Task.FromResult(distrists);
            }
        }
        public Task<List<Ward>> GetWard(string parent_code)
        {
            using (StreamReader r = File.OpenText("Data/json/xa_phuong.json"))
            {
                //Get values tinh_tp
                string json = r.ReadToEnd();
                var js = JObject.Parse(json);
                IList<JToken> values = js.Properties().Where(x => x.First.SelectToken("parent_code").ToString() == parent_code).Select(x => x.First).ToList();
                List<Ward> wards = new List<Ward>();
                foreach (var item in values)
                {
                    Ward pro = new Ward();
                    pro = item.ToObject<Ward>();
                    wards.Add(pro);
                }
                return Task.FromResult(wards);
            }
        }
    }
}
