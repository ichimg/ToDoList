using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    class TaskDeadlineComparer : IComparer<MyTask>
    {
        public int Compare(MyTask? x, MyTask? y)
        {
            return x.Deadline.CompareTo(y.Deadline);
        }
    }
}
