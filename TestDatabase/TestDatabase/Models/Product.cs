using CsvHelper.Configuration;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using CsvHelper.TypeConversion;
using CsvHelper.Expressions;

using CsvHelper;
using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace TestDatabase.Models
{
     [Table("product")]
     public class Product
     {


          [PrimaryKey, MaxLength(250), Unique]
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


          public string Date { get; set; }


          public int Key { get; set; }


          public int numInStock { get; set; }

          public int prevSales { get; set; }

          public int minStock { get; set; }

          public int lastWeekStock { get; set; }

          public string SaleHour { get; set; }

          public int numToRestock { get; set; }

          //Needs to be set before new calculation is made 
          public IDictionary<string, string> prevCalc = new Dictionary<string, string>() { { "currentStock", null }, { "minStock", null }, { "estRestock", null } };

          public string listOfProviders { get; set; }


          [ManyToMany(typeof(Supplies))]
          public List<Provider> Providers { get; set; } = new List<Provider>();

     }

     public class InventoryImport : ClassMap<Product>
     {
          public InventoryImport()
          {
               Map(m => m.Name).Name("Name");
               Map(m => m.Description).Name("Description");
               Map(m => m.Category).Name("Category");
               Map(m => m.Department).Name("Department");
               Map(m => m.Price).Name("Price");
               Map(m => m.TotalSale).Name("Total Sales");
               Map(m => m.Date).Name("Date");
               Map(m => m.Quantity).Name("Quantity");
               Map(m => m.numInStock).Name("NumInStock");
               Map(m => m.prevSales).Name("PrevSales");
               Map(m => m.SaleHour).Name("SaleHour");

               
               Map(m => m.listOfProviders).Name("Provider(s)");




          }
     }


     public class NewProductMap : ClassMap<Product>
     {
          public NewProductMap()
          {
               Map(m => m.Name).Name("Name");
               Map(m => m.Description).Name("Description");
               Map(m => m.Category).Name("Category");
               Map(m => m.Department).Name("Departament");
               Map(m => m.Date).Name("Date of Sale");
               Map(m => m.Quantity).Name("Sold Quantity");
               Map(m => m.TotalSale).Name("Total Sales");
               // Map(m => m.TotalWithoutTax).Name("Total (Without Tax)");
               Map(m => m.SaleHour).Name("Sale hour");
               Map(m => m.minStock).Name("Minimum stock");
               // Map(m => m.ProvidedBy).Name("Provider");
          }
     }



     public class ProductMap : ClassMap<Product>
     {
          public ProductMap()
          {
               Map(m => m.Name).Name("Name");
               Map(m => m.Description).Name("Article Description");
               Map(m => m.Category).Name("Category");
               Map(m => m.Department).Name("Departament");
               Map(m => m.Date).Name("Date of Sale");
               Map(m => m.Quantity).Name("Sold Quantity");
               Map(m => m.TotalSale).Name("Total Sales");
               // Map(m => m.TotalWithoutTax).Name("Total (Without Tax)");
               Map(m => m.SaleHour).Name("Sale hour");
               //Map(m => m.ProvidedBy).Name("Provider");
          }
     }





}


