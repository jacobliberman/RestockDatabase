using SQLite;
using Xamarin.Forms;

namespace TestDatabase
{
     public partial class App : Application
     {
          string dbPath => FileAccessHelper.GetLocalFilePath("products.db3");
          public static ProductRepository ProductRepo { get; private set; }
          public static ProviderRepository ProviderRepo { get; private set; }
          public static SuppliesRepository SuppliesRepo { get; private set; }

          private SQLiteAsyncConnection conn;
          public App()
          {
               InitializeComponent();

               conn = new SQLiteAsyncConnection(dbPath);
               ProductRepo = new ProductRepository(conn);
               ProviderRepo = new ProviderRepository(conn);
               SuppliesRepo = new SuppliesRepository(conn);

               
               var tabbedPage = new tPage();

               //tabbedPage.Children.Add(new MainPage());
              

               MainPage = tabbedPage;


               //MainPage = new MainPage();
          }

          protected override void OnStart()
          {
          }

          protected override void OnSleep()
          {
          }

          protected override void OnResume()
          {
          }
     }
}
