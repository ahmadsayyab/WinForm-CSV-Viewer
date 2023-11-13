using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadCSVFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string path = @"C:\Users\Sayyabkhan\Desktop\SudentData.csv";

        DataTable dataTable = new DataTable();
        private void LoadData()
        {
            

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Get all records
                var records = csv.GetRecords<dynamic>().ToList();


                // Add columns to DataTable using the first record

                foreach (var record in records.First())
                {
                    dataTable.Columns.Add(record.Key);
                }

                // Add rows to DataTable
                foreach (var record in records)
                {
                    DataRow row = dataTable.NewRow();

                    foreach (var field in record)
                    {
                        row[field.Key] = field.Value;
                    }
                    dataTable.Rows.Add(row);
                }

            }

            dataGridView1.DataSource = dataTable;
        }



        //Display csv file data in a gridView
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadData();

        }


        //Search a record
        private void Search(string record)
        {
            dataGridView1.ClearSelection();

            // Variable to track if any matching record is found
            bool matchFound = false;

            // Create a new DataTable with the same structure as the original DataTable
            DataTable filteredTable = dataTable.Clone();

            foreach (DataRow row in dataTable.Rows)
            {
                bool rowVisible = false;

                foreach (DataColumn column in dataTable.Columns)
                {
                    if (row[column] != null && row[column].ToString().Equals(record, StringComparison.OrdinalIgnoreCase))
                    {
                        rowVisible = true;
                        matchFound = true;
                        break;
                    }
                }

                // Add the row to the filtered DataTable if it matches the search term
                if (rowVisible)
                {
                    filteredTable.ImportRow(row);
                }
            }

            // Set the DataGridView's DataSource to the filtered DataTable
            dataGridView1.DataSource = filteredTable;

            if (!matchFound)
            {
                MessageBox.Show("Record not found!!!", "Record Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }







        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchRecord = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchRecord))
            {
                Search(searchRecord);
            }
            else
            {
                MessageBox.Show("Please enter into the Search bar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
