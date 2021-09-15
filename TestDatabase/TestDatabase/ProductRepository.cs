using System;
using System.Collections.Generic;
using System.Text;
using TestDatabase.Models;
using SQLite;



namespace TestDatabase
{
     public class ProductRepository
     {
          public string StatusMessage { get; set; }
          private SQLiteConnection conn;
          public ProductRepository(string dbPath)
          {
               conn = new SQLiteConnection(dbPath);
               conn.CreateTable<Product>();
          }

          public void AddNewProduct(string name, string desc, double price)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");
                    if (double.IsNaN(price))
                         throw new Exception("Valid price required");
                         

                    result = conn.Insert(new Product { Name = name, Description=desc, Price=price});

                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
               }

          }

          public List<Product> GetAllProducts()
          {
               try
               {
                    return conn.Table<Product>().ToList();
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
               }
               return new List<Product>();
          }

     }
}
