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
     public partial class ProviderPage : ContentPage
     {
          public ProviderPage()
          {
               InitializeComponent();
          }

          public async void OnNewButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";


               await App.ProviderRepo.AddNewProvider(newProviderName.Text, newProviderId.Text);

               statusMessage.Text = App.ProductRepo.StatusMessage;
          }

          public async void OnGetButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";

               List<Provider> providers = await App.ProviderRepo.GetAllProviders();
               providerList.ItemsSource = providers;

          }
     }
}