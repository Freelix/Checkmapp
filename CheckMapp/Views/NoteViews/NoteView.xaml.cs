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
using Utility = CheckMapp.Utils.Utility;
using Microsoft.Phone.Tasks;

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
            if (MessageBox.Show(AppResources.ConfirmationDeleteNote, "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                // Call the appropriate function in ViewModel
                var vm = DataContext as NoteViewModel;
                vm.DeleteNoteCommand.Execute(null);

                (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
            }
        }

        private void IconEdit_Click(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Mode"] = Mode.edit;
            NavigationService.Navigate(new Uri("/Views/NoteViews/AddEditNoteView.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            int myNote = (int)PhoneApplicationService.Current.State["Note"];
            this.DataContext = new NoteViewModel(myNote);
            base.OnNavigatedTo(e);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Share;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Edit;
                (ApplicationBar.Buttons[2] as ApplicationBarIconButton).Text = AppResources.Delete;
            }
        }

        private void IconShare_Click(object sender, EventArgs e)
        {
            ShareStatusTask status = new ShareStatusTask();
            status.Status = (this.DataContext as NoteViewModel).Note.Message;
            status.Show();
        }
    }
}