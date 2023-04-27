using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.DataAccess;
using ToDoList.ViewModels;
using ToDoList.Views;

namespace ToDoList.Services
{
    class WindowService : IWindowService
    {
        public static event Action EditFormChanged;
        public void ShowWindow(object viewModel)
        {
            var win = new Window()
            {
                Height = 250,
                Width = 400,
                Content = viewModel
            };
            win.ShowDialog();
        }
        public void ShowAddOrEditToDoListView(HomeViewModel homeViewModel, ContextViewModel contextViewModel, bool isEditing)
        {
            AddOrEditToDoListViewModel addToDoListViewModel = new AddOrEditToDoListViewModel(homeViewModel, contextViewModel, isEditing);
            var win = new AddOrEditTDLView(addToDoListViewModel);
            win.ShowDialog();
        }

        public void ShowAddOrEditTaskView(HomeViewModel homeViewModel, ContextViewModel contextViewModel, bool isEditing)
        {
            AddOrEditTaskViewModel addOrEditTaskViewModel = new AddOrEditTaskViewModel(homeViewModel, contextViewModel, isEditing);
            var win = new AddTaskView(addOrEditTaskViewModel);

            if (isEditing)
            { 
                EditFormChanged?.Invoke();
            }

            win.ShowDialog();
        }

        public void ShowSetDone(HomeViewModel homeViewModel)
        {
            SetDoneViewModel viewModel = new SetDoneViewModel(homeViewModel);
            var win = new SetDoneView(viewModel);
            win.ShowDialog();
        }

        public void ShowFindTaskView(HomeViewModel homeViewModel, ContextViewModel contextViewModel)
        {
            FindTaskViewModel viewModel = new FindTaskViewModel(homeViewModel, contextViewModel);
            var win = new FindTaskView(viewModel);
            win.ShowDialog();
        }

        public void ShowSortTasksView(HomeViewModel homeViewModel)
        {
            SortTasksViewModel viewModel = new SortTasksViewModel(homeViewModel);
            var win = new SortTasksView(viewModel);
            win.ShowDialog();
        }

        public void ShowFilterTasksView(HomeViewModel homeViewModel)
        {
            FilterTasksViewModel viewModel = new FilterTasksViewModel(homeViewModel);
            var win = new FilterTasksView(viewModel);
            win.ShowDialog();
        }

        public void ShowAboutView()
        {
            var win = new AboutView();
            win.ShowDialog();
        }
       public void ShowSelectSaveView(HomeViewModel homeViewModel, JsonFileSerializer jsonFileSerializer)
        {
            SelectSaveViewModel selectSaveViewModel = new SelectSaveViewModel(homeViewModel, jsonFileSerializer);
            var win = new SelectSaveView(selectSaveViewModel);
            win.ShowDialog();
        }
    }
}
