﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfPrototype.additionalLogic.entities
{
    public class DocAttribute : NotifyPropertyChangedBase
    {

        //public String Name { get; set; }
        //public String Value { get; set; }
        //public String Type { get; set; }
        //public int StartingXLocation { get; set; }
        //public int StartingYLocation { get; set; }
        //public int EndingXLocation { get; set; }
        //public int EndingYLocation { get; set; }
        private String _Name;
        public String Name { get { return _Name; } set { _Name = value; RaisePropertyChanged(nameof(Name)); } }

        //public String Value { get; set; }
        private String _Value;
        public String Value { get { return _Value; } set { _Value = value; RaisePropertyChanged(nameof(Value)); } }

        private String _Type;
        public String Type { get { return _Type; } set { _Type = value; RaisePropertyChanged(nameof(Value)); } }
        private int _StartingXLocation;
        public int StartingXLocation { get { return _StartingXLocation; } set { _StartingXLocation = value; RaisePropertyChanged(nameof(StartingXLocation)); } }
        private int _StartingYLocation;
        public int StartingYLocation { get { return _StartingYLocation; } set { _StartingYLocation = value; RaisePropertyChanged(nameof(StartingYLocation)); } }
        private int _EndingXLocation;
        public int EndingXLocation { get { return _EndingXLocation; } set { _EndingXLocation = value; RaisePropertyChanged(nameof(EndingXLocation)); } }
        private int _EndingYLocation;
        public int EndingYLocation { get { return _EndingYLocation; } set { _EndingYLocation = value; RaisePropertyChanged(nameof(EndingYLocation)); } }

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
    }
}
