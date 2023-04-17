using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public enum EStatus
    {
        Created, 
        InProgress,
        Done
    }

    public enum EPriority
    {
        Low,
        Medium,
        High
    }
    public class Task
    {
        public string Name { get; }
        public string Description { get; }
        public EStatus Status { get; }
        public EPriority Priority {get; }
        public DateTime Deadline { get; }
        public DateTime? DoneDate { get; }

        public Task(string name, string description, EStatus status, EPriority priority, DateTime deadline)
        { 
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));
            Name = name;
            Description = description;
            Status = status;
            Priority = priority;
            Deadline = deadline;
        }
    }
}
