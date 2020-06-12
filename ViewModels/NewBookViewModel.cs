using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareBase.ViewModelBase;
using System.Threading.Tasks;
using BookWishList.Models;
using BookWishList.View;
using System.Windows;

namespace BookWishList.ViewModels
{
    public class NewBookViewModel : ViewModelBase
    {
        public NewBookViewModel()
        {
            this.Book = new Book() { Titel = "Beispieltitel", Description = "Besipeilhabsd", Price = "19,99€" };
            this.SaveCommand = new DelegateCommand<object>(Save, null);
        }
        private Book _book;
        public Book Book
        {
            get { return this._book; }
            set
            {
                if (this._book != value)
                {
                    this._book = value;
                    OnPropertyChanged();
                }
            }
        }
        public DelegateCommand<object> SaveCommand { get; set; }

        public void Save(object o)
        {
            ((MainWindow)Application.Current.MainWindow)._mainWindowViewModel.Books.Add(this.Book);
            ((MainWindow)Application.Current.MainWindow)._mainWindowViewModel.SaveBooks();
        }
    }
}
