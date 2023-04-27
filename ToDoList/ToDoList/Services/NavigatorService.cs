using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccess;
using ToDoList.ViewModels;

namespace ToDoList.Services
{
    public static class NavigatorService
    {
        private static IWindowService windowService;

        static NavigatorService() 
        { 
            windowService = new WindowService();
        }

        public static void OpenAddOrEditTDL(HomeViewModel homeViewModel, ContextViewModel contextViewModel, bool isEditing)
        {
            windowService.ShowAddOrEditToDoListView(homeViewModel, contextViewModel, isEditing);
        }

        public static void OpenAddOrEditTask(HomeViewModel homeViewModel, ContextViewModel contextViewModel, bool isEditing)
        {
            windowService.ShowAddOrEditTaskView(homeViewModel, contextViewModel, isEditing);
        }

        public static void OpenSetDoneView(HomeViewModel homeViewModel)
        {
            windowService.ShowSetDone(homeViewModel);
        }

        public static void OpenFindTaskView(HomeViewModel homeViewModel, ContextViewModel contextViewModel)
        {
            windowService.ShowFindTaskView(homeViewModel, contextViewModel);
        }

        public static void OpenSortTasksView(HomeViewModel homeViewModel)
        {
            windowService.ShowSortTasksView(homeViewModel);
        }

        public static void OpenFilterTasksView(HomeViewModel homeViewModel)
        {
            windowService.ShowFilterTasksView(homeViewModel);
        }

        public static void OpenAboutView()
        {
            windowService.ShowAboutView();
        }

        public static void OpenSelectSaveView(HomeViewModel homeViewModel, JsonFileSerializer jsonFileSerializer)
        {
            windowService.ShowSelectSaveView(homeViewModel, jsonFileSerializer);
        }
    }
}
