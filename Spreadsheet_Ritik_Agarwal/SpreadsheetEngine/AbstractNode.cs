// <copyright file="AbstractNode.cs" company="PlaceholderCompany">
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Empty Abstract class. This class will be the parent class of other nodes.
    /// </summary>
    public abstract class AbstractNode
    {
        /// <summary>
        /// Abstract Method Evaluate.
        /// </summary>
        /// <param name="left">Left Value.</param>
        /// <param name="right">Right Value.</param>
        /// <returns>Evaluated Value.</returns>
        public abstract double Evaluate(double left, double right);
    }
}
