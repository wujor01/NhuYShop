using Firebase.Database;
using Firebase.Database.Query;
using NhuYShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhuYShop.DAO
{
    public class OrderDAO
    {
        FirebaseClient firebase = new FirebaseClient("https://quickstart-1604847633747-default-rtdb.firebaseio.com/");

        public async Task<List<OrderModel>> GetAllOrder()
        {
            return (await firebase
              .Child("OrderModel")
              .OnceAsync<OrderModel>()).Select(item => new OrderModel
              {
                  ID = item.Object.ID,
                  CREATEUSER = item.Object.CREATEUSER,
                  customer_address = item.Object.customer_address,
                  customer_ward = item.Object.customer_ward,
                  customer_distrist = item.Object.customer_distrist,
                  customer_province = item.Object.customer_province,
                  customer_tel = item.Object.customer_tel,
                  customer_street = item.Object.customer_street,
                  customer_name = item.Object.customer_name,
                  commission = item.Object.commission,
                  UPDATEUSER = item.Object.UPDATEUSER,
                  detail = item.Object.detail,
                  value = item.Object.value,
                  is_freeship = item.Object.is_freeship,
                  is_completed = item.Object.is_completed,
                  CREATEDATE = item.Object.CREATEDATE,
                  UPDATEDATE = item.Object.UPDATEDATE,
                  customer_distrist_code = item.Object.customer_distrist_code,
                  customer_province_code = item.Object.customer_province_code,
                  customer_ward_code = item.Object.customer_ward_code
              }).OrderByDescending(x=>x.CREATEDATE).ToList();
        }
        public async Task<string> AddOrder(OrderModel order)
        {
            try
            {
                await firebase.Child("OrderModel").PostAsync(order);
                return "success";
            }
            catch (Exception)
            {
                return "fail";
            }
        }
        public async Task<OrderModel> GetOrder(string ID)
        {
            var allOrders = await GetAllOrder();
            await firebase
              .Child("OrderModel")
              .OnceAsync<OrderModel>();
            return allOrders.Where(a => a.ID == ID).FirstOrDefault();
        }
        public async Task UpdateOrder(OrderModel order)
        {
            var toUpdateOrder = (await firebase
              .Child("OrderModel")
              .OnceAsync<OrderModel>()).Where(a => a.Object.ID == order.ID).FirstOrDefault();

            await firebase
              .Child("OrderModel")
              .Child(toUpdateOrder.Key)
              .PutAsync(order);
        }
    }
}
