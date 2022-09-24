using OLEDSaver.Models;
using OLEDSaver.Providers;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows;

namespace OLEDSaver.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Private properties

        private double _windowWidth;
        private double _windowHeight;
        private double _windowTop;
        private double _windowLeft;
        private string _windowResolutionText;
        private WindowStyle _windowStyle;
        private WindowState _windowState;
        private WindowStartupLocation _windowStartupLocation;
        private bool _displayGui;
        private double _displayWidth;
        private double _displayHeight;
        private string _displayResolutionText;
        private string _configSettingsFilepath;
        private bool _appBarVisible;
        bool _blackoutSwitch;

        WindowConfigSettingsModel _windowConfigSettings;
        ConfigSettingsProvider _configSettingsProvider;

        private DelegateCommand _toggleWindowStateCommand;
        private DelegateCommand _toggleWindowStyleCommand;
        private DelegateCommand _toggleGUICommand;
        private DelegateCommand _toggleBlackoutCommand;
        private DelegateCommand _exitApplicationCommand;
        private DelegateCommand _updateDisplayInfoCommand;
        private DelegateCommand _loadConfigSettingsCommand;
        private DelegateCommand _saveConfigSettingsCommand;

        #endregion

        public MainWindowViewModel()
        {
            // property init
            _windowConfigSettings = new WindowConfigSettingsModel();
            _configSettingsProvider = new ConfigSettingsProvider();
            _configSettingsFilepath = Path.Combine(Environment.CurrentDirectory,
                "OLEDSaverConfig.json");

            // command init
            _exitApplicationCommand = new DelegateCommand(OnExitApplication);
            _toggleWindowStateCommand = new DelegateCommand(ToggleWindowState);
            _toggleWindowStyleCommand = new DelegateCommand(ToggleWindowStyle);
            _toggleGUICommand = new DelegateCommand(ToggleGuiVisibility);
            _toggleBlackoutCommand = new DelegateCommand(OnBlackout);
            _updateDisplayInfoCommand = new DelegateCommand(OnUpdateDisplayInfo);
            _loadConfigSettingsCommand = new DelegateCommand(OnLoadConfigSettings);
            _saveConfigSettingsCommand = new DelegateCommand(OnSaveConfigSettings);

            // Everything else
            SetWindowDimensions();
            SetDisplayInfo();

            // Defaults
            DisplayGui = true;
            BlackoutSwitch = false;
            AppBarVisible = true;
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
        public DelegateCommand LoadConfigSettingsCommand { get { return _loadConfigSettingsCommand; } }
        public DelegateCommand SaveConfigSettingsCommand { get { return _saveConfigSettingsCommand; } }

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
        public double WindowTop
        { 
            get { return _windowTop; }
            set { _windowTop = value; RaisePropertyChanged(); }
        }
        public double WindowLeft
        {
            get { return _windowLeft; }
            set { _windowLeft = value; RaisePropertyChanged(); }
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

        public bool AppBarVisible 
        { 
            get { return _appBarVisible; }
            set { _appBarVisible = value; RaisePropertyChanged(); }
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
            WindowWidth = _windowConfigSettings.OriginalWidth;
            WindowHeight = _windowConfigSettings.OriginalHeight;
        }
        public bool BlackoutSwitch 
        { 
            get { return _blackoutSwitch; }
            set { _blackoutSwitch = value; RaisePropertyChanged(); }
        }
        Dictionary<string, object> _displayPositions = new();

        private void OnBlackout()
        {
            if (_blackoutSwitch)
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.SingleBorderWindow;
                RestoreWindowPositions();
                AppBarVisible = true;
                DisplayGui = true;
                BlackoutSwitch = false;
            }
            else
            {
                SaveWindowPositions();
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
                AppBarVisible = false;
                DisplayGui = false;
                BlackoutSwitch = true;
            }
        }

        private void RestoreWindowPositions()
        {
            if (_displayPositions == null) return;

            if (_displayPositions.ContainsKey("windowLeft"))
                WindowLeft = (double)_displayPositions["windowLeft"];

            if (_displayPositions.ContainsKey("windowTop"))
                WindowTop = (double)_displayPositions["windowTop"];

            if (_displayPositions.ContainsKey("windowHeight"))
                WindowHeight = (double)_displayPositions["windowHeight"];

            if (_displayPositions.ContainsKey("windowWidth"))
                WindowWidth = (double)_displayPositions["windowWidth"];
        }

        private void SaveWindowPositions()
        {
            _displayPositions ??= new();

            if (_displayPositions.ContainsKey("windowLeft"))
                _displayPositions["windowLeft"] = WindowLeft;
            else
                _displayPositions.Add("windowLeft", WindowLeft);

            if (_displayPositions.ContainsKey("windowTop"))
                _displayPositions["windowTop"] = WindowTop;
            else
                _displayPositions.Add("windowTop", WindowTop);

            if (_displayPositions.ContainsKey("windowHeight"))
                _displayPositions["windowHeight"] = WindowHeight;
            else
                _displayPositions.Add("windowHeight", WindowHeight);

            if (_displayPositions.ContainsKey("windowWidth"))
                _displayPositions["windowWidth"] = WindowWidth;
            else
                _displayPositions.Add("windowWidth", WindowWidth);
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
            {
                WindowStyle = WindowStyle.None;
                AppBarVisible = false;
            }
            else
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                AppBarVisible = true;
            }
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
            //SaveWindowPositions();
        }

        private void SetDisplayInfo()
        {
            DisplayWidth = SystemParameters.PrimaryScreenWidth;
            DisplayHeight = SystemParameters.PrimaryScreenHeight;
            WindowWidth = _windowConfigSettings.OriginalWidth;
            WindowHeight = _windowConfigSettings.OriginalHeight;

            WindowResolutionText = $"Current dimensions: {WindowWidth}x{WindowHeight}";
            DisplayResolutionText = $"Display dimensions: {DisplayWidth}x{DisplayHeight}";
        }

        private void OnSaveConfigSettings()
        {
            if (_windowConfigSettings != null)
                _configSettingsProvider.SaveConfigSettings(_configSettingsFilepath, _windowConfigSettings);
        }

        private void OnLoadConfigSettings()
        {
            _configSettingsProvider.LoadConfigSettings(_configSettingsFilepath);
            _windowConfigSettings = _configSettingsProvider.GetWindowConfigSettings();
        }

        #endregion
    }
}
