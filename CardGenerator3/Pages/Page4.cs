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
	public partial class Page4 : Gtk.Bin
	{

		public Page4 ()
		{
			this.Build ();
			BuildTable ();
			RefreshTable ();
		}

		private CardOutput.eCARDORDERTYPE getOrderType ()
		{
			CardOutput.eCARDORDERTYPE ret = CardOutput.eCARDORDERTYPE.eCARDORDERRANDOM;
			if (rbOrderCardTypes.Active == true)
				ret = CardOutput.eCARDORDERTYPE.eCARDORDERTYPES;
			else if (rbOrderVocab.Active == true)
				ret = CardOutput.eCARDORDERTYPE.eCARDORDERVOCAB;

			return ret;
		}

		private void RefreshTable ()
		{
			List<String> cardNames = CardGeneratorDB.Instance.getSingleColumnDataFromTable (CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionName);
			int iPos = 0;

			List<String> showCardNames = new List<String>();

			TreeIter iter;

			store.GetIterFirst (out iter);

			bool bContinue = true;
			while (bContinue) {
				bool bChecked = (bool)store.GetValue (iter, 1);
				if (bChecked)
				{
					showCardNames.Add(cardNames[iPos]);
				}
				iPos++;
				bContinue = store.IterNext (ref iter);
			}

			CardOutput.BuildCardOutputTable (showCardNames, getOrderType ());

			String strTable = CardGeneratorDB.TABLECardOutput;
			List<String> columns = CardGeneratorDB.Instance.GetColumnNames (strTable);
			//List<Dictionary<String, String>> data = CardGeneratorDB.Instance.getDataFromTable (strTable);

			// add data
			System.Type[] types = new System.Type[columns.Count];
			for (int i = 0; i < columns.Count; i++) {
				types [i] = columns [i].GetType ();
			}

//			Gtk.ListStore listStore = new Gtk.ListStore (types);

//			// Add some data to the store
//			foreach (Dictionary <String, String> entry in data) {
//				String[] row = new String[columns.Count];
//				for (int i = 0; i < columns.Count; i++) {
//					row [i] = entry [columns [i]];
//				}
//				listStore.AppendValues (row);
//			}

//			treeview1.Model = listStore;
			entryCardCount.Text = ""+CardGeneratorDB.Instance.GetRowCount(CardGeneratorDB.TABLECardOutput);
		}

		private TreeStore store;

		private void BuildTable ()
		{
//			String strTable = CardGeneratorDB.TABLECardOutput;
//			List<String> columns = CardGeneratorDB.Instance.GetColumnNames (strTable);

			// columns
			// Create a column for the artist name
//			for (int i = 0; i < columns.Count; i++) {
//				Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
//				artistColumn.Title = columns [i];
//
//				// Create the text cell that will display the artist name
//				Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText ();
//
//				// Add the cell to the column
//				artistColumn.PackStart (artistNameCell, true);
//
//				// Create a column for the song title
//				treeview1.AppendColumn (artistColumn);
//
//				artistColumn.AddAttribute (artistNameCell, "text", i);
//			}

			{

				// populate store..

				store = new TreeStore (typeof(string), typeof(bool));
				List<String> cardNames = CardGeneratorDB.Instance.getSingleColumnDataFromTable (CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionName);
				foreach (String item in cardNames) {
					store.AppendValues (item, true);
				}

				treeviewCardTypes.Model = store;
				treeviewCardTypes.HeadersVisible = true;

				treeviewCardTypes.AppendColumn ("Name", new CellRendererText (), "text", 0);

				CellRendererToggle crt = new CellRendererToggle ();
				crt.Activatable = true;
				crt.Toggled += crt_toggled;
				//treeviewCardTypes.AppendColumn ("CheckMe", crt, "active", 1);
				treeviewCardTypes.AppendColumn ("CheckMe", crt, "active", 1);

				// add the TreeView to some window...

				// !!

				//
				/*
				Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
				artistColumn.Title = "Column";

				// Create the text cell that will display the artist name
				Gtk.CellRendererToggle artistNameCell = new Gtk.CellRendererToggle ();

				// Add the cell to the column
				artistColumn.PackStart (artistNameCell, true);

				// Create a column for the song title
				treeviewCardTypes.AppendColumn ("Name", new CellRendererText (), "text", 0);

				artistColumn.AddAttribute (artistNameCell, "text", 0);

				Gtk.ListStore listStore = new Gtk.ListStore (typeof(String), typeof(bool));
				treeviewCardTypes.Model = listStore;

				treeviewCardTypes.HeadersVisible = true;


				listStore.AppendValues("Stars");
				*/

			}
		}

		private void crt_toggled (object o, ToggledArgs args)
		{
			TreeIter iter;

			if (store.GetIter (out iter, new TreePath (args.Path))) {
				bool old = (bool)store.GetValue (iter, 1);
				store.SetValue (iter, 1, !old);
			}
		}

		protected void onButtonBuildOuput (object sender, EventArgs e)
		{

			RefreshTable ();
		}
	}
}

