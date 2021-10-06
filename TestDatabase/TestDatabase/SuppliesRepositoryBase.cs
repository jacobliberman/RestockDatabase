namespace TestDatabase
{
     public class SuppliesRepositoryBase
     {

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
     }
}