using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using BookWishList.Models;
using BookWishList.View;
using SoftwareBase;
using SoftwareBase.ViewModelBase;

namespace BookWishList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructor

        public MainWindowViewModel()
        {
            this.Books = new ObservableCollection<Book>();
            this.ShowNewBookWindow = new DelegateCommand<object>(ShowWindow, null);
            this.DeleteCommand = new DelegateCommand<Book>((Book b) => RemoveBook(b), null);
            this.EditCommand = new DelegateCommand<Book>((Book b) => MessageBox.Show("Aktuell noch nicht Implementiert. Wird in einer zukünftigen Version Hinzugefügt", "Info", MessageBoxButton.OK, MessageBoxImage.Information), null);
            this.MainFolder = new Folder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Bookywish");
            this.CheckDataExist(MainFolder.DirectoryPath + "Bookywish.xml");
        }

        #endregion

        #region Propertys
        private ObservableCollection<Book> _books;
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

        private Book _selectedBook;
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                if (_selectedBook != value)
                {
                    _selectedBook = value;
                    OnPropertyChanged();
                }
            }
        }
        private Folder MainFolder { get; set; }
        public DelegateCommand<object> ShowNewBookWindow { get; set; }
        public DelegateCommand<Book> DeleteCommand { get; set; }
        public DelegateCommand<Book> EditCommand { get; set; }
        #endregion

        #region Methods
        private void CheckDataExist(string path)
        {
            if (!File.Exists(path))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Book>));
                using (Stream s = File.OpenWrite(MainFolder.DirectoryPath + "Bookywish.xml"))
                {
                    xml.Serialize(s, new ObservableCollection<Book>() { new Book() { Titel = "Placeholder" } });
                }
            }
            LoadBooks();
        }

        private void LoadBooks()
        {
            ObservableCollection<Book> books;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Book>));
            using (Stream s = File.OpenRead(MainFolder.DirectoryPath + "Bookywish.xml"))
            {
                books = (ObservableCollection<Book>)xmlSerializer.Deserialize(s);
            }
            foreach (Book book in books)
            {
                Books.Add(book);
            }
            SelectedBook = Books[0];
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
            SelectedBook = Books[0];
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
            File.Delete(MainFolder.DirectoryPath + "Bookywish.xml");
            using (Stream s = File.Create(MainFolder.DirectoryPath + "Bookywish.xml"))
            {
                xml.Serialize(s, Books);
            }
        }
        #endregion
    }
}
