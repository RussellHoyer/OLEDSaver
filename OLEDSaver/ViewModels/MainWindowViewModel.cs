using OLEDSaver.Models;
using Prism.Mvvm;
using System.Windows;

namespace OLEDSaver.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        WindowConfigSettingsModel windowConfigSettings;
        public MainWindowViewModel()
        {
            // property init
            windowConfigSettings = new WindowConfigSettingsModel();

            // command init


            // Everything else
            SetWindowDimensions();
        }

        #region Bindable Properties

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

        #endregion

        #region Methods
        private void SetWindowDimensions()
        {
            WindowWidth = windowConfigSettings.OriginalWidth;
            WindowHeight = windowConfigSettings.OriginalHeight;
        }
        #endregion
    }
}
