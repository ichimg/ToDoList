using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    public interface IMessageBoxService
    {
        void ShowError(string messageBoxText);

        void ShowWarning(string messageBoxText);

        void ShowInformation(string messageBoxText);
    }
}
