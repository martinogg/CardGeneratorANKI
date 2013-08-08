// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using System.Collections.Generic;
using Gtk;

namespace CardGenerator
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class Page1 : Gtk.Bin
	{
		private String strTable = "CardData";

		public Page1 ()
		{
			this.Build ();
			try
			{
				List<String> columnNames = CardGeneratorDB.Instance.GetColumnNames (strTable);
				List<Dictionary<String, String>> data = CardGeneratorDB.Instance.getDataFromTable (strTable);
				BuildTableColumns(columnNames);
				BuildTable (columnNames, data);
			}
			catch (Exception _ee) {
				Console.WriteLine("ERROR:"+_ee.ToString());
				int rr = 3;
				rr++;
			}
		}

			private void BuildTableColumns(List<String> columns)
		{
			foreach (TreeViewColumn col in treeview1.Columns) { 
				treeview1.RemoveColumn (col); 
			} 

			// columns
			// Create a column for the artist name
			for (int i = 0; i < columns.Count; i++) {
				Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
				artistColumn.Title = columns [i];

				// Create the text cell that will display the artist name
				Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText ();

				// Add the cell to the column
				artistColumn.PackStart (artistNameCell, true);

				// Create a column for the song title
				treeview1.AppendColumn (artistColumn);

				artistColumn.AddAttribute (artistNameCell, "text", i);
			}
		}

		private void BuildTable (List<String> columns, List<Dictionary<String, String>> data)
		{
			// Fill the table out from here

			// add data
			System.Type[] types = new System.Type[columns.Count];
			for (int i = 0; i < columns.Count; i++) {
				types [i] = columns [i].GetType ();
			}

			Gtk.ListStore listStore = new Gtk.ListStore (types);

			treeview1.Model = listStore;

			// Add some data to the store
			foreach (Dictionary <String, String> entry in data) {
				String[] row = new String[columns.Count];
				for (int i = 0; i < columns.Count; i++) {
					row [i] = entry [columns [i]];
				}
				listStore.AppendValues (row);
			}
		}

		protected void onBnRemoveData (object sender, EventArgs e)
		{
			CardGeneratorDB.Instance.removeDataFromTable(CardGeneratorDB.TABLECardData);

			List<String> columnNames = CardGeneratorDB.Instance.GetColumnNames (strTable);
			List<Dictionary<String, String>> data = CardGeneratorDB.Instance.getDataFromTable (strTable);

			BuildTable (columnNames, data);

		}

		protected void onBnRemoveColumn (object sender, EventArgs e)
		{
			DialogRemoveColumn dialog = new DialogRemoveColumn();

			if (dialog.Run() == (int)ResponseType.Ok)
			{
				CardGeneratorDB.Instance.removeColumn(CardGeneratorDB.TABLECardData, dialog.Column);

				List<String> columnNames = CardGeneratorDB.Instance.GetColumnNames (strTable);
				List<Dictionary<String, String>> data = CardGeneratorDB.Instance.getDataFromTable (strTable);

				BuildTableColumns(columnNames);
				BuildTable (columnNames, data);
			}
			dialog.Destroy();

		}

		protected void onBnImportFromCSV (object sender, EventArgs e)
		{
			DialogAddRows dialog = new DialogAddRows();
			if (dialog.Run() == (int)ResponseType.Ok)
			{
				List<String> columns = CardGeneratorDB.Instance.GetColumnNames(CardGeneratorDB.TABLECardData);
				columns.Remove(CardGeneratorDB.COLUMNCardDataID);

				List<List<String>> data = dialog.getRowData();
				foreach (List<String> item in data)
				{
					List<String> dataLine = new List<String>();
					for (int i = 0; i < columns.Count; i++)
					{
						dataLine.Add(columns[i]);
						String addText = "";
						if (i < item.Count)
							addText = item[i];
						dataLine.Add(addText);
					}
					CardGeneratorDB.Instance.addRowToTable(CardGeneratorDB.TABLECardData, dataLine.ToArray());
				}

				List<String> columnNames = CardGeneratorDB.Instance.GetColumnNames (strTable);
				List<Dictionary<String, String>> dataT = CardGeneratorDB.Instance.getDataFromTable (strTable);

				BuildTable (columnNames, dataT);
			}
			dialog.Destroy();
		}

		protected void onBnAddColumn (object sender, EventArgs e)
		{

			DialogAddCardType dialog = new DialogAddCardType();
			dialog.LabelText.Text = "Enter new Column Name";

			if (dialog.Run() == (int)ResponseType.Ok)
			{
				CardGeneratorDB.Instance.addColumn(CardGeneratorDB.TABLECardData, dialog.CardName);

				List<String> columnNames = CardGeneratorDB.Instance.GetColumnNames (strTable);
				List<Dictionary<String, String>> data = CardGeneratorDB.Instance.getDataFromTable (strTable);

				BuildTableColumns(columnNames);
				BuildTable (columnNames, data);
			}
			dialog.Destroy();
		}
	}
}

