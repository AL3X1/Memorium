using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Memorium.Views
{
    public sealed partial class MainPage : Page
    {
        private ViewModels.TaskViewModel viewModel = new ViewModels.TaskViewModel();

        public MainPage()
        {            
            this.InitializeComponent();

            if (viewModel.Tasks.Count == 0)
                newTaskBtn.Visibility = Visibility.Visible;

            DataContext = new ViewModels.TaskViewModel();
        }

        private void taskStatus_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Finish(sender, e);
        }

        private void menuBtn_Click(object sender, RoutedEventArgs e)
        {
            menuPanel.IsPaneOpen = !menuPanel.IsPaneOpen;

            if (menuPanel.IsPaneOpen)
                tasksList.Margin = new Thickness(200, 50, 0, 0);
            else
                tasksList.Margin = new Thickness(0, 50, 0, 0);
        }
    }
}
