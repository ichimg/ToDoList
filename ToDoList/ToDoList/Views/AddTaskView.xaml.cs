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
    /// Interaction logic for AddTaskView.xaml
    /// </summary>
    public partial class AddTaskView : Window
    {
        public AddTaskView(AddOrEditTaskViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));  
        }
    }
}
