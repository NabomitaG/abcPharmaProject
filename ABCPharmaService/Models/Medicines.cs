using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABCPharmaService.Models
{
    public class Medicines
    {
        public int itemId { get; set; }
        public string medicineName { get; set; }
        public DateTime? pkgDate { get; set; }
        public DateTime? expiryDate { get; set; }
        public int? unitPrice { get; set; }
        public int? quantity { get; set; }
    }
}