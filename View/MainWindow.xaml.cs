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
using BookWishList.ViewModels;

namespace BookWishList.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel _mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            this._mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = _mainWindowViewModel;
            this._mainWindowViewModel.Window = GetWindow(this);
        }
    }
}
