using System;
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
using CheckMapp.Model;

namespace CheckMapp.Views.NoteViews
{
    public partial class ListNoteView : PhoneApplicationPage
    {
        public ListNoteView()
        {
            InitializeComponent();
            loadData();
            ListboxNote.SelectionChanged += ListboxNote_SelectionChanged;
        }

        private void loadData()
        {
            this.DataContext = new ListNoteViewModel();
            ListboxNote.DataContext = (this.DataContext as ListNoteViewModel).NoteList;
        }

        /// <summary>
        /// Click sur les notes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ListboxNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Note> noteList = (this.DataContext as ListNoteViewModel).NoteList.ToList();
            int index = ListboxNote.SelectedIndex;

            if (index > -1)
            {
                int noteId = noteList[index].Id;
                PhoneApplicationService.Current.State["id"] = noteId;
                NavigationService.Navigate(new Uri("/Views/NoteViews/NoteView.xaml", UriKind.Relative));
            }
        }

        /// <summary>
        /// Ajout d'une note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconAdd_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NoteViews/AddNoteView.xaml", UriKind.Relative));
        }

        /// <summary>
        /// On assigne les titres des boutons au démarrage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.AddNote;
            }
        }

        /// <summary>
        /// Click sur les options du menu contextuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Name)
                {
                    case "Edit":
                        break;
                    case "Delete":
                        MessageBox.Show("Are you sure you want to delete this trip?");
                        break;
                }
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
                loadData();
        }
    }
}