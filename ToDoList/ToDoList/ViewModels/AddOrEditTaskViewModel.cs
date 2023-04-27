using Autofac.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class AddOrEditTaskViewModel : ViewModelBase, IDisposable
    {
        private readonly HomeViewModel homeViewModel;
        private readonly ContextViewModel contextViewModel;
        private readonly IMessageBoxService messageBoxService;

        private bool isEditing;
        private bool isDisposed;

        private string taskName;
        public string TaskName
        {
            get
            { return taskName; }

            set
            {
                if (!string.Equals(taskName, value))
                {
                    taskName = value;
                    OnPropertyChanged(nameof(TaskName));
                }
            }

        }

        private string taskDescription;
        public string TaskDescription
        {
            get
            { return taskDescription; }

            set
            {
                if (!string.Equals(taskDescription, value))
                {
                    taskDescription = value;
                    OnPropertyChanged(nameof(TaskDescription));
                }
            }

        }


        private EStatus taskStatus;
        public EStatus TaskStatus
        {
            get
            { return taskStatus; }

            set
            {
                if (!Equals(taskStatus, value))
                {
                    taskStatus = value;
                    OnPropertyChanged(nameof(TaskStatus));
                }
            }

        }

        private EPriority taskPriority;
        public EPriority TaskPriority
        {
            get
            { return taskPriority; }

            set
            {
                if (!Equals(taskPriority, value))
                {
                    taskPriority = value;
                    OnPropertyChanged(nameof(TaskPriority));
                }
            }

        }

        private DateTime taskDeadline;
        public DateTime TaskDeadline
        {
            get
            { return taskDeadline; }

            set
            {
                if (!Equals(taskDeadline, value))
                {
                    taskDeadline = value;
                    OnPropertyChanged(nameof(TaskDeadline));
                }
            }

        }

        private ICommand addOrEditTaskCommand;
        public ICommand AddOrEditTaskCommand
        {
            get
            {
                if (addOrEditTaskCommand is null)
                {
                    if (contextViewModel.SelectedToDoList is not null && !isEditing)
                    {
                        addOrEditTaskCommand = new RelayCommand(AddTask, parm => ValidateFormService.ValidateTaskForm(TaskName, TaskDescription));
                    }

                    else
                    {
                        addOrEditTaskCommand = new RelayCommand(EditTask, param => ValidateFormService.ValidateTaskForm(TaskName, TaskDescription));
                    }
                }
                return addOrEditTaskCommand;
            }
        }


        public AddOrEditTaskViewModel(HomeViewModel homeViewModel, ContextViewModel contextViewModel, bool isEditing)
        {
            this.homeViewModel = homeViewModel ?? throw new ArgumentNullException(nameof(homeViewModel));
            this.contextViewModel = contextViewModel ?? throw new ArgumentNullException(nameof(contextViewModel));
            messageBoxService = new MessageBoxService();
            this.isEditing = isEditing;
            WindowService.EditFormChanged += Handle_EditFormChanged;
        }

        private async void Handle_EditFormChanged()
        {
            TaskName = homeViewModel.SelectedTask.Name;
            TaskDescription = homeViewModel.SelectedTask.Description;
            TaskStatus = homeViewModel.SelectedTask.Status;
            TaskPriority = homeViewModel.SelectedTask.Priority;
            TaskDeadline = homeViewModel.SelectedTask.Deadline;
        }

        private void AddTask()
        {
            contextViewModel.SelectedToDoList.Tasks.Add(new MyTask(TaskName, TaskDescription, TaskStatus, TaskPriority, TaskDeadline));
            messageBoxService.ShowInformation("Task added succesfully!");
            homeViewModel.RefreshTasks();
        }

        private void EditTask()
        {
            homeViewModel.SelectedTask.Name = TaskName;
            homeViewModel.SelectedTask.Description = TaskDescription;
            homeViewModel.SelectedTask.Status = TaskStatus;
            homeViewModel.SelectedTask.Priority = TaskPriority;
            homeViewModel.SelectedTask.Deadline = TaskDeadline;

            var selectedTask = homeViewModel.SelectedTask;
            homeViewModel.SelectedTdlTasks.Remove(selectedTask);
            homeViewModel.SelectedTdlTasks.Add(selectedTask); // if I raise the event here, why isn't changing
            messageBoxService.ShowInformation("Task edited succesfully!");
            homeViewModel.RefreshTasks();

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    WindowService.EditFormChanged -= Handle_EditFormChanged;
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
    }
}
