// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Ritik_Agarwal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Spreadsheet_Ritik_Agarwal for winforms.
    /// </summary>
    public partial class Spreadsheet_Ritik_Agarwal : Form
    {
        private CPTS321.Spreadsheet sheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet_Ritik_Agarwal"/> class.
        /// </summary>
        [Obsolete]
        public Spreadsheet_Ritik_Agarwal()
        {
            this.InitializeComponent();
            this.Load += new EventHandler(this.Form1_Load);

            // Creating the spread sheet object.
            this.sheet = new CPTS321.Spreadsheet(50, 26);

            // Property change.
            this.sheet.PropertyChanged += this.UdateCellPropertyChange;
        }

        /// <summary>
        /// Form1_Load function for loading the spreadsheet.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.InitializeGrid();
        }

        /// <summary>
        /// Function is called for the creation of rows and columns.
        /// </summary>
        private void InitializeGrid()
        {
            // Clearing the rows and columns.
            this.dataGridView.Rows.Clear();
            this.dataGridView.Columns.Clear();
            this.dataGridView.AutoResizeColumns();

            // Setting up the font and colors.
            this.dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            this.dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(this.dataGridView.Font, FontStyle.Bold);
            this.undoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Enabled = false;

            for (char i = 'A'; i <= 'Z'; i++)
            {
                this.dataGridView.Columns.Add(i.ToString(), i.ToString());
            }

            this.dataGridView.Rows.Add(50);
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        /// <summary>
        /// Updates the cell in the spreadsheet.
        /// </summary>
        /// <param name="sender">The object which is getting updated.</param>
        /// <param name="e"> Event that is triggered.</param>
        private void UdateCellPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender is CPTS321.Cell)
            {
                // If the property changed is the Text.
                if (e.PropertyName.CompareTo("Text") == 0)
                {
                    this.dataGridView.Rows[((CPTS321.Cell)sender).RowIndex].Cells[((CPTS321.Cell)sender).ColumnIndex].Value = ((CPTS321.Cell)sender).Value;
                }

                // If the property changed is the BGColor
                if (e.PropertyName.CompareTo("BGColor") == 0)
                {
                    this.dataGridView.Rows[((CPTS321.Cell)sender).RowIndex].Cells[((CPTS321.Cell)sender).ColumnIndex].Style.BackColor = Color.FromArgb((int)this.sheet.SpreadsheetArray[((CPTS321.Cell)sender).RowIndex, ((CPTS321.Cell)sender).ColumnIndex].BGColor);
                }
            }
            else if (sender is CPTS321.IUndoRedo)
            {
                string temp = e.PropertyName;

                if (temp == "Undo not empty")
                {
                    this.undoToolStripMenuItem.Enabled = true;
                }
                else if (temp == "Undo empty")
                {
                    this.undoToolStripMenuItem.Enabled = false;
                }
                else if (temp == "Redo not empty")
                {
                    this.redoToolStripMenuItem.Enabled = true;
                }
                else if (temp == "Redo empty")
                {
                    this.redoToolStripMenuItem.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Updating the cell value changed.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event object.</param>
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Initializing the grid causes e.ColumnIndex to return -1. This if statement is needed to avoid out of bounds errors.
            if (e.ColumnIndex != -1)
            {
                // Sets the text in the cell in the sheet to the text in the corresponding dataGridView cell.
                this.sheet.GetCell(e.RowIndex, e.ColumnIndex).Text = this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        /// <summary>
        /// DataGrid view cell content.
        /// </summary>
        /// <param name="sender"> object sender.</param>
        /// <param name="e"> event varibale.</param>
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// Click event implementation.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event object.</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            // Reinitialize these variables in every loop to get a unique random number.
            for (int i = 0; i < 50; i++)
            {
                Random random1 = new Random();
                Random random2 = new Random();
                int row, col;
                row = random1.Next(0, 50);
                col = random2.Next(0, 26);

                this.sheet.SpreadsheetArray[row, col].Text = "I love C#";
            }

            // Loop for column B
            for (int j = 0; j < this.sheet.RowCount; j++)
            {
                this.sheet.SpreadsheetArray[j, 1].Text = "Cell B" + (j + 1).ToString();
            }

            // Loop for column A
            for (int k = 0; k < this.sheet.RowCount; k++)
            {
                this.sheet.SpreadsheetArray[k, 0].Text = "=B" + (k + 1).ToString();
            }
        }

        private void Spreadsheet_Ritik_Agarwal_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Event handler for CellEndEdit, changes the DataGridView's Value to match the Cell's evaluated Value.
        /// </summary>
        /// <param name="sender">The sender is the obejct of the class calling the method.</param>
        /// <param name="e">Event object.</param>
        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CPTS321.Cell cell = this.sheet.GetCell(e.RowIndex, e.ColumnIndex);

            if ((string)this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                // Creating the object for Text changed event and then adding on to the Undo stack.
                CPTS321.TextChangedEvents command = new CPTS321.TextChangedEvents(cell, cell.Text, this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), "Text");

                this.sheet.AddUndoStack(command);
                this.undoToolStripMenuItem.Text = "Undo " + this.sheet.GetTypeOfUndoStackTop();
                this.sheet.SpreadsheetArray[e.RowIndex, e.ColumnIndex].Text = this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.sheet.SpreadsheetArray[e.RowIndex, e.ColumnIndex].Value;
            }
        }

        /// <summary>
        /// Event handler for CellBeginEdit, changes the DataGridView's respective Cell value to match the Cell Text property.
        /// </summary>
        /// <param name="sender">The sender is the obejct of the class calling the method.</param>
        /// <param name="e">Event object.</param>
        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.sheet.SpreadsheetArray[e.RowIndex, e.ColumnIndex].Text;
        }

        private void ChangeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog myDialog = new ColorDialog();

            // Stack of the selected cells and old colors.
            List<CPTS321.Cell> cells = new List<CPTS321.Cell>();
            List<uint> oldColors = new List<uint>();

            // Keeps the user from selecting a custom color.
            myDialog.AllowFullOpen = false;

            // Allows the user to get help. (The default is false.)
            myDialog.ShowHelp = true;

            // Sets the initial color select to the current text color.
            myDialog.Color = this.dataGridView.SelectedCells[0].Style.BackColor;

            // Update the text box color if the user clicks OK
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < this.dataGridView.SelectedCells.Count; i++)
                {
                    // Getting the row and the column number of the selected cell.
                    int row = this.dataGridView.SelectedCells[i].RowIndex;
                    int col = this.dataGridView.SelectedCells[i].ColumnIndex;

                    // Adding the cell changed and the old color to the list.
                    cells.Add(this.sheet.GetCell(row, col));
                    oldColors.Add(this.sheet.GetCell(row, col).BGColor);

                    // Setting the background color property of the cell.
                    this.sheet.GetCell(row, col).BGColor = this.sheet.ColorToUint(myDialog.Color);

                    // Changing the UI cell color.
                    this.dataGridView.SelectedCells[i].Style.BackColor = myDialog.Color;
                }

                CPTS321.ColorChangedEvent command = new CPTS321.ColorChangedEvent(cells, oldColors, this.sheet.ColorToUint(myDialog.Color), "Background Color");
                this.sheet.AddUndoStack(command);
                this.undoToolStripMenuItem.Text = "Undo " + this.sheet.GetTypeOfUndoStackTop();
            }
        }

        /// <summary>
        /// Undo the spreasheet event when the button is clicked.
        /// </summary>
        /// <param name="sender">Sender is the object.</param>
        /// <param name="e">Event happening. </param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sheet.Undo();
            if (this.sheet.GetCountUndoStack() > 0)
            {
                this.undoToolStripMenuItem.Text = "Undo " + this.sheet.GetTypeOfUndoStackTop();
            }

            if (this.sheet.GetCountRedoStack() > 0)
            {
                this.redoToolStripMenuItem.Text = "Redo " + this.sheet.GetTypeOfRedoStackTop();
            }
        }

        /// <summary>
        /// Redo the spreasheet event when the button is clicked.
        /// </summary>
        /// <param name="sender">Sender is the object.</param>
        /// <param name="e">Event happening. </param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sheet.Redo();
            if (this.sheet.GetCountUndoStack() > 0)
            {
                this.undoToolStripMenuItem.Text = "Undo " + this.sheet.GetTypeOfUndoStackTop();
            }

            if (this.sheet.GetCountRedoStack() > 0)
            {
                this.redoToolStripMenuItem.Text = "Redo " + this.sheet.GetTypeOfRedoStackTop();
            }
        }

        /// <summary>
        /// Saves the spreadsheet in the XML file format.
        /// </summary>
        /// <param name="sender">Sender is the object.</param>
        /// <param name="e">Event happening.</param>
        private void SaveSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream fileStream;

            // Opening the save file window.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML File|*.xml";
            saveFileDialog.Title = "Save an XML File";

            // Setting the initial directory as the base directory.
            saveFileDialog.InitialDirectory = AppContext.BaseDirectory;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((fileStream = saveFileDialog.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    this.sheet.SaveSheet(fileStream);
                    fileStream.Close();
                }
            }
        }

        [Obsolete]
        private void LoadSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML File|*.xml";
            openFileDialog.Title = "Open an XML File";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != string.Empty)
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)openFileDialog.OpenFile();

                this.ClearSheet();
                this.sheet.LoadSheet(fs);
                fs.Close();
            }
        }

        // Clears the sheet so that it can be loaded from a file.
        private void ClearSheet()
        {
            // Clear the data grid view
            this.dataGridView.Rows.Clear();
            this.dataGridView.Columns.Clear();
            this.dataGridView.Refresh();

            // Make sure the user can't click the undo/redo buttons and crash the program since the stack is about to be emptied.
            this.undoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Enabled = false;

            // This is the event that is fired to create the data grid view and the spreadsheet. Calling it here reinitializes everything to base values.
            this.Form1_Load(this, new EventArgs());
        }
    }
}
