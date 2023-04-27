
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.DataObjects;
using ToDoList.Extensions;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class FindTaskViewModel : ViewModelBase
    {
        private readonly HomeViewModel homeViewModel;
        private readonly ContextViewModel contextViewModel;
        private readonly IMessageBoxService messageBoxService;
        private ObservableCollection<MyTaskDTO> foundTasks;
        public ObservableCollection<MyTaskDTO> FoundTasks
        {
            get 
            {
                return foundTasks;
            }
            set
            {
                foundTasks = value;
                OnPropertyChanged(nameof(FoundTasks));
            }
        }

        private string findWhat;
        public string FindWhat
        {
            get
            {
                return findWhat;
            }

            set
            {
                findWhat = value;
                OnPropertyChanged(nameof(FindWhat));
            }
        }

        private DateTime byDeadline;
        public DateTime ByDeadline
        {
            get
            {
                return byDeadline;
            }

            set
            {
                byDeadline = value;
                OnPropertyChanged(nameof(FindWhat));
            }
        }

        private ICommand findByNameCommand;
        public ICommand FindByNameCommand
        {
            get
            {
                if (findByNameCommand is null)
                {
                    findByNameCommand = new RelayCommand(FindByName);
                }
                return findByNameCommand;
            }
        }

        private ICommand findByDeadlineCommand;
        public ICommand FindByDeadlineCommand
        {
            get
            {
                if (findByDeadlineCommand is null)
                {
                    findByDeadlineCommand = new RelayCommand(FindByDeadline);
                }
                return findByDeadlineCommand;
            }
        }

        public FindTaskViewModel(HomeViewModel homeViewModel, ContextViewModel contextViewModel)
        {
            this.homeViewModel = homeViewModel ?? throw new ArgumentNullException(nameof(homeViewModel));
            this.contextViewModel = contextViewModel ?? throw new ArgumentNullException(nameof(contextViewModel));
            messageBoxService = new MessageBoxService();

            FoundTasks = new ObservableCollection<MyTaskDTO>();
        }

        private void FindByName()
        {
            FoundTasks.Clear();
            var list = homeViewModel.SelectedTdlTasks
                .Where(t => t.Name == FindWhat)
                .Select(t => new MyTaskDTO(t.Name, contextViewModel.SelectedToDoList));
            FoundTasks.AddRange(list);

            if (FoundTasks.Count == 0)
                messageBoxService.ShowWarning("No tasks found!");
        }

        private void FindByDeadline()
        {
            FoundTasks.Clear();
            var list = homeViewModel.SelectedTdlTasks
                .Where(t => t.Deadline == ByDeadline)
                .Select(t => new MyTaskDTO(t.Name, contextViewModel.SelectedToDoList));
            FoundTasks.AddRange(list);

            if (FoundTasks.Count == 0)
                messageBoxService.ShowWarning("No tasks found!");
        }
    }
}
