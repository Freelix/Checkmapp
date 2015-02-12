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
using CheckMapp.ViewModels;
using CheckMapp.Model.Tables;

namespace CheckMapp.Views.NoteViews
{
    public partial class AddEditNoteView : PhoneApplicationPage
    {
        public AddEditNoteView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Trip trip = (Trip)PhoneApplicationService.Current.State["Trip"];
            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            Note currentNote = (Note)PhoneApplicationService.Current.State["Note"];
            this.DataContext = new AddEditNoteViewModel(trip, currentNote, mode);

            //Assigne le titre de la page
            AddEditNoteViewModel vm = DataContext as AddEditNoteViewModel;
            if (vm.Mode == Mode.add)
                TitleTextblock.Text = AppResources.AddNote.ToLower();
            else if (vm.Mode == Mode.edit)
                TitleTextblock.Text = AppResources.EditNote.ToLower();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Sauvegarder la note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();

            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                var vm = DataContext as AddEditNoteViewModel;
                if (vm != null)
                {
                    vm.AddEditNoteCommand.Execute(null);
                }
                // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour le panorama
                (Application.Current.RootVisual as PhoneApplicationFrame).GoBack(); 
            });
        }

        /// <summary>
        /// Annuler l'ajout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconCancel_Click(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
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
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Save;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Cancel;
            }
        }

        
    }
}