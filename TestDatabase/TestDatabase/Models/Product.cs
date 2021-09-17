using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
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

          [MaxLength(250)]
          public string Description { get; set; }
          

          [MaxLength(250)]
          public string Category { get; set; }

          [MaxLength(250)]
          public string Department { get; set; }


          public double Price { get; set; }


          public int TotalSale { get; set; }


          public int Quantity { get; set; }


          public DateTime Date { get; set; }


          public int Key { get; set; }





     }


    public class ProductMap : ClassMap<Product>
     {
          public ProductMap()
          {
               Map(m => m.Description).Name("Article Description");
               Map(m => m.Category).Name("Category");
               Map(m => m.Department).Name("Department");
               Map(m => m.Date).Name("Date of Sale");
               Map(m => m.Quantity).Name("Sold Quantity");
               Map(m => m.TotalSale).Name("Total Sales");
          }
     }


}


