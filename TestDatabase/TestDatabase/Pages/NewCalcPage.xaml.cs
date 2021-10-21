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

    }
}
