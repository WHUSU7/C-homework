﻿#pragma checksum "..\..\..\Pages\Home.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "464908646C470C6F95FB8E368D03E721FDAF342E8F9380A5AE5C6D11827BC813"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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
using work.Pages;


namespace work.Pages {
    
    
    /// <summary>
    /// Home
    /// </summary>
    public partial class Home : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AIBtn;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PeopleBtn;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LocalBtn;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HistoryBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/work;component/pages/home.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Home.xaml"
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
            this.AIBtn = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\Pages\Home.xaml"
            this.AIBtn.Click += new System.Windows.RoutedEventHandler(this.jumpToAI);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PeopleBtn = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Pages\Home.xaml"
            this.PeopleBtn.Click += new System.Windows.RoutedEventHandler(this.jumpToPvp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LocalBtn = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\Pages\Home.xaml"
            this.LocalBtn.Click += new System.Windows.RoutedEventHandler(this.jumpToLocal);
            
            #line default
            #line hidden
            return;
            case 4:
            this.HistoryBtn = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Pages\Home.xaml"
            this.HistoryBtn.Click += new System.Windows.RoutedEventHandler(this.jumpToHistory);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

