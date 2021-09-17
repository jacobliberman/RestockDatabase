using TestDatabase.Models;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace TestDatabase
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void OnNewButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            App.ProductRepo.AddNewProduct(newProduct.Text,newDesc.Text,Convert.ToDouble(newPrice.Text)) ;
            statusMessage.Text = App.ProductRepo.StatusMessage;
        }

        public void OnGetButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            List<Product> products = App.ProductRepo.GetAllProducts();
               productsList.ItemsSource = products;
               
          }

         private  async void OnFileUploadButtonClicked(object sender, EventArgs args)
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
                              
                         };



                         using (var reader = new StreamReader(result.FullPath)) 
                         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                         {
                              csv.Context.RegisterClassMap<ProductMap>();
                              var records = csv.GetRecords<Product>();
                              foreach (Product prod in records)
                              {
                                   Console.Write($"{prod.Name}");
                              }

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
