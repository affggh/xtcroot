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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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

        private bool isRootPageWarningShowed = false;
        public MainWindow()
        {
            this.InitializeComponent();
            SystemBackdrop = new MicaBackdrop()
            { Kind = MicaKind.BaseAlt };
            ExtendsContentIntoTitleBar = true;

            // Select default item of NavigationView
            SelectedNavItem = navigationView.MenuItems[0];
        }

        private DispatcherTimer timer;
        private ContentDialog rootWarningDialog;
        private const int defaultWarningDisabledSeconds = 10;
        private int warningRemainingSeconds = defaultWarningDisabledSeconds;
        private bool isWarningCancled = false;
        //private Button warningPrimaryButton;
        private void OnTimerTick(object sender, object e)
        {
            if (warningRemainingSeconds == 0)
            {
                rootWarningDialog.PrimaryButtonText = "我已知晓并继续";
                rootWarningDialog.DefaultButton = ContentDialogButton.Primary;
                timer.Stop();
                rootWarningDialog.IsPrimaryButtonEnabled = true;
            } else if (isWarningCancled)
            {
                timer.Stop();
                isWarningCancled = false;
            } else 
            {
                warningRemainingSeconds--;
                rootWarningDialog.PrimaryButtonText = $"我已知晓并继续({warningRemainingSeconds}s)";
            }
        }
        private async void OnNavigatingRootPage()
        {
            if (isRootPageWarningShowed)
            {
                return;
            }
            rootWarningDialog = new ContentDialog
            {
                Title = "⚠警告",
                Content = "ROOT可能会导致您的设备损坏,如果因为您的操作不当造成设备损坏,Rexwe iMoo无任何责任\n" +
              "如果是有人帮您付费代刷,恭喜您被骗了,请查看我们的官方教程https://b23.tv/94ugyjO\n" +
              "在root之前,您必须掌握官方教程中的一些常用概念,否则您的设备可能会损坏,因此请确定您已经掌握了一些概念",
                PrimaryButtonText = "我已知晓并继续",
                CloseButtonText = "我不同意",
                DefaultButton = ContentDialogButton.Primary,
            };

            rootWarningDialog.IsPrimaryButtonEnabled = false;
            rootWarningDialog.XamlRoot = this.Content.XamlRoot;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += OnTimerTick;
            timer.Start();
            var result = await rootWarningDialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                contentFrame.Navigate(typeof(HomePage));
                navigationView.SelectedItem = navigationView.MenuItems[0];
                var selectedItem = navigationView.SelectedItem as NavigationViewItem;
                if (selectedItem != null)
                {
                    headerText.Text = selectedItem.Content.ToString();
                }
                warningRemainingSeconds = defaultWarningDisabledSeconds; // reset this to 30 seconds
                isWarningCancled = true;      // warning cancled
            } else
            {
                isRootPageWarningShowed = true;
            }

        }


        private object selectedNavItem;
        public object SelectedNavItem
        {
            get { return selectedNavItem; }
            set {
                if (selectedNavItem != value)
                {
                    selectedNavItem = value;
                }
            }
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
                        OnNavigatingRootPage();
                        contentFrame.Navigate(typeof(RootPage));
                        break;
                    case "AboutPage":
                        contentFrame.Navigate(typeof(AboutPage));
                        break;
                }
                headerText.Text = invokedItem.Content.ToString();
            }
        }
    }
}


