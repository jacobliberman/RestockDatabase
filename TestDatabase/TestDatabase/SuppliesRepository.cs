using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDatabase.Models;

namespace TestDatabase
{
     public class SuppliesRepository
     {
          public string StatusMessage { get; set; }
          public string TestMessage { get; set; }
          private SQLiteAsyncConnection conn;
          public SuppliesRepository(SQLiteAsyncConnection conn)
          {
               this.conn = conn;
               //conn = new SQLiteAsyncConnection(dbPath);
               //conn.DropTableAsync<Supplies>().Wait(); 
               conn.CreateTableAsync<Supplies>().Wait();
          }

          public async Task AddNewSupplies(string providerid, string productid)
          {
               int result = 0;
               try
               {
                    //TODO: Check if ProductID exists, Check if ProviderID exists
                    // Then, Add Provider to product and product to provider

                    try
                    {
                         Product prod = await conn.GetAsync<Product>(productid);
                         TestMessage = string.Format("Product with ID {0} has name {1}", productid, prod.ID);
                                                  
                    }
                    catch (Exception ex)
                    {
                         StatusMessage = string.Format("Product with id {0} not found. Error ", productid, ex);
                    }

                    result = await conn.InsertAsync(new Supplies { ProductID = productid, ProviderID = providerid });

                    //StatusMessage = string.Format("{0} record(s) added [ID: {1})", result, productid);
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", productid, ex.Message);
               }


          }

          public async Task<List<Supplies>> GetAllSupplies()
          {
               try
               {
                    return await conn.Table<Supplies>().ToListAsync();
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
               }
               return new List<Supplies>();
          }
          /*
          public void DeleteSupplies(Supplies delete)
          {
               //ToDO
               conn.DeleteAsync<Supplies>(delete.ProductID);
               return;
          }
          */

          public Task<int> ClearAllSupplies<T>()
          {
               return conn.DeleteAllAsync<Supplies>();
          }
     }
}

