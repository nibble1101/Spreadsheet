﻿// <copyright file="NodeVaribale.cs" company="PlaceholderCompany">
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
    /// Node variable class implementation.
    /// </summary>
    internal class NodeVaribale : AbstractNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVaribale"/> class.
        /// </summary>
        /// <param name="name">Name of the varibale.</param>
        /// <param name="value">Value of the variable.</param>
        public NodeVaribale(string name, double value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Overriden Method Evaluate.
        /// </summary>
        /// <param name="left">Left Value.</param>
        /// <param name="right">Right Value.</param>
        /// <returns>None.</returns>
        public override double Evaluate(double left, double right)
        {
            throw new NotImplementedException();
        }
    }
}
