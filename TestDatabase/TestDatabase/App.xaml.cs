using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Xaml;
using TestDatabase.Pages;

namespace TestDatabase
{
     public partial class App : Application
     {
          string dbPath => FileAccessHelper.GetLocalFilePath("products.db3");
          public static ProductRepository ProductRepo { get; private set; }
          public App()
          {
               InitializeComponent();

               ProductRepo = new ProductRepository(dbPath);

            MainPage = new MainPage();

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
