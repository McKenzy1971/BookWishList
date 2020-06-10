using SoftwareBase.ViewModelBase;
using BookWishList.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System;

namespace BookWishList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructors

        public MainWindowViewModel()
        {
            Books = new ObservableCollection<Book>();
        }

        #endregion

        #region Propertys

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

        private string FolderPath { get; set; }
        #endregion

        public void InitialLoad()
        {
            this.FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Bookywish\";
            this.CheckDircetoryExist(FolderPath);
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

        private void LoadBooks()
        {
            this.CheckDataExist(FolderPath + @"Bookywish.xml");
            using (FileStream fileStream = File.OpenRead(FolderPath + @"Bookywish.xml"))
            {
                byte[] data = new byte[100];
                fileStream.Read(data, 0, data.Length);
                if (String.IsNullOrEmpty(data.ToString()))
                    throw new NotImplementedException();
            }
        }

        private void CheckDircetoryExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(FolderPath);
            this.LoadBooks();
        }

        private void CheckDataExist(string path)
        {
            if (!File.Exists(path))
                File.Create(path);
        }
    }
}
