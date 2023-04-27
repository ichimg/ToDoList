using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.DataObjects
{
    public class MyTaskDTO
    {
        public string Name { get; set; }
        public string? Location { get; set; }

        public TDL ParentTdl { get; set; }

        public MyTaskDTO(string name, TDL parent) 
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
            ParentTdl = parent ?? throw new ArgumentNullException(nameof(parent));
            Location = SetLocation();
        }

        private string? SetLocation()
        {
            string location = "";
            List<TDL> tree = new List<TDL>
            {
                ParentTdl
            };

            while(ParentTdl.Parent != null)
            {
                tree.Add(ParentTdl.Parent);
                ParentTdl = ParentTdl.Parent;
            }

            tree.Reverse();

            for(int i = 0; i < tree.Count; i++) 
            {
                location += $"{tree[i].Name}";
                if(i != tree.Count - 1)
                    location += " -> ";
            }
            return location;
        }
    }
}
