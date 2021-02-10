using Firebase.Database;
using Firebase.Database.Query;
using NhuYShop.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
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
                  customer_ward_code = item.Object.customer_ward_code,
                  delivery_option_id = item.Object.delivery_option_id,
                  feeship = item.Object.feeship,
                  pickaddress_id = item.Object.pickaddress_id,
                  pickaddress_name = item.Object.pickaddress_name,
                  weight = item.Object.weight,
                  orther_type = (item.Object.orther_type == null ? "" : item.Object.orther_type)
              }).OrderByDescending(x => x.CREATEDATE).ToList();
        }
        public async Task<List<OrderModel>> GetAllOrder(DateTime datepick)
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
                  customer_ward_code = item.Object.customer_ward_code,
                  delivery_option_id = item.Object.delivery_option_id,
                  feeship = item.Object.feeship,
                  pickaddress_id = item.Object.pickaddress_id,
                  pickaddress_name = item.Object.pickaddress_name,
                  weight = item.Object.weight,
                  orther_type = (item.Object.orther_type == null ? "" : item.Object.orther_type)
              }).Where(x=>x.CREATEDATE.Month == datepick.Month && x.CREATEDATE.Year == datepick.Year).OrderByDescending(x=>x.CREATEDATE).ToList();
        }
        public async Task<int> GetCountOrderCTV(string ID)
        {
            return (await firebase
              .Child("OrderModel")
              .OnceAsync<OrderModel>()).Select(item => new OrderModel
              {
                  ID = item.Object.ID,
              }).Where(x=>x.ID == ID).Count();
        }

        public async Task<int> GetValueOrderCTV(string ID)
        {

            var a = (await firebase
              .Child("OrderModel")
              .OnceAsync<OrderModel>()).Select(item => new OrderModel
              {
                  value = item.Object.value,
              }).Where(x => x.ID == ID).ToList();
            
            return a.Select(x=>x.value).Sum();
        }

        public async Task<List<OrderModel>> GetAllOrder(bool is_completed)
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
                  customer_ward_code = item.Object.customer_ward_code,
                  delivery_option_id = item.Object.delivery_option_id,
                  feeship = item.Object.feeship,
                  pickaddress_id = item.Object.pickaddress_id,
                  pickaddress_name = item.Object.pickaddress_name,
                  weight = item.Object.weight
              }).OrderByDescending(x => x.CREATEDATE).Where(x=>x.is_completed == is_completed).ToList();
        }
        public async Task<List<OrderModel>> GetAllOrderDetail(DateTime month)
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
                  customer_ward_code = item.Object.customer_ward_code,
                  delivery_option_id = item.Object.delivery_option_id,
                  feeship = item.Object.feeship,
                  pickaddress_id = item.Object.pickaddress_id,
                  pickaddress_name = item.Object.pickaddress_name,
                  weight = item.Object.weight
              }).OrderByDescending(x => x.CREATEDATE).Where(x => x.CREATEDATE.Month == month.Month && x.CREATEDATE.Year == month.Year).ToList();
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
        public async Task<string> UpdateOrder(OrderModel order)
        {
            try
            {
                var toUpdateOrder = (await firebase
                .Child("OrderModel")
                .OnceAsync<OrderModel>()).Where(a => a.Object.ID == order.ID).FirstOrDefault();

                await firebase
                  .Child("OrderModel")
                  .Child(toUpdateOrder.Key)
                  .PutAsync(order);
                return "success";
            }
            catch (Exception)
            {
                return "failed";
                throw;
            }
        }

        public async Task<string> UpdateOrders(List<OrderModel> orders)
        {
            try
            {
                var toUpdateOrder = (await firebase
                .Child("OrderModel")
                .OnceAsync<List<OrderModel>>()).Where(a => a.Object == orders).FirstOrDefault();

                await firebase
                  .Child("OrderModel")
                  .Child(toUpdateOrder.Key)
                  .PutAsync(orders);
                return "success";
            }
            catch (Exception)
            {
                return "failed";
                throw;
            }
        }


        public async Task<string> DeleteOrder(string ID)
        {
            try
            {
                var toDeleteUser = (await firebase
                .Child("OrderModel")
                .OnceAsync<OrderModel>()).Where(a => a.Object.ID == ID).FirstOrDefault();
                await firebase.Child("OrderModel").Child(toDeleteUser.Key).DeleteAsync();
                return "success";
            }
            catch (Exception)
            {
                return "failed";
            }

        }

        public void OrderExportExcelAsync(List<OrderModel> orders,string filename, DateTime month, bool isMobie)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table  
            //  
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = "Ngày tạo";
            workSheet.Cells[1, 2].Value = "CTV";
            workSheet.Cells[1, 3].Value = "ID";
            workSheet.Cells[1, 4].Value = "Đ/C khách hàng";
            workSheet.Cells[1, 5].Value = "Số điện thoại";
            workSheet.Cells[1, 6].Value = "Chi tiết đơn hàng";
            workSheet.Cells[1, 7].Value = "Phí ship";
            workSheet.Cells[1, 8].Value = "Đã đăng?";
            workSheet.Cells[1, 9].Value = "Freeship?";
            workSheet.Cells[1, 10].Value = "Giá trị đơn hàng";
            workSheet.Cells[1, 11].Value = "Loại khác";
            workSheet.Cells[1, 12].Value = "Hoa hồng";
            //Body of table  
            //  
            int recordIndex = 2;
            foreach (var item in orders)
            {
                workSheet.Cells[recordIndex, 1].Value = item.CREATEDATE.ToString("dd/MM/yyyy");
                workSheet.Cells[recordIndex, 2].Value = item.CREATEUSER;
                workSheet.Cells[recordIndex, 3].Value = item.ID;
                workSheet.Cells[recordIndex, 4].Value = item.customer_address;
                workSheet.Cells[recordIndex, 5].Value = item.customer_tel;
                workSheet.Cells[recordIndex, 6].Value = item.detail;
                workSheet.Cells[recordIndex, 7].Value = item.feeship;
                workSheet.Cells[recordIndex, 8].Value = item.is_completed;
                workSheet.Cells[recordIndex, 9].Value = item.is_freeship;
                workSheet.Cells[recordIndex, 10].Value = item.value;
                workSheet.Cells[recordIndex, 11].Value = item.orther_type;
                workSheet.Cells[recordIndex, 12].Value = item.commission;
                recordIndex++;
            }
            if (!isMobie)
            {
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).Width = 20;
                workSheet.Column(4).Style.WrapText = true;
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).Width = 50;
                workSheet.Column(6).Style.WrapText = true;
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();

                //pc
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                File.WriteAllBytes(path + "/" + filename, excel.GetAsByteArray());
            }
            else
            {
                string documentsPath = "/storage/emulated/0/Download";
                // If directory does not exist, create it. 
                if (!Directory.Exists(documentsPath))
                {
                    Directory.CreateDirectory(documentsPath);
                }
                //mobile
                string localPath = Path.Combine(documentsPath, filename);
                File.WriteAllBytes(localPath, excel.GetAsByteArray());
            }
        }
    }
}
