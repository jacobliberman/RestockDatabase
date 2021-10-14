using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace TestDatabase.Models
{

     [Table("provider")]
     public class Provider
     {
          [PrimaryKey, Unique]
          public string Name { get; set; }
                   
                 
          [ManyToMany(typeof(Supplies))]
          public List<Product> Products { get; set; }



          public string Frequency{ get; set; }

   

     }
}
