﻿#pragma checksum "..\..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B08367896AC6C6E2A8FE01D9ED6870853F06B71F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DocumentManagementApp;
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


namespace DocumentManagementApp {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 14 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition collapsedForm;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition listView;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition blankAfterCollapsedForm;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonFilter;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgFilter;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listOfFilters;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonAddNewFilter;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonApplyFilters;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView PLCLV;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonAnalyzeNewDocuments;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonAnalyzeOldDocument;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSettings;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DocumentManagementApp;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.collapsedForm = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 2:
            this.listView = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 3:
            this.blankAfterCollapsedForm = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 4:
            this.buttonFilter = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\MainWindow.xaml"
            this.buttonFilter.Click += new System.Windows.RoutedEventHandler(this.ButtonFilter_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.imgFilter = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.listOfFilters = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            this.buttonAddNewFilter = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\..\MainWindow.xaml"
            this.buttonAddNewFilter.Click += new System.Windows.RoutedEventHandler(this.buttonAddNewFilter_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.buttonApplyFilters = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\..\..\MainWindow.xaml"
            this.buttonApplyFilters.Click += new System.Windows.RoutedEventHandler(this.buttonApplyFilters_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.PLCLV = ((System.Windows.Controls.ListView)(target));
            return;
            case 11:
            this.buttonAnalyzeNewDocuments = ((System.Windows.Controls.Button)(target));
            
            #line 144 "..\..\..\..\MainWindow.xaml"
            this.buttonAnalyzeNewDocuments.Click += new System.Windows.RoutedEventHandler(this.ButtonAnalyzeNewDocuments_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.buttonAnalyzeOldDocument = ((System.Windows.Controls.Button)(target));
            
            #line 152 "..\..\..\..\MainWindow.xaml"
            this.buttonAnalyzeOldDocument.Click += new System.Windows.RoutedEventHandler(this.ButtonAnalyzeOldDocument_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.buttonSettings = ((System.Windows.Controls.Button)(target));
            
            #line 160 "..\..\..\..\MainWindow.xaml"
            this.buttonSettings.Click += new System.Windows.RoutedEventHandler(this.ButtonSettings_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 7:
            
            #line 54 "..\..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnDeleteFilter_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
