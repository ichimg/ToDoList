using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    public static class ValidateFormService
    {
        public static bool ValidateTdlForm(string name)
        {
            if(string.IsNullOrEmpty(name)) return false;

            return true;
        }

        public static bool ValidateTaskForm(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (string.IsNullOrEmpty(description)) return false;

            return true;
        }
    }
}
