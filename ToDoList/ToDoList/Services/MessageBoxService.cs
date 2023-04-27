using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoList.Services
{
    internal class MessageBoxService : IMessageBoxService
    {
        public void ShowError(string messageBoxText)
        {
            MessageBox.Show(messageBoxText, "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error); ;
        }

        public void ShowInformation(string messageBoxText)
        {
            MessageBox.Show(messageBoxText, "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
        }

        public void ShowWarning(string messageBoxText)
        {
            MessageBox.Show(messageBoxText, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
        }
    }
}
