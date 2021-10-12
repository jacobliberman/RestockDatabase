using SQLiteNetExtensions.Attributes;

namespace TestDatabase.Models
{
     public class Supplies
     {

          [ForeignKey(typeof(Provider))]
          public string ProviderID { get; set; }


          [ForeignKey(typeof(Product))]
          public string ProductID { get; set; }

     }
}
