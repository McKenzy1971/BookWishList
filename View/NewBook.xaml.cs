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
    /// Interaction logic for NewBook.xaml
    /// </summary>
    public partial class NewBook : Window
    {
        private NewBookViewModel _NewBookViewModel;
        public NewBook()
        {
            InitializeComponent();
            _NewBookViewModel = new NewBookViewModel();
            DataContext = _NewBookViewModel;
            _NewBookViewModel.Window = Window.GetWindow(this);
            Closing += _NewBookViewModel.OnWindowClosing;
        }
    }
}
