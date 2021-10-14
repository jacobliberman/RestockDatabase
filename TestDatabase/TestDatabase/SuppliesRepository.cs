using System;
using System.Collections.Generic;
using System.Text;
using TestDatabase.Models;
using SQLite;
using System.Threading.Tasks;

namespace TestDatabase
{
    class SuppliesRepository
    {
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;
        public SuppliesRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Supplies>();
        }

        public async Task AddNewSupplies(int providerid, int productid)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered

                result = await conn.InsertAsync(new Supplies { ProductID = productid, ProviderID = providerid });

                StatusMessage = string.Format("{0} record(s) added [ID: {1})", result, productid);
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
        public void DeleteSupplies(Supplies delete)
        {
            //ToDO
            conn.DeleteAsync<Supplies>(delete.ProductID);
            return;
        }

        public Task<int> ClearAllSupplies<T>()
        {
            return conn.DeleteAllAsync<Supplies>();
        }
    }
}

