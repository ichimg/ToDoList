using System;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class ContextViewModel : ViewModelBase
    {
        private TDL selectedToDoList;
        public TDL SelectedToDoList
        {
            get
            { return selectedToDoList; }

            set
            {
                if (!Equals(selectedToDoList, value))
                {
                    selectedToDoList = value;
                    OnPropertyChanged(nameof(SelectedToDoList));
                }
            }
        }


    }
}
