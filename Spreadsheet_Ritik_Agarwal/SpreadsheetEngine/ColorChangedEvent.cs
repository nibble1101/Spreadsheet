// <copyright file="ColorChangedEvent.cs" company="PlaceholderCompany">
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
    /// Class for handling the color change events.
    /// </summary>
    public class ColorChangedEvent : IUndoRedo
    {
        private uint newColor;
        private List<uint> oldColor;
        private List<Cell> cells;
        private string type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChangedEvent"/> class.
        /// The class object is created whenever the color changed property is invoked.
        /// </summary>
        /// <param name="cells">The list of the selected cell.</param>
        /// <param name="oldColor">List of old colors before the property was changed.</param>
        /// <param name="newColor">New color being set.</param>
        /// <param name="type">Type of the event.</param>
        public ColorChangedEvent(List<Cell> cells, List<uint> oldColor, uint newColor, string type)
        {
            this.oldColor = oldColor;
            this.newColor = newColor;
            this.cells = cells;
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
            for (int i = 0; i < this.cells.Count(); i++)
            {
                this.cells[i].BGColor = this.oldColor[i];
            }
        }

        /// <summary>
        /// Redo the cell color change.
        /// </summary>
        public void Redo()
        {
            for (int i = 0; i < this.cells.Count(); i++)
            {
                this.cells[i].BGColor = this.newColor;
            }
        }
    }
}
