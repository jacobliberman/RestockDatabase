using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace TestDatabase.Models
{
    class Supplies
    {
          
        [ForeignKey(typeof(Provider))]
        public int ProviderID { get; set; }


        [ForeignKey(typeof(Product))]
        public int ProductID{ get; set; }

     }
}
