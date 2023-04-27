using System;
using System.Linq;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.Extensions;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class SortTasksViewModel : ViewModelBase
    {
        private readonly HomeViewModel homeViewModel;

        public SortTasksViewModel(HomeViewModel homeViewModel)
        {
            this.homeViewModel = homeViewModel ?? throw new ArgumentNullException(nameof(homeViewModel));
        }

        private ICommand sortByDeadlineCommand;
        public ICommand SortByDeadlineCommand
        {
            get
            {
                if(sortByDeadlineCommand is null)
                {
                    sortByDeadlineCommand = new RelayCommand(SortByDeadline);
                }
                return sortByDeadlineCommand;
            }
        }

        private ICommand sortByPriorityCommand;
        public ICommand SortByPriorityCommand
        {
            get
            {
                if (sortByPriorityCommand is null)
                {
                    sortByPriorityCommand = new RelayCommand(SortByPriority);
                }
                return sortByPriorityCommand;
            }
        }

        private void SortByDeadline()
        {
            var list = homeViewModel.SelectedTdlTasks.ToList();
            list.Sort(new TaskDeadlineComparer());
            homeViewModel.SelectedTdlTasks.Clear();
            homeViewModel.SelectedTdlTasks.AddRange(list);
        }

        private void SortByPriority()
        {
            var list = homeViewModel.SelectedTdlTasks.ToList();
            list.Sort(new TaskPriorityComparer());
            homeViewModel.SelectedTdlTasks.Clear();
            homeViewModel.SelectedTdlTasks.AddRange(list);
        }
    }
}
