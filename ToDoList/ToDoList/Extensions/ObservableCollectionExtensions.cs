using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
        }

       public static List<MyTask> CloneTasks(this ObservableCollection<MyTask> originalCollection)
        {
            List<MyTask> copiedList = new List<MyTask>();
            foreach (MyTask t in originalCollection)
            {
                MyTask task = (MyTask)t.Clone();
                copiedList.Add(task);
            }

            return copiedList;
        }
    }
}

