// <copyright file="TextChangedEvents.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CPTS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for handling the Undo and Redo operations on the text changed events.
    /// </summary>
    public class TextChangedEvents : IUndoRedo
    {
        private string newString;
        private string oldString;
        private Cell cells;
        private string type;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChangedEvents"/> class.
        /// The class object is created whenever the color changed property is invoked.
        /// </summary>
        /// <param name="cell">The cell being updated.</param>
        /// <param name="oldString">List of old colors before the property was changed.</param>
        /// <param name="newString">New color being set.</param>
        /// <param name="type">Type of the event.</param>
        public TextChangedEvents(Cell cell, string oldString, string newString, string type)
        {
            this.oldString = oldString;
            this.newString = newString;
            this.cells = cell;
            this.type = type;
        }

        /// <summary>
        /// Returns the type of the event.
        /// </summary>
        /// <returns>Type of event.</returns>
        public string ReturnType()
        {
            return this.type;
        }

        /// <summary>
        /// Undo the cell color change.
        /// </summary>
        public void Undo()
        {
            this.cells.Text = this.oldString;
        }

        /// <summary>
        /// Redo the cell color change.
        /// </summary>
        public void Redo()
        {
            this.cells.Text = this.newString;
        }
    }
}
