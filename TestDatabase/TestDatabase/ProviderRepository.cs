using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDatabase.Models;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

using SQLiteNetExtensions.Extensions;

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
               //conn.DropTableAsync<Provider>().Wait();
               conn.CreateTableAsync<Provider>();
          }

          public async Task AddNewProvider(string name)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");

                    try
                    {
                         result = await conn.InsertAsync(new Provider { Name = name});
                         StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);

                    }
                    catch (SQLiteException ex)
                    {
                         StatusMessage = string.Format("Failed to Add Provider. Provider with Name \"{0}\" already exists "); 
                         StatusMessage = string.Format("Failed to Add Provider. Provider with Name \"{0}\" already exists "); 
                    }
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

          public async Task<List<Product>> GetAllProducts(Provider prov)
          {
               Provider p = await conn.GetWithChildrenAsync<Provider>(prov.Name);
               return p.Products;

          }

          public async void DeleteProvider(Provider delete)
          {
               
               delete.Products = null;
               await conn.UpdateWithChildrenAsync(delete);
               await conn.DeleteAsync(delete, recursive: true);

          }

          public Task<int> ClearAllProviders<T>()
          {
               return conn.DeleteAllAsync<Provider>();
          }
    
     
     
     
     }
}
