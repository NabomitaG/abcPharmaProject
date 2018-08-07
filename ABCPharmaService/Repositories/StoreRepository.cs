using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABCPharmaService.Models;
using ABCPharmaService.DataBase;

namespace ABCPharmaService.Repositories
{
    public class StoreRepository
    {
        public static List<Stores> GetAllStores()
        {

            using (var db = new ABCPharma_DBEntities())
            {
                var storeList = (from s in db.Stores
                                 select new Stores
                                 {
                                     storeId = s.StoreId,
                                     storeName = s.StoreName,
                                     storeAddress = s.StoreAddress,
                                     storePhoneNo = s.StorePhoneNo
                                 }).ToList();
                return storeList;
            }
        }

        public static Stores GetStoreById(int storeId_)
        {

            using (var db = new ABCPharma_DBEntities())
            {
                var store_ = (from s in db.Stores
                                where s.StoreId.Equals(storeId_)
                                select new Stores
                                {
                                    storeId = s.StoreId,
                                    storeName = s.StoreName,
                                    storeAddress = s.StoreAddress,
                                    storePhoneNo = s.StorePhoneNo
                                }).FirstOrDefault();
                return store_;
            }
        }

        public bool updateDeleteStore(Stores store_detail)
        {
            using (ABCPharma_DBEntities context = new ABCPharma_DBEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Store store = new Store();
                        store.StoreName = store_detail.storeName;
                        store.StoreAddress = store_detail.storeAddress;
                        store.StorePhoneNo = store_detail.storePhoneNo;
                        context.Stores.Add(store);

                        context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (System.Exception ex)
                    {
                        var abc = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            return true;
        }
    }
}