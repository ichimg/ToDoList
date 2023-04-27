using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ToDoList.Commands;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class AddOrEditToDoListViewModel : ViewModelBase
    {
        public static List<Icon> IconList { get; set; }
        private readonly HomeViewModel homeViewModel;
        private readonly ContextViewModel contextViewModel;
        private readonly IMessageBoxService messageBoxService;

        private bool isEditing;

        public AddOrEditToDoListViewModel(HomeViewModel homeViewModel, ContextViewModel contextViewModel, bool isEditing)
        {
            this.homeViewModel = homeViewModel ?? throw new ArgumentNullException(nameof(homeViewModel));
            this.contextViewModel = contextViewModel;
            messageBoxService = new MessageBoxService();

            IconList = Directory.GetFiles(@"../../../Resources/Icons/")
             .Select(x => new Icon("Icon", new BitmapImage(new Uri("/ToDoList;component" + x.Substring(8), UriKind.Relative)))).ToList();
            this.isEditing = isEditing;
        }

        private string tdlName;
        public string TdlName
        {
            get
            { return tdlName; }

            set
            {
                if(!string.Equals(tdlName, value))
                {
                    tdlName = value;
                    OnPropertyChanged(nameof(TdlName));
                }
            }

        }

        private string tdlIconPath;
        public string TdlIconPath
        {
            get
            { return tdlIconPath; }

            set
            {
                if (!string.Equals(tdlIconPath, value))
                {
                    tdlIconPath = value;
                    OnPropertyChanged(nameof(TdlIconPath));
                }
            }
        }

        private ICommand addOrEditTDLCommand;
        public ICommand AddOrEditTDLCommand
        {
            get
            {
                if (addOrEditTDLCommand is null)
                {
                    if (contextViewModel.SelectedToDoList is null)
                    {
                        addOrEditTDLCommand = new RelayCommand(AddRootTDL, param => ValidateFormService.ValidateTdlForm(TdlName));
                    }
                    else
                    {
                        if (isEditing)
                        {
                            TdlName = contextViewModel.SelectedToDoList.Name;
                            addOrEditTDLCommand = new RelayCommand(EditTDL, param => ValidateFormService.ValidateTdlForm(TdlName)); 
                        }
                        else
                        {
                            addOrEditTDLCommand = new RelayCommand(AddSubTDL, param => ValidateFormService.ValidateTdlForm(TdlName));
                        }
                    }
                }
                return addOrEditTDLCommand;
            }
        }

        private void AddRootTDL()
        {
            var duplicates = homeViewModel.ToDoListItems.Where(t => t.Name == tdlName).ToList();
            if (duplicates.Count != 0)
            { 
                messageBoxService.ShowError("TDL already exists!");
                return;
            }
           homeViewModel.ToDoListItems.Add(new TDL(null, TdlName, TdlIconPath));
           messageBoxService.ShowInformation("TDL added succesfully!");
        }

        private void AddSubTDL()
        {
            var subTdl = contextViewModel.SelectedToDoList;
            var duplicates = homeViewModel.ToDoListItems
                .Where(t => t.Name == subTdl.Name)?
                .FirstOrDefault()?.Children?
                .Where(c => c.Name == TdlName)?.ToList();


            if (duplicates?.Count != 0 && duplicates is not null)
            {
                messageBoxService.ShowError("TDL already exists!");
                return;
            }

            subTdl.Children.Add(new TDL(subTdl, TdlName, TdlIconPath));
            messageBoxService.ShowInformation("TDL added succesfully!");
        }

        private void EditTDL()
        {
            contextViewModel.SelectedToDoList.Name = TdlName;
            contextViewModel.SelectedToDoList.IconPath = TdlIconPath;
            messageBoxService.ShowInformation("TDL edited succesfully!");
        }

    }
}
