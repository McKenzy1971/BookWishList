using SoftwareBase.ViewModelBase;
using SoftwareBase;
using BookWishList.Models;
using System.Collections.ObjectModel;
using System.IO;
using System;
using System.Xml.Serialization;
using BookWishList.View;
using System.Runtime.CompilerServices;

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
            this.MainFolder = new Folder();
            this.MainFolder.DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Bookywish\";
            this.MainFolder.AddFile("Bookywish.xml");
            this.MainFolder.SetActivFile("Bookywish.xml");
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

        public void RemoveBook(Book book)
        {
            this.Books.Remove(book);
            this.SaveBooks();
        }

        public void ShowWindow(object o)
        {
            NewBook nb = new NewBook();
            nb.Show();
            nb.ShowInTaskbar = true;
        }

        public void InitialLoad()
        {
            this.CheckDircetoryExist(this.MainFolder.DirectoryPath);
            this.CheckDataExist(this.MainFolder.File);
        }
        public void SaveBooks()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Book>));
            File.Delete(this.MainFolder.File);
            using (Stream s = File.OpenWrite(this.MainFolder.File))
            {
                xml.Serialize(s, this.Books);
            }
        }

        private void LoadBooks()
        {
            ObservableCollection<Book> books;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Book>));
            using (Stream s = File.OpenRead(this.MainFolder.File))
            {
                books = (ObservableCollection<Book>)xmlSerializer.Deserialize(s);
            }
            foreach (Book book in books)
            {
                Books.Add(book);
            }
        }

        private void CheckDircetoryExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(this.MainFolder.DirectoryPath);
        }

        private void CheckDataExist(string path)
        {
            if (!File.Exists(path))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Book>));
                using (Stream s = File.OpenWrite(this.MainFolder.File))
                {
                    xml.Serialize(s, new ObservableCollection<Book>());
                }
            }
            this.LoadBooks();
        }
        #endregion
    }
}
