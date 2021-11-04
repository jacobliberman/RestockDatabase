using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDatabase.Models;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;
using System.Linq;
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

          /// <summary>
          /// Adds Provider to Provider Database
          /// </summary>
          /// <param name="p">Provider to add</param>
          /// <returns></returns>
          public async Task AddNewProvider(Provider p)
          {
               int result = 0;
               try
               {
                    result = await conn.InsertAsync(p);
                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, p.Name);

               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", p.Name, ex.Message);
               }

          }

          /// <summary>
          /// Adds Provider to Provider Database
          /// </summary>
          /// <param name="name">Name of provider to add</param>
          /// <returns></returns>
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
                         result = await conn.InsertAsync(new Provider { Name = name });
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

          /// <summary>
          /// Gets Provider with given name from Provider Database
          /// </summary>
          /// <param name="pname">Name of Provider</param>
          /// <returns>Provider</returns>
          public async Task<Provider> GetProvider(string pname)
          {
               return await App.conn.GetAsync<Provider>(pname);
          }


          /// <summary>
          /// Gets list of all providers in Database
          /// </summary>
          /// <returns>List of Providers</returns>
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


          /// <summary>
          /// Gets list of all Products given Provider supplies
          /// </summary>
          /// <param name="prov">Provider</param>
          /// <returns>List of Products</returns>
          public async Task<List<Product>> GetAllProducts(Provider prov)
          {
               Provider p = await conn.GetWithChildrenAsync<Provider>(prov.Name);
               return p.Products;

          }

          /// <summary>
          /// Deletes Given Provider 
          /// </summary>
          /// <param name="delete"></param>
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

          public async Task LinkProductList(Provider p)
          {
               List<string> prodList = p.listOfProducts.Split(';').ToList();
               List<Product> pList = new List<Product>();
               foreach (string prod in prodList)
               {
                    //Check if provider exists
                    Product pExists = await conn.FindAsync<Product>(prod);
                    if (pExists is null)
                    { //Does not exist, Make new provider
                         Console.WriteLine("Provider {0} does not exist.", prod);
                         await App.ProductRepo.AddNewProduct(prod);
                         pExists = await App.ProductRepo.GetProduct(prod);
                    }
                    pList.Add(pExists);
               }
               p.Products = pList;
               try
               {
                    await conn.UpdateWithChildrenAsync(p);
               }
               catch (Exception ex)
               {
                    Console.WriteLine("Could not add list of providers. {0}", ex);
               }

          }



     }
}
