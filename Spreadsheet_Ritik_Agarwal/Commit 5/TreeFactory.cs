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
    /// <summary>
    /// Tree factory is the class where the tree nodes for the operator are made and the valid operator are checked.
    /// </summary>
    internal class TreeFactory
    {
        /// <summary>
        /// This method is to check if the operator is valid or not for the tree expression.
        /// </summary>
        /// <param name="ch">The character to be checked.</param>
        /// <returns>True or False.</returns>
        public bool IsValidOperator(char ch)
        {
            switch (ch)
            {
                case '+':
                    return true;
                case '-':
                    return true;
                case '*':
                    return true;
                case '/':
                    return true;
                case '(':
                    return true;
                case ')':
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Return the object of the Node operator.
        /// </summary>
        /// <param name="ch">COperator type.</param>
        /// <returns>Object of the Node operator.</returns>
        public NodeOperator InstantiateOperatorNode(char ch)
        {
            switch (ch)
            {
                case '+':
                    return new NodeAddition();
                case '-':
                    return new NodeSubtraction();
                case '*':
                    return new NodeMutiplication();
                case '/':
                    return new NodeDivision();
                case '(':
                    return new NodeOpenParentheses();
                case ')':
                    return new NodeCloseParentheses();
            }

            return null;
        }
    }
}
