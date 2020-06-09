using SoftwareBase.ViewModelBase;
using BookWishList.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookWishList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Books = new ObservableCollection<Book>();
        }

        public ObservableCollection<Book> Books { get; }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get { return this._selectedBook; }
            set
            {
                if (this._selectedBook != value)
                {
                    this._selectedBook = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Load()
        {
            List<Book> books = new List<Book>
            {
                new Book() { Titel = "Placeholder One" },
                new Book() { Titel = "Placeholder Two" },
                new Book() { Titel = "Placeholder Three" },
                new Book() { Titel = "Placeholder Four" },
                new Book() { Titel = "Placeholder Five" }
            };

            Books.Clear();
            foreach (Book book in books)
                Books.Add(book);
            SelectedBook = Books[0];
        }
    }
}
