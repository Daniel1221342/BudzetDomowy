using BudzetNarpwaiony.Models;
using BudzetNarpwaiony.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;

namespace BudzetNarpwaiony.ViewModels
{
    public partial class AddTransactionViewModel : ViewModelBase
    {
        public override string Header => "Nowa Transakcja";
        private readonly BudgetService _budgetService;
        public AppSettings Settings { get; }

        [ObservableProperty] private string _name = string.Empty;
        [ObservableProperty] private decimal _amount;
        [ObservableProperty] private string _selectedCategory = "Inne";
        [ObservableProperty] private decimal _monthlyBudget = 2000;

        public string SubmitButtonText => "Dodaj transakcję";
        public decimal TotalBalance => _budgetService.Transactions.Sum(t => t.Amount);
        public decimal TotalExpenses => _budgetService.Transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);
        public decimal RemainingBudget => MonthlyBudget + TotalExpenses;
        public string[] Categories { get; } = { "Przychód", "Jedzenie", "Transport", "Rachunki", "Rozrywka", "Inne" };

        public AddTransactionViewModel(BudgetService budgetService, AppSettings settings)
        {
            _budgetService = budgetService;
            Settings = settings;
            _budgetService.Transactions.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(TotalBalance));
                OnPropertyChanged(nameof(TotalExpenses));
                OnPropertyChanged(nameof(RemainingBudget));
            };
        }

        [RelayCommand]
        private void Submit()
        {
            if (string.IsNullOrWhiteSpace(Name) || Amount == 0) return;
            var transaction = new Transaction { Name = Name, Amount = Amount, Category = SelectedCategory, Date = DateTime.Now };
            _budgetService.AddTransaction(transaction);
            Name = string.Empty;
            Amount = 0;
        }
    }
}