using System.Windows;
using System.Windows.Controls;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private readonly ContextViewModel contextViewModel;
        private readonly HomeViewModel homeViewModel;

        public HomeView()
        {
            contextViewModel = new ContextViewModel();
            InitializeComponent();
            homeViewModel = new HomeViewModel(contextViewModel);
            DataContext = homeViewModel;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            contextViewModel.SelectedToDoList = e.NewValue as TDL;
            homeViewModel.SelectedTdlTasks = contextViewModel.SelectedToDoList?.Tasks;
        }

    }
}
