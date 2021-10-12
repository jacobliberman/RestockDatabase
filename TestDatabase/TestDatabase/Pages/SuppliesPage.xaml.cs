using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TestDatabase.Models;
using Xamarin.Essentials;
using Xamarin.Forms;



using Xamarin.Forms.Xaml;

namespace TestDatabase
{
     [XamlCompilation(XamlCompilationOptions.Compile)]
     public partial class SuppliesPage : ContentPage
     {
          public SuppliesPage()
          {
               InitializeComponent();
          }

          public async void OnNewButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";


               Product prod = await App.conn.GetAsync<Product>(productId.Text);
               Provider prov = await App.conn.GetAsync<Provider>(providerId.Text);
               await App.ProductRepo.AddProvider(prod, prov);


               //await App.SuppliesRepo.AddNewSupplies(providerId.Text, productId.Text);

               statusMessage.Text = App.SuppliesRepo.StatusMessage;
               testMessage.Text = App.SuppliesRepo.TestMessage;
          }

          public async void OnSuppliesGetButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";

               List<Supplies> supplies = await App.SuppliesRepo.GetAllSupplies();
               suppliesList.ItemsSource = supplies;


          }

          public async void OnSearchButtonClicked(object sender, EventArgs args)
          {
               try
               {
                    Product prod = await App.conn.GetAsync<Product>(searchProductId.Text);
                    statusMessage.Text = string.Format("Product Name: {0}", prod.Name);
               }
               catch (Exception ex)
               {
                    statusMessage.Text = string.Format("product Id {0} not found. Error: {1}", searchProductId, ex);
               }
          }

     }
}