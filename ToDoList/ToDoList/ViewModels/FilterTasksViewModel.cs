using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Windows.Documents;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.Extensions;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class FilterTasksViewModel
    {
        private readonly HomeViewModel homeViewModel;

        private List<MyTask> OriginalListing { get; set; }

        public FilterTasksViewModel(HomeViewModel homeViewModel)
        {
            this.homeViewModel = homeViewModel ?? throw new ArgumentNullException(nameof(homeViewModel));
            OriginalListing = new List<MyTask>();
            foreach (var item in homeViewModel.SelectedTdlTasks)
            {
                OriginalListing.Add((MyTask)item.Clone());
            }

        }

        private ICommand showCompletedTasksCommand;
        public ICommand ShowCompletedTasksCommand
        {
            get
            {
                if (showCompletedTasksCommand is null)
                {
                    showCompletedTasksCommand = new RelayCommand(ShowCompletedTasks);
                }
                return showCompletedTasksCommand;
            }
        }


        private ICommand showOverdueTasksCommand;
        public ICommand ShowOverdueTasksCommand
        {
            get
            {
                if (showOverdueTasksCommand is null)
                {
                    showOverdueTasksCommand = new RelayCommand(ShowOverdueTasks);
                }
                return showOverdueTasksCommand;
            }
        }


        private ICommand showPastDueTasksCommand;
        public ICommand ShowPastDueTasksCommand
        {
            get
            {
                if (showPastDueTasksCommand is null)
                {
                   showPastDueTasksCommand = new RelayCommand(ShowPastdueTasks);
                }
                return showPastDueTasksCommand;
            }
        }


        private ICommand showUpcomingTasksCommand;
        public ICommand ShowUpcomingTasksCommand
        {
            get
            {
                if (showUpcomingTasksCommand is null)
                {
                    showUpcomingTasksCommand = new RelayCommand(ShowUpcomingTasks);
                }
                return showUpcomingTasksCommand;
            }
        }


        private ICommand revertFilteringCommand;
        public ICommand RevertFilteringCommand
        {
            get
            {
                if (revertFilteringCommand is null)
                {
                    revertFilteringCommand = new RelayCommand(RevertFiltering);
                }
                return revertFilteringCommand;
            }
        }

        private void ShowCompletedTasks() // task urile completate
        {
            var list = homeViewModel.SelectedTdlTasks.Where(t => t.Status == EStatus.Done).ToList();
            homeViewModel.SelectedTdlTasks.Clear();
            homeViewModel.SelectedTdlTasks.AddRange(list);
        }

        private void ShowOverdueTasks() // task urile terminate, dar cu deadline depasit
        {
            var list = homeViewModel.SelectedTdlTasks.Where(t => t.DoneDate > t.Deadline).ToList();
            homeViewModel.SelectedTdlTasks.Clear();
            homeViewModel.SelectedTdlTasks.AddRange(list);
        }

        private void ShowPastdueTasks() // task urile neterminate, cu deadline depasit
        {
            var list = homeViewModel.SelectedTdlTasks
                .Where(t => t.IsDone == false && t.Deadline < DateTime.Now).ToList();
            homeViewModel.SelectedTdlTasks.Clear();
            homeViewModel.SelectedTdlTasks.AddRange(list);
        }

        private void RevertFiltering() // revert
        {
            homeViewModel.SelectedTdlTasks.Clear();
            homeViewModel.SelectedTdlTasks.AddRange(OriginalListing);
        }

        private void ShowUpcomingTasks() // task uri neterminate, inca valabile
        {
            var list = homeViewModel.SelectedTdlTasks
                  .Where(t => t.IsDone == false && t.Deadline > DateTime.Now).ToList();
            homeViewModel.SelectedTdlTasks.Clear();
            homeViewModel.SelectedTdlTasks.AddRange(list);
        }

    }
}
