using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDatabase.Models;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync;
using SQLiteNetExtensions;

namespace TestDatabase
{
     public class ProviderRepository
     {
          public string StatusMessage { get; set; }
          private SQLiteAsyncConnection conn;
          public ProviderRepository(SQLiteAsyncConnection conn)
          {
               this.conn = conn;
               //conn = new SQLiteAsyncConnection(dbPath);
               conn.CreateTableAsync<Provider>();
          }

          public async Task AddNewProvider(string name, string id)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");

                    result = await conn.InsertAsync(new Provider { Name = name, ID = id });

                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
               }


          }

          public async Task<List<Provider>> GetAllProviders()
          {
               try
               {
                    return await conn.Table<Provider>().ToListAsync();
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
               }
               return new List<Provider>();
          }
          public void DeleteProvider(Provider delete)
          {
               //ToDO
               conn.DeleteAsync<Provider>(delete.ID);
               return;
          }

          public Task<int> ClearAllProviders<T>()
          {
               return conn.DeleteAllAsync<Provider>();
          }
    
          public void provToString(Product prod)
          {
               List < Provider > provs = prod.Providers;
               string str ="";
               
               foreach (Provider p in provs)
               {
                    Console.Write("------------------------------------------------TEST-------------------------");
               }
          }
     
     
     
     }
}
