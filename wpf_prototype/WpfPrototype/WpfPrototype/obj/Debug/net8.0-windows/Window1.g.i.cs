﻿#pragma checksum "..\..\..\Window1.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6542761D74A53EE6EC80CC3516550D5A9BF277D4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfPrototype;


namespace WpfPrototype {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 20 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonBack;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSave;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition listView;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition listView2;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition panel;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewTemplates;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewAttributes;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel templateAndAttributeStackPanel;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonLeft;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonRight;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelSelectedFile;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgAnalyzedDocument;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfPrototype;component/window1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Window1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\Window1.xaml"
            ((WpfPrototype.Window1)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.buttonBack = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Window1.xaml"
            this.buttonBack.Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonSave = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Window1.xaml"
            this.buttonSave.Click += new System.Windows.RoutedEventHandler(this.ButtonSave_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listView = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 5:
            this.listView2 = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 6:
            this.panel = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 7:
            this.listViewTemplates = ((System.Windows.Controls.ListView)(target));
            
            #line 35 "..\..\..\Window1.xaml"
            this.listViewTemplates.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListViewTemplatesAndAttributes_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.listViewAttributes = ((System.Windows.Controls.ListView)(target));
            
            #line 47 "..\..\..\Window1.xaml"
            this.listViewAttributes.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.listViewAttributes_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 47 "..\..\..\Window1.xaml"
            this.listViewAttributes.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listViewAttributes_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.templateAndAttributeStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 11:
            this.buttonLeft = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\Window1.xaml"
            this.buttonLeft.Click += new System.Windows.RoutedEventHandler(this.ButtonLeft_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.buttonRight = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\..\Window1.xaml"
            this.buttonRight.Click += new System.Windows.RoutedEventHandler(this.ButtonRight_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.labelSelectedFile = ((System.Windows.Controls.Label)(target));
            return;
            case 14:
            this.imgAnalyzedDocument = ((System.Windows.Controls.Image)(target));
            
            #line 79 "..\..\..\Window1.xaml"
            this.imgAnalyzedDocument.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.imgAnalyzedDocument_MouseDown);
            
            #line default
            #line hidden
            
            #line 79 "..\..\..\Window1.xaml"
            this.imgAnalyzedDocument.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.imgAnalyzedDocument_MouseUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 9:
            
            #line 52 "..\..\..\Window1.xaml"
            ((System.Windows.Controls.TextBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 52 "..\..\..\Window1.xaml"
            ((System.Windows.Controls.TextBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 52 "..\..\..\Window1.xaml"
            ((System.Windows.Controls.TextBox)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.txtBox_MouseDown);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

