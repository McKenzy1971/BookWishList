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
        public EditWindowViewModel _editWindowViewModel;
        public EditWindow()
        {
            InitializeComponent();
            this._editWindowViewModel = new EditWindowViewModel();
            this.DataContext = this._editWindowViewModel;
            this._editWindowViewModel.Window = GetWindow(this);
            this.Closing += this._editWindowViewModel.OnWindowClosing;
        }
    }
}
