using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.DataAccess;
using ToDoList.Extensions;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class HomeViewModel : ViewModelBase, IDisposable
    {
        
        public ObservableCollection<TDL> ToDoListItems { get; set; }
        private readonly JsonFileSerializer jsonFileSerializer;
        private ObservableCollection<MyTask> selectedTdlTasks;
        public ObservableCollection<MyTask> SelectedTdlTasks
        { 
            get
            {
                return selectedTdlTasks;
            }

            set 
            {
                selectedTdlTasks = value;
                OnPropertyChanged(nameof(SelectedTdlTasks));
            } 
        }

        private MyTask selectedTask;
        public MyTask SelectedTask
        {
            get
            {
                return selectedTask;
            }

            set
            {
                selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        private int tasksDueToday;
        public int TasksDueToday
        {
            get
            {
                return tasksDueToday;
            }

            set
            {
                tasksDueToday = value;
                OnPropertyChanged(nameof(TasksDueToday));
            }
        }

        private int tasksDueTomorrow;
        public int TasksDueTomorrow
        {
            get
            {
                return tasksDueTomorrow;
            }

            set
            {
                tasksDueTomorrow = value;
                OnPropertyChanged(nameof(TasksDueTomorrow));
            }
        }

        private int tasksOverdue;
        public int TasksOverdue
        {
            get
            {
                return tasksOverdue;
            }

            set
            {
                tasksOverdue = value;
                OnPropertyChanged(nameof(TasksOverdue));
            }
        }

        private int tasksDone;
        public int TasksDone
        {
            get
            {
                return tasksDone;
            }

            set
            {
                tasksDone = value;
                OnPropertyChanged(nameof(TasksDone));
            }
        }

        private int tasksToBeDone;
        public int TasksToBeDone
        {
            get
            {
                return tasksToBeDone;
            }

            set
            {
                tasksToBeDone = value;
                OnPropertyChanged(nameof(TasksToBeDone));
            }
        }


        public ContextViewModel ContextViewModel { get; set; }
        private readonly IMessageBoxService messageBoxService;

        private ICommand openAddTDLView;
        public ICommand OpenAddTDLView
        {
            get
            {
                if(openAddTDLView is null)
                {
                    openAddTDLView = new RelayCommand(() => NavigatorService.OpenAddOrEditTDL(this, ContextViewModel, false));
                }
                return openAddTDLView;
            }
        }

        private ICommand openEditTDLView;
        public ICommand OpenEditTDLView
        {
            get
            {
                if (openEditTDLView is null)
                {
                    openEditTDLView = new RelayCommand(() => NavigatorService.OpenAddOrEditTDL(this, ContextViewModel, true),
                        param => ContextViewModel.SelectedToDoList != null);
                }
                return openEditTDLView;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand is null)
                {
                    deleteCommand = new RelayCommand(Delete, param => ContextViewModel.SelectedToDoList != null);
                }
                return deleteCommand;
            }
        }

        private ICommand moveUpCommand;
        public ICommand MoveUpCommand
        {
            get
            {
                if (moveUpCommand is null)
                {
                    moveUpCommand = new RelayCommand(MoveUp, param => ContextViewModel.SelectedToDoList != null);
                }
                return moveUpCommand;
            }
        }

        private ICommand moveDownCommand;
        public ICommand MoveDownCommand
        {
            get
            {
                if (moveDownCommand is null)
                {
                    moveDownCommand = new RelayCommand(MoveDown, param => ContextViewModel.SelectedToDoList != null);
                }
                return moveDownCommand;
            }
        }

        private ICommand openAddTaskView;
        public ICommand OpenAddTaskView
        {
            get
            {
                if (openAddTaskView is null)
                {
                    openAddTaskView = new RelayCommand(() => NavigatorService.OpenAddOrEditTask(this, ContextViewModel, false), param => ContextViewModel.SelectedToDoList != null);
                }
                return openAddTaskView;
            }
        }

        private ICommand openEditTaskView;
        public ICommand OpenEditTaskView
        {
            get
            {
                if (openEditTaskView is null)
                {
                    openEditTaskView = new RelayCommand(() => NavigatorService.OpenAddOrEditTask(this, ContextViewModel, true), param => SelectedTask != null);
                }
                return openEditTaskView;
            }
        }

        private ICommand deleteTaskCommand;
        public ICommand DeleteTaskCommand
        {
            get
            {
                if (deleteTaskCommand is null)
                {
                    deleteTaskCommand = new RelayCommand(DeleteTask, param => SelectedTask != null);
                }
                return deleteTaskCommand;
            }
        }

        private ICommand setDoneTaskCommand;
        public ICommand SetDoneTaskCommand
        {
            get
            {
                if (setDoneTaskCommand is null)
                {
                    setDoneTaskCommand = new RelayCommand(SetDone, param => SelectedTask != null 
                    && SelectedTask.Status != EStatus.Done);
                }
                return setDoneTaskCommand;
            }


        }

        private ICommand moveDownTaskCommand;
        public ICommand MoveDownTaskCommand
        {
            get
            {
                if (moveDownTaskCommand is null)
                {
                    moveDownTaskCommand = new RelayCommand(MoveDownTask, param => SelectedTask != null);
                }
                return moveDownTaskCommand;
            }
        }

        private ICommand moveUpTaskCommand;
        public ICommand MoveUpTaskCommand
        {
            get
            {
                if (moveUpTaskCommand is null)
                {
                    moveUpTaskCommand = new RelayCommand(MoveUpTask, param => SelectedTask != null);
                }
                return moveUpTaskCommand;
            }
        }

        private ICommand openFindTaskCommand;
        public ICommand OpenFindTaskCommand
        {
            get
            {
                if (openFindTaskCommand is null)
                {
                    openFindTaskCommand = new RelayCommand(() => NavigatorService.OpenFindTaskView(this, ContextViewModel));
                }
                return openFindTaskCommand;
            }
        }

        private ICommand openSortTasksCommand;
        public ICommand OpenSortTasksCommand
        {
            get
            {
                if (openSortTasksCommand is null)
                {
                    openSortTasksCommand = new RelayCommand(() => NavigatorService.OpenSortTasksView(this));
                }
                return openSortTasksCommand;
            }
        }

        private ICommand openFilterTasksCommand;
        public ICommand OpenFilterTasksCommand
        {
            get
            {
                if (openFilterTasksCommand is null)
                {
                    openFilterTasksCommand = new RelayCommand(() => NavigatorService.OpenFilterTasksView(this));
                }
                return openFilterTasksCommand;
            }
        }

        private ICommand openAboutViewCommand;
        public ICommand OpenAboutViewCommand
        {
            get
            {
                if (openAboutViewCommand is null)
                {
                    openAboutViewCommand = new RelayCommand(() => NavigatorService.OpenAboutView());
                }
                return openAboutViewCommand;
            }
        }

        private ICommand archiveCurrentCommand;
        public ICommand ArchiveCurrentCommand
        {
            get
            {
                if (archiveCurrentCommand is null)
                {
                    archiveCurrentCommand = new RelayCommand(ArchiveCurrent);
                }
                return archiveCurrentCommand;
            }
        }

        private ICommand openDatabaseCommand;
        public ICommand OpenDatabaseCommand
        {
            get
            {
                if (openDatabaseCommand is null)
                {
                    openDatabaseCommand = new RelayCommand(() => NavigatorService.OpenSelectSaveView(this, jsonFileSerializer));
                }
                return openDatabaseCommand;
            }
        }

        public HomeViewModel(ContextViewModel contextViewModel)
        {
            ToDoListItems = new ObservableCollection<TDL>();
            ContextViewModel = contextViewModel ?? throw new ArgumentNullException(nameof(contextViewModel));
            messageBoxService = new MessageBoxService();
            SelectedTdlTasks = new ObservableCollection<MyTask>();
            jsonFileSerializer = new JsonFileSerializer();
            MyTask.DescriptionChanged += Handle_DescriptionChanged;
        }

        private void ArchiveCurrent()
        {
            jsonFileSerializer.Serialize(ToDoListItems);
        }


        private void DeleteTask()
        {
            SelectedTdlTasks.Remove(SelectedTask);
            messageBoxService.ShowInformation("Task deleted successfully!");
            RefreshTasks();
        }

        private async void Handle_DescriptionChanged()
        {
            OnPropertyChanged(nameof(SelectedTask));
            OnPropertyChanged(nameof(SelectedTask.Description));
        }

        private void Delete()
        {
            if (ContextViewModel.SelectedToDoList.Parent == null)
            {
                DeleteTDL(ToDoListItems);
            }
            else
            {
                DeleteTDL(ContextViewModel.SelectedToDoList.Parent.Children);
            }
        }
            private void DeleteTDL(ObservableCollection<TDL> TdlList)
        {
            TdlList.Remove(TdlList.Where(t => t == ContextViewModel.SelectedToDoList).FirstOrDefault());

            messageBoxService.ShowInformation("Succesfully deleted!");
        }

        private void MoveUp()
        {
            if(ContextViewModel.SelectedToDoList.Parent == null)
            {
                MoveUpTDL(ToDoListItems);
            }
            else 
            {
                MoveUpTDL(ContextViewModel.SelectedToDoList.Parent.Children);
            }
        }

        private void MoveUpTDL(ObservableCollection<TDL> TdlList)
        {
            int indexOfTDL = TdlList.ToList().IndexOf(ContextViewModel.SelectedToDoList);

            if(indexOfTDL == 0)
            {
                messageBoxService.ShowError("The TDL cannot be moved higher than that!");
                return;
            }

            var temp = TdlList[indexOfTDL];
            TdlList[indexOfTDL] = TdlList[indexOfTDL - 1];
            TdlList[indexOfTDL - 1] = temp;
        }

        private void MoveDown()
        {
            if (ContextViewModel.SelectedToDoList.Parent == null)
            {
                MoveDownTDL(ToDoListItems);
            }
            else
            {
                MoveDownTDL(ContextViewModel.SelectedToDoList.Parent.Children);
            }
        }

        private void MoveDownTDL(ObservableCollection<TDL> TdlList)
        {
            int indexOfTDL = TdlList.ToList().IndexOf(ContextViewModel.SelectedToDoList);

            if (indexOfTDL == TdlList.Count - 1)
            {
                messageBoxService.ShowError("The TDL cannot be moved lower than that!");
                return;
            }

            var temp = TdlList[indexOfTDL];
            TdlList[indexOfTDL] = TdlList[indexOfTDL + 1];
            TdlList[indexOfTDL + 1] = temp;
        }

        private void SetDone()
        {
            SelectedTask.Status = EStatus.Done;
            SelectedTask.DoneDate = DateTime.Today;
            var selectedTask = SelectedTask;
            SelectedTdlTasks.Remove(selectedTask);
            SelectedTdlTasks.Add(selectedTask);

            NavigatorService.OpenSetDoneView(this);
            RefreshTasks();
        }

        private void MoveDownTask()
        {
            int indexOfTask = SelectedTdlTasks.ToList().IndexOf(SelectedTask);

            if (indexOfTask == SelectedTdlTasks.Count - 1)
            {
                messageBoxService.ShowError("The TDL cannot be moved lower than that!");
                return;
            }

            var temp = SelectedTdlTasks[indexOfTask];
            SelectedTdlTasks[indexOfTask] = SelectedTdlTasks[indexOfTask + 1];
            SelectedTdlTasks[indexOfTask + 1] = temp;
        }

        private void MoveUpTask()
        {
            int indexOfTask = SelectedTdlTasks.ToList().IndexOf(SelectedTask);

            if (indexOfTask == 0)
            {
                messageBoxService.ShowError("The TDL cannot be moved higher than that!");
                return;
            }

            var temp = SelectedTdlTasks[indexOfTask];
            SelectedTdlTasks[indexOfTask] = SelectedTdlTasks[indexOfTask - 1];
            SelectedTdlTasks[indexOfTask - 1] = temp;
        }

        public async Task RefreshTasks()
        {
            TasksDueToday = 0;
            TasksDueTomorrow = 0;
            TasksOverdue = 0;
            TasksDone = 0;
            TasksToBeDone = 0;

            foreach (var tdl in ToDoListItems)
            {
                foreach (var child in tdl.Children)
                {
                    TasksDueToday += child.Tasks.Where(t => t.Deadline == DateTime.Today && !t.IsDone).ToList().Count;
                    TasksDueTomorrow += child.Tasks.Where(t => t.Deadline == DateTime.Today.AddDays(1) && !t.IsDone).ToList().Count;
                    TasksOverdue += child.Tasks.Where(t => t.DoneDate > t.Deadline && t.IsDone).ToList().Count;
                    TasksDone += child.Tasks.Where(t => t.Status == EStatus.Done).ToList().Count;
                    TasksToBeDone += child.Tasks.Where(t => t.Status != EStatus.Done).ToList().Count;
                }
                TasksDueToday += tdl.Tasks.Where(t => t.Deadline == DateTime.Today && !t.IsDone).ToList().Count;
                TasksDueTomorrow += tdl.Tasks.Where(t => t.Deadline == DateTime.Today.AddDays(1) && !t.IsDone).ToList().Count;
                TasksOverdue += tdl.Tasks.Where(t => t.DoneDate > t.Deadline && t.IsDone).ToList().Count;
                TasksDone += tdl.Tasks.Where(t => t.Status == EStatus.Done).ToList().Count;
                TasksToBeDone += tdl.Tasks.Where(t => t.Status != EStatus.Done).ToList().Count;
            }

        }

        public void Dispose()
        {
            MyTask.DescriptionChanged -= Handle_DescriptionChanged;
        }
    }
}
