﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineGUI
{
    // RPECK 26/03/2023
    // ScanItem class for the 'scan' page -- allows us to populate a datagrid without having to define the items manually
    // Underlying benefit is that it gives us the means to extend the underlying scan feature as needed
    public class ScanItem
    {
        // Internal values
        //private enum scanItemType { File, Setting }; // this is a choice between file or setting when using the scanner 
        private string name;                 // name of the scanning item
        private string label;                // label of the item
        private string image;                // filename of image icon
        private bool   enabled;              // is the item enabled?
        private bool   is_selected = true;   // primarily used for the frontend

        // Constructor
        // https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-create-and-bind-to-an-observablecollection?view=netframeworkdesktop-4.8
        public ScanItem(string name, string label, bool enabled = true, string image = null)
        {
            this.name  = name;
            this.enabled = enabled;
            this.label = label;
            if(image != null) this.image = image;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public bool IsEnabled
        {
            get { return enabled; }
            set { enabled = value;  }
        }

        public bool IsSelected
        {
            get { return is_selected;  }
            set { is_selected = value; }
        }

    }

}
