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
  
        public class CalculationRepository
        {
            public string StatusMessage { get; set; }
            private SQLiteAsyncConnection conn;

            public CalculationRepository(SQLiteAsyncConnection conn)
            {
                this.conn = conn;

                // ***************** UNCOMMENT TO CLEAR TABLE ON REBOOT *********************
                //conn.DropTableAsync<Product>().Wait(); 

                //conn = new SQLiteAsyncConnection(dbPath);
                conn.CreateTableAsync<Product>();
            }
            public async Task AddNewCalculation(string name, string e, int a)
            {
                int result = 0;
                try
                {
                    //basic validation to ensure a name was entered
                    if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(e))
                        throw new Exception("Valid name required");

                    try
                    {
                        result = await conn.InsertAsync(new Calculations { ProductName = name, EquationSymbol = e, Amount = a });
                        StatusMessage = string.Format("{0} record(s) added  {1} {2} {3})", result, name, e, a);

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

            public async Task<List<Calculations>> GetAllCalculation()
            {
                try
                {
                    return await conn.Table<Calculations>().ToListAsync();
                }
                catch (Exception ex)
                {
                    StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
                }
                return new List<Calculations>();
            }

          

            public async void DeleteCalculation(Calculations delete)
            {

                
                await conn.UpdateWithChildrenAsync(delete);
                await conn.DeleteAsync(delete, recursive: true);

            }

            public Task<int> ClearAllCalculation<T>()
            {
                return conn.DeleteAllAsync<Calculations>();
            }
        }
    
}
