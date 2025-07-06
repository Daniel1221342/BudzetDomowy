using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BudzetNarpwaiony.ViewModels;
using BudzetNarpwaiony.Views;

namespace BudzetNarpwaiony
{
    public partial class App : Application
    {
        public static Window? MainWindow { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel()
                };
                desktop.MainWindow = MainWindow;
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}