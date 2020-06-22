using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BookWishList.Models;
using BookWishList.View;
using SoftwareBase.ViewModelBase;

namespace BookWishList.ViewModels
{
    public class EditWindowViewModel : ViewModelBase
    {
        #region Constructor
        public EditWindowViewModel()
        {
            this.Book = ((MainWindow)Application.Current.MainWindow).MainWindowViewModel.SelectedBook;
            this.DelegateCommand = new DelegateCommand<Book>(this.Save, null);
        }
        #endregion

        #region Properties
        public Window Window { get; set; }
        public Book Book { get; set; }
        public IEnumerable<Theme> Themes
        {
            get { return Enum.GetValues(typeof(Theme)).Cast<Theme>(); }
        }
        public DelegateCommand<Book> DelegateCommand { get; set; }
        #endregion

        #region Methods
        public void OnWindowClosing(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
        }
        public void Save(object o)
        {
            ((MainWindow)Application.Current.MainWindow).MainWindowViewModel.SaveBooks();
            this.Window.Close();
        }
        #endregion
    }
}