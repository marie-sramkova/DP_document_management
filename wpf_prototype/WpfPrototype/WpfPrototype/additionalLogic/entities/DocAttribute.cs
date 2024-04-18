﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPrototype.additionalLogic.entities
{
    public class DocAttribute : INotifyPropertyChanged
    {

        public String Name { get; set; }
        //public String Name { get { return Name; } set { Name = value; OnPropertyChanged(); } }
        public String Value { get; set; }
        public String Type { get; set; }
        public int StartingXLocation { get; set; }
        public int StartingYLocation { get; set; }
        public int EndingXLocation { get; set; }
        public int EndingYLocation { get; set; }

        public DocAttribute(string name, string value, string type, int startingXLocation, int startingYLocation, int endingXLocation, int endingYLocation)
        {
            Name = name;
            Value = value;
            Type = type;
            StartingXLocation = startingXLocation;
            StartingYLocation = startingYLocation;
            EndingXLocation = endingXLocation;
            EndingYLocation = endingYLocation;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
