using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace BudzetDomowy.Models
{
    
    public partial class Transaction : ObservableObject
    {
        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private string _category = string.Empty;
    }
}