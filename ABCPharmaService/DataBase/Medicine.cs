//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ABCPharmaService.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Medicine
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public Nullable<System.DateTime> PKGDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> UnitPrice { get; set; }
        public Nullable<int> QuantityAvailable { get; set; }
    }
}
