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
using Utility = CheckMapp.Utils.Utility;

namespace CheckMapp.Views.NoteViews
{
    public partial class AddEditNoteView : PhoneApplicationPage
    {
        public AddEditNoteView()
        {
            InitializeComponent();
            LoadPage();
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
                vm.AddEditNoteCommand.Execute(null);

                if (vm.IsFormValid)
                {
                    // En appelant directement la page principale on rafraichit celle-ci pour mettre a jour le panorama
                    (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
                }
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

        private void LoadPage()
        {
            Trip trip = (Trip)PhoneApplicationService.Current.State["Trip"];
            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            Note currentNote = (Note)PhoneApplicationService.Current.State["Note"];
            AddEditNoteViewModel vm = new AddEditNoteViewModel(trip, currentNote, mode);
            this.DataContext = vm;

            //Assigne le titre de la page
            if (vm.Mode == Mode.add)
                TitleTextblock.Text = AppResources.AddNote.ToLower();
            else if (vm.Mode == Mode.edit)
                TitleTextblock.Text = AppResources.EditNote.ToLower();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Save the note instance to retrieve when a tombstone occured
            AddEditNoteViewModel vm = DataContext as AddEditNoteViewModel;

            //On annule les changeemnts si l'usager fait BACK
            if (e.NavigationMode == NavigationMode.Back && !vm.IsFormValid)
                vm.CancelNoteCommand.Execute(null);

            PhoneApplicationService.Current.State["Note"] = vm.Note;

            if (vm.POISelected != null)
                PhoneApplicationService.Current.State["POISelected"] = vm.POISelected;
            else if (vm.PoiList.Count > 0)
                PhoneApplicationService.Current.State["POISelected"] = vm.PoiList[0];
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State["POISelected"] != null)
            {
                AddEditNoteViewModel vm = DataContext as AddEditNoteViewModel;
                vm.POISelected = (PointOfInterest)PhoneApplicationService.Current.State["POISelected"];
                POIControl.chkNoPOI.IsChecked = false;
            }
            if (Utility.IsTombstoned())
            {
                PhoneApplicationService.Current.State["TombstoneMode"] = true;
                LoadPage();
            }
        }
    }
}