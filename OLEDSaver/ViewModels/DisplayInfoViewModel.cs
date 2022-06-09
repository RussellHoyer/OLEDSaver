using OLEDSaver.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OLEDSaver.ViewModels
{
    public class DisplayInfoViewModel : BindableBase
    {
        WindowConfigSettingsModel windowConfigSettings;

        public DisplayInfoViewModel()
        {
            windowConfigSettings = new WindowConfigSettingsModel(); 
            SetDisplayData();
        }

        #region Bindable Properties

        private double _displayWidth;
        public double DisplayWidth
        {
            get { return _displayWidth; }
            set { _displayWidth = value; RaisePropertyChanged(); }
        }
        private double _displayHeight;
        public double DisplayHeight
        {
            get { return _displayHeight; }
            set { _displayHeight = value; RaisePropertyChanged(); }
        }
        private string _displayResolutionText;
        public string DisplayResolutionText
        {
            get { return _displayResolutionText; }
            set { _displayResolutionText = value; }
        }


        private double _windowWidth;
        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; RaisePropertyChanged(); }
        }
        private double _windowHeight;
        public double WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; RaisePropertyChanged(); }
        }
        private string _windowResolutionText;
        public string WindowResolutionText
        {
            get { return _windowResolutionText; }
            set { _windowResolutionText = value; RaisePropertyChanged(); }
        }

        #endregion
        #region Methods

        private void SetDisplayData()
        {
            DisplayWidth = SystemParameters.PrimaryScreenWidth;
            DisplayHeight = SystemParameters.PrimaryScreenHeight;
            WindowWidth = windowConfigSettings.OriginalWidth;
            WindowHeight = windowConfigSettings.OriginalHeight;

            WindowResolutionText = $"Current dimensions: {WindowWidth}x{WindowHeight}";
            DisplayResolutionText = $"Display dimensions: {DisplayWidth}x{DisplayHeight}";
        }

        #endregion
    }
}
