using CommunityToolkit.Mvvm.ComponentModel;

namespace BudzetDomowy.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public abstract string Header { get; }
    }
}