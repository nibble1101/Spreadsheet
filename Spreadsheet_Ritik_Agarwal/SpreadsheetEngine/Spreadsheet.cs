// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/*
 CPTS321 Spreadsheet assignment.

 Submitted by: Ritik Agarwal.
 WSU ID: 011707455.

 */

/*
 PS: THE SPREADSHEET IS MADE AGAIN IN .NETFRAMEWORK AS I MADE SPREADSHEET IN .NETCORE BY MISTAKE. PROFESSOR VENERA ASKED ME TO MAKE IT AGAIN AND SUBMIT IT WITH HW5.
 */

namespace CPTS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Spreadsheet class.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// Spreadsheet array.
        /// </summary>
        public Cell[,] SpreadsheetArray;

        private int columnCount;
        private int rowCount;

        private PropertyChangedEventHandler propertyChangedEventHandler;

        // The undo and redo command stacks
        private Stack<IUndoRedo> undoStack = new Stack<IUndoRedo>();
        private Stack<IUndoRedo> redoStack = new Stack<IUndoRedo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        [Obsolete]
        public Spreadsheet(int rows, int columns)
        {
            this.columnCount = columns;
            this.rowCount = rows;
            this.SpreadsheetArray = new SpreadsheetCell[rows, columns];
            this.propertyChangedEventHandler = this.OnCellPropertyChanged;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    this.SpreadsheetArray[r, c] = new SpreadsheetCell(r, c);
                    this.SpreadsheetArray[r, c].Text = string.Empty;
                    this.SpreadsheetArray[r, c].Value = string.Empty;
                    this.SpreadsheetArray[r, c].PropertyChanged += this.OnCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Property change object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        /// <summary>
        /// Gets property for returning the count of the rows in the spreadsheet.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>
        /// Gets property for returning the count of the columns in the spreadsheet.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
        }

        /// <summary>
        /// The function to get the object of the cell from the Array of the cell.
        /// </summary>
        /// <param name="row">Row number.</param>
        /// <param name="col">Column number.</param>
        /// <returns>Returns the cell object.</returns>
        public Cell GetCell(int row, int col)
        {
            if (row < this.RowCount && row >= 0 && col < this.ColumnCount && col >= 0)
            {
                return this.SpreadsheetArray[row, col];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets/Returns the cell at the specified cell name, or null if out of bounds.
        /// </summary>
        /// <param name="cellName">Name of the cell.</param>
        /// <returns>Cell object.</returns>
        public Cell GetCell(string cellName)
        {
            if (cellName.Length == 0)
            {
                return null;
            }

            int rowIndex;
            if (int.TryParse(cellName.Substring(1), out rowIndex))
            {
                int colIndex = (int)cellName[0] - 65;
                return this.GetCell(rowIndex - 1, colIndex);
            }

            // index out of bounds or other error.
            return null;
        }

        /// <summary>
        /// Method to check if the text entered needs to be evaluated.
        /// </summary>
        /// <param name="sender">Sender obejct.</param>
        /// <param name="e">Event variable e.</param>
        [Obsolete]
        public void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell changedCell;
            string newTextValue = ((Cell)sender).Text;
            int updatedRow = ((Cell)sender).RowIndex;
            int updatedCol = ((Cell)sender).ColumnIndex;

            if (sender is Cell)
            {
                if (e.PropertyName.ToString().CompareTo("Text") == 0)
                {
                    changedCell = this.GetCell(updatedRow, updatedCol);

                    if (changedCell != null && changedCell.Text.StartsWith("="))
                    {
                        try
                        {
                            if (changedCell.MyExpressionTree != null)
                            {
                                List<string> oldVariables = changedCell.MyExpressionTree.GetVariableNames();
                                foreach (var varName in oldVariables)
                                {
                                    var otherCell = this.GetCell(varName);
                                    if (otherCell != null)
                                    {
                                        // Unsubscribing.
                                        otherCell.PropertyChanged -= changedCell.CellPropertyChanged;
                                    }
                                }
                            }

                            changedCell.NewExpression(changedCell.Text.Substring(1));

                            if (this.CheckRef(ref changedCell))
                            {
                                changedCell.MyExpressionTree = new ExpressionTree(changedCell.Text.Substring(1));
                                changedCell.MyExpressionTree.Evaluate();
                                List<string> variables = changedCell.MyExpressionTree.GetVariableNames();

                                if (variables.Count == 0)
                                {
                                    this.SpreadsheetArray[updatedRow, updatedCol].Value = changedCell.MyExpressionTree.Evaluate().ToString();
                                }
                                else
                                {
                                    foreach (var varName in variables)
                                    {
                                        // Subscribe to new events based on variables in expression (Text).
                                        var otherCell = this.GetCell(varName);
                                        if (otherCell != null)
                                        {
                                            otherCell.PropertyChanged += changedCell.CellPropertyChanged;
                                        }

                                        string varValue = this.GetVariableValue(varName);
                                        double varValueAsDouble;

                                        double.TryParse(varValue, out varValueAsDouble);

                                        // Need to do this because strings are not handled by ExpressionTree.
                                        if (double.TryParse(varValue, out varValueAsDouble))
                                        {
                                            changedCell.MyExpressionTree.SetVariable(varName, varValueAsDouble);
                                            this.SpreadsheetArray[updatedRow, updatedCol].Value = changedCell.MyExpressionTree.Evaluate().ToString();
                                        }
                                        else
                                        {
                                            this.SpreadsheetArray[updatedRow, updatedCol].Value = varValue;
                                        }
                                    }
                                }
                            }
                        }
                        catch (MemberAccessException exception)
                        {
                            throw new InvalidCastException("Member access invalid.", exception);
                        }
                        catch (InvalidCastException exception)
                        {
                            throw new InvalidCastException("Illegal cast in CellPropertyChanged.", exception);
                        }
                        catch (Exception)
                        {
                            // Console.WriteLine("Unhandled Exception in [Spreadsheet] CellPropertyChanged");
                            return;
                        }
                    }

                    // Else block for if the expression doesn't start with =
                    else
                    {
                        // Expression tree object will be null.
                        changedCell.MyExpressionTree = null;
                    }

                    this.PropertyChanged?.Invoke(changedCell, new PropertyChangedEventArgs("Text"));
                }
                else if (e.PropertyName.ToString().CompareTo("BGColor") == 0)
                {
                    changedCell = this.GetCell(updatedRow, updatedCol);
                    this.PropertyChanged?.Invoke(changedCell, new PropertyChangedEventArgs("BGColor"));
                }
            }
            else
            {
                throw new Exception("Sender was not type Cell.");
            }
        }

        /// <summary>
        /// Gets the string value of the cell referred to by varName.
        /// </summary>
        /// <param name="varName">Cell name of form: "A1" or "D10".</param>
        /// <returns>Value of cell as string.</returns>
        private string GetVariableValue(string varName)
        {
            int colIndex = (int)varName[0] - 65;
            int rowIndex;
            if (int.TryParse(varName.Substring(1), out rowIndex))
            {
                rowIndex--;

                string valueAtVarName = this.SpreadsheetArray[rowIndex, colIndex].Value;

                if (valueAtVarName != string.Empty)
                {
                    return valueAtVarName;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                // Column index was not recognized as an integer.
                return "Errored";
            }
        }

        /// <summary>
        /// Pushes the new object of the UndoRedo interface and notifies the UI about undo stack not empty.
        /// </summary>
        /// <param name="command">Interface object.</param>
        public void AddUndoStack(CPTS321.IUndoRedo command)
        {
            this.undoStack.Push(command);
            this.PropertyChanged(command, new PropertyChangedEventArgs("Undo not empty"));
        }

        /// <summary>
        /// Undo the cell property of the spreadsheet.
        /// </summary>
        public void Undo()
        {
            try
            {
                this.undoStack.Peek().Undo();
                this.redoStack.Push(this.undoStack.Pop());
            }
            catch (InvalidOperationException exception)
            {
                throw exception;
            }

            var listeners = this.PropertyChanged;
            if (listeners != null)
            {
                listeners(this.redoStack.Peek(), new PropertyChangedEventArgs("Redo not empty"));
            }

            if (this.undoStack.Count > 0)
            {
                if (listeners != null)
                {
                    listeners(this.redoStack.Peek(), new PropertyChangedEventArgs("Undo not empty"));
                }
            }
            else
            {
                if (listeners != null)
                {
                    listeners(this.redoStack.Peek(), new PropertyChangedEventArgs("Undo empty"));
                }
            }
        }

        /// <summary>
        /// Redo the cell property of the spreadsheet.
        /// </summary>
        public void Redo()
        {
            try
            {
                this.redoStack.Peek().Redo();
                this.undoStack.Push(this.redoStack.Pop());
            }
            catch (InvalidOperationException exception)
            {
                throw exception;
            }

            var listeners = this.PropertyChanged;
            if (listeners != null)
            {
                listeners(this.undoStack.Peek(), new PropertyChangedEventArgs("Undo not empty"));
            }

            if (this.redoStack.Count > 0)
            {
                if (listeners != null)
                {
                    listeners(this.undoStack.Peek(), new PropertyChangedEventArgs("Redo not empty"));
                }
            }
            else
            {
                if (listeners != null)
                {
                    listeners(this.undoStack.Peek(), new PropertyChangedEventArgs("Redo empty"));
                }
            }
        }

        /// <summary>
        /// Returns the type of the undo stack.
        /// </summary>
        /// <returns>Type of undostack.</returns>
        public string GetTypeOfUndoStackTop() => this.undoStack.Peek().ReturnType();

        /// <summary>
        /// Returns the type of the redo stack.
        /// </summary>
        /// <returns>Type of redostack.</returns>
        public string GetTypeOfRedoStackTop() => this.redoStack.Peek().ReturnType();

        /// <summary>
        /// Returns the count of the undo stack.
        /// </summary>
        /// <returns>Count of undostack.</returns>
        public int GetCountUndoStack() => this.undoStack.Count();

        /// <summary>
        /// Returns the count of the redo stack.
        /// </summary>
        /// <returns>Count of redostack.</returns>
        public int GetCountRedoStack() => this.redoStack.Count();

        /// <summary>
        /// Converts color to unsigned integer value. Help taken from http://www.java2s.com/example/csharp/system.drawing/convert-a-color-to-unsigned-int.html.
        /// </summary>
        /// <param name="c">Color of the cell.</param>
        /// <returns>Unsigned integer value of the color.</returns>
        public uint ColorToUint(Color c)
        {
            uint u = (uint)c.A << 24;
            u += (uint)c.R << 16;
            u += (uint)c.G << 8;
            u += c.B;
            return u;
        }

        /// <summary>
        /// Saves the sheet in XML File Format.
        /// </summary>
        /// <param name="stream">Stream writer object.</param>
        public void SaveSheet(Stream stream)
        {
            // Writer setting object for formatting the XML Sheet.
            XmlWriterSettings format = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "    ",
            };

            using (XmlWriter writer = XmlWriter.Create(stream, format))
            {
                writer.WriteStartElement("CPTS321SpreadSheet");
                foreach (var cell in this.SpreadsheetArray)
                {
                    // Writing only if the cell value has been changed.
                    if (cell.IsChanged())
                    {
                        int row = cell.RowIndex;
                        int col = cell.ColumnIndex + 'A';
                        string cellName = ((char)col).ToString() + (row + 1).ToString();

                        writer.WriteStartElement("cell");
                        writer.WriteStartElement("name");
                        writer.WriteString(cellName);
                        writer.WriteEndElement();
                        writer.WriteStartElement("bgcolor");
                        writer.WriteString(cell.BGColor.ToString());
                        writer.WriteEndElement();
                        writer.WriteStartElement("text");
                        writer.WriteString(cell.Text);
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement(); // ends CPTS321SpreadSheet element
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        /// <summary>
        /// Reloads the sheet from XML File Format.
        /// </summary>
        /// <param name="stream">Stream writer object.</param>
        [Obsolete]
        public void LoadSheet(Stream stream)
        {
            this.undoStack.Clear();
            this.redoStack.Clear();

            var format = new XmlReaderSettings()
            {
                IgnoreWhitespace = true,
            };

            XmlReader reader = XmlReader.Create(stream, format);
            string temp;
            Cell cell;

            reader.ReadStartElement("CPTS321SpreadSheet");

            while (reader.Name == "cell")
            {
                // Read the next cell.
                reader.ReadStartElement("cell");

                // Read the name of the cell and get the corresponding cell from the spreadsheet.
                reader.ReadStartElement("name");
                temp = reader.ReadContentAsString();
                cell = this.GetCell(temp);
                reader.ReadEndElement();

                // Read the color of the cell in xml and set it to the spreadsheet cell given from above.
                reader.ReadStartElement("bgcolor");
                temp = reader.ReadContentAsString();
                uint.TryParse(temp, out uint result);
                cell.BGColor = result;
                reader.ReadEndElement();

                // Read the text of the cell in xml and set it to the spreadsheet cell given from above.
                reader.ReadStartElement("text");
                temp = reader.ReadContentAsString();
                cell.Text = temp;
                reader.ReadEndElement();

                // The end of the cell in xml.
                reader.ReadEndElement();
            }

            reader.ReadEndElement();
        }

        /// <summary>
        /// Checks for the bad cell reference when a cell is referenced.
        /// </summary>
        /// <param name="name">Cell name.</param>
        /// <returns>True or false about the bad cell reference.</returns>
        private bool CheckBadCellReference(string name)
        {
            // Column for storing the column name from the entered cell.
            char column;
            int row, col;

            // get the row and column from the user's inputted text.
            column = name[0];
            int.TryParse(name.Substring(1), out row);

            // convert col to its unicode representation.
            col = Convert.ToInt32(column) - 65;

            // check if the cell is within the bounds of the spreadsheet.
            if (col < 0 || col > this.ColumnCount || row <= 0 || row > this.RowCount)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks for unacceptable references of a cell.
        /// </summary>
        /// <param name="cell">Cell object.</param>
        /// <returns>True or false whether a cell has some unacceptable reference.</returns>
        private bool CheckRef(ref Cell cell)
        {
            // loop over each of the cells that cell has in its variable dictionary.
            foreach (string varname in cell.VarNames)
            {
                // if the index of the current cell in the dictionary is not valid than there is a bad ref.
                if (!this.CheckBadCellReference(varname))
                {
                    this.SpreadsheetArray[cell.RowIndex, cell.ColumnIndex].Value = "!(bad reference)";
                    return false;
                }

                // If the list of the varibales in the expression contains the cell name itself.
                if (varname == ((char)(cell.ColumnIndex + 65)).ToString() + (cell.RowIndex + 1).ToString())
                {
                    cell.Value = "!(self reference)";
                    return false;
                }

                // if the index of the current cell in the dictionary also references the index name of the passed in cell parameter than there is a circular ref.
                else if (!this.CheckNotCircular(varname, ((char)(cell.ColumnIndex + 65)).ToString() + (cell.RowIndex + 1).ToString()))
                {
                    cell.Value = "!(circular reference)";
                    return false;
                }
            }

            // if the function gets this far than there are no bad references so return true.
            return true;
        }

        /// <summary>
        ///  Reccursively checks to see if the cell does not have a circular reference.
        /// </summary>
        /// <param name="name">name of the cell.</param>
        /// <param name="parent">Parent cell name.</param>
        /// <returns> Returns true if there are none.</returns>
        private bool CheckNotCircular(string name, string parent)
        {
            Cell cell;

            cell = this.GetCell(name);

            // loops over all of the cells that cell is subscribed to.
            foreach (string varname in cell.VarNames)
            {
                // return false if the cell is subscribed to its parent since this is a circular reference.
                if (varname == parent)
                {
                    return false;
                }

                return this.CheckNotCircular(varname, parent);
            }

            // return true if no circular references are found.
            return true;
        }
    }
}
