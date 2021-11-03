using System;
using System.Collections.Generic;

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
            //await App.ProductRepo.fetchRestock(prod_id_calc.Text);
            // call the function fetch restock and print the estimated restock amount on screen
            //print on Grid column 1 row 8

        }
    }
}
