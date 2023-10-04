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
        public float boredom { get; set; } = 1f;
        //ik heb loneliness verkeerd geschreven, maar ik heb het nu op zoveel plekken in mn script zitten, dat ik bang ben dat ik mn code breek als ik het nu nog aanpas.
        public float Loneliness { get; set; } = .5f;
        public float tired { get; set; } = 1f;
        public string Name { get; set; } = "Vincent";
        public int Id { get; set; } = 1;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
