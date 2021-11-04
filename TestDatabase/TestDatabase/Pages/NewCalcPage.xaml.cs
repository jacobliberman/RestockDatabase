using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TestDatabase.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TestDatabase.Pages
{
    public partial class NewCalcPage : ContentPage
    {
        public NewCalcPage()
        {
            InitializeComponent();

        }


        // Button to return to Functions Clicked 
        private async void BacktoFunctionsClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TestDatabase.Pages.InitFunctionsPage());
        }

        private async void new_restock_calc_btn_Clicked(System.Object sender, System.EventArgs e)
        {
            statusMessage.Text = "";
            //App.ProductRepo.fetchRestock(prod_id_calc.Text);
            // call the function fetch restock and print the estimated restock amount on screen
            //print on Grid column 1 row 8
            //Console.WriteLine("{0}", new_calc_days.Text);
            App.ProductRepo.getCalculation(prod_id_calc.Text, Int32.Parse(new_calc_days.Text));
            statusMessage.Text = App.ProductRepo.StatusMessage;

        }
    }
}
