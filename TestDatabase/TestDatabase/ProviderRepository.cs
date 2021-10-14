using System;
using System.Collections.Generic;
using System.Text;
using TestDatabase.Models;
using SQLite;
using System.Threading.Tasks;

namespace TestDatabase
{
    class ProviderRepository
    {
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;
        public ProviderRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Provider>();
        }

        public async Task AddNewProvider(string name, int id)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(new Product { Name = name, ID = id});

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }


        }

        public async Task<List<Provider>> GetAllProvider()
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
    }
}
