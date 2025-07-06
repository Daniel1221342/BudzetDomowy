using CommunityToolkit.Mvvm.ComponentModel;

namespace BudzetNarpwaiony.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public abstract string Header { get; }
    }
}