// <copyright file="TreeFactory.cs" company="PlaceholderCompany">
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
    /// Tree factory is the class where the tree nodes for the operator are made and the valid operator are checked.
    /// </summary>
    internal class TreeFactory
    {
        /// <summary>
        /// Factory that can create individual OperatorNodes.
        /// </summary>
        public Dictionary<char, Type> Operators = new Dictionary<char, Type>
        {
            { '+', typeof(NodeAddition) },
            { '-', typeof(NodeSubtraction) },
            { '*', typeof(NodeMutiplication) },
            { '/', typeof(NodeDivision) },
            { '(', typeof(NodeOpenParentheses) },
            { ')', typeof(NodeCloseParentheses) },
        };

        /// <summary>
        /// Return the object of the Node operator.
        /// </summary>
        /// <param name="ch">COperator type.</param>
        /// <returns>Object of the Node operator.</returns>
        public NodeOperator InstantiateOperatorNode(char ch)
        {
            if (this.Operators.ContainsKey(ch))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.Operators[ch]);
                if (operatorNodeObject is NodeOperator)
                {
                    return (NodeOperator)operatorNodeObject;
                }

                throw new Exception("Unhandled exception");
            }

            return null;
        }
    }
}
