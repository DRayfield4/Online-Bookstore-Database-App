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

namespace OnlineBookstore
{
    public partial class HomepageForm : Form
    {
        private int _maxbook = 0;
        public HomepageForm()
        {
            InitializeComponent();

        }



        private void HomepageForm_Load(object sender, EventArgs e)
        {

            uxBuy.Enabled = false;
            uxRemove.Enabled = false;
            //disables buttons

            //cant test if this works but quite sure it dosnt. 
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string query =
            @"SELECT 
                b.Title AS BookTitle, 
                a.AuthorName
            FROM Authors a
            INNER JOIN Books b ON a.AuthorID = b.AuthorID
            ORDER BY b.Title";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string bookTitle = reader["BookTitle"].ToString();
                        string AuthorID = reader["AuthorID"].ToString();

                        uxBookList.Items.Add($"{bookTitle} by {AuthorID}");
                    }
                }
            }
            _maxbook = uxBookList.Items.Count; //gets max book count
            uxDisplaying.Text = _maxbook + " of " + _maxbook;
        }


        private void uxSearchButton_Click(object sender, EventArgs e)
        {
            //if user serches for "" should they be able to return a list of everythign just in a different order?
            string searchterm = uxSearchBox.Text.ToString();
            uxBookList.Items.Clear();
            switch (uxFilter.SelectedItem.ToString())
            {
                case "Title":
                    ExecuteQueryTitle(searchterm);
                    break;
                case "Author":
                    ExecuteQueryAuthor(searchterm);
                    break;
                case "ISBN":
                    ExecuteQueryISBN(searchterm);
                    break;
                case "Genre":
                    ExecuteQueryGenre(searchterm);
                    break;
                case "Price":
                    ExecuteQueryPrice(searchterm);
                    break;
                case "Publisher":
                    ExecuteQueryPublisher(searchterm);
                    break;
                default:
                    MessageBox.Show("Something went wrong");
                    break;
            }
            uxDisplaying.Text = uxBookList.Items.Count + " of " + _maxbook;
        }
        private void uxBuy_Click(object sender, EventArgs e)
        {

            //in the future would open the buy form and somehow send all of the buylist to it
        }
        //important all sql queres here
        #region Queries to complete
        private void ExecuteQueryTitle(string filter)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string query =
                @"QUERY";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) // getting ISBN error here
                {
                    while (reader.Read())
                    {

                        uxBookList.Items.Add($"");
                    }
                }
            }
        }
        private void ExecuteQueryAuthor(string filter)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string query =
                @"QUERY";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) // getting ISBN error here
                {
                    while (reader.Read())
                    {

                        uxBookList.Items.Add($"");
                    }
                }
            }
        }
        private void ExecuteQueryISBN(string filter)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string query =
                @"QUERY";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) // getting ISBN error here
                {
                    while (reader.Read())
                    {

                        uxBookList.Items.Add($"");
                    }
                }
            }
        }
        private void ExecuteQueryGenre(string filter)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string query =
                @"QUERY";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) // getting ISBN error here
                {
                    while (reader.Read())
                    {

                        uxBookList.Items.Add($"");
                    }
                }
            }
        }
        private void ExecuteQueryPrice(string filter)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string query =
                @"QUERY";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) // getting ISBN error here
                {
                    while (reader.Read())
                    {

                        uxBookList.Items.Add($"");
                    }
                }
            }
        }
        private void ExecuteQueryPublisher(string filter)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string query =
                @"QUERY";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) // getting ISBN error here
                {
                    while (reader.Read())
                    {

                        uxBookList.Items.Add($"");
                    }
                }
            }
        }
        #endregion 
        //important all sql queres here


        private void uxAdd_Click(object sender, EventArgs e)
        {
            string isbn = (uxBookList.SelectedItem as dynamic).ISBN;
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBookstoreDb"].ConnectionString;
            string query = @"
        SELECT ISBN, Title, Price
        FROM Books 
        WHERE ISBN = @ISBN";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", isbn);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        uxBuyList.Items.Add(reader["Title"].ToString() + reader["Price"].ToString());
                    }
                }
            }
            //uxBuyList.Items.Add(uxBookList.SelectedItem);
            if (uxRemove.Enabled = false && uxBuyList.Items.Count >= 1)
            {
                uxRemove.Enabled = true;
            }
            updatePrice();
        }
        private void updatePrice()
        {
            double price = 0.0;
            foreach (var item in uxBuyList.Items)
            {
                uxBuyList.Items[uxBuyList.SelectedIndex];
                string item2 = item.ToString();
                int index = item2.IndexOf('$') + 1;
                string substring = item2.Substring(index);
                price += double.Parse(substring);//should work probably
            }
            uxPrice.Text = "Total: $" + price.ToString();
        }
        private void uxRemove_Click(object sender, EventArgs e)
        {
            uxBuyList.Items.RemoveAt(uxBuyList.SelectedIndex);
            if (uxBuyList.Items.Count - 1 <= 0)
            {
                uxRemove.Enabled = false;
            }
            updatePrice();
        }

        private void uxBuyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //add funcanality so if nothing selected then disable button
            if (uxBuyList.Items.Count >= 1)
            {
                uxRemove.Enabled = true;
            }

        }
    }
}