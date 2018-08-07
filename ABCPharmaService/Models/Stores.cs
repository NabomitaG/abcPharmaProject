using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABCPharmaService.Models
{
    public class Stores
    {
        public int storeId { get; set; }
        public string storeName { get; set; }
        public string storeAddress { get; set; }
        public int storePhoneNo { get; set; }
    }
}