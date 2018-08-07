using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABCPharmaService.Models;
using ABCPharmaService.DataBase;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ABCPharmaService.Repositories
{
    public class PurchaseRepository
    {
        public Invoice SavePurchaseDetails(Purchases purchases_, JObject json)
        {
            using (ABCPharma_DBEntities context = new ABCPharma_DBEntities())
            {
                List<PurchasedMedicine> purchasedMedicines = MedicinesList(json);
                Invoice inv = new Invoice();
                Purchase purchase = new Purchase();
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (purchases_.storeId != null)
                        {
                            purchase.StoreId = purchases_.storeId;
                            purchase.PurchaseDetails = purchases_.purchaseDetails;
                            purchase.PurchaseDate = purchases_.purchaseDate;
                            inv = GenerateInvoice(purchasedMedicines);
                            purchase.InvoiceDetails = JsonConvert.SerializeObject(inv);
                            context.Purchases.Add(purchase);
                        }
                        context.SaveChanges();
                        inv.purchaseId = purchase.PurchaseId;
                        transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        var abc = ex.Message;
                        transaction.Rollback();
                    }
                }

                return inv;
            }
        }

        private Invoice GenerateInvoice(List<PurchasedMedicine> purchasedMedicines)
        {
           Invoice inv = new Invoice();
           //List<string> m = new List<string>();

            //calculateTotalAmount
            foreach (PurchasedMedicine med_ in purchasedMedicines)
            {
                inv.totalAmount = inv.totalAmount + med_.subTotalPrice;
                //m.Add(med_.medicinePurchased);
            }

            inv.purchasedMed = purchasedMedicines;
            //calculate Discount
            if(inv.totalAmount >= 1000 && inv.totalAmount < 5000)
            {
                inv.discounts = Convert.ToDecimal(0.03) * inv.totalAmount;
            }
            if (inv.totalAmount >= 5000 && inv.totalAmount < 10000)
            {
                inv.discounts = Convert.ToDecimal(0.03) * inv.totalAmount;
            }
            if (inv.totalAmount >= 10000)
            {
                inv.discounts = Convert.ToDecimal(0.03) * inv.totalAmount;
            }
            inv.storeDiscounts = (inv.totalAmount - inv.discounts) * Convert.ToDecimal(0.05);
            inv.taxes = (inv.totalAmount - inv.storeDiscounts) * Convert.ToDecimal(0.026);
            inv.finalAmount = inv.totalAmount + inv.taxes;
            return inv;
        }

            // recursively yield all children of json
            private List<PurchasedMedicine> MedicinesList(JObject json)
        {
            List<PurchasedMedicine> purchasedMedicines = new List<PurchasedMedicine>();
            PurchasedMedicine purchasedMedicine = null;

            var resultObjects = AllChildren(json)
                .First(c => c.Type == JTokenType.Array && c.Path.Contains("medicineList"))
                .Children<JObject>();

            foreach (JObject result in resultObjects)
            {
                //foreach (JProperty property in result.Properties())
                //{
                    purchasedMedicine = new PurchasedMedicine();
                    purchasedMedicine.medicineId = Convert.ToInt32(JsonConvert.DeserializeObject<int>(result.GetValue("medicineId").ToString()));
                    purchasedMedicine.quantityToPurchase = Convert.ToInt32(JsonConvert.DeserializeObject<decimal>(result.GetValue("quantityToPurchase").ToString()));
                    purchasedMedicine.subTotalPrice = Convert.ToInt32(JsonConvert.DeserializeObject<decimal>(result.GetValue("subTotalPrice").ToString()));
                    purchasedMedicine.medicinePurchased = MedicineRepository.GetMedicineById(purchasedMedicine.medicineId);
                    //if (property.Name.Equals("medicineId"))
                    //{
                    //    purchasedMedicine.medicinePurchased = MedicineRepository.GetMedicineById(Convert.ToInt32(property.Value));
                    //}
                    //if (property.Name.Equals("quantityToPurchase"))
                    //{
                    //    purchasedMedicine.quantityToPurchase = Convert.ToInt32(property.Value);
                    //}
                    //if (property.Name.Equals("subTotalPrice"))
                    //{
                    //    purchasedMedicine.subTotalPrice = Convert.ToDecimal(property.Value);
                    //}
                //}
                purchasedMedicines.Add(purchasedMedicine);
            }

            return purchasedMedicines;
        }

        // recursively yield all children of json
        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}