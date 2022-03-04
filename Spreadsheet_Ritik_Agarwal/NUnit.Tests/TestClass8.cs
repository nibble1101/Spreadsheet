// <copyright file="HW8TestClass.cs" company="PlaceholderCompany">
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    /// <summary>
    /// Test class for HW8.
    /// </summary>
    [TestFixture]
    public class HW8TestClass
    {
        /// <summary>
        /// General test for undo function.
        /// </summary>
        [Test]
        [Obsolete]
        public void UndoTest()
        {
            CPTS321.Spreadsheet testSheet = new CPTS321.Spreadsheet(5, 5);
            CPTS321.Cell cellA1 = testSheet.GetCell(0, 0);
            cellA1.Text = "100";
            CPTS321.TextChangedEvents command = new CPTS321.TextChangedEvents(cellA1, string.Empty, "100", "Text");
            testSheet.AddUndoStack(command);
            testSheet.Undo();
            Assert.AreEqual(cellA1.Value, string.Empty);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        [Obsolete]
        public void UndoTestSecond()
        {
            CPTS321.Spreadsheet testSheet = new CPTS321.Spreadsheet(5, 5);

            CPTS321.Cell cellA1 = testSheet.GetCell(0, 0);
            cellA1.Text = "100";
            CPTS321.TextChangedEvents command = new CPTS321.TextChangedEvents(cellA1, string.Empty, "100", "Text");
            testSheet.AddUndoStack(command);
            CPTS321.Cell cellB1 = testSheet.GetCell(1, 0);
            cellB1.Text = "=A1";
            CPTS321.TextChangedEvents command2 = new CPTS321.TextChangedEvents(cellB1, string.Empty, "=A1", "Text");
            testSheet.AddUndoStack(command2);
            testSheet.Undo();
            Assert.AreEqual(cellB1.Value, string.Empty);
            testSheet.Undo();
            Assert.AreEqual(cellA1.Value, string.Empty);
        }

        /// <summary>
        /// If nothing to undo, then there should not be any change.
        /// </summary>
        [Test]
        [Obsolete]
        public void UndoNullTest()
        {
            CPTS321.Spreadsheet testSheet = new CPTS321.Spreadsheet(2, 2);
            Assert.That(() => testSheet.Undo(), Throws.TypeOf<System.InvalidOperationException>());

            // going through entire sphreadsheet and making sure that all fields are empty.
            for (int i = 0; i < testSheet.SpreadsheetArray.Length; i++)
            {
                for (int j = 0; j < testSheet.SpreadsheetArray.Length; j++)
                {
                    Assert.AreEqual(testSheet.GetCell(i, j).Value, string.Empty);
                }
            }
        }

        /// <summary>
        /// General test for Redo.
        /// </summary>
        [Test]
        [Obsolete]
        public void RedoTest()
        {
            CPTS321.Spreadsheet testSheet = new CPTS321.Spreadsheet(3, 3);
            CPTS321.Cell cellA1 = testSheet.GetCell(0, 0);
            cellA1.Text = "100";
            CPTS321.TextChangedEvents command2 = new CPTS321.TextChangedEvents(cellA1, string.Empty, "100", "Text");
            testSheet.AddUndoStack(command2);
            testSheet.Undo();
            testSheet.Redo();
            Assert.AreEqual(cellA1.Value, "100");
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        [Obsolete]
        public void RedoTestSecond()
        {
            CPTS321.Spreadsheet sheet = new CPTS321.Spreadsheet(5, 5);
            var cellA1 = sheet.GetCell(0, 0);
            cellA1.Text = "100";
            CPTS321.TextChangedEvents command1 = new CPTS321.TextChangedEvents(cellA1, string.Empty, "100", "Text");
            sheet.AddUndoStack(command1);
            var cellB1 = sheet.GetCell(0, 1);
            cellB1.Text = "=A1+100";
            CPTS321.TextChangedEvents command2 = new CPTS321.TextChangedEvents(cellB1, string.Empty, "=A1+100", "Text");
            sheet.AddUndoStack(command2);
            sheet.Undo();
            sheet.Redo();
            Assert.AreEqual(cellB1.Value, "200");
            sheet.Undo();
            sheet.Redo();
            Assert.AreEqual(cellA1.Value, "100");
        }

        /// <summary>
        /// If nothing to Redo, then there should not be any change.
        /// </summary>
        [Test]
        [Obsolete]
        public void RedoNullTest()
        {
            CPTS321.Spreadsheet testSheet = new CPTS321.Spreadsheet(5, 5);
            Assert.That(() => testSheet.Redo(), Throws.TypeOf<System.InvalidOperationException>());

            // going through entire sphreadsheet and making sure that all fields are empty.
            for (int i = 0; i < testSheet.SpreadsheetArray.Length; i++)
            {
                for (int j = 0; j < testSheet.SpreadsheetArray.Length; j++)
                {
                    CPTS321.Cell cell = testSheet.GetCell(i, j);
                    string val = cell.Value;
                    Assert.AreEqual(val, string.Empty);
                }
            }
        }
    }
}