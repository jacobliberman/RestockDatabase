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
using SQLite;

namespace TestDatabase
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Adds new product to SQLite Table on button press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnNewButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";


               //await App.ProductRepo.AddNewProduct(newProduct.Text, newID.Text, Convert.ToDouble(newID.Text));
             
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
                                   await App.ProductRepo.AddNewProduct(prod.Description, prod.Quantity, prod.TotalSale, prod.TotalWithoutTax, prod.Category, prod.Department);


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
