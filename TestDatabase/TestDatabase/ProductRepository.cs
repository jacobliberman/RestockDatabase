using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDatabase.Models;
using SQLiteNetExtensionsAsync.Extensions;

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

          public async Task AddNewProduct(string name, string Id)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");
                    if (string.IsNullOrEmpty(Id))
                         throw new Exception("Valid ID required");


                    result = await conn.InsertAsync(new Product { Name = name, ID=Id });

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

          /*  public async Task AddNewProduct(string desc, string category, string department, string date, string soldQuantity, string totalNoTax,string hour)
            {
                 int result = 0;
                 try
                 {
                      if (string.IsNullOrEmpty(desc))
                           throw new Exception("Valid Description Required");
                      if (string.IsNullOrEmpty(category))
                           throw new Exception("Valid Category Required");
                      if (string.IsNullOrEmpty(department))
                           throw new Exception("Valid Department Required");
                      if (string.IsNullOrEmpty(date))
                           throw new Exception("Valid Date Required");
                     // if (double.IsNaN(totalNoTax))
                     //      throw new Exception("Valid Total Required");
                      if (string.IsNullOrEmpty(hour))
                           throw new Exception("Valid Sale Hour Required");

                      result = await conn.InsertAsync(new Product { Description = desc, Category=category, Department=department,Date=date,Quantity=soldQuantity,TotalWithoutTax=totalNoTax,SaleHour=hour });
                      StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, desc);

                 }

                 catch (Exception ex)
                 {
                      StatusMessage = string.Format("Failed to add {0}. Error: {1}", desc, ex.Message);
                 }
            }
          */


          public async Task AddProvider(Product prod, Provider prov){
               try
               {
                    prod.Providers.Add(prov);
                    await conn.UpdateWithChildrenAsync(prod);
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to Add Provider {0} to Product {1}", prov, prod);
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
          public void DeleteProduct(Product delete)
          {
               //ToDO
               conn.DeleteAsync<Product>(delete.ID);
               return;
          }

          public Task<int> ClearAllItems<T>()
          {
               return conn.DeleteAllAsync<Product>();
          }

     }
}
