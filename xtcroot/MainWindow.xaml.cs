using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace xtcroot
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            SystemBackdrop = new MicaBackdrop()
            { Kind = MicaKind.BaseAlt };
            ExtendsContentIntoTitleBar = true;

            // Select default item of NavigationView
            SelectedNavItem = navigationView.MenuItems[0];
        }

        private object selectedNavItem;
        public object SelectedNavItem
        {
            get { return selectedNavItem; }
            set { selectedNavItem = value; }
        }
        private void NavigationView_Loaded(object sender, RoutedEventArgs e) // Set default frame
        {
            contentFrame.Navigate(typeof(HomePage));
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var invokedItem = args.InvokedItemContainer as NavigationViewItem;
            if (invokedItem != null)
            {
                switch (invokedItem.Tag.ToString())
                {
                    case "HomePage":
                        contentFrame.Navigate(typeof(HomePage));
                        break;
                    case "ModulePage":
                        contentFrame.Navigate(typeof(ModulePage));
                        break;
                    case "RollBackPage":
                        contentFrame.Navigate(typeof(RollBackPage));
                        break;
                    case "RootPage":
                        contentFrame.Navigate(typeof(RootPage));
                        break;
                    case "AboutPage":
                        contentFrame.Navigate(typeof(AboutPage));
                        break;
                }
            }
        }
    }
}


