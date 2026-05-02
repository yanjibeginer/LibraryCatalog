using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryCatalog
{
    public partial class Form1 : Form
    {
        private List<Book> catalogList;
        public Form1()
        {
            InitializeComponent();

            catalogList = new List<Book>();

            catalogList.Add(new Book());
            RefreshCatalogList();
        }

        //refresh the ListBox
        private void RefreshCatalogList()
        {
            lstBooks.DataSource = null;
            lstBooks.DataSource = catalogList;
            lstBooks.DisplayMember = "ListBoxDisplay";
        }

        //add Book 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string isbn = txtISBN.Text.Trim();
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string yearInput = txtYear.Text.Trim();
            string copiesInput = txtCopies.Text.Trim();

            // empty required fields
            if (string.IsNullOrEmpty(isbn) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
            {
                MessageBox.Show("ISBN, Title, and Author are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Book newBook;

            // Check if Year and Copies are left blank to use partial constructor
            if (string.IsNullOrEmpty(yearInput) && string.IsNullOrEmpty(copiesInput))
            {
                newBook = new Book(isbn, title, author); // Uses partial constructor
            }
            else
            {
                //Handle if wrong year and copies input
                if (!int.TryParse(yearInput, out int year) || !int.TryParse(copiesInput, out int copies))
                {
                    MessageBox.Show("Year Published and Copies Available must be valid integers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                newBook = new Book(isbn, title, author, year, copies); 
            }

            catalogList.Add(newBook);
            RefreshCatalogList();
            btnClear_Click(sender, e); // clear after added
        }

        //ListBox
        private void lstBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDetailsView();
        }

        // update the Details text box
        private void RefreshDetailsView()
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                txtDetails.Text = selectedBook.GetDetails();
            }
            else
            {
                txtDetails.Clear();
            }
        }

        // Borrow button
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                bool success = selectedBook.BorrowCopy();
                if (!success)
                {
                    MessageBox.Show("No copies are available to borrow.", "Borrow Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                RefreshDetailsView(); 
            }
        }

        //Return button 
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                selectedBook.ReturnCopy();
                RefreshDetailsView(); 
            }
        }

        // Clear Fields
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtISBN.Clear();
            txtTitle.Clear();
            txtAuthor.Clear();
            txtYear.Clear();
            txtCopies.Clear();
            txtISBN.Focus();
        }

        // Remove Book 
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                catalogList.Remove(selectedBook);
                RefreshCatalogList();
                txtDetails.Clear();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
    }
    
