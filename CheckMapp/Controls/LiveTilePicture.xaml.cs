using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Animation;

namespace CheckMapp.Controls
{
    public partial class LiveTilePicture : UserControl
    {
        public LiveTilePicture()
        {
            InitializeComponent();
            this.DataContext = this;
            Storyboard anim = (Storyboard)FindName("liveTileAnimTop");
            anim.Begin();
        }

        private void liveTileAnimTop_Completed_1(object sender, EventArgs e)
        {
            Storyboard anim = (Storyboard)FindName("liveTileAnimBottom");
            anim.Begin();
        }

        private void liveTileAnimBottom_Completed_1(object sender, EventArgs e)
        {
            Storyboard anim = (Storyboard)FindName("liveTileAnimTop");
            anim.Begin();
        }

        public static readonly DependencyProperty SourceImageProperty =
           DependencyProperty.Register("Images", typeof(string), typeof(LiveTilePicture), null);

        /// <summary>
        /// La source de l'image
        /// </summary>
        public string SourceImage
        {
            get { return GetValue(SourceImageProperty) as string; }
            set
            {
                SetValue(SourceImageProperty, value);
            }
        }

        private void imgPhoto_Loaded(object sender, RoutedEventArgs e)
        {
            splineDouble.Value = -imgPhoto.ActualHeight + 200;

        }
    }
}
