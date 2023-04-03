﻿using System.Windows.Controls;
using System.Windows;
using JCS;
using System.Diagnostics;
using System;
using System.Linq;

namespace FrontLineGUI
{

    // Scan Page
    // Used to provide core scanning functionality
    public partial class Scan : Page
    {

        // Public class vars
        public CPUUtilization CPUInfo { get; set; }
        public ScanItemsCollection ScanItemsObject { get; set; }

        #region Constructor

        // Constructor
        public Scan()
        {

            // Required for OSIcon
            DataContext = this;

            // Define CPUInfo
            CPUInfo = new CPUUtilization(56, 10, 25, 11);

            // RPECK 26/03/2023
            // Scan Items Collection
            // Presents an ObservableListCollection of "ScanItem" classes
            ScanItemsObject = new ScanItemsCollection()
            {
                new ScanItem("Registry Errors", "Clean registry errors.", true, "/Resources/Scan/registry_errors.png"),
                new ScanItem("Invalid Files", "Clear invalid file errors.", true, "/Resources/Scan/invalid_files.png"),
                new ScanItem("Application Errors", "Remove old application settings.", true, "/Resources/Scan/application_errors.png"),
                new ScanItem("DLL Errors", "Fix orphaned DLL entries.", true, "/Resources/Scan/dll_errors.png"),
                new ScanItem("Internet Errors", "Fix web browser errors.", true, "/Resources/Scan/internet_errors.png"),
                new ScanItem("Windows Errors", "Resolve Windows registry errors.", true, "/Resources/Scan/windows_errors.png"),
                new ScanItem("Temp Files", "Clear Windows temp files.", true, "/Resources/Scan/temp_files.png"),
                new ScanItem("Junk Files", "Remove Windows junk files.", true, "/Resources/Scan/recycle_bin.png"),
                new ScanItem("Internet Cache", "Clear privacy data from browser caches.", true, "/Resources/Scan/internet_cache.png")
            };

            // Initialize the form
            InitializeComponent();

            // RPECK 28/03/2023
            // Allows us to bind to the CPUInfo object
            CPUInformation.DataContext = CPUInfo;

            // RPECK 02/04/2023
            // Bind to OSVersionInfo
            OSName.Text    = OSVersionInfo.Name + " (" + OSVersionInfo.OSBits.ToString().Remove(0,3) + "bit)";
            OSEdition.Text = OSVersionInfo.VersionString.ToString();

            // RPECK 02/04/2023
            // Update CPU infos
            CPUInfo.RAMPower = 90;

        }

        #endregion

        #region Methods

        // OS Icon
        // Shows the image at the bottom of the page (next to the OSVersion)
        public string OSIcon
        {
            get
            {

                // OSVersion
                // Get the version of the OS from the above method and then extract the version of Windows from it
                // https://stackoverflow.com/a/5651002/1143732
                return "/Resources/OS/" + OSVersionInfo.Name.Replace(" ", "-").ToLower() + ".png";

            }
        }

        #endregion

        #region XAMLEvents

        // RPECK 28/03/2023
        // Fire once form has loaded

        public void ScanForm_Loaded(object sender, RoutedEventArgs e)
        {

            // CPUID
            // Initialize ticker if CPUID is present
            if (App.ConfigOptions.CPUID)
            {

                string s;
                float fValue;
                int dummy = 0;

                // RPECK 26/03/2023
                // Class instance to return the values for each of the 4 properties of the system (CPU, RAM, HDD, GPU)
                fValue = App.pSDK.GetSensorTypeValue(CPUIDSDK.SENSOR_UTILIZATION_CPU, ref dummy, ref dummy);
                if (Math.Round(fValue, 0) >= 0)
                {
                    s = Convert.ToString(Math.Round(fValue, 1));
                    s = s + "  %";
                    Debug.Write(s);
                }

            }

        }

        // Select All Button Click
        public void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {

            if (App.pSDK != null)
            {
                string s;
                float fValue;
                int dummy = 0;

                // RPECK 26/03/2023
                // Class instance to return the values for each of the 4 properties of the system (CPU, RAM, HDD, GPU)
                fValue = App.pSDK.GetSensorTypeValue(CPUIDSDK.SENSOR_UTILIZATION_CPU, ref dummy, ref dummy);
                Debug.Write(fValue);
                if (Math.Round(fValue, 0) >= 0)
                {
                    s = Convert.ToString(Math.Round(fValue, 1));
                    s = s + "  %";
                    Debug.Write(s);
                }
            }

            // Find ListBox
            ListBox ScanItems = FindName("ScanItemsElement") as ListBox;

            // If present, selectAll else deselect everything
            if (ScanItems.SelectedItems.Count != ScanItems.Items.Count)
            {
                ScanItems.SelectAll();
            }
            else
            {
                ScanItems.UnselectAll();
            }

        }

        // Delete ScanItem Click
        public void ScanItemDelete_Click(object sender, RoutedEventArgs e)
        {
            Debug.Write("Test");
        }

    }

    #endregion

}
