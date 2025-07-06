using BudzetNarpwaiony.Models;
using BudzetNarpwaiony.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq;

namespace BudzetNarpwaiony.ViewModels
{
    public partial class DashboardViewModel : ViewModelBase
    {
        public override string Header => "Pulpit";
        private readonly BudgetService _budgetService;
        public AppSettings Settings { get; }

        [ObservableProperty] private decimal _incomeThisMonth;
        [ObservableProperty] private decimal _expensesThisMonth;
        [ObservableProperty] private string _topSpendingCategory = "-";
        [ObservableProperty] private decimal _biggestExpense = 0;

        public DashboardViewModel(BudgetService budgetService, AppSettings settings)
        {
            _budgetService = budgetService;
            Settings = settings;
            _budgetService.Transactions.CollectionChanged += (s, e) => RecalculateDashboard();
            RecalculateDashboard();
        }

        private void RecalculateDashboard()
        {
            var now = DateTime.Now;
            var transactionsThisMonth = _budgetService.Transactions
                .Where(t => t.Date.Year == now.Year && t.Date.Month == now.Month);

            IncomeThisMonth = transactionsThisMonth.Where(t => t.Amount > 0).Sum(t => t.Amount);
            var expenses = transactionsThisMonth.Where(t => t.Amount < 0);
            ExpensesThisMonth = expenses.Sum(t => t.Amount);

            if (expenses.Any())
            {
                BiggestExpense = expenses.Min(t => t.Amount);
                TopSpendingCategory = expenses.GroupBy(t => t.Category)
                    .Select(g => new { Category = g.Key, Total = g.Sum(t => t.Amount) })
                    .OrderBy(g => g.Total).FirstOrDefault()?.Category ?? "-";
            }
            else
            {
                BiggestExpense = 0;
                TopSpendingCategory = "-";
            }
        }
    }
}