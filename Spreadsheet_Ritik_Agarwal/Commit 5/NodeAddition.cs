// <copyright file="NodeAddition.cs" company="PlaceholderCompany">
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
    /// Node addition operation implementation.
    /// </summary>
    internal class NodeAddition : NodeOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeAddition"/> class.
        /// </summary>
        public NodeAddition()
            : base('+')
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

        /// <inheritdoc/>
        public override double Evaluate(double left, double right)
        {
            return right + left;
        }
    }
}
