using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tomogachi
{
    public class Creature : INotifyPropertyChanged
    {
        public float Hunger { get; set; } = 0;
        public float Thirst { get; set; } = 0;
        public string Name { get; set; } = "Vincent";
        public int Id { get; set; } = 1;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
