using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoList.ViewModels;

namespace ToDoList.Models
{
    public class TDL : ModelBase, IEquatable<TDL>
    {

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
             name = value;
             OnPropertyChanged(nameof(Name));
            }
        }
        [JsonIgnore]
        public TDL? Parent { get; set; }
        public string IconPath { get; set; }
        public ObservableCollection<TDL>? Children { get; }
        public ObservableCollection<MyTask>? Tasks { get; }

        public TDL(TDL parent, string name, string iconPath)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            //if (string.IsNullOrEmpty(iconPath)) throw new ArgumentNullException(nameof(iconPath));
            Parent = parent;
            Name = name;
            IconPath = iconPath;
            Children = new ObservableCollection<TDL>();
            Tasks = new ObservableCollection<MyTask>();
        }

        public bool Equals(TDL? other)
        {
            return Name == other.Name && IconPath == other.IconPath;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, IconPath);
        }
    }
}
