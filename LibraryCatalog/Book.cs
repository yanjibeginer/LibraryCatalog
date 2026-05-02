using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalog
{
    public class Book
    {
    // 4. Private instance variables
        private string _isbn;
        private string _title;
        private string _author;
        private int _yearPublished;
        private int _copies;

        // 5. Public properties with Encapsulation
        public string ISBN
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public int YearPublished
        {
            get { return _yearPublished; }
            set
            {
                // Rejects values below 1450 or above the current year
                int currentYear = DateTime.Now.Year;
                if (value < 1450 || value > currentYear)
                    _yearPublished = 1450;
                else
                    _yearPublished = value;
            }
        }

        public int Copies
        {
            get { return _copies; }
            set
            {
                // Rejects values below 0
                if (value < 0)
                    _copies = 0;
                else
                    _copies = value;
            }
        }

        // Helper property to format the string for the ListBox
        public string ListBoxDisplay
        {
            get { return $"{Title} {ISBN}"; }
        }

        // 6. Default constructor (No-argument)
        public Book()
        {
            ISBN = "000-0000000000";
            Title = "Untitled";
            Author = "Unknown";
            YearPublished = 1450;
            Copies = 0;
        }

        // 7. Partial overloaded constructor
        // Chains to the default constructor to ensure Year and Copies get default values
        public Book(string isbn, string title, string author) : this()
        {
            ISBN = isbn;
            Title = title;
            Author = author;
        }

        // 8. Full overloaded constructor
        public Book(string isbn, string title, string author, int yearPublished, int copies)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            YearPublished = yearPublished; // Assigned through property to trigger validation
            Copies = copies;               // Assigned through property to trigger validation
        }

        // 9. GetDetails() method
        public string GetDetails()
        {
            return $"Title: {Title}\r\n" +
                   $"Author: {Author}\r\n" +
                   $"ISBN: {ISBN}\r\n" +
                   $"Year Published: {YearPublished}\r\n" +
                   $"Copies Available: {Copies}";
        }

        // 10. BorrowCopy() and ReturnCopy() methods
        public bool BorrowCopy()
        {
            if (Copies > 0)
            {
                Copies--;
                return true;
            }
            return false;
        }

        public bool ReturnCopy()
        {
            Copies++;
            return true;
        }
    }
}