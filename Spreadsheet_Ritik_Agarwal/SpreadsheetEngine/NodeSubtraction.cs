// <copyright file="NodeSubtraction.cs" company="PlaceholderCompany">
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
    /// Subtraction Node class implementation.
    /// </summary>
    internal class NodeSubtraction : NodeOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeSubtraction"/> class.
        /// </summary>
        public NodeSubtraction()
            : base('-')
        {
        }

        /// <summary>
        /// Gets the Precedence used in the evaluation (Overriden).
        /// </summary>
        public override int Precedence
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Evaluates the value of the left and the right child.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Evaluated Value.</returns>
        public override double Evaluate(double left, double right)
        {
            try
            {
                return left - right;
            }
            catch (Exception)
            {
                Console.WriteLine("---Error applying operator to children of the node subtraction---");
                throw new Exception("Left or Right child was not a constant node or Value was not set.");
            }
        }
    }
}
