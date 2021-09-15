using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace TestDatabase.Models
{
     [Table("product")]
     public class Product
     {
          [PrimaryKey,AutoIncrement]
          public int Id { get; set; }

          [MaxLength(250),Unique]
          public string Name { get; set; }

          [MaxLength(250), Unique]
          public string Description { get; set; }
          
          [MaxLength(250)]
          public string Department { get; set; }


          public double Price { get; set; }


          public int TotalSale { get; set; }


          public int Quantity { get; set; }


          public DateTime Date { get; set; }


          public int Key { get; set; }





     }
}
