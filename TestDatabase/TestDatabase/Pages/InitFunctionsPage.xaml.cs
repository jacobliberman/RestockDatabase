using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using Xamarin.Forms.Xaml;

namespace TestDatabase.Pages
{
    public partial class InitFunctionsPage : ContentPage
    {
        public InitFunctionsPage()
        {
            InitializeComponent();
        }


        private async void BtnNewCalcPg_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Pages.NewCalcPage());
        }

    

        private async void BtnViewPrevCalculation_Clicked(System.Object sender, System.EventArgs e)
        {
           // await Navigation.PushAsync(new ViewPrevCalcPage());
        }

        private async void BtnToUpdatePg_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Pages.UpdateFuncsPage());
        }




    }
}
