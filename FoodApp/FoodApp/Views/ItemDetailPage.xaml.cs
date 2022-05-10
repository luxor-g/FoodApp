using FoodApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FoodApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}