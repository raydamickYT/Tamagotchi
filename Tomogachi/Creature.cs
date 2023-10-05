using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tomogachi
{
    public class Creature : INotifyPropertyChanged
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = string.Empty;
        public float Hunger { get; set; } = 1f;
        public float Thirst { get; set; } = 1f;
        public float Boredom { get; set; } = 0f;
        public float Loneliness { get; set; } = .5f;
        public float Tired { get; set; } = 1f;
        public bool Sleeping { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
