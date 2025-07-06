using BudzetDomowy.Models;
using BudzetDomowy.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BudzetDomowy.ViewModels
{
    public partial class HistoryViewModel : ViewModelBase
    {
        public override string Header => "Historia";
        private readonly BudgetService _budgetService;
        public AppSettings Settings { get; }

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string? _selectedFilterCategory;

        [ObservableProperty]
        private System.DateTimeOffset? _startDate;

        [ObservableProperty]
        private System.DateTimeOffset? _endDate;

        public List<string> FilterCategories { get; }
        public ObservableCollection<Transaction> FilteredTransactions { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveTransactionCommand))]
        private Transaction? _selectedTransaction;

        [ObservableProperty]
        private bool _isConfirmingDelete = false;

        public HistoryViewModel(BudgetService budgetService, AppSettings settings)
        {
            _budgetService = budgetService;
            Settings = settings;

            FilterCategories = new List<string> { "Wszystkie" };
            FilterCategories.AddRange(new AddTransactionViewModel(budgetService, settings).Categories);
            _selectedFilterCategory = "Wszystkie";

            _budgetService.Transactions.CollectionChanged += (s, e) => ApplyFilter();
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName is nameof(SearchText) or nameof(SelectedFilterCategory) or nameof(StartDate) or nameof(EndDate))
                {
                    ApplyFilter();
                }
            };
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var filtered = _budgetService.Transactions.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(t => t.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));
            }

            if (SelectedFilterCategory != "Wszystkie" && SelectedFilterCategory is not null)
            {
                filtered = filtered.Where(t => t.Category == SelectedFilterCategory);
            }

            if (StartDate is not null)
            {
                filtered = filtered.Where(t => t.Date.Date >= StartDate.Value.Date);
            }

            if (EndDate is not null)
            {
                filtered = filtered.Where(t => t.Date.Date <= EndDate.Value.Date);
            }

            FilteredTransactions.Clear();
            foreach (var transaction in filtered.OrderByDescending(t => t.Date))
            {
                FilteredTransactions.Add(transaction);
            }
        }

        [RelayCommand(CanExecute = nameof(CanRemoveTransaction))]
        private void RemoveTransaction()
        {
            IsConfirmingDelete = true;
        }

        [RelayCommand]
        private void ConfirmDelete()
        {
            if (SelectedTransaction is null) return;
            _budgetService.RemoveTransaction(SelectedTransaction);
            IsConfirmingDelete = false;

            ApplyFilter();
        }

        [RelayCommand]
        private void CancelDelete()
        {
            IsConfirmingDelete = false;
        }

        private bool CanRemoveTransaction() => SelectedTransaction is not null;
    }
}