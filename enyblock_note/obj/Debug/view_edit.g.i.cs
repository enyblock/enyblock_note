﻿#pragma checksum "G:\program\windows phone\code\enyblock_note\enyblock_note\view_edit.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "36AB36791040098157AA63146BCFC980"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
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


namespace enyblock_note {
    
    
    public partial class view_edit : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock display_note_textblock;
        
        internal System.Windows.Controls.TextBox edit_note_textbox;
        
        internal System.Windows.Controls.Canvas confirm_dialog_canvas;
        
        internal System.Windows.Controls.Button cancle_button;
        
        internal System.Windows.Controls.Button ok_button;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/enyblock_note;component/view_edit.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.display_note_textblock = ((System.Windows.Controls.TextBlock)(this.FindName("display_note_textblock")));
            this.edit_note_textbox = ((System.Windows.Controls.TextBox)(this.FindName("edit_note_textbox")));
            this.confirm_dialog_canvas = ((System.Windows.Controls.Canvas)(this.FindName("confirm_dialog_canvas")));
            this.cancle_button = ((System.Windows.Controls.Button)(this.FindName("cancle_button")));
            this.ok_button = ((System.Windows.Controls.Button)(this.FindName("ok_button")));
        }
    }
}

