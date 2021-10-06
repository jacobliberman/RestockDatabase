using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace TestDatabase.Models
{

     [Table("provider")]
     public class Provider
     {
          [PrimaryKey]
          public string Name { get; set; }

          public string ID{ get; set; }

          public List<Product> ProductsSupplied { get; set; }

          [ManyToMany(typeof(Supplies))]
          public List<Product> Products{ get; set; }

     }
}
