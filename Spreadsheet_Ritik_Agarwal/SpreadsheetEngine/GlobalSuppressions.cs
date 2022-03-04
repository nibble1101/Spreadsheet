// <copyright file="GlobalSuppressions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Promoting encapsultion>", Scope = "member", Target = "~F:CPTS321.Cell.text")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Promoting encapsultion>", Scope = "member", Target = "~F:CPTS321.Cell.value")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Promoting encapsultion>", Scope = "member", Target = "~F:CPTS321.Cell.rowIndex")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Promoting encapsultion>", Scope = "member", Target = "~F:CPTS321.Cell.columnIndex")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "<Constructor required.>", Scope = "member", Target = "~M:CPTS321.Cell.#ctor(System.Int32,System.Int32)")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Declaring the field as private.>", Scope = "member", Target = "~F:CPTS321.Spreadsheet.SpreadsheetArray")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "<Reorganization of code was messy.>", Scope = "member", Target = "~M:CPTS321.ExpressionTree.Evaluate~System.Double")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "<Reorganization of the code was messy.>", Scope = "member", Target = "~M:CPTS321.Spreadsheet.OnCellPropertyChanged(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<The field is required to be public for accessing it in the spreadsheet class.>", Scope = "member", Target = "~F:CPTS321.Cell.MyExpressionTree")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "<Reorganization of code will be messy.>", Scope = "member", Target = "~M:CPTS321.ExpressionTree.GetVariableNames~System.Collections.Generic.List{System.String}")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Requires to be public for it to be accessed by expression tree class.>", Scope = "member", Target = "~F:CPTS321.TreeFactory.Operators")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "<Reorganization of code will be messy.>", Scope = "member", Target = "~M:CPTS321.Spreadsheet.OnCellPropertyChanged(System.Object,System.ComponentModel.PropertyChangedEventArgs)")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Promoting encapsulation.>", Scope = "member", Target = "~F:CPTS321.Cell.bgColor")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "<Reorganization of code is messy.>", Scope = "member", Target = "~M:CPTS321.Spreadsheet.Undo")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "<Reorganization of code is messy.>", Scope = "member", Target = "~M:CPTS321.Spreadsheet.ColorToUint(System.Drawing.Color)~System.UInt32")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "<Reorganization of code is messy.>", Scope = "member", Target = "~M:CPTS321.Spreadsheet.AddUndoStack(CPTS321.IUndoRedo)")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "<Reorganization of code is messy.>", Scope = "member", Target = "~M:CPTS321.Cell.IsChanged~System.Boolean")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "<Reorganization of code is messy.>", Scope = "member", Target = "~F:CPTS321.Cell.VarNames")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<For easy access of the object.>", Scope = "member", Target = "~F:CPTS321.Cell.VarNames")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<For easy access of the object.>", Scope = "member", Target = "~F:CPTS321.ExpressionTree.Variables")]
