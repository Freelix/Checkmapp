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
            Mode mode = (Mode)PhoneApplicationService.Current.State["Mode"];
            this.DataContext = new AddEditNoteViewModel(mode);

            //Assigne le titre de la page
            AddEditNoteViewModel vm = DataContext as AddEditNoteViewModel;
            if (vm.Mode == Mode.add)
            {
                TitleTextblock.Text = AppResources.AddNote;
            }
            else
            {
                TitleTextblock.Text = AppResources.EditNote;
            }

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Sauvegarder la note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();
            var vm = DataContext as AddEditNoteViewModel;
            if (vm != null)
            {
                vm.AddNoteCommand.Execute(null);
            }
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