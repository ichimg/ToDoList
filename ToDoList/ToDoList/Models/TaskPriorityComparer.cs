using System;
using System.Collections.Generic;


namespace ToDoList.Models
{
    class TaskPriorityComparer : IComparer<MyTask>
    {
        public int Compare(MyTask? x, MyTask? y)
        {
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
