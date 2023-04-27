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
    /// Interaction logic for SelectSaveView.xaml
    /// </summary>
    public partial class SelectSaveView : Window
    {
        public SelectSaveView(SelectSaveViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel ?? throw new ArgumentException(nameof(viewModel));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
