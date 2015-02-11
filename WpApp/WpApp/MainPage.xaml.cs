﻿using DataContract;
using ServiceContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WpApp.Task;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool DevelopMode = false;
        private string DeviceId = "";
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client(DevelopMode);

            string registerCode = TextBox_RegisterCode.Text.Trim();
            var resp = client.Register("Miliu-Lumia", registerCode);

            if(!string.IsNullOrEmpty(resp))
            {
                MessageDialog msgBox = new MessageDialog(resp);
                msgBox.ShowAsync().GetResults();

                DeviceId = resp;
            }
        }

        private void Button_Sync_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client(DevelopMode);

            var taskDetails = client.PullTask(DeviceId);

            foreach(TaskDetail taskDetail in taskDetails)
            {
                try
                {
                    ITask taskWorker = TaskFactory.CreateTask(taskDetail);
                    taskWorker.Execute();
                }
                catch{ }
            }
        }
    }
}
