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
        }

        private void loadData()
        {
            int currentTrip = (int)PhoneApplicationService.Current.State["Trip"];
            this.DataContext = new ListNoteViewModel(currentTrip);
            NoteLLS.ItemsSource = (this.DataContext as ListNoteViewModel).GroupedNotes;
        }

        private void loadData(int poiId)
        {
            int currentTrip = (int)PhoneApplicationService.Current.State["Trip"];
            this.DataContext = new ListNoteViewModel(currentTrip, poiId);
            NoteLLS.ItemsSource = (this.DataContext as ListNoteViewModel).GroupedNotes;
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
            var appbar = this.Resources["AppBarList"] as ApplicationBar;
            if (appbar.Buttons != null)
            {
                (appbar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Select;
                (appbar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.AddPicture;
            }

            var appbarSelect = this.Resources["AppBarListSelect"] as ApplicationBar;
            if (appbarSelect.Buttons != null)
            {
                (appbarSelect.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Delete;
            }

            ApplicationBar = this.Resources["AppBarList"] as ApplicationBar;
            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (NoteLLS.ItemsSource.Count > 0);
        }

        /// <summary>
        /// Click sur les options du menu contextuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && ((sender as MenuItem).DataContext is Note))
            {
                Note noteSelected = ((sender as MenuItem).DataContext as Note);
                switch (menuItem.Name)
                {
                    case "EditNote":
                        PhoneApplicationService.Current.State["Note"] = noteSelected.Id;
                        PhoneApplicationService.Current.State["Mode"] = Mode.edit;
                        (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NoteViews/AddEditNoteView.xaml", UriKind.Relative));
                        break;
                    case "DeleteNote":
                        if (MessageBox.Show(AppResources.ConfirmationDeleteNote, "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            var vm = DataContext as ListNoteViewModel;
                            vm.DeleteNoteCommand.Execute(noteSelected);
                            NoteLLS.ItemsSource = vm.GroupedNotes;

                            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (NoteLLS.ItemsSource.Count > 0);
                        }

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
            PhoneApplicationService.Current.State["Note"] = 0;

            if ((int)PhoneApplicationService.Current.State["poiId"] > 0)
            {
                int poiId = (int)PhoneApplicationService.Current.State["poiId"];
                loadData(poiId);
            }
            else
                loadData();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back && NoteLLS.IsSelectionEnabled)
            {
                NoteLLS.IsSelectionEnabled = false;
                e.Cancel = true;
            }
        }

        private void IconMultiSelect_Click(object sender, EventArgs e)
        {
            NoteLLS.IsSelectionEnabled = !NoteLLS.IsSelectionEnabled;
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NoteLLS.IsSelectionEnabled = false;

            if (!NoteLLS.IsSelectionEnabled)
            {
                Note itemTapped = (sender as FrameworkElement).DataContext as Note;
                PhoneApplicationService.Current.State["Note"] = itemTapped.Id;
                NavigationService.Navigate(new Uri("/Views/NoteViews/NoteView.xaml", UriKind.Relative));
            }
        }

        private void IconDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(AppResources.ConfirmationDeleteNotes, "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var vm = DataContext as ListNoteViewModel;
                List<Note> noteList = new List<Note>();

                vm.DeleteNotesCommand.Execute(new List<object>(NoteLLS.SelectedItems as IList<object>));
                NoteLLS.ItemsSource = vm.GroupedNotes;

                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (NoteLLS.ItemsSource.Count > 0);
            }
        }

        private void NoteLLS_IsSelectionEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (NoteLLS.IsSelectionEnabled)
                ApplicationBar = this.Resources["AppBarListSelect"] as ApplicationBar;
            else
                ApplicationBar = this.Resources["AppBarList"] as ApplicationBar;

            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = !NoteLLS.IsSelectionEnabled;
        }

        private void NoteLLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NoteLLS.IsSelectionEnabled)
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (NoteLLS.SelectedItems.Count > 0);
        }
    }
}