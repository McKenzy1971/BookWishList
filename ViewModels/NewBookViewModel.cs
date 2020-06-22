using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using BookWishList.Models;
using BookWishList.View;
using SoftwareBase.ViewModelBase;

namespace BookWishList.ViewModels
{
    public class NewBookViewModel : ViewModelBase
    {
        #region Constructor
        public NewBookViewModel()
        {
            this.AddedBook = new Book() { Titel = "Dein Hasi", Description = "Bester Freund auf der Welt", Price = "Unbezahlbar", Author = "Dennis Schröter", Theme = Theme.Psychology };
            this.SaveCommand = new DelegateCommand<object>(Save, null);
        }
        #endregion

        #region Fields
        private Book _addedbook;
        #endregion

        #region Properties
        public Book AddedBook
        {
            get { return _addedbook; }
            set
            {
                if (_addedbook != value)
                {
                    _addedbook = value;
                    OnPropertyChanged();
                }
            }
        }
        public IEnumerable<Theme> Themes
        {
            get { return Enum.GetValues(typeof(Theme)).Cast<Theme>(); }
        }
        public DelegateCommand<object> SaveCommand { get; set; }
        public Window Window { get; set; }
        #endregion

        #region Methods
        public void Save(object o)
        {
            ((MainWindow)Application.Current.MainWindow).MainWindowViewModel.Books.Add(this.AddedBook);
            ((MainWindow)Application.Current.MainWindow).MainWindowViewModel.SaveBooks();
            this.Window.Close();
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainWindowViewModel.ChangeIsActiv(true);
            ((MainWindow)Application.Current.MainWindow).MainWindowViewModel.Window.Visibility = Visibility.Visible;
        }
        #endregion
    }
}
