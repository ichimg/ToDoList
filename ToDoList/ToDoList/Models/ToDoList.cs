using System;
using System.Collections.Generic;


namespace ToDoList.Models
{
    public class ToDoList
    {
        public string Name { get; }
        public string IconPath { get; }
        public List<ToDoList> Childrens { get; }
        public List<Task> Tasks { get; }

        public ToDoList(string name, string iconPath, List<ToDoList> childrens, List<Task> tasks)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(iconPath)) throw new ArgumentNullException(nameof(iconPath));
            Name = name;
            IconPath = iconPath;
            Childrens = childrens;
            Tasks = tasks;
        }
    }
}
