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


          [MaxLength(250)]
          public string Name { get; set; }

          [MaxLength(250)]
          public string Description { get; set; }
          

          [MaxLength(250)]
          public string Category { get; set; }

          [MaxLength(250)]
          public string Department { get; set; }


          public double Price { get; set; }


          public string TotalSale { get; set; }


          public string Quantity { get; set; }


          public string Date { get; set; }


          public int Key { get; set; }


          public string TotalWithoutTax { get;set; }

          public string SaleHour { get; set; }

          public Provider ProvidedBy { get; set; }

     }

          public class NewProductMap : ClassMap<Product>
     {
          public NewProductMap()
          {
               Map(m => m.Description).Name("Description");
               Map(m => m.Quantity).Name("Quantity Sold");
               Map(m => m.TotalSale).Name("Total Sales");
               Map(m => m.TotalWithoutTax).Name("Total (Without Tax)");
               Map(m => m.Category).Name("Category");
               Map(m => m.Department).Name("Department");
          }
     }




    public class ProductMap : ClassMap<Product>
     {
          public ProductMap()
          {
               Map(m => m.Name).Name("Name");
               Map(m => m.Description).Name("Description");
               Map(m => m.Category).Name("Category");
               Map(m => m.Department).Name("Departament");
               Map(m => m.Date).Name("Date");
               Map(m => m.Quantity).Name("Quantity");
               Map(m => m.TotalSale).Name("Total Sales");
               Map(m => m.Price).Name("Price");
               Map(m => m.ProvidedBy).Name("Provider(s)");
               Map(m => m.SaleHour).Name("SaleHour");
          }
     }





}


