using System.Windows;
using BookWishList.ViewModels;

namespace BookWishList.View
{
    /// <summary>
    /// Interaction logic for NewBook.xaml
    /// </summary>
    public partial class NewBook : Window
    {
        public NewBookViewModel NewBookViewModel => this.DataContext as NewBookViewModel;
        public NewBook()
        {
            InitializeComponent();
            NewBookViewModel.Window = GetWindow(this);
            Closing += NewBookViewModel.OnWindowClosing;
        }
    }
}
