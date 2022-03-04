// <copyright file="HW9TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/*
 CPTS321 Spreadsheet assignment.

 Submitted by: Ritik Agarwal.
 WSU ID: 011707455.

 */

namespace NUnit.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    /// <summary>
    /// Test class for HW8.
    /// </summary>
    [TestFixture]
    public class HW9TestClass
    {
        /// <summary>
        /// General test for SaveSheet function.
        /// </summary>
        [Test]
        [Obsolete]
        public void SaveSheetTest()
        {
            CPTS321.Spreadsheet testSheet = new CPTS321.Spreadsheet(5, 5);
            CPTS321.Cell cellA1 = testSheet.GetCell(0, 0);
            CPTS321.Cell cellA2 = testSheet.GetCell(1, 0);
            CPTS321.Cell cellB1 = testSheet.GetCell(0, 1);
            CPTS321.Cell cellB2 = testSheet.GetCell(1, 1);

            string fileName = "HW9TestXML";
            Stream fs = new FileStream(fileName, FileMode.Create);

            // Changing the text of the cell.
            cellA1.Text = "100";
            cellB1.Text = "=A1*100";
            cellA2.Text = "=B1+20";
            cellB2.Text = "200";

            // Changing the Color of the cell.
            cellA1.BGColor = 0xFF8000FF;
            cellB1.BGColor = 0xFF8000FF;
            cellA2.BGColor = 0xFF8000FF;
            cellB2.BGColor = 0xFF8000FF;

            testSheet.SaveSheet(fs);
            fs.Close();
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        [Obsolete]
        public void LoadSheetTest()
        {
            CPTS321.Spreadsheet testSheet = new CPTS321.Spreadsheet(5, 5);
            string fileName = "HW9TestXML";
            Stream fs = new FileStream(fileName, FileMode.Open);

            testSheet.LoadSheet(fs);
            fs.Close();

            // Cell A1
            Assert.AreEqual(testSheet.GetCell(0, 0).Value, "100");
            Assert.AreEqual(testSheet.GetCell(0, 0).BGColor, 0xFF8000FF);

            // Cell B1
            Assert.AreEqual(testSheet.GetCell(0, 1).Value, "10000");
            Assert.AreEqual(testSheet.GetCell(0, 1).BGColor, 0xFF8000FF);

            // Cell A2
            Assert.AreEqual(testSheet.GetCell(1, 0).Value, "10020");
            Assert.AreEqual(testSheet.GetCell(1, 0).BGColor, 0xFF8000FF);

            // Cell B2
            Assert.AreEqual(testSheet.GetCell(1, 1).Value, "200");
            Assert.AreEqual(testSheet.GetCell(1, 1).BGColor, 0xFF8000FF);
        }
    }
}