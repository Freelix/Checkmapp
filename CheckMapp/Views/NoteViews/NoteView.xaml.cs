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

namespace CheckMapp.Views.NoteViews
{
    public partial class NoteView : PhoneApplicationPage
    {
        public NoteView()
        {
            InitializeComponent();
            this.DataContext = new NoteViewModel();
        }
    }
}