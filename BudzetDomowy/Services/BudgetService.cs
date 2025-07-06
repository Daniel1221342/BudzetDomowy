using BudzetDomowy.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BudzetDomowy.Services
{
    public class BudgetService
    {
        private readonly string _filePath;
        public ObservableCollection<Transaction> Transactions { get; } = new();

        public BudgetService()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _filePath = Path.Combine(documentsPath, "budget-transactions.json");
            LoadTransactions();
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            SaveTransactions();
        }

        public void RemoveTransaction(Transaction transaction)
        {
            Transactions.Remove(transaction);
            SaveTransactions();
        }

        private void SaveTransactions()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Transactions, options);
            File.WriteAllText(_filePath, json);
        }

        private void LoadTransactions()
        {
            if (!File.Exists(_filePath)) return;
            try
            {
                string json = File.ReadAllText(_filePath);
                var loadedTransactions = JsonSerializer.Deserialize<ObservableCollection<Transaction>>(json);
                if (loadedTransactions is not null)
                {
                    Transactions.Clear();
                    foreach (var t in loadedTransactions)
                    {
                        Transactions.Add(t);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd wczytywania transakcji: {ex.Message}");
            }
        }
        // W pliku Services/BudgetService.cs
        public void UpdateTransaction()
        {
            SaveTransactions();
        }
    }
}