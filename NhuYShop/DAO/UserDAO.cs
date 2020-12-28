﻿using Firebase.Database;
using NhuYShop.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Linq;

namespace NhuYShop.DAO
{
    public class UserDAO
    {
        FirebaseClient firebase = new FirebaseClient("https://quickstart-1604847633747-default-rtdb.firebaseio.com/");

        public async Task<List<UserModel>> GetAllUser()
        {

            return (await firebase
              .Child("UserModel")
              .OnceAsync<UserModel>()).Select(item => new UserModel
              {
                  ID = item.Object.ID,
                  NAME = item.Object.NAME,
                  SOCIAL = item.Object.SOCIAL,
                  PHONE = item.Object.PHONE,
                  CREATEDATE = item.Object.CREATEDATE,
                  UPDATEDATE = item.Object.UPDATEDATE
              }).ToList();
        }
        public async Task AddUser(UserModel user)
        {

            await firebase
              .Child("UserModel")
              .PostAsync(user);
        }
        public async Task<UserModel> GetUser(string ID)
        {
            var allPersons = await GetAllUser();
            await firebase
              .Child("UserModel")
              .OnceAsync<UserModel>();
            return allPersons.Where(a => a.ID == ID).FirstOrDefault();
        }
        public async Task UpdateUser(UserModel user)
        {
            var toUpdatePerson = (await firebase
              .Child("UserModel")
              .OnceAsync<UserModel>()).Where(a => a.Object.ID == user.ID).FirstOrDefault();

            await firebase
              .Child("UserModel")
              .Child(toUpdatePerson.Key)
              .PutAsync(user);
        }
    }
}
