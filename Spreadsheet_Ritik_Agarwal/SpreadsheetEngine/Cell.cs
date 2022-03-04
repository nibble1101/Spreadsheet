// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/*
 PS: THE SPREADSHEET IS MADE AGAIN IN .NETFRAMEWORK AS I MADE SPREADSHEET IN .NETCORE BY MISTAKE. PROFESSOR VENERA ASKED ME TO MAKE IT AGAIN AND SUBMIT IT WITH HW5.
 */

/*
 CPTS321 Spreadsheet assignment.

 Submitted by: Ritik Agarwal.
 WSU ID: 011707455.

 */

namespace CPTS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Cell class definition.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Expression Tree object.
        /// </summary>
        public ExpressionTree MyExpressionTree;

        /// <summary>
        /// Row index field.
        /// </summary>
        protected int rowIndex;

        /// <summary>
        /// Column index field.
        /// </summary>
        protected int columnIndex;

        /// <summary>
        /// text field.
        /// </summary>
        protected string text;

        /// <summary>
        /// Value field.
        /// </summary>
        protected string value;

        /// <summary>
        /// Background color of the cell.
        /// </summary>
        protected uint bgColor;

        /// <summary>
        /// Check for property change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The list of all of the variable names in which the cell references.
        /// </summary>
        public List<string> VarNames = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// Initialies the row index and the column index.
        /// </summary>
        /// <param name="row"> Index of the row.</param>
        /// <param name="column"> Index of the Cloumn.</param>
        public Cell(int row, int column)
        {
            this.value = string.Empty;
            this.text = string.Empty;
            this.rowIndex = row;
            this.columnIndex = column;
            this.bgColor = 0xFFFFFFFF;
        }

        /// <summary>
        /// Gets or sets get value of rowIndex.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set => this.rowIndex = value;
        }

        /// <summary>
        /// Gets or sets get value of columnIndex.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
            set => this.columnIndex = value;
        }

        /// <summary>
        /// Gets or sets property Text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text.CompareTo(value) != 0)
                {
                    this.text = value;
                    this.OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets or sets property definition of the Value.
        /// </summary>
        public string Value
        {
            get
            {
                if (this.text.Length > 0)
                {
                    if (this.text[0] == '=')
                    {
                        return this.value;
                    }
                    else
                    {
                        return this.text;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                // This is to check if the set method is called by the Spreadsheet class.
                if (Environment.StackTrace.Contains("Spreadsheet"))
                {
                    this.value = value;
                    this.OnPropertyChanged("Value");
                }
                else
                {
                    throw new MemberAccessException("Only the Spreadsheet class should be able to access this property.");
                }
            }
        }

        /// <summary>
        /// Gets or sets background color property.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.bgColor;
            }

            set
            {
                if (this.bgColor != value)
                {
                    this.bgColor = value;
                    this.OnPropertyChanged("BGColor");
                }
            }
        }

        /// <summary>
        /// Event Handler anytime a cell's text is changed.
        /// </summary>
        /// <param name="sender">Object of the class who is calling the method.</param>
        /// <param name="e">Event object.</param>
        public void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }

        /// <summary>
        /// Notifies the UI layer that the property has been changed.
        /// </summary>
        /// <param name="name"> Name of the property.</param>
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Checks if the cell attributes have been changed or not.
        /// </summary>
        /// <returns>Returns true if changed else false.</returns>
        public bool IsChanged()
        {
            if (this.Text != string.Empty || this.Value != string.Empty | this.bgColor != 0xFFFFFFFF)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new expression tree for the cell.
        /// </summary>
        /// <param name="exp">Expression types in the cell.</param>
        public void NewExpression(string exp)
        {
            this.MyExpressionTree = new ExpressionTree(exp);
            this.VarNames = this.MyExpressionTree.GetVariableNames();
        }
    }
}
