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
    public class ProductDAO
    {
        FirebaseClient firebase = new FirebaseClient("https://quickstart-1604847633747-default-rtdb.firebaseio.com/");

        public async Task<List<ProductModel>> GetAllProduct()
        {
            return (await firebase
              .Child("ProductModel")
              .OnceAsync<ProductModel>()).Select(item => new ProductModel
              {
                  PRODUCTID = item.Object.PRODUCTID,
                  PRODUCTNAME = item.Object.PRODUCTNAME,
                  VALUE1 = item.Object.VALUE1,
                  VALUE2 = item.Object.VALUE2,
                  VALUE3 = item.Object.VALUE3
              }).ToList();
        }
        public async Task<string> AddProduct(ProductModel product)
        {
            try
            {
                await firebase.Child("ProductModel").PostAsync(product);
                return "success";
            }
            catch (Exception)
            {
                return "fail";
            }
        }
        public async Task<ProductModel> GetProduct(int ID)
        {
            var allProducts = await GetAllProduct();
            await firebase
              .Child("ProductModel")
              .OnceAsync<ProductModel>();
            return allProducts.Where(a => a.PRODUCTID == ID).FirstOrDefault();
        }
        public async Task<string> UpdateProduct(ProductModel product)
        {
            try
            {
                var toUpdateProduct = (await firebase
                .Child("ProductModel")
                .OnceAsync<ProductModel>()).Where(a => a.Object.PRODUCTID == product.PRODUCTID).FirstOrDefault();

                await firebase
                  .Child("ProductModel")
                  .Child(toUpdateProduct.Key)
                  .PutAsync(product);
                return "success";
            }
            catch (Exception)
            {
                return "failed";
                throw;
            }
        }
        public async Task<string> DeleteProduct(int ID)
        {
            try
            {
                var toDeleteUser = (await firebase
                .Child("ProductModel")
                .OnceAsync<ProductModel>()).Where(a => a.Object.PRODUCTID == ID).FirstOrDefault();
                await firebase.Child("ProductModel").Child(toDeleteUser.Key).DeleteAsync();
                return "success";
            }
            catch (Exception)
            {
                return "failed";
            }

        }
    }
}
