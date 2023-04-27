using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccess;
using ToDoList.ViewModels;

namespace ToDoList.Services
{
    interface IWindowService
    {
        void ShowWindow(object viewModel);
        void ShowAddOrEditToDoListView(HomeViewModel mainViewViewModel, ContextViewModel contextViewModel, bool isEditing);
        void ShowAddOrEditTaskView(HomeViewModel mainViewViewModel, ContextViewModel contextViewModel, bool isEditing);
        void ShowSetDone(HomeViewModel mainViewViewModel);
        void ShowFindTaskView(HomeViewModel mainViewViewModel, ContextViewModel contextViewModel);
        void ShowSortTasksView(HomeViewModel mainViewViewModel);
        void ShowFilterTasksView(HomeViewModel mainViewViewModel);
        void ShowAboutView();
        void ShowSelectSaveView(HomeViewModel mainViewViewModel, JsonFileSerializer jsonFileSerializer);
    }
}
