using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.DataAccess;
using ToDoList.Extensions;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class SelectSaveViewModel : ViewModelBase
    {
        private readonly HomeViewModel homeViewModel;
        private readonly JsonFileSerializer jsonFileSerializer;
        private readonly IMessageBoxService messageBoxService;

        private ObservableCollection<string> saves;
        public ObservableCollection<string> Saves 
        {
            get
            {
                return saves;
            }
            set
            {
                saves = value;
                OnPropertyChanged(nameof(Saves));
            }
        }

        private string selectedSave;
        public string SelectedSave
        {
            get { return selectedSave; }
            set
            {
                selectedSave = value;
                OnPropertyChanged(nameof(SelectedSave));
            }
        }

        private ICommand launchSaveCommand;
        public ICommand LaunchSaveCommand
        {
            get
            {
                if (launchSaveCommand is null)
                {
                    launchSaveCommand = new RelayCommand(LaunchSave, param => !string.IsNullOrEmpty(SelectedSave));
                }
                return launchSaveCommand;
            }
        }

        public SelectSaveViewModel(HomeViewModel homeViewModel, JsonFileSerializer jsonFileSerializer)
        {
            this.homeViewModel = homeViewModel ?? throw new ArgumentNullException(nameof(homeViewModel));
            this.jsonFileSerializer = jsonFileSerializer ?? throw new ArgumentNullException(nameof(jsonFileSerializer));
            var list = Directory.GetFiles(@"../../../Saves/").Select(x => x.Substring(15));
            saves = new ObservableCollection<string>();
            saves.AddRange(list);
            messageBoxService = new MessageBoxService();
        }

        private void LaunchSave()
        {
            var list = jsonFileSerializer.Deserialize<TDL>(SelectedSave).ToList();
            homeViewModel.ToDoListItems.AddRange(list);
            messageBoxService.ShowInformation("Save loaded successfully!");
            homeViewModel.RefreshTasks();
        }
    }
}
