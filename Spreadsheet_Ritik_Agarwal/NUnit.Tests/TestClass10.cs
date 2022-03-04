// <copyright file="HW10TestClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    /// Test class for HW10 testing Bad reference, Self reference circular reference.
    /// </summary>
    [TestFixture]
    public class HW10TestClass
    {
        /// <summary>
        /// General test for bad reference cell value.
        /// </summary>
        [Test]
        [Obsolete]
        public void BadReferenceGeneralTest()
        {
            CPTS321.Spreadsheet sheet = new CPTS321.Spreadsheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=6+Cell*27";
            Assert.AreEqual(cell1.Value, "!(bad reference)");
        }

        /// <summary>
        /// Test for bad reference.
        /// </summary>
        [Test]
        [Obsolete]
        public void BadReferenceEmptyCellTest()
        {
            CPTS321.Spreadsheet sheet = new CPTS321.Spreadsheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=Aa";
            Assert.AreEqual(cell1.Value, "!(bad reference)");
        }

        /// <summary>
        /// Test for bad reference when out of range cell value is entered.
        /// </summary>
        [Test]
        [Obsolete]
        public void BadReferenceOutOfRangeCellTest()
        {
            CPTS321.Spreadsheet sheet = new CPTS321.Spreadsheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=A1112";
            Assert.AreEqual(cell1.Value, "!(bad reference)");
        }

        /// <summary>
        /// General test for self reference.
        /// </summary>
        [Test]
        [Obsolete]
        public void SelfReferenceGeneralTest()
        {
            CPTS321.Spreadsheet sheet = new CPTS321.Spreadsheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=A1+5";
            Assert.AreEqual(cell1.Value, "!(self reference)");
        }

        /// <summary>
        /// Test for self reference.
        /// </summary>
        [Test]
        [Obsolete]
        public void SelfReferenceNullTest()
        {
            CPTS321.Spreadsheet sheet = new CPTS321.Spreadsheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=(100+(5/A1))*2";
            Assert.AreEqual(cell1.Value, "!(self reference)");
        }

        /// <summary>
        /// General test for circular reference.
        /// </summary>
        [Test]
        [Obsolete]
        public void CircularReferenceGeneralTest()
        {
            CPTS321.Spreadsheet sheet = new CPTS321.Spreadsheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            var cell2 = sheet.GetCell("B1");
            cell1.Text = "100";
            cell2.Text = "50";
            cell1.Text = "=100+B1";
            cell2.Text = "=200-A1";
            Assert.AreEqual(cell2.Value, "!(circular reference)");
        }
    }
}
