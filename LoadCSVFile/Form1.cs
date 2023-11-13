using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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


        private void LoadData(string path)
        {
            if(!string.IsNullOrEmpty(path))
            {
                //using (StreamReader readFile = new StreamReader(path))
                using (TextFieldParser parseFiled = new TextFieldParser(path))
                {
                    //shows that the data is delimited through commas
                    parseFiled.TextFieldType = FieldType.Delimited;
                    parseFiled.SetDelimiters(",");


                    //print column names
                    String[] headers = parseFiled.ReadFields();

                    foreach (String header in headers)
                    {
                        dataGridView1.Columns.Add(header, header);

                    }


                    //print rows
                    while (!parseFiled.EndOfData)
                    {
                        string[] rows = parseFiled.ReadFields();

                        dataGridView1.Rows.Add(rows);
                    }


                }


            }

            else
            {
                MessageBox.Show("Please upload a file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //upload a file to display its data
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();
            txtBrowseFile.Text = openFileDialog.FileName;
        }


        //Display the uploaded file data in a gridView
        private void btnLoadData_Click(object sender, EventArgs e)
        {
           LoadData(txtBrowseFile.Text);
            txtBrowseFile.Clear();
        }


        //Search a record
        private void Search(string record)
        {
            dataGridView1.ClearSelection();

            // Variable to track if any matching record is found
            bool matchFound = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Equals(record, StringComparison.OrdinalIgnoreCase))
                        {
                            row.Visible = true;
                            matchFound = true; 
                            break;
                        }
                        else
                        {
                            // Hide non-matching rows
                            row.Visible = false; 
                        }
                    }
                }
            }

                
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
