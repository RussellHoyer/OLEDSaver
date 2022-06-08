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
        double screenWidth, screenHeight;
        double currentWidth, currentHeight;
        double originalTop, originalLeft;

        double originalHeight = 450;
        double originalWidth = 800;

        public MainWindow()
        {
            InitializeComponent();

            screenWidth = SystemParameters.PrimaryScreenWidth;
            screenHeight = SystemParameters.PrimaryScreenHeight;

            UpdateCurrentWindowSize();

            //Lbl1.Content = $"Your resolution is- w: {screenWidth}, h: {screenHeight}";
        }

        private void UpdateCurrentWindowSize()
        {
            currentHeight = MainWindowObject.Height;
            currentWidth = MainWindowObject.Width;
        }
        private void UpdateWindowPosition()
        {
            originalTop = MainWindowObject.Top;
            originalLeft = MainWindowObject.Left;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C) Application.Current.Shutdown();
            if (e.Key == Key.F)
            {
                // Not fullscreen
                if (currentHeight < screenHeight
                    && currentWidth < screenWidth)
                {
                    MainWindowObject.Height = screenHeight;
                    MainWindowObject.Width = screenWidth;
                    UpdateWindowPosition();
                    MainWindowObject.Top = 0;
                    MainWindowObject.Left = 0;
                }
                else
                {
                    MainWindowObject.Height = originalHeight;
                    MainWindowObject.Width = originalWidth;
                    MainWindowObject.Top = originalTop;
                    MainWindowObject.Left = originalLeft;
                }
                UpdateCurrentWindowSize();
            }
            if (e.Key == Key.M)
            {
                if (MainWindowObject.WindowState == WindowState.Maximized)
                    MainWindowObject.WindowState = WindowState.Normal;
                else
                    MainWindowObject.WindowState = WindowState.Maximized;
            }
            if (e.Key == Key.S)
            {
                if (MainWindowObject.WindowStyle == WindowStyle.SingleBorderWindow)
                    MainWindowObject.WindowStyle = WindowStyle.None;
                else
                    MainWindowObject.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            if (e.Key == Key.D)
            {
                if (MainWindowGrid.Visibility == Visibility.Visible)
                    MainWindowGrid.Visibility = Visibility.Hidden;
                else
                    MainWindowGrid.Visibility = Visibility.Visible;
            }
        }

    }
}
