using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OLEDSaver.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //_screenWidth = SystemParameters.PrimaryScreenWidth;
            //_screenHeight = SystemParameters.PrimaryScreenHeight;

            //_currentHeight = MainWindowObject.Height;
            //_currentWidth = MainWindowObject.Width;

            UpdateDisplayInfo();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C) Application.Current.Shutdown();
            if (e.Key == Key.F)
            {
                ToggleWindowState();
                ToggleWindowStyle();
            }
            if (e.Key == Key.M)
            {
                ToggleWindowState();
            }
            if (e.Key == Key.S)
            {
                ToggleWindowStyle();
            }
            if (e.Key == Key.D)
            {
                ToggleGridVisibility();
            }
        }

        /// <summary>
        /// Toggles between the Visible and Hidden Visibility state for the <see cref="MainWindowGrid"/>.
        /// </summary>
        private void ToggleGridVisibility()
        {
            if (MainWindowGrid.Visibility == Visibility.Visible)
                MainWindowGrid.Visibility = Visibility.Hidden;
            else
                MainWindowGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Toggles between the SingleBorderWindow and None WindowStyle for the <see cref="MainWindowObject"/>.
        /// </summary>
        private void ToggleWindowStyle()
        {
            if (MainWindowObject.WindowStyle == WindowStyle.SingleBorderWindow)
                MainWindowObject.WindowStyle = WindowStyle.None;
            else
                MainWindowObject.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        /// <summary>
        /// Toggles between the Normal and Maximized WindowState for the <see cref="MainWindowObject"/>.
        /// </summary>
        private void ToggleWindowState()
        {
            if (MainWindowObject.WindowState == WindowState.Maximized)
                MainWindowObject.WindowState = WindowState.Normal;
            else
                MainWindowObject.WindowState = WindowState.Maximized;
        }

        private void UpdateDisplayInfo()
        {
            //WindowHeightLabel.Content = $"Window Height: {_currentHeight}";
            //WindowWidthLabel.Content = $"Window Width: {_currentWidth}";

            //DisplayHeightLabel.Content = $"Display Height: {_screenHeight}";
            //DisplayWidthLabel.Content = $"Display Width: {_screenWidth}";
        }
    }
}
