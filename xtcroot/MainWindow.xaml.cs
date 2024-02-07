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
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
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
                    case "Home":
                        contentFrame.Navigate(typeof(HomePage));
                        break;
                }
            }
        }
    }
}
