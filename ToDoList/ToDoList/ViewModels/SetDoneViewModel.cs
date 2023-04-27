
using System;
using System.Windows.Input;

namespace ToDoList.ViewModels
{
    public class SetDoneViewModel : ViewModelBase
    {
        private readonly HomeViewModel homeViewModel;
        public ICommand DeleteCommand => homeViewModel.DeleteTaskCommand;

        public SetDoneViewModel(HomeViewModel homeViewModel)
        {
            this.homeViewModel = homeViewModel ?? throw new ArgumentNullException(nameof(homeViewModel));
        }
    }
}
