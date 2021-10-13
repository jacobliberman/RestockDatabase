using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace TestDatabase.Models
{

     [Table("provider")]
     public class Provider
     {
          
          public string Name { get; set; }

          [PrimaryKey]
          public string ID { get; set; }
                  
          [ManyToMany(typeof(Supplies))]
          public List<Product> Products { get; set; }

     }
}
