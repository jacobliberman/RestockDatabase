using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDatabase.Models;
using SQLiteNetExtensionsAsync.Extensions;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

namespace TestDatabase
{

     public class ProductRepository
     {
          public string StatusMessage { get; set; }
          private SQLiteAsyncConnection conn;

          public ProductRepository(SQLiteAsyncConnection conn)
          {
               this.conn = conn;
               
               // ***************** UNCOMMENT TO CLEAR TABLE ON REBOOT *********************
               //conn.DropTableAsync<Product>().Wait(); 
               
               //conn = new SQLiteAsyncConnection(dbPath);
               conn.CreateTableAsync<Product>();
          }

          public async Task AddNewProduct(string name, string desc, double price)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");
                    if (double.IsNaN(price))
                         throw new Exception("Valid price required");


                    result = await conn.InsertAsync(new Product { Name = name, Description = desc, Price = price });

                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
               }

          }

          public async Task AddNewProduct(string name)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");
             


                    result = await conn.InsertAsync(new Product { Name = name});

                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
               }

          }

          public async Task AddNewProduct(string name, int numInStock, int previousSales)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");
                    



                    result = await conn.InsertAsync(new Product { Name = name, numInStock = numInStock, prevSales= previousSales});

                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
               }

          }


          public async Task<List<Provider>> GetAllProviders(Product prod)
          {             
                    Product p = await conn.GetWithChildrenAsync<Product>(prod.Name);
                    return p.Providers;
                            
          }
         
          public async Task AddProvider(Product prod, Provider prov){
               try
               {
                    try
                    {
                         prod.Providers.Add(prov);
                         await conn.UpdateWithChildrenAsync(prod);
                         StatusMessage = string.Format("Added Provider {0} to Product {1}", prov, prod);
                    }
                    catch (Exception ex)
                    {
                         StatusMessage = string.Format("Could not add provider");
                    }
                    
                   
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to Add Provider {0} to Product {1}", prov.Name, prod.Name);
               }
          }

          public async Task<List<Product>> GetAllProducts()
          {
               try
               {
                    return await conn.GetAllWithChildrenAsync<Product>();
                    //return await conn.Table<Product>().ToListAsync();
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
               }
               return new List<Product>();
          }
          public async void DeleteProduct(Product delete)
          {
               delete.Providers = null;
               await conn.UpdateWithChildrenAsync(delete);
               await conn.DeleteAsync(delete, recursive: true);
          }

          public Task<int> ClearAllItems<T>()
          {
               return conn.DeleteAllAsync<Product>();
          }

     }
}
