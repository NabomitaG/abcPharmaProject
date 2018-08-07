using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABCPharmaService.Models
{
    public partial class Purchases
    {
        public int purchaseId { get; set; }
        public int storeId { get; set; }
        public string purchaseDetails { get; set; }
        public string invoiceDetails { get; set; }
        public DateTime purchaseDate { get; set; }
    }

    public partial class PurchasedMedicine
    {
        public int medicineId { get; set; }
        public Medicines medicinePurchased { get; set; }
        public int quantityToPurchase { get; set; }
        public decimal subTotalPrice { get; set; }
    }

    public partial class Invoice
    {

        public int purchaseId { get; set; }
        public List<PurchasedMedicine> purchasedMed { get; set; }
        public decimal totalAmount { get; set; }
        public decimal taxes { get; set; }
        public decimal discounts { get; set; }
        public decimal storeDiscounts { get; set; }
        public decimal finalAmount { get; set; }
    }
}