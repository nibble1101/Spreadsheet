// <copyright file="HW7TestClass.cs" company="PlaceholderCompany">
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
    public class HW7TestClass
    {
        private CPTS321.Spreadsheet testsheet;
        private CPTS321.SpreadsheetCell a1;
        private CPTS321.SpreadsheetCell a2;
        private CPTS321.SpreadsheetCell b1;
        private CPTS321.SpreadsheetCell b2;

        /// <summary>
        /// Set up method for the variables used in the class.
        /// </summary>
        [SetUp]
        [System.Obsolete]
        public void Setup()
        {
            // Creating a spreasheet of 10X10 cell.
            this.testsheet = new CPTS321.Spreadsheet(10, 10);

            // Initialzing the cells using get cell method.
            this.a1 = (CPTS321.SpreadsheetCell)this.testsheet.GetCell(0, 0);
            this.a2 = (CPTS321.SpreadsheetCell)this.testsheet.GetCell(1, 0);
            this.b1 = (CPTS321.SpreadsheetCell)this.testsheet.GetCell(0, 1);
            this.b2 = (CPTS321.SpreadsheetCell)this.testsheet.GetCell(1, 1);
        }

        /// <summary>
        /// Test cases testing the equation entered in the cell.
        /// </summary>
        [Test]
        public void TestCellTextValue()
        {
            this.a1.Text = "22";
            Assert.AreEqual(this.a1.Text, this.a1.Value);

            this.b2.Text = "33";
            Assert.AreEqual(this.b2.Text, this.b2.Value);

            this.a2.Text = "=A1+100*5";
            Assert.AreEqual(System.Convert.ToDouble(this.a1.Value) + (100 * 5), System.Convert.ToDouble(this.a2.Value));

            this.a1.Text = "=A2+B2";
            Assert.AreEqual(System.Convert.ToDouble("555"), System.Convert.ToDouble(this.a1.Value));

            this.b1.Text = "=A2*2";
            Assert.AreEqual("1044", this.b1.Value);

            this.b2.Text = "11";
            Assert.AreEqual("566", this.a1.Value);

            Assert.AreEqual("33", this.a1.Value);
        }
    }
}