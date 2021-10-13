using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using SQLite;

namespace TestDatabase.Models
{

     [Table("provider")]
     public class Provider
     {
          [PrimaryKey]
          public string Name { get; set; }


          public string Frequency{ get; set; }

          public List<Product> ProductsSupplied { get; set; }

     }
}
