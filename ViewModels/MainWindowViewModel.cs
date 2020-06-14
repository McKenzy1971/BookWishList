using SoftwareBase.ViewModelBase;
using SoftwareBase;
using BookWishList.Models;
using System.Collections.ObjectModel;
using System.IO;
using System;
using System.Xml.Serialization;
using BookWishList.View;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookWishList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructor

        public MainWindowViewModel()
        {
            Books = new ObservableCollection<Book>();
            this.ShowNewBookWindow = new DelegateCommand<object>(ShowWindow, null);
            this.DeleteCommand = new DelegateCommand<Book>((Book b) => RemoveBook(b), null);
            this.MainFolder = new Folder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Bookywish");
            this.CheckDataExist(this.MainFolder.DirectoryPath + "Bookywish.xml");
        }

        #endregion

        #region Propertys
        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get { return this._books; }
            set
            {
                if (this._books != value)
                {
                    this._books = value;
                    OnPropertyChanged();
                }
            }
        }

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
        private Folder MainFolder { get; set; }
        public DelegateCommand<object> ShowNewBookWindow { get; set; }
        public DelegateCommand<Book> DeleteCommand { get; set; }
        #endregion

        #region Methods
        private void CheckDataExist(string path)
        {
            if (!File.Exists(path))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Book>));
                using (Stream s = File.OpenWrite(this.MainFolder.DirectoryPath + "Bookywish.xml"))
                {
                    xml.Serialize(s, new ObservableCollection<Book>() { new Book() { Titel = "Placeholder" } });
                }
            }
            this.LoadBooks();
        }

        private void LoadBooks()
        {
            ObservableCollection<Book> books;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Book>));
            using (Stream s = File.OpenRead(this.MainFolder.DirectoryPath + "Bookywish.xml"))
            {
                books = (ObservableCollection<Book>)xmlSerializer.Deserialize(s);
            }
            foreach (Book book in books)
            {
                Books.Add(book);
            }
        }

        public void RemoveBook(Book book)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Möchtest du das Buch wirklich löschen?", "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    this.Books.Remove(book);
                    this.SaveBooks();
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }

        public void ShowWindow(object o)
        {
            NewBook nb = new NewBook();
            nb.Show();
            nb.ShowInTaskbar = true;
        }

        public void SaveBooks()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Book>));
            File.Delete(this.MainFolder.DirectoryPath + "Bookywish.xml");
            using (Stream s = File.Create(this.MainFolder.DirectoryPath + "Bookywish.xml"))
            {
                xml.Serialize(s, this.Books);
            }
        }
        #endregion
    }
}
