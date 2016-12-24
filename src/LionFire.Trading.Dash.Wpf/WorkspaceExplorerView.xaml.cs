﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LionFire.Trading.Dash.Wpf
{
    /// <summary>
    /// Interaction logic for WorkspaceExplorerView.xaml
    /// </summary>
    public partial class WorkspaceExplorerView : UserControl
    {
        public string DisplayName { get { return "Explorer"; } }

        public WorkspaceExplorerView()
        {
            InitializeComponent();
        }
    }
}
