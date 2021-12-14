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


               await App.ProviderRepo.AddNewProvider(newProviderName.Text);

               statusMessage.Text = App.ProviderRepo.StatusMessage;
          }

          public async void OnGetButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";
            try
            {
               List<Provider> providers = await App.ProviderRepo.GetAllProviders();
               providerList.ItemsSource = providers;
            }
            catch (Exception ex)
            {
                 statusMessage.Text = $"Failed to retrieve data. {ex.Message}";
            }




        }
          private async void OnFileUploadButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";

               try
               {
                    var result = await FilePicker.PickAsync();
                    if (result != null)
                    {
                         statusMessage.Text = $"File Name: {FileAccessHelper.GetLocalFilePath(result.FileName)}";

                         var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                         {
                              HeaderValidated = null,
                              MissingFieldFound = null,


                         };



                         using (var reader = new StreamReader(result.FullPath))
                         using (var csv = new CsvReader(reader, config))
                         {
                              //string title = reader.ReadLine();
                              //string Filter = reader.ReadLine();

                              //Console.WriteLine(title);
                              //Console.WriteLine(Filter);

                              //string line;
                              //while ((line = reader.ReadLine()) != null)
                              //{
                              //     Console.WriteLine(line);
                              //}

                              csv.Context.RegisterClassMap<ProviderMap>();
                              var records = csv.GetRecords<Provider>();

                              foreach (Provider prov in records)
                              {

                                   await App.ProviderRepo.AddNewProvider(prov);
                                   await App.ProviderRepo.LinkProductList(prov);
                                   //await App.ProductRepo.AddNewProduct(prod.Name,prod.Description,prod.Category,prod.Department,prod.Price,prod.TotalSale,prod.Quantity,prod.Date,prod.numInStock,prod.prevSales,prod.SaleHour,prod.listOfProviders);
                                   Console.WriteLine("Provider Added. {0}", prov.Name);
                              }
                              statusMessage.Text = App.ProviderRepo.StatusMessage;


                         }


                    }

               }
               catch (Exception ex)
               {
                    statusMessage.Text = ex.Message;
                    Console.Write(ex.Message);
               }

          }
     }
}