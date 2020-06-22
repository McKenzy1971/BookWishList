using System;
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
using System.Windows.Shapes;
using BookWishList.ViewModels;

namespace BookWishList.View
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindowViewModel EditWindowViewModel => this.DataContext as EditWindowViewModel;
        public EditWindow()
        {
            InitializeComponent();
            this.EditWindowViewModel.Window = GetWindow(this);
            this.Closing += this.EditWindowViewModel.OnWindowClosing;
        }
    }
}
