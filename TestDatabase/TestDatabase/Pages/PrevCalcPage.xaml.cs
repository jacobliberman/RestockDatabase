using System;
using System.Collections.Generic;
using TestDatabase.Models;
using Xamarin.Forms;

namespace TestDatabase.Pages
{
    public partial class PrevCalcPage : ContentPage
    {
        public PrevCalcPage()
        {
            InitializeComponent();
        }


        public async void getPreviousRestock(object sender, EventArgs args)
          {
               List<Product> products = await App.ProductRepo.GetAllProducts();
               //List<Dictionary<string,string> temp = new List<Dictionary<string,string>>; 
               //foreach (Product prod in products)
               //{

               //}
               
               productsList.ItemsSource = products;
          }
    }
}
