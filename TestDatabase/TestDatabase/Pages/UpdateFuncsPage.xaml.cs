using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TestDatabase.Pages
{
    public partial class UpdateFuncsPage : ContentPage
    {
        public UpdateFuncsPage()
        {
            InitializeComponent();
        }

        private async void BtnUpdateSupplierPg_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SuppliesPage());
        }

        private async void BtnUpdateProductPg_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage());

        }

        //private async void BtnUpdateProvider_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    await Navigation.PushAsync(new ProviderPage());
        //}


    }
}
