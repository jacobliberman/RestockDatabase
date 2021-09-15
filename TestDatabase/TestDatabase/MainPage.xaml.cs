using TestDatabase.Models;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

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
     
    }
}
