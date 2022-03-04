// <copyright file="TestClass.cs" company="PlaceholderCompany">
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
    public class TestClass
    {
        /// <summary>
        /// Test method for the GetCell Method General Cases.
        /// </summary>
        [Test]
        public void TestGetCellMethodGeneralCases()
        {
#pragma warning disable CS0612 // Type or member is obsolete
            Assert.IsTrue(new CPTS321.Spreadsheet(50, 26).GetCell(0, 0) is CPTS321.SpreadsheetCell);
            Assert.IsTrue(new CPTS321.Spreadsheet(50, 26).GetCell(49, 25) is CPTS321.SpreadsheetCell);
#pragma warning restore CS0612 // Type or member is obsolete
        }

        /// <summary>
        /// Test method for the GetCell Method End Cases.
        /// </summary>
        [Test]
        public void TestGetCellMethodEndCases()
        {
#pragma warning disable CS0612 // Type or member is obsolete
            Assert.IsTrue(new CPTS321.Spreadsheet(50, 26).GetCell(-1, 50) is null);
            Assert.IsTrue(new CPTS321.Spreadsheet(50, 26).GetCell(26, 50) is null);
            Assert.IsTrue(new CPTS321.Spreadsheet(50, 26).GetCell(100, 100) is null);
#pragma warning restore CS0612 // Type or member is obsolete
        }
    }
}
