using OLEDSaver.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.CodeDom;
using System.Reflection;
using System.Windows;

namespace OLEDSaver.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Private properties

        private double _windowWidth;
        private double _windowHeight;
        private string _windowResolutionText;
        private WindowStyle _windowStyle;
        private WindowState _windowState;
        private WindowStartupLocation _windowStartupLocation;
        private bool _displayGui;
        private double _displayWidth;
        private double _displayHeight;
        private string _displayResolutionText;

        WindowConfigSettingsModel windowConfigSettings;

        private DelegateCommand _toggleWindowStateCommand;
        private DelegateCommand _toggleWindowStyleCommand;
        private DelegateCommand _toggleGUICommand;
        private DelegateCommand _toggleBlackoutCommand;
        private DelegateCommand _exitApplicationCommand;
        private DelegateCommand _updateDisplayInfoCommand;

        #endregion

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
            _updateDisplayInfoCommand = new DelegateCommand(OnUpdateDisplayInfo);

            // Everything else
            SetWindowDimensions();
            SetDisplayInfo();

            // Defaults
            DisplayGui = true;
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Normal;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _windowResolutionText = $"Current dimensions: ";
            _displayResolutionText = $"Display dimensions: ";
        }

        #region Bindable Properties

        public DelegateCommand ToggleWindowStateCommand { get { return _toggleWindowStateCommand; } }
        public DelegateCommand ToggleWindowStyleCommand { get { return _toggleWindowStyleCommand; } }
        public DelegateCommand ToggleGUICommand { get { return _toggleGUICommand; } }
        public DelegateCommand ToggleBlackoutCommand { get { return _toggleBlackoutCommand; } }
        public DelegateCommand ExitApplicationCommand { get { return _exitApplicationCommand; } }
        public DelegateCommand UpdateDisplayInfoCommand { get { return _updateDisplayInfoCommand; } }

        public string CurrentAppVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; RaisePropertyChanged(); }
        }
        public double WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; RaisePropertyChanged(); }
        }
        public string WindowResolutionText
        {
            get { return _windowResolutionText; }
            set { _windowResolutionText = value; RaisePropertyChanged(); }
        }
        public WindowStyle WindowStyle
        {
            get { return _windowStyle; }
            set { _windowStyle = value; RaisePropertyChanged(); }
        }
        public WindowState WindowState
        {
            get { return _windowState; }
            set { _windowState = value; RaisePropertyChanged(); }
        }
        public WindowStartupLocation WindowStartupLocation
        {
            get { return _windowStartupLocation; }
            set { _windowStartupLocation = value; RaisePropertyChanged(); }
        }
        public bool DisplayGui
        {
            get { return _displayGui; }
            set { _displayGui = value; RaisePropertyChanged(); }
        }
        public double DisplayWidth
        {
            get { return _displayWidth; }
            set { _displayWidth = value; RaisePropertyChanged(); }
        }
        public double DisplayHeight
        {
            get { return _displayHeight; }
            set { _displayHeight = value; RaisePropertyChanged(); }
        }
        public string DisplayResolutionText
        {
            get { return _displayResolutionText; }
            set { _displayResolutionText = value; RaisePropertyChanged(); }
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

        private void OnUpdateDisplayInfo()
        {
            WindowResolutionText = $"Current dimensions: {WindowWidth}x{WindowHeight}";
            DisplayResolutionText = $"Display dimensions: {DisplayWidth}x{DisplayHeight}";
        }

        private void SetDisplayInfo()
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
