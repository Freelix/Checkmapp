﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.ViewModels.NoteViewModels;
using CheckMapp.Resources;

namespace CheckMapp.Views.NoteViews
{
    public partial class NoteView : PhoneApplicationPage
    {
        public NoteView()
        {
            InitializeComponent();
        }

        private void IconDelete_Click(object sender, EventArgs e)
        {
            // Call the appropriate function in ViewModel
            var vm = DataContext as NoteViewModel;
            if (vm != null)
            {
                vm.DeleteNoteCommand.Execute(null);
            }

            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
            //(Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NoteViews/ListNoteView.xaml", UriKind.Relative));
        }

        private void IconEdit_Click(object sender, EventArgs e)
        {

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            int id = (int)PhoneApplicationService.Current.State["id"];
            this.DataContext = new NoteViewModel(id);
            base.OnNavigatedTo(e);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Edit;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Delete;
            }
        }
    }
}