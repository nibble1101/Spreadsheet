// <copyright file="IUndoRedo.cs" company="PlaceholderCompany">
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
    /// Interface UndoRedo class with methods declarations Undo and Redo.
    /// </summary>
    public interface IUndoRedo
    {
        /// <summary>
        /// Redo the command called on the event.
        /// </summary>
        void Redo();

        /// <summary>
        /// Undo the command called on the event.
        /// </summary>
        void Undo();

        /// <summary>
        /// Returns the type of the event.
        /// </summary>
        /// <returns>Type of event.</returns>
        string ReturnType();
    }
}
