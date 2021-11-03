using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDatabase.Models;
using SQLiteNetExtensionsAsync.Extensions;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.Linq;

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
               
               
               conn.CreateTableAsync<Product>();
          }



          public async Task AddNewProduct(Product p)
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

          public async Task AddNewProduct(string name, string desc, string cat, string dept, double price, int totalSale, int quan, string date, int numStock, int prevSales, string saleHour,string provs)
          {
               int result = 0;
               try
               {
                    /*
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");
                    if (string.IsNullOrEmpty(desc))
                         throw new Exception("Valid description required");
                    if (string.IsNullOrEmpty(cat))
                         throw new Exception("Valid category required");
                    if (string.IsNullOrEmpty(dept))
                         throw new Exception("Valid department required");
                    if (double.IsNaN(price))
                         throw new Exception("Valid price required");                              
                    if (string.IsNullOrEmpty(date))
                         throw new Exception("Valid date required");
                    if (string.IsNullOrEmpty(saleHour))
                         throw new Exception("Valid sale hour required");
      */


                    //Product newP = new Product { Name = name, Description = desc, Category = cat, Department = dept, Price = price, TotalSale = totalSale, Quantity = quan, Date = date, numInStock = numStock, prevSales = prevSales, SaleHour = saleHour, listOfProviders = provs };
                    //result = await conn.InsertAsync(newP);


                    result = await conn.InsertAsync(new Product { Name = name, Description = desc, Category = cat, Department = dept, Price = price, TotalSale = totalSale, Quantity = quan, Date = date, numInStock = numStock, prevSales = prevSales, SaleHour = saleHour, listOfProviders = provs });
                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
                    //return newP;
               }
               catch (Exception ex)
               {
                   StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
                    //return null;
               }

          }




          /// <summary>
          /// Adds new product to Product Database
          /// </summary>
          /// <param name="name"></param>
          /// <param name="desc"></param>
          /// <param name="price"></param>
          /// <returns></returns>
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


          /// <summary>
          /// Gets List of all providers of a product
          /// </summary>
          /// <param name="prod"></param>
          /// <returns>List of Providers</returns>
          public async Task<List<Provider>> GetAllProviders(Product prod)
          {             
                    Product p = await conn.GetWithChildrenAsync<Product>(prod.Name);
                    return p.Providers;
                            
          }
         
          /// <summary>
          /// Adds Provider to Product and vice-versa
          /// </summary>
          /// <param name="prod"></param>
          /// <param name="prov"></param>
          /// <returns></returns>
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



          public async Task<Product> GetProduct(string pname)
          {
               return await App.conn.GetAsync<Product>(pname);
          }

          /// <summary>
          /// Gets list of all Products in database
          /// </summary>
          /// <returns>List of Products</returns>
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

          /// <summary>
          /// Deletes Product from Database
          /// </summary>
          /// <param name="delete">Product to delete</param>
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

          /// <summary>
          /// Links list of providers to their respective entries in the database, or creates a new provider if it does not exists
          /// </summary>
          /// <param name="p">Product to add providers to</param>
          public async Task LinkProviderList(Product p)
          {
               List<string> provList = p.listOfProviders.Split(';').ToList();
               List<Provider> pList = new List<Provider>();
               foreach (string prov in provList)
               {
                    //Check if provider exists
                    Provider pExists = await conn.FindAsync<Provider>(prov);
                    if (pExists is null)
                    { //Does not exist, Make new provider
                         Console.WriteLine("Provider {0} does not exist.", prov);
                         await App.ProviderRepo.AddNewProvider(prov);
                         pExists = await App.ProviderRepo.GetProvider(prov);
                    }
                    pList.Add(pExists);
               }
               p.Providers = pList;
               try
               {
                    await conn.UpdateWithChildrenAsync(p);
               }
               catch (Exception ex)
               {
                    Console.WriteLine("Could not add list of providers. {0}", ex);
               }
               
          }
        public async void update(string name, int newQuantity)
        {
            Product p = await conn.FindAsync<Product>(name);
            int temp = p.Quantity;
            p.Quantity = p.Quantity - newQuantity;
            Console.WriteLine("Amount sold in last week was: {0}", p.Quantity);
            if (p.Quantity < p.minStock + 5)
            {
                Console.WriteLine("NOTIFICATION: Low on {0} item", p.Name);
            }
            else if(p.Quantity <=p.lastWeekStock) {
                Console.WriteLine("NOTIFICATION: Low on {0} item", p.Name);
            }
            p.lastWeekStock = temp;
            await conn.UpdateWithChildrenAsync(p);

        }
        public async void fetchRestock(string name)
        {
            Product p = await conn.FindAsync<Product>(name);
            Console.WriteLine("Amount sold in last week was: {0}", p.Quantity);
            if (p.Quantity < p.minStock + 5)
            {
                Console.WriteLine("NOTIFICATION: Low on {0} item", p.Name);
            }
            else if (p.Quantity <= p.lastWeekStock)         //must try this cuz what if they didn't order last week and sold?
            {
                Console.WriteLine("NOTIFICATION: Low on {0} item", p.Name);
            }
        }

        public async void getCalculation(string name)
        {
            Product p = await conn.FindAsync<Product>(name);
            Console.WriteLine("Last weeks amount ordered: {0}", p.lastWeekStock-p.minStock);
        }

        public async void updateCalculation(String name, int newminstock, int newlastweek)
        {
            Product p = await conn.FindAsync<Product>(name);
            p.minStock = newminstock;
            p.lastWeekStock = newlastweek;
            await conn.UpdateWithChildrenAsync(p);

        }

     }
}
