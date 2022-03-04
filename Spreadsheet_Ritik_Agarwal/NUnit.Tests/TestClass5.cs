// <copyright file="HW5TestClass.cs" company="PlaceholderCompany">
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
    public class HW5TestClass
    {
        /// <summary>
        /// Test method for the Evaluate Method Addition Cases.
        /// </summary>
        [Test]
        public void TestEvaluateForAddition()
        {
            Assert.AreEqual((double)6.0, System.Math.Round(new CPTS321.ExpressionTree("1+2+3").Evaluate(), 2));
            Assert.AreEqual((double)123.7, System.Math.Round(new CPTS321.ExpressionTree("100.4+23.30").Evaluate(), 2));
            Assert.AreEqual((double)120.5, System.Math.Round(new CPTS321.ExpressionTree("90.5+30").Evaluate(), 2));
        }

        /// <summary>
        /// Test method for the Evaluate Method Mutiplication Cases.
        /// </summary>
        [Test]
        public void TestEvaluateForMultiplication()
        {
            Assert.AreEqual((double)6.00, System.Math.Round(new CPTS321.ExpressionTree("1*2*3").Evaluate(), 2));
            Assert.AreEqual((double)2339.32, System.Math.Round(new CPTS321.ExpressionTree("100.4*23.30").Evaluate(), 2));
            Assert.AreEqual((double)2715.00, System.Math.Round(new CPTS321.ExpressionTree("90.5*30").Evaluate(), 2));
        }

        /// <summary>
        /// Test method for the Evaluate Method Subtraction Cases.
        /// </summary>
        [Test]
        public void TestEvaluateForSubtraction()
        {
            Assert.AreEqual((double)77.10, System.Math.Round(new CPTS321.ExpressionTree("100.4-23.30").Evaluate(), 2));
            Assert.AreEqual((double)60.50, System.Math.Round(new CPTS321.ExpressionTree("90.5-30").Evaluate(), 2));
        }

        /// <summary>
        /// Test method for the Evaluate Method Division Cases.
        /// </summary>
        [Test]
        public void TestEvaluateForDivision()
        {
            Assert.AreEqual((double)0.50, System.Math.Round(new CPTS321.ExpressionTree("1/2").Evaluate(), 2));
            Assert.AreEqual((double)4.29, System.Math.Round(new CPTS321.ExpressionTree("100/23.30").Evaluate(), 2));
            Assert.AreEqual((double)3.00, System.Math.Round(new CPTS321.ExpressionTree("90/30").Evaluate(), 2));
        }
    }
}