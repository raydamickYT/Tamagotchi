using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tomogachi
{
    public class Creature : INotifyPropertyChanged
    {
        public float Hunger { get; set; } = 1f;
        public float Thirst { get; set; } = 1f;
        public string Name { get; set; } = "Vincent";
        public int Id { get; set; } = 1;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
