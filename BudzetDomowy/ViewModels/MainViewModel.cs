using BudzetDomowy.Models;
using BudzetDomowy.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace BudzetDomowy.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public override string Header => "Główny";
        public ObservableCollection<ViewModelBase> Pages { get; }

        [ObservableProperty]
        private ViewModelBase? _selectedPage;

        public MainViewModel()
        {

            var appSettings = new AppSettings();
            var budgetService = new BudgetService();

           
            Pages = new ObservableCollection<ViewModelBase>
            {
                new AddTransactionViewModel(budgetService, appSettings),
                new HistoryViewModel(budgetService, appSettings),
                new DashboardViewModel(budgetService, appSettings),
                new SettingsViewModel(appSettings)
            };
            SelectedPage = Pages[0];
        }
    }
}