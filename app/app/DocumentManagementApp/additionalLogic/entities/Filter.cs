using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementApp.additionalLogic.entities
{
    public class Filter : NotifyPropertyChangedBase
    {
        private String _Title;

        public String Title { get { return _Title; } set { _Title = value; RaisePropertyChanged(nameof(Title)); } }
        private String _Type;
        public String Type { get { return _Type; } set { _Type = value; RaisePropertyChanged(nameof(Type)); } }
        private String _Value;
        public String Value { get { return _Value; } set { _Value = value; RaisePropertyChanged(nameof(Value)); } }


        private BindingList<String> _FiltersTitle;
        public BindingList<String> FiltersTitle { get { return _FiltersTitle; } set { _FiltersTitle = value; RaisePropertyChanged(nameof(FiltersTitle)); } }

        private BindingList<String> _FiltersType;
        public BindingList<String> FiltersType { get { return _FiltersType; } set { _FiltersType = value; RaisePropertyChanged(nameof(FiltersType)); } }

        [JsonConstructor]
        public Filter()
        {
            FiltersTitle = new BindingList<String> { "Item", "Amount" };
            FiltersType = new BindingList<string> { "rgx", ">", "<" };
            Title = FiltersTitle[0];
            Type = FiltersType[0];
            Value = "";
        }
        public Filter(String title, String type, String value)
        {
            FiltersTitle = new BindingList<String> { "Item", "Amount"};
            FiltersType = new BindingList<string> { "rgx", ">", "<" };
            Title = title;
            Type = type;
            Value = value;
        }
    }
}
