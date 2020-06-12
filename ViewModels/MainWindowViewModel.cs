using SoftwareBase.ViewModelBase;
using BookWishList.Models;
using System.Collections.ObjectModel;
using System.IO;
using System;
using System.Xml.Serialization;
using BookWishList.View;

namespace BookWishList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructors

        public MainWindowViewModel()
        {
            Books = new ObservableCollection<Book>();
            this.ShowNewBookWindow = new DelegateCommand<object>(ShowWindow, null);
        }

        #endregion

        #region Propertys
        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get { return this._books; }
            set
            {
                if(this._books != value)
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

        private string FolderPath { get; set; }
        #endregion
        public DelegateCommand<object> ShowNewBookWindow { get; set; }
        #region Methods

        public void ShowWindow(object o)
        {
            NewBook nb = new NewBook();
            nb.Show();
            nb.ShowInTaskbar = true;
        }

        public void InitialLoad()
        {
            this.FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Bookywish";
            this.CheckDircetoryExist(FolderPath);
            this.CheckDataExist(FolderPath + @"\Bookywish.xml");
        }
        public void SaveBooks()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Book>));
            File.Delete(FolderPath + @"\Bookywish.xml");
            using (Stream s = File.OpenWrite(FolderPath + @"\Bookywish.xml"))
            {
                xml.Serialize(s, this.Books);
            } 
        }

        private void LoadBooks()
        {
            ObservableCollection<Book> books;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Book>));
            using (Stream s = File.OpenRead(FolderPath + @"\Bookywish.xml"))
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
                Directory.CreateDirectory(FolderPath);
        }

        private void CheckDataExist(string path)
        {
            if (!File.Exists(path))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Book>));
                using (Stream s = File.OpenWrite(FolderPath + @"\Bookywish.xml"))
                {
                    xml.Serialize(s, new ObservableCollection<Book>()
                    {
                        new Book() { Titel = "Placeholder", Description = "Test", Price = "Kostenlos" },
                        new Book() { Titel = "Placeholder2", Description = "Test2", Price = "Kostenlos2" }
                    });
                }
            }
            this.LoadBooks();
        }
        #endregion
    }
}
