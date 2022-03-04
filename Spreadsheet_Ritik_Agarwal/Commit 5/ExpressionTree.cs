// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
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
    /// Expression Tree Class.
    /// </summary>
    public class ExpressionTree
    {
        private AbstractNode root;
        private Stack<AbstractNode> postFixExpressionStack = new Stack<AbstractNode>();
        private Stack<AbstractNode> finalPostFixExpressionStack = new Stack<AbstractNode>();
        private Stack<AbstractNode> operatorStack = new Stack<AbstractNode>();
        private Dictionary<string, double> variables = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">The expression which needs to be evaluated.</param>
        public ExpressionTree(string expression)
        {
            this.Expression = expression;
            this.DoCompile(expression);
            this.root = this.BuildTree();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        public ExpressionTree()
        {
            this.Expression = "A1+B1+C1";
            this.DoCompile("A1+B1+C1");
            this.root = this.BuildTree();
        }

        /// <summary>
        /// Gets or sets the value of the expression.
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// Method that compiles the expression used in the current tree.
        /// </summary>
        /// <param name="expression">Expression to be compiled.</param>
        private void Compile(string expression)
        {
            double number;
            int index = 0;
            string subString = string.Empty;

            TreeFactory factory = new TreeFactory();

            // Checks if expression holds anything
            if (expression.Length != 0)
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    index = i;

                    // Looks for type of first operator inside the expression
                    while (index < expression.Length && !factory.IsValidOperator(expression[index]))
                    {
                        index++;
                    }

                    // Checking if the i is the operator.
                    if (index == i)
                    {
                        subString = expression[i].ToString();
                    }

                    // finds the correct substring otherwise
                    else
                    {
                        subString = expression.Substring(i, index - i);
                        if (index - i > 1)
                        {
                            i += index - i;
                            i--;
                        }
                    }

                     // 1. If the incoming symbols is an operand, output it..

                    // Checks for any other operator. operatorIndex will be greater than
                    // expression.Length if no operator is found
                    if (factory.IsValidOperator(subString[0]))
                    {
                        // Creates operator node and divides expression accordingly
                        NodeOperator newNode = factory.InstantiateOperatorNode(subString[0]);

                        // 2. If the incoming symbol is a left parentheses, push it on the stack.
                        if (newNode.Operator == '(')
                        {
                            this.operatorStack.Push(newNode);
                        }

                        // 3. If the incoming symbol is a right parenthesis: discard the right parenthesis, pop and print the stack symbols until you see a left parenthesis. Pop the left parenthesis and discard it.
                        else if (newNode.Operator == ')')
                        {
                            while ((char)this.operatorStack.Peek().GetType().GetProperty("Operator").GetValue(this.operatorStack.Peek()) != '(')
                            {
                                this.postFixExpressionStack.Push(this.operatorStack.Pop());
                            }

                            this.operatorStack.Pop();
                        }

                        // 4. If the incoming symbol is an operator and the stack is empty or contains a left parentheses on top push the incoming operator onto the stack.
                        else if (this.operatorStack.Count == 0 || (char)this.operatorStack.Peek().GetType().GetProperty("Operator").GetValue(this.operatorStack.Peek()) == '(')
                        {
                            this.operatorStack.Push(newNode);
                        }

                        // 5. If the incoming symbol is an operator and has either higher precedence than the operator on the top of the stack, or has the same precedence as the operator on the top of the stack and is right associative--push it on the stack.
                        else if (newNode.Precedence > (int)this.operatorStack.Peek().GetType().GetProperty("Precedence").GetValue(this.operatorStack.Peek()) ||
                            (newNode.Precedence == (int)this.operatorStack.Peek().GetType().GetProperty("Precedence").GetValue(this.operatorStack.Peek())))
                        {
                            this.operatorStack.Push(newNode);
                        }

                        // 6. If the incoming symbol is an operator and has either lower precedence than the operator on the top of the stack, or has the same precedence as the operator on the top of the stack and is left associative--continue to pop the stack until this is not true. Then, push the incoming operator.
                        else if (newNode.Precedence < (int)this.operatorStack.Peek().GetType().GetProperty("Precedence").GetValue(this.operatorStack.Peek()) ||
                            (newNode.Precedence == (int)this.operatorStack.Peek().GetType().GetProperty("Precedence").GetValue(this.operatorStack.Peek())))
                        {
                            // OperatorNode temp = (OperatorNode)this.opStack.Peek();
                            while (this.operatorStack.Count > 0 && ((NodeOperator)this.operatorStack.Peek()).Operator != '(' && ((NodeOperator)this.operatorStack.Peek()).Precedence >= newNode.Precedence)
                            {
                                this.postFixExpressionStack.Push(this.operatorStack.Pop());
                            }

                            this.operatorStack.Push(newNode);
                        }
                    }

                    // Checks if the expression contains only a number
                    else if (double.TryParse(subString, out number))
                    {
                        // Creates new constant node to return
                        NodeConstant newNode = new NodeConstant();
                        newNode.Value = number;

                        this.postFixExpressionStack.Push(newNode);
                    }

                    // Checks if the expression contains only a variable
                    else
                    {
                        // Creates new variable node to return
                        NodeVaribale newNode = new NodeVaribale();
                        newNode.Name = subString;

                        this.postFixExpressionStack.Push(newNode);
                    }
                }
            }
        }

        /// <summary>
        /// Evaluates the expression tree.
        /// </summary>
        /// <returns>The evaluated value of type double.</returns>
        public double Evaluate()
        {
            return this.Evaluate(this.root);
        }

        /// <summary>
        /// Set the value of a variable.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="variableValue">Value to be associated with the variable.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables.Add(variableName, variableValue);
        }

        private void DoCompile(string expression)
        {
            this.Compile(expression);

            // 7. At the end of the expression, pop and print all operators on the stack. (No parentheses should remain.)
            while (this.operatorStack.Count > 0)
            {
                this.postFixExpressionStack.Push(this.operatorStack.Pop());
            }

            /*Console.WriteLine("Post fix expression: ");
            this.StackPrint(this.postFixExpressionStack);
            Console.WriteLine("\n Post fix expression count: " + this.postFixExpressionStack.Count.ToString());*/
            while (this.postFixExpressionStack.Count != 0)
            {
                this.finalPostFixExpressionStack.Push(this.postFixExpressionStack.Pop());
            }

            /*Console.WriteLine("\nPost fix expression: ");
            this.StackPrint(this.finalPostFixExpressionStack);
            Console.WriteLine("\n Post fix expression count: " + this.postFixExpressionStack.Count.ToString());*/
        }

        /// <summary>
        /// Demo program for priting the contents of the stack.
        /// </summary>
        /// <param name="stack">Stack to be printed.</param>
        private void StackPrint(Stack<AbstractNode> stack)
        {
            foreach (var nodes in stack)
            {
                if (nodes is NodeConstant)
                {
                    NodeConstant newNode = (NodeConstant)nodes;
                    Console.WriteLine(newNode.Value.ToString() + " ");
                }
                else if (nodes is NodeVaribale)
                {
                    NodeVaribale newNode = (NodeVaribale)nodes;
                    Console.WriteLine(newNode.Name.ToString() + " ");
                }
                else if (nodes is NodeOperator)
                {
                    NodeOperator newNode = (NodeOperator)nodes;
                    Console.WriteLine(newNode.Operator.ToString() + " ");
                }
            }
        }

        /*private AbstractNode BuildTree(AbstractNode cur)
        {
            if (cur is NodeOperator)
            {
                NodeOperator temp = (NodeOperator)cur;
                temp.Left = this.BuildTree(this.postFixExpressionStack.Pop());
                temp.Right = this.BuildTree(this.postFixExpressionStack.Pop());
            }
            else
            {
            }

            return cur;
        }*/

        private AbstractNode BuildTree()
        {
            Stack<AbstractNode> stack = new Stack<AbstractNode>();
            AbstractNode tRight, tLeft;

            while (this.finalPostFixExpressionStack.Count > 0)
            {
                AbstractNode poppedNode = this.finalPostFixExpressionStack.Pop();

                if (poppedNode is NodeConstant)
                {
                    NodeConstant newNode = (NodeConstant)poppedNode;
                    stack.Push(newNode);
                }
                else if (poppedNode is NodeVaribale)
                {
                    NodeVaribale newNode = (NodeVaribale)poppedNode;
                    stack.Push(newNode);
                }
                else if (poppedNode is NodeOperator)
                {
                    NodeOperator newNode = (NodeOperator)poppedNode;
                    tRight = stack.Pop();
                    tLeft = stack.Pop();
                    newNode.Left = tLeft;
                    newNode.Right = tRight;
                    stack.Push(newNode);
                }
            }

            return stack.Pop();
        }

        private double Evaluate(AbstractNode theNode)
        {
            // Evaluate left and right nodes if theNode is an operator.
            if (theNode != null && theNode is NodeOperator)
            {
                NodeOperator temp = (NodeOperator)theNode;

                return temp.Evaluate(this.Evaluate(temp.Left), this.Evaluate(temp.Right));
            }

            // Return the value of theNode after looking for it in the dictionary if it is a variable.
            if (theNode != null && theNode is NodeVaribale)
            {
                NodeVaribale temp = (NodeVaribale)theNode;
                return this.variables[temp.Name];
            }

            // Return the value of theNode if it is a constant.
            if (theNode != null && theNode is NodeConstant)
            {
                NodeConstant temp = (NodeConstant)theNode;
                return temp.Value;
            }

            return 0;
        }
    }
}
