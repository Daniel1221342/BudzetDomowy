using CommunityToolkit.Mvvm.ComponentModel;

namespace BudzetNarpwaiony.Models
{
    public partial class AppSettings : ObservableObject
    {
        [ObservableProperty]
        private string _currencySymbol = "zł"; 
    }
}