using OLEDSaver.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.CodeDom;
using System.Windows;

namespace OLEDSaver.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        WindowConfigSettingsModel windowConfigSettings;

        private DelegateCommand _toggleWindowStateCommand;
        public DelegateCommand ToggleWindowStateCommand { get { return _toggleWindowStateCommand; } }

        private DelegateCommand _toggleWindowStyleCommand;
        public DelegateCommand ToggleWindowStyleCommand { get { return _toggleWindowStyleCommand; } }

        private DelegateCommand _toggleGUICommand;
        public DelegateCommand ToggleGUICommand { get { return _toggleGUICommand; } }

        private DelegateCommand _toggleBlackoutCommand;
        public DelegateCommand ToggleBlackoutCommand { get { return _toggleBlackoutCommand; } }

        private DelegateCommand _exitApplicationCommand;
        public DelegateCommand ExitApplicationCommand { get { return _exitApplicationCommand; } }

        public MainWindowViewModel()
        {
            // property init
            windowConfigSettings = new WindowConfigSettingsModel();

            // command init
            _exitApplicationCommand = new DelegateCommand(OnExitApplication);
            _toggleWindowStateCommand = new DelegateCommand(ToggleWindowState);
            _toggleWindowStyleCommand = new DelegateCommand(ToggleWindowStyle);
            _toggleGUICommand = new DelegateCommand(ToggleGuiVisibility);
            _toggleBlackoutCommand = new DelegateCommand(OnBlackout);

            // Everything else
            SetWindowDimensions();

            // Defaults
            DisplayGui = true;
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Normal;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
        
        private WindowStyle _windowStyle;        
        public WindowStyle WindowStyle
        {
            get { return _windowStyle; }
            set { _windowStyle = value; RaisePropertyChanged(); }
        }
        private WindowState _windowState;
        public WindowState WindowState
        {
            get { return _windowState; }
            set { _windowState = value; RaisePropertyChanged(); }
        }
        private WindowStartupLocation _windowStartupLocation;
        public WindowStartupLocation WindowStartupLocation
        {
            get { return _windowStartupLocation; }
            set { _windowStartupLocation = value; RaisePropertyChanged(); }
        }

        private bool _displayGui;
        public bool DisplayGui
        {
            get { return _displayGui; }
            set { _displayGui = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Methods

        private void SetWindowDimensions()
        {
            WindowWidth = windowConfigSettings.OriginalWidth;
            WindowHeight = windowConfigSettings.OriginalHeight;
        }
        private void OnBlackout()
        {
            ToggleWindowState();
            ToggleWindowStyle();
        }

        private void OnExitApplication()
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Toggles between the Visible and Hidden Visibility state for the <see cref="MainWindowGrid"/>.
        /// </summary>
        private void ToggleGuiVisibility()
        {
            if (DisplayGui)
                DisplayGui = false;
            else
                DisplayGui = true;
        }

        /// <summary>
        /// Toggles between the SingleBorderWindow and None WindowStyle for the <see cref="MainWindowObject"/>.
        /// </summary>
        private void ToggleWindowStyle()
        {
            if (WindowStyle == WindowStyle.SingleBorderWindow)
                WindowStyle = WindowStyle.None;
            else
                WindowStyle = WindowStyle.SingleBorderWindow;
        }

        /// <summary>
        /// Toggles between the Normal and Maximized WindowState for the <see cref="MainWindowObject"/>.
        /// </summary>
        private void ToggleWindowState()
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }
        #endregion
    }
}
