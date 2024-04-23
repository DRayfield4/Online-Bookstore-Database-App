﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net;

namespace OnlineBookstore
{
    public partial class BuyForm : Form
    {
        private List<string> _booksToPurchase;

        public BuyForm(List<string> booksToPurchase)
        {
            InitializeComponent();
            _booksToPurchase = booksToPurchase;
            DisplayBooks();
        }

        private void DisplayBooks()
        {
            foreach (string book in _booksToPurchase)
            {
                uxBuyList.Items.Add(book);
            }
        }

        private void uxConfirmPurchase_Click(object sender, EventArgs e)
        {
            string firstName = uxFirstNameTextBox.Text;
            string lastName = uxLastNameTextBox.Text;
            string address = uxAddressTextBox.Text;
            string phone = uxPhoneNumberTextBox.Text;

            // ADD ERROR HANDLING FOR FORMATTING

            if(InsertCustomerData(firstName, lastName, address, phone))
            {
                MessageBox.Show("Purchase confirmed!");
                this.Close();
            }
        }

        private bool InsertCustomerData(string firstName, string lastName, string address, string phone)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string customerQuery = "INSERT INTO Customers (FirstName, LastName, Address, Phone) VALUES (@FirstName, @LastName, @Address, @Phone);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(customerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
                return false;
            }
        }
    }
}
