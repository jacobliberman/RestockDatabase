using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDatabase
{
     [XamlCompilation(XamlCompilationOptions.Compile)]
     public partial class tPage : TabbedPage
     {
          public tPage()
          {
               InitializeComponent();
               this.Children.Add(new MainPage());
               this.Children.Add(new ProviderPage());
          }
     }
}