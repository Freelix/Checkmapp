﻿#pragma checksum "C:\Users\John-William\SkyDrive\Documents\GitHub\Checkmapp\CheckMapp\Views\PhotoViews\AddEditPhotoView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E7BD953F268324A283573D6F7E95AA08"
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
    
    
    public partial class AddEditPhotoView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock TitleTextblock;
        
        internal System.Windows.Controls.TextBlock idNoteTextBlock;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.HubTile hubTile;
        
        internal Microsoft.Phone.Controls.ListPicker poiListPicker;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton IconSave;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton IconCancel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/CheckMapp;component/Views/PhotoViews/AddEditPhotoView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitleTextblock = ((System.Windows.Controls.TextBlock)(this.FindName("TitleTextblock")));
            this.idNoteTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("idNoteTextBlock")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.hubTile = ((Microsoft.Phone.Controls.HubTile)(this.FindName("hubTile")));
            this.poiListPicker = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("poiListPicker")));
            this.IconSave = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("IconSave")));
            this.IconCancel = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("IconCancel")));
        }
    }
}

