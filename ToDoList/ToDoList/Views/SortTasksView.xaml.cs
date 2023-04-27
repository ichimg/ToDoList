using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for SortTasksView.xaml
    /// </summary>
    public partial class SortTasksView : Window
    {
        public SortTasksView(SortTasksViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        private void DeadlineClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PriorityClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
