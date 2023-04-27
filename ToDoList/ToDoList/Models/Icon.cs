using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ToDoList.Models
{
    public class Icon
    { 
        public string Name { get; set; }
        public ImageSource IconImage { get; set; }

        public Icon(string name, ImageSource iconImage)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
            IconImage = iconImage ?? throw new ArgumentNullException(nameof(iconImage));
        }
    }
}
