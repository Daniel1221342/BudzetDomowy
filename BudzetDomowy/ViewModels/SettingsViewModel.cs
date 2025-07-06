using Avalonia;
using Avalonia.Styling;
using BudzetDomowy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;

namespace BudzetDomowy.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        public override string Header => "Ustawienia";

        public AppSettings Settings { get; }
        public List<string> AvailableCurrencies { get; } = new List<string> { "zł", "€", "$", "£" };

        [ObservableProperty]
        private bool _isLightMode = Application.Current!.RequestedThemeVariant == ThemeVariant.Light;

        public SettingsViewModel(AppSettings settings)
        {
            Settings = settings;
        }

        [RelayCommand]
        private void ToggleTheme()
        {
            Application.Current!.RequestedThemeVariant = IsLightMode ? ThemeVariant.Light : ThemeVariant.Dark;
        }
    }
}