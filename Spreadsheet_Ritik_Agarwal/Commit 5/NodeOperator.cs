// <copyright file="NodeOperator.cs" company="PlaceholderCompany">
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
    /// The Operator Node class inherited from Abstract Node class.
    /// </summary>
    internal abstract class NodeOperator : AbstractNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeOperator"/> class.
        /// Node operator class.
        /// </summary>
        /// <param name="c">Stores the type of the operator.</param>
        public NodeOperator(char c)
        {
            this.Operator = c;
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Gets or sets the operator property.
        /// </summary>
        public char Operator { get; set; }

        /// <summary>
        /// Gets or sets the Left node of the operator.
        /// </summary>
        public AbstractNode Left { get; set; }

        /// <summary>
        /// Gets or sets the Left node of the operator.
        /// </summary>
        public AbstractNode Right { get; set; }

        /// <summary>
        /// Gets the abstract Precedence Property.
        /// </summary>
        public abstract int Precedence
        {
            get;
        }
    }
}
