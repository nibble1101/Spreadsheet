// <copyright file="HW6TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/*
 CPTS321 Spreadsheet assignment.

 Submitted by: Ritik Agarwal.
 WSU ID: 011707455.

 */

namespace NUnit.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Test class for the test cases.
    /// </summary>
    [TestFixture]
    public class HW6TestClass
    {
        /// <summary>
        /// Test method for the Evaluate Method Addition Cases with parentheses.
        /// </summary>
        [Test]
        public void TestEvaluationParentheses()
        {
            Assert.AreEqual((double)6.0, System.Math.Round(new CPTS321.ExpressionTree("((1+2)+3)").Evaluate(), 2));
            Assert.AreEqual((double)123.7, System.Math.Round(new CPTS321.ExpressionTree("(((((100.4+23.30)))))").Evaluate(), 2));
            Assert.AreEqual((double)120.5, System.Math.Round(new CPTS321.ExpressionTree("(90.5+30)").Evaluate(), 2));
            Assert.AreEqual((double)1.00, System.Math.Round(new CPTS321.ExpressionTree("10/(2*5)").Evaluate(), 2));
            Assert.AreEqual((double)5.00, System.Math.Round(new CPTS321.ExpressionTree("10/(2)").Evaluate(), 2));

            Assert.AreEqual((double)20.00, System.Math.Round(new CPTS321.ExpressionTree("(10+(20-10))").Evaluate(), 2));
            Assert.AreEqual((double)30.00, System.Math.Round(new CPTS321.ExpressionTree("((20-10)+(30-10))").Evaluate(), 2));
            Assert.AreEqual((double)200.00, System.Math.Round(new CPTS321.ExpressionTree("((20*10)/1)").Evaluate(), 2));
        }

        /// <summary>
        /// Test method for the Evaluate according to Precedence.
        /// </summary>
        [Test]
        public void TestEvaluationPrecedence()
        {
            Assert.AreEqual((double)1.00, System.Math.Round(new CPTS321.ExpressionTree("100/10*10").Evaluate(), 2));
            Assert.AreEqual((double)96.00, System.Math.Round(new CPTS321.ExpressionTree("84-14-26").Evaluate(), 2));
            Assert.AreEqual((double)17.00, System.Math.Round(new CPTS321.ExpressionTree("2+3*5").Evaluate(), 2));
            Assert.AreEqual((double)11.00, System.Math.Round(new CPTS321.ExpressionTree("2*3+5").Evaluate(), 2));
        }

        /// <summary>
        /// Test method for the Evaluate Method Subtraction Cases.
        /// </summary>
        [Test]
        public void TestEvaluateForSpecialCases()
        {
            Assert.AreEqual(double.PositiveInfinity, new CPTS321.ExpressionTree("5/0").Evaluate());
        }
    }
}