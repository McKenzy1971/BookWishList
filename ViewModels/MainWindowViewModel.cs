using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using BookWishList.Models;
using BookWishList.View;
using SoftwareBase;
using SoftwareBase.Serialize;
using SoftwareBase.ViewModelBase;

namespace BookWishList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructor
        public MainWindowViewModel()
        {
            this.Books = new ObservableCollection<Book>();
            this.ShowNewBookWindow = new DelegateCommand<object>(this.ShowWindow, (b) => this._isActiv);
            this.DeleteCommand = new DelegateCommand<Book>((Book b) => this.RemoveBook(b), (b) => this.SelectedBook != null);
            this.EditCommand = new DelegateCommand<Book>(this.EditBook, (b) => this.SelectedBook != null);
            this.MainFolder = new Folder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Bookywish");
            this.CheckDataExist();
        }
        #endregion

        #region Fields
        private Book _selectedBook;
        private ObservableCollection<Book> _books;
        private bool _isActiv = true;
        #endregion

        #region Properties
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                if (_books != value)
                {
                    _books = value;
                    OnPropertyChanged();
                }
            }
        }
        public Window Window { get; set; }
        public Book SelectedBook
        {
            get { return this._selectedBook; }
            set
            {
                if (this._selectedBook != value)
                {
                    this._selectedBook = value;
                    this.OnPropertyChanged();
                    this.EditCommand.RaiseCanExecuteChanged();
                    this.DeleteCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public Folder MainFolder { get; set; }
        public DelegateCommand<object> ShowNewBookWindow { get; set; }
        public DelegateCommand<Book> DeleteCommand { get; set; }
        public DelegateCommand<Book> EditCommand { get; set; }
        #endregion

        #region Methods
        private void CheckDataExist()
        {
            if (!this.MainFolder.CheckFileExist("Bookywish.xml"))
                Serialize<ObservableCollection<Book>>.SaveData(new ObservableCollection<Book>() { new Book() { Titel = "Placeholder" } }, this.MainFolder.DirectoryPath + "Bookywish.xml");
            this.LoadBooks();
        }
        private void LoadBooks()
        {
            this.Books = Serialize<ObservableCollection<Book>>.LoadData(MainFolder.DirectoryPath + "Bookywish.xml");
            if (this.Books.Count != 0)
                this.SelectedBook = this.Books[0];
        }
        public void RemoveBook(Book book)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Möchtest du das Buch wirklich löschen?", "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    Books.Remove(book);
                    SaveBooks();
                    break;
                case MessageBoxResult.No:
                    return;
            }
            if (this.Books.Count != 0)
                this.SelectedBook = this.Books[0];
        }
        public void ShowWindow(object o)
        {
            this.ChangeIsActiv(false);
            NewBook nb = new NewBook();
            nb.Show();
            this.Window.Hide();
            nb.ShowInTaskbar = false;
        }
        public void SaveBooks()
        {
            File.Delete(MainFolder.DirectoryPath + "Bookywish.xml");
            Serialize<ObservableCollection<Book>>.SaveData(this.Books, this.MainFolder.DirectoryPath + "Bookywish.xml");
        }
        public void ChangeIsActiv(bool b)
        {
            this._isActiv = b;
            this.ShowNewBookWindow.RaiseCanExecuteChanged();
            this.DeleteCommand.RaiseCanExecuteChanged();
            this.EditCommand.RaiseCanExecuteChanged();
        }
        public void EditBook(object o)
        {
            EditWindow eb = new EditWindow();
            this.Window.Hide();
            eb.Show();
            eb.ShowInTaskbar = false;
            eb.Activate();
        }
        #endregion
    }
}
