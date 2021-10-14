using SQLiteNetExtensions.Attributes;

namespace TestDatabase.Models
{
     public class Supplies
     {

          [ForeignKey(typeof(Provider))]
          public string ProviderName { get; set; }


          [ForeignKey(typeof(Product))]
          public string ProductName { get; set; }

     }
}
