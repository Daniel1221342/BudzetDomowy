using CommunityToolkit.Mvvm.ComponentModel;

namespace BudzetDomowy.Models
{
    public partial class AppSettings : ObservableObject
    {
        [ObservableProperty]
        private string _currencySymbol = "zł"; 
    }
}