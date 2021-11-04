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



          /// <summary>
          /// Adds new product to Product Database
          /// </summary>
          /// <param name="p">Product to be added</param>
          /// <returns></returns>
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

          /// <summary>
          /// Adds new product to Product Database
          /// </summary>
          /// <param name="name">Name of Product</param>
          /// <param name="desc">Description of Product</param>
          /// <param name="cat">Category of Product</param>
          /// <param name="dept">Department of Product</param>
          /// <param name="price">Price of Product</param>
          /// <param name="totalSale">Total amount sold of Product</param>
          /// <param name="quan">Quantity of Product</param>
          /// <param name="date">Date of Product</param>
          /// <param name="numStock">Number in Stock of Product</param>
          /// <param name="prevSales">Previous sales of Product</param>
          /// <param name="saleHour">Hour of sale of Product</param>
          /// <param name="provs">Providers of Product</param>
          /// <returns></returns>
          public async Task AddNewProduct(string name, string desc, string cat, string dept, double price, int totalSale, int quan, string date, int numStock, int prevSales, string saleHour, string provs)
          {
               int result = 0;
               try
               {

                    result = await conn.InsertAsync(new Product { Name = name, Description = desc, Category = cat, Department = dept, Price = price, TotalSale = totalSale, Quantity = quan, Date = date, numInStock = numStock, prevSales = prevSales, SaleHour = saleHour, listOfProviders = provs });
                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);

               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);

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

          /// <summary>
          /// Adds new product to Product Database
          /// </summary>
          /// <param name="name">Name of Product</param>
          /// <returns></returns>
          public async Task AddNewProduct(string name)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");



                    result = await conn.InsertAsync(new Product { Name = name });

                    StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
               }
               catch (Exception ex)
               {
                    StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
               }

          }

          /// <summary>
          /// Adds new product to Product Database
          /// </summary>
          /// <param name="name">Name of Product</param>
          /// <param name="numInStock">Number in stock of Product</param>
          /// <param name="previousSales">Previous sales of Product</param>
          /// <returns></returns>
          public async Task AddNewProduct(string name, int numInStock, int previousSales)
          {
               int result = 0;
               try
               {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name))
                         throw new Exception("Valid name required");




                    result = await conn.InsertAsync(new Product { Name = name, Quantity = numInStock, lastWeekStock= previousSales});
                    result = await conn.InsertAsync(new Product { Name = name, numInStock = numInStock, prevSales = previousSales });

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
          /// <param name="prod">Product</param>
          /// <returns>List of Providers</returns>
          public async Task<List<Provider>> GetAllProviders(Product prod)
          {
               Product p = await conn.GetWithChildrenAsync<Product>(prod.Name);
               return p.Providers;

          }

          /// <summary>
          /// Adds Provider to Product and vice-versa
          /// </summary>
          /// <param name="prod">Product to add to provider</param>
          /// <param name="prov">Provider to add to product</param>
          /// <returns></returns>
          public async Task AddProvider(Product prod, Provider prov)
          {
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



          /// <summary>
          /// Gets product from Product Database with given name
          /// </summary>
          /// <param name="pname">Name of Product</param>
          /// <returns>Product</returns>
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
            try
            {
                Product p = await conn.FindAsync<Product>(name);
                int temp = p.Quantity;
                p.Quantity = p.Quantity - newQuantity;
                StatusMessage = string.Format("Amount sold in last week was: {0}", p.Quantity);
                Console.WriteLine("Amount sold in last week was: {0}", p.Quantity);
                if (p.Quantity < p.minStock + 5)
                {
                    StatusMessage = string.Format("NOTIFICATION: Low on {0} item", p.Name);
                    Console.WriteLine("NOTIFICATION: Low on {0} item", p.Name);
                }
                else if (p.Quantity <= p.lastWeekStock)
                {
                    StatusMessage = string.Format("NOTIFICATION: Low on {0} item", p.Name);
                    Console.WriteLine("NOTIFICATION: Low on {0} item", p.Name);
                }
                p.lastWeekStock = temp;
                await conn.UpdateWithChildrenAsync(p);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

        }
        public async void fetchRestock(string name)
        {
            try
            {
                Product p = await conn.FindAsync<Product>(name);

                StatusMessage = string.Format("Amount sold in last week was: {0}", p.Quantity);
                Console.WriteLine("Amount sold in last week was: {0}", p.Quantity);
                if (p.Quantity < p.minStock + 5)
                {
                    StatusMessage = string.Format("NOTIFICATION: Low on {0} item", p.Name);
                    Console.WriteLine("NOTIFICATION: Low on {0} item", p.Name);
                }
                else if (p.Quantity <= p.lastWeekStock)         //must try this cuz what if they didn't order last week and sold?
                {
                    StatusMessage = string.Format("NOTIFICATION: Low on {0} item", p.Name);
                    Console.WriteLine("NOTIFICATION: Low on {0} item", p.Name);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
        }

        public async void getCalculation(string name, int time)
        {
            try
            {
                Product p = await conn.FindAsync<Product>(name);

                if (p.lastWeekStock == 0)
                {
                    Console.WriteLine("Last weeks amount ordered: {0}", p.Quantity);
                    StatusMessage = string.Format("Last weeks amount ordered: {0}", p.Quantity - p.minStock * (time / 7));
                    Console.WriteLine("Last weeks amount ordered: {0}", p.Quantity - p.minStock);
                }
                else
                {
                    StatusMessage = string.Format("Last weeks amount ordered: {0}", p.lastWeekStock - p.minStock * (time / 7));
                    Console.WriteLine("Last weeks amount ordered: {0}", p.lastWeekStock - p.minStock);
                }
            }
            catch (Exception ex)
               {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }


        }

        public async void updateCalculation(String name, int newminstock, int newlastweek)
        {
            try
            {
                Product p = await conn.FindAsync<Product>(name);

                p.minStock = newminstock;
                p.lastWeekStock = newlastweek;
                await conn.UpdateWithChildrenAsync(p);
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
        }

     }
}
