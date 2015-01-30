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
using CheckMapp.Model.Tables;
using CheckMapp.ViewModels;

namespace CheckMapp.Views.NoteViews
{
    public partial class ListNoteView : PhoneApplicationPage
    {
        public ListNoteView()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            this.DataContext = new ListNoteViewModel();
            NoteLLS.ItemsSource = (this.DataContext as ListNoteViewModel).GroupedNotes;
            NoteLLS.SelectionChanged += ListboxNote_SelectionChanged;
            NoteLLS.SelectedItem = null;
        }

        /// <summary>
        /// Click sur les notes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ListboxNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NoteLLS.SelectedItem != null)
            {
                PhoneApplicationService.Current.State["Note"] = (NoteLLS.SelectedItem as Note);
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
            PhoneApplicationService.Current.State["Mode"] = Mode.add;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NoteViews/AddEditNoteView.xaml", UriKind.Relative));
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
                    case "EditNote":
                        if (((sender as MenuItem).DataContext is Note))
                        {
                            PhoneApplicationService.Current.State["Note"] = ((sender as MenuItem).DataContext as Note);
                            PhoneApplicationService.Current.State["Mode"] = Mode.edit;
                            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NoteViews/AddEditNoteView.xaml", UriKind.Relative));
                        }
                        break;
                    case "DeleteNote":
                        MessageBox.Show("Are you sure you want to delete this trip?");
                        break;
                }
            }
        }

        /// <summary>
        /// J'ai besoin de ça pour mettre à jour mon ContextMenu lorsque je reviens à un changement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var menu = (ContextMenu)sender;
            var owner = (FrameworkElement)menu.Owner;
            if (owner.DataContext != menu.DataContext)
                menu.DataContext = owner.DataContext;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
                loadData();
        }
    }
}