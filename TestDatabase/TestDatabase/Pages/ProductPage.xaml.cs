using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TestDatabase.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TestDatabase
{
     public partial class ProductPage : ContentPage
     {
          public ProductPage()
          {
               InitializeComponent();
               statusMessage.Text = FileSystem.AppDataDirectory;
          }

          /// <summary>
          ///  Adds new product to SQLite Table on button press
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="args"></param>
          public async void OnNewButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";

               await App.ProductRepo.AddNewProduct(newProduct.Text);
               //await App.ProductRepo.AddNewProduct(newProduct.Text, newDesc.Text, Convert.ToDouble(newPrice.Text));

               statusMessage.Text = App.ProductRepo.StatusMessage;
          }

          /// <summary>
          /// Gets and prints list of products in database 
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="args"></param>
          public async void OnGetButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";

               List<Product> products = await App.ProductRepo.GetAllProducts();
               productsList.ItemsSource = products;
               
               //List<string> provs = 
               //statusMessage.Text = string.Format("{0}", products[0].Providers.ToString());
               
          }

          /// <summary>
          /// Deletes all entries from SQLite Database (With no warning)
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
          private async void OnDeleteAllButtonClicked(object sender, EventArgs e)
          {
               await App.ProductRepo.ClearAllItems<Product>();
               string dbPath = FileAccessHelper.GetLocalFilePath("products.db3");
               FileInfo fi = new FileInfo(dbPath);
               /*try
               {
                    if (fi.Exists)
                    {
                         SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath + ";");
                         connection.Close();
                         GC.Collect();
                         GC.WaitForPendingFinalizers();
                         fi.Delete();
                    }
               }
               catch (Exception ex)
               {
                    fi.Delete();
               }
               */

          }

          public async void BtnWelcome_Clicked(object sender, EventArgs args)
          {
               return;
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





                              csv.Context.RegisterClassMap<NewProductMap>();
                              var records = csv.GetRecords<Product>();

                              foreach (Product prod in records)
                              {
                                   Console.WriteLine($"!!!!{prod.Description}!!!!");
                                   //await App.ProductRepo.AddNewProduct(prod.Description, prod.Category, prod.Department, prod.Date, prod.Quantity, prod.TotalWithoutTax, prod.SaleHour);
                                   await App.ProductRepo.AddNewProduct(prod.Description, prod.Quantity, prod.Price);


                              }
                              statusMessage.Text = App.ProductRepo.StatusMessage;


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
