﻿#pragma checksum "C:\Users\John-William\SkyDrive\Documents\GitHub\Checkmapp\CheckMapp\Views\PhotoViews\PhotoView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "39BEEC302976685735A6D5748A05780A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34209
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace CheckMapp.Views.PhotoViews {
    
    
    public partial class PhotoView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image myImage;
        
        internal System.Windows.Controls.TextBlock DescriptionTextBlock;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton IconEdit;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton IconDelete;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/CheckMapp;component/Views/PhotoViews/PhotoView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.myImage = ((System.Windows.Controls.Image)(this.FindName("myImage")));
            this.DescriptionTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("DescriptionTextBlock")));
            this.IconEdit = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("IconEdit")));
            this.IconDelete = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("IconDelete")));
        }
    }
}

