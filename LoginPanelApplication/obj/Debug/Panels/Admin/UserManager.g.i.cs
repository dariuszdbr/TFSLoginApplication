﻿#pragma checksum "..\..\..\..\Panels\Admin\UserManager.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A66F3762E61669E5CD342EE9615FABC0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LoginPanelApplication.Converters;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace LoginPanelApplication.Panels {
    
    
    /// <summary>
    /// UserManager
    /// </summary>
    public partial class UserManager : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\Panels\Admin\UserManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LoginPanelApplication.Panels.UserManager UserMgr;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Panels\Admin\UserManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridManager;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\Panels\Admin\UserManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn comboBoxStatus;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\Panels\Admin\UserManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddUser;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\..\Panels\Admin\UserManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDetails;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\Panels\Admin\UserManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteUser;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\..\Panels\Admin\UserManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LoginPanelApplication;component/panels/admin/usermanager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Panels\Admin\UserManager.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserMgr = ((LoginPanelApplication.Panels.UserManager)(target));
            return;
            case 2:
            this.DataGridManager = ((System.Windows.Controls.DataGrid)(target));
            
            #line 28 "..\..\..\..\Panels\Admin\UserManager.xaml"
            this.DataGridManager.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DataGridManager_MouseDoubleClick_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.comboBoxStatus = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 4:
            this.btnAddUser = ((System.Windows.Controls.Button)(target));
            
            #line 129 "..\..\..\..\Panels\Admin\UserManager.xaml"
            this.btnAddUser.Click += new System.Windows.RoutedEventHandler(this.btnAddUser_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnDetails = ((System.Windows.Controls.Button)(target));
            
            #line 130 "..\..\..\..\Panels\Admin\UserManager.xaml"
            this.btnDetails.Click += new System.Windows.RoutedEventHandler(this.btnDetails_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnDeleteUser = ((System.Windows.Controls.Button)(target));
            
            #line 131 "..\..\..\..\Panels\Admin\UserManager.xaml"
            this.btnDeleteUser.Click += new System.Windows.RoutedEventHandler(this.btnDeleteUser_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 133 "..\..\..\..\Panels\Admin\UserManager.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.btnBack_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
