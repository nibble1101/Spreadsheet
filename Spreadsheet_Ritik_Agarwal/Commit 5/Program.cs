// <copyright file="Program.cs" company="PlaceholderCompany">
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
    /// Program class for the main function.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            string choice = string.Empty;
            string expression = string.Empty;
            string finalAnswer = string.Empty;
            string variableName = string.Empty;
            string variableValue = string.Empty;

            // Creating a default tree with expression: A1+B1+C1;
            ExpressionTree tree = new ExpressionTree();

            while (true)
            {
                // Printing the menu.
                Console.WriteLine("Menu (current expression=\"" + tree.Expression + "\")");
                Console.WriteLine("1 = Enter a new expression");
                Console.WriteLine("2 = Set a variable value");
                Console.WriteLine("3 = Evaluate tree");
                Console.WriteLine("4 = Quit");

                // Getting the appropriate user choice from the menu.
                do
                {
                    choice = Console.ReadLine();
                }
                while (Convert.ToInt32(choice) >= 5 && Convert.ToInt32(choice) <= 0);

                switch (Convert.ToInt32(choice))
                {
                    // Choice selected by the user for entering an expression.
                    case 1:
                        Console.WriteLine("Enter a new expression: ");
                        expression = Console.ReadLine();
                        tree = new ExpressionTree(expression);
                        break;

                    // Choice selected by the user for entering an expression.
                    case 2:
                        Console.WriteLine("Enter the name of the variable: ");
                        variableName = Console.ReadLine();
                        Console.WriteLine("Enter the value of the variable: ");
                        variableValue = Console.ReadLine();
                        tree.SetVariable(variableName, Convert.ToDouble(variableValue));
                        break;

                    // Choice selected by the user to evaluate the expression.
                    case 3:
                        Console.WriteLine(tree.Evaluate());
                        break;

                    // Choice selected by the user for exiting the program.
                    case 4:
                        System.Environment.Exit(1);
                        break;
                }
            }
        }
    }
}
