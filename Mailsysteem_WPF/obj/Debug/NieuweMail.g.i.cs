﻿#pragma checksum "..\..\NieuweMail.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "16986FC6CD61EBB49CB3BD5F0D19ACA1499A8E66B5050E33EAC3B68D8CE55885"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Mailsysteem_WPF;
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


namespace Mailsysteem_WPF {
    
    
    /// <summary>
    /// NieuweMail
    /// </summary>
    public partial class NieuweMail : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\NieuweMail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnVerzenden;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\NieuweMail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblAan;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\NieuweMail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbOntvangers;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\NieuweMail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCc;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\NieuweMail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbOntvangersCc;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\NieuweMail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblOnderwerp;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\NieuweMail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbOnderwerp;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\NieuweMail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbBerichtBody;
        
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
            System.Uri resourceLocater = new System.Uri("/Mailsysteem_WPF;component/nieuwemail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NieuweMail.xaml"
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
            this.btnVerzenden = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\NieuweMail.xaml"
            this.btnVerzenden.Click += new System.Windows.RoutedEventHandler(this.btnVerzenden_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblAan = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.tbOntvangers = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.lblCc = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.tbOntvangersCc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.lblOnderwerp = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.tbOnderwerp = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.tbBerichtBody = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

