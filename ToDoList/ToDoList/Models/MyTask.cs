using System;

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
    public class MyTask : ModelBase, IEquatable<MyTask>, ICloneable
    {
        public static event Action DescriptionChanged;
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
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                DescriptionChanged?.Invoke();
                OnPropertyChanged(nameof(Description));
            }
        }

        private EStatus status;
        public EStatus Status
        { 
            get
            {
                return status;
            }

            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(IsDone));
            }
        
        }

        private EPriority priority;
        public EPriority Priority
        {
            get
            {
                return priority;
            }

            set
            {
                priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        private DateTime deadline;
        public DateTime Deadline 
        { 
            get
            {
                return deadline;
            }

            set
            {
                deadline = value;
                OnPropertyChanged(nameof(Deadline));
            }
        }

        private DateTime doneDate;
        public DateTime DoneDate 
        { 
            get
            {
                return doneDate;
            }

            set
            {
                doneDate = value;
                OnPropertyChanged(nameof(DoneDate));
            }
        }

        private bool isDone => Status == EStatus.Done;
        public bool IsDone
        {
            get { return isDone; }

        }


        public MyTask(string name, string description, EStatus status, EPriority priority, DateTime deadline)
        { 
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));
            Name = name;
            Description = description;
            Status = status;
            Priority = priority;
            Deadline = deadline;
        }

        public MyTask()
        {

        }

        public bool Equals(MyTask? other)
        {
            return Name == other.Name && Description == other.Description
                && Status == other.Status && Priority == other.Priority 
                && Deadline == other.Deadline;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Status, Priority, Deadline);
        }

        public object Clone()
        {
            MyTask newTask = new MyTask
            {
                Name = this.Name,
                Description = this.Description,
                Status = this.Status,
                Priority = this.Priority,
                Deadline = this.Deadline,
                DoneDate = this.DoneDate
            };
            return newTask;
        }
    }
}
