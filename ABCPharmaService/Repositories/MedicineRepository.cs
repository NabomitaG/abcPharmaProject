using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABCPharmaService.Models;
using ABCPharmaService.DataBase;

namespace ABCPharmaService.Repositories
{
    public class MedicineRepository
    {
        public static List<Medicines> GetAllMedicines()
        {

            using (var db = new ABCPharma_DBEntities())
            {
                var medicineList = (from m in db.Medicines
                                 select new Medicines
                                 {
                                     itemId = m.MedicineId,
                                     medicineName = m.MedicineName,
                                     pkgDate = m.PKGDate,
                                     expiryDate = m.ExpiryDate,
                                     quantity = m.QuantityAvailable,
                                     unitPrice = m.UnitPrice
                                 }).ToList();
                return medicineList;
            }
        }

        public static List<Medicines> GetMedicineByName(string medicineName_)
        {

            using (var db = new ABCPharma_DBEntities())
            {
                var medicineList = (from m in db.Medicines
                                    where m.MedicineName.Contains(medicineName_)
                                    select new Medicines
                                    {
                                        itemId = m.MedicineId,
                                        medicineName = m.MedicineName,
                                        pkgDate = m.PKGDate,
                                        expiryDate = m.ExpiryDate,
                                        quantity = m.QuantityAvailable,
                                        unitPrice = m.UnitPrice
                                    }).ToList();
                return medicineList;
            }
        }

        public static Medicines GetMedicineById(int medicineId_)
        {

            using (var db = new ABCPharma_DBEntities())
            {
                var medicine = (from m in db.Medicines
                                    where m.MedicineId.Equals(medicineId_)
                                    select new Medicines
                                    {
                                        itemId = m.MedicineId,
                                        medicineName = m.MedicineName,
                                        pkgDate = m.PKGDate,
                                        expiryDate = m.ExpiryDate,
                                        quantity = m.QuantityAvailable,
                                        unitPrice = m.UnitPrice
                                    }).FirstOrDefault();
                return medicine;
            }
        }

        public bool updateDeleteMedicine(Medicines medicines_detail)
        {
            using (ABCPharma_DBEntities context = new ABCPharma_DBEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Medicine medicineToAdd = new Medicine();
                        medicineToAdd.MedicineName = medicines_detail.medicineName;
                        medicineToAdd.PKGDate = medicines_detail.pkgDate;
                        medicineToAdd.ExpiryDate = medicines_detail.expiryDate;
                        medicineToAdd.QuantityAvailable = medicines_detail.quantity;
                        medicineToAdd.UnitPrice = medicines_detail.unitPrice;

                        context.Medicines.Add(medicineToAdd);

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