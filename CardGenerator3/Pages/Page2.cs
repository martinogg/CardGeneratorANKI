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
	public partial class Page2 : Gtk.Bin
	{
		private String strTable = "CardDefinitions";
		private String _SelectedCardTypeName = "";

		public Page2 ()
		{
			this.Build ();
			List<String> columnNames = CardGeneratorDB.Instance.GetColumnNames (strTable);
			List<Dictionary<String, String>> data = CardGeneratorDB.Instance.getDataFromTable (strTable);
			BuildTable (columnNames, data);

			BuildCardTypeTable();
			BuildFaceTable(treeviewFace1);
			BuildFaceTable(treeviewFace2);
		}

		private void BuildFaceTable(TreeView tv)
		{
			// Fill the table out from here

			// Create a column for the artist name
			String[] columns = {"Column Name","Language Code", "Voice"};
			for (int i = 0; i < columns.Length; i++) {
				Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
				artistColumn.Title = columns [i];

				// Create the text cell that will display the artist name
				Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText ();

				// Add the cell to the column
				artistColumn.PackStart (artistNameCell, true);

				// Create a column for the song title
				tv.AppendColumn (artistColumn);

				artistColumn.AddAttribute (artistNameCell, "text", i);
			}

			// add data
//			System.Type[] types = new System.Type[columns.Length];
//			for (int i = 0; i < columns.Length; i++) {
//				types [i] = columns [i].GetType ();
//			}
			System.Type[] types = {typeof(String), typeof(String), typeof(String)};
			Gtk.ListStore listStore = new Gtk.ListStore (types);

			// Add some data to the store
//			foreach (Dictionary <String, String> entry in data) {
//				String[] row = new String[columns.Length];
//				for (int i = 0; i < columns.Length; i++) {
//					row [i] = entry [columns [i]];
//				}
//				listStore.AppendValues (row);
//			}

			tv.Model = listStore;
		}



		private void BuildCardTypeTable()
		{
			// columns
			// Create a column for the artist name
			//for (int i = 0; i < columns.Length; i++) 
			{
				Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
				artistColumn.Title = "Card Type Name";

				// Create the text cell that will display the artist name
				Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText ();

				// Add the cell to the column
				artistColumn.PackStart (artistNameCell, true);

				// Create a column for the song title
				treeviewCardType.AppendColumn (artistColumn);

				artistColumn.AddAttribute (artistNameCell, "text", 0);
			}

		
			System.Type[] types = new System.Type[1];
			types [0] = typeof(String);
			
			treeviewCardType.Model = new Gtk.ListStore (types);

			RebuildCardTypeTable();

		}

		private void RebuildCardTypeTable()
		{
			Gtk.ListStore ls = (Gtk.ListStore)treeviewCardType.Model;
			ls.Clear();

			// add data
			List<String> data = CardGeneratorDB.Instance.getSingleColumnDataFromTable (strTable, "FaceCardName");

			// Add some data to the store
			foreach (String entry in data) {
				ls.AppendValues (entry);
			}

		}

		private void BuildTable (List<String> columns, List<Dictionary<String, String>> data)
		{
			// Fill the table out from here

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

//			// add data
//			System.Type[] types = new System.Type[columns.Count];
//			for (int i = 0; i < columns.Count; i++) {
//				types [i] = columns [i].GetType ();
//			}
//
//			Gtk.ListStore listStore = new Gtk.ListStore (types);
//
//			// Add some data to the store
//			foreach (Dictionary <String, String> entry in data) {
//				String[] row = new String[columns.Count];
//				for (int i = 0; i < columns.Count; i++) {
//					row [i] = entry [columns [i]];
//				}
//				listStore.AppendValues (row);
//			}
//
//			treeview1.Model = listStore;
		}

		protected void onBnCardTypeAdd (object sender, EventArgs e)
		{
			// add new card type
			DialogAddCardType newCardName = new DialogAddCardType();
			int iResponse = (int)newCardName.Run();
			if (iResponse == (int)ResponseType.Ok)
			{
				String newCard = newCardName.CardName;
				CardGeneratorDB.Instance.addCardType(newCard);
				RebuildCardTypeTable();
			}
			newCardName.Destroy();
		}

		protected void onBnCardTypeRemove (object sender, EventArgs e)
		{
			// remove the selected from treeviewCardType

			TreeSelection selection = treeviewCardType.Selection;
			TreeModel model;
			TreeIter iter;

			String selID = null;
			if (selection.CountSelectedRows() == 1)
			{
				if(selection.GetSelected(out model, out iter)){
					selID = model.GetValue (iter, 0).ToString();
					CardGeneratorDB.Instance.removeRowFromTable(CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionName, selID);
					SelectCardType(null);
					RebuildCardTypeTable();
				}
			}

		}

		protected void onBnFace1Add (object sender, EventArgs e)
		{
			DialogAddFaceData dialog = new DialogAddFaceData();
			if (dialog.Run() == (int)ResponseType.Ok)
			{

				Dictionary<String, String> cardDefinition = CardGeneratorDB.Instance.getRowFromTable(CardGeneratorDB.TABLECardDefinitions, "FaceCardName", _SelectedCardTypeName);
				List<CardDefinitions.CardDefClass> face1 = CardDefinitions.faceCardStructureFromJSONString(cardDefinition["Face1"]);

				CardDefinitions.CardDefClass newFaceDef = new CardDefinitions.CardDefClass();
				newFaceDef.COLUMNSOURCE = dialog.Column;
				newFaceDef.LANGUAGECODE = dialog.LanguageCode;
				newFaceDef.TYPE = dialog.Voice;

				face1.Add(newFaceDef);
				String face1JSONString = CardDefinitions.JSONStringFromfaceCardStructure(face1);
				CardGeneratorDB.Instance.updateRowField(CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionID, cardDefinition[CardGeneratorDB.COLUMNCardDefinitionID], "Face1", face1JSONString);
				SelectCardType(_SelectedCardTypeName);

			}
			dialog.Destroy();
		}

		protected void onBnFace1Remove (object sender, EventArgs e)
		{
			TreeSelection selection = treeviewFace1.Selection;
			TreeModel model;
			TreeIter iter;

			if (selection.CountSelectedRows() == 1)
			{
				TreePath tp = selection.GetSelectedRows()[0];
				int iPlace = tp.Indices[0];

				if(selection.GetSelected(out model, out iter)){

					// get the face1 list
					//
						
					Dictionary<String, String> cardDefinition = CardGeneratorDB.Instance.getRowFromTable(CardGeneratorDB.TABLECardDefinitions, "FaceCardName", _SelectedCardTypeName);
					List<CardDefinitions.CardDefClass> face1 = CardDefinitions.faceCardStructureFromJSONString(cardDefinition["Face1"]);
					face1.RemoveAt(iPlace);
					String face1JSONString = CardDefinitions.JSONStringFromfaceCardStructure(face1);

					CardGeneratorDB.Instance.updateRowField(CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionID, cardDefinition[CardGeneratorDB.COLUMNCardDefinitionID], "Face1", face1JSONString);
					
					SelectCardType(_SelectedCardTypeName);

					}
			}

		}

		protected void onBnFace2Add (object sender, EventArgs e)
		{
			DialogAddFaceData dialog = new DialogAddFaceData();
			if (dialog.Run() == (int)ResponseType.Ok)
			{

				Dictionary<String, String> cardDefinition = CardGeneratorDB.Instance.getRowFromTable(CardGeneratorDB.TABLECardDefinitions, "FaceCardName", _SelectedCardTypeName);
				List<CardDefinitions.CardDefClass> face = CardDefinitions.faceCardStructureFromJSONString(cardDefinition["Face2"]);

				CardDefinitions.CardDefClass newFaceDef = new CardDefinitions.CardDefClass();
				newFaceDef.COLUMNSOURCE = dialog.Column;
				newFaceDef.LANGUAGECODE = dialog.LanguageCode;
				newFaceDef.TYPE = dialog.Voice;

				face.Add(newFaceDef);
				String faceJSONString = CardDefinitions.JSONStringFromfaceCardStructure(face);
				CardGeneratorDB.Instance.updateRowField(CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionID, cardDefinition[CardGeneratorDB.COLUMNCardDefinitionID], "Face2", faceJSONString);
				SelectCardType(_SelectedCardTypeName);

			}
			dialog.Destroy();
		}

		protected void onBnFace2Remove (object sender, EventArgs e)
		{
			TreeSelection selection = treeviewFace2.Selection;
			TreeModel model;
			TreeIter iter;

			if (selection.CountSelectedRows() == 1)
			{
				TreePath tp = selection.GetSelectedRows()[0];
				int iPlace = tp.Indices[0];

				if(selection.GetSelected(out model, out iter)){

					// get the face1 list
					//

					Dictionary<String, String> cardDefinition = CardGeneratorDB.Instance.getRowFromTable(CardGeneratorDB.TABLECardDefinitions, "FaceCardName", _SelectedCardTypeName);
					List<CardDefinitions.CardDefClass> face1 = CardDefinitions.faceCardStructureFromJSONString(cardDefinition["Face2"]);
					face1.RemoveAt(iPlace);
					String face1JSONString = CardDefinitions.JSONStringFromfaceCardStructure(face1);

					CardGeneratorDB.Instance.updateRowField(CardGeneratorDB.TABLECardDefinitions, CardGeneratorDB.COLUMNCardDefinitionID, cardDefinition[CardGeneratorDB.COLUMNCardDefinitionID], "Face2", face1JSONString);

					SelectCardType(_SelectedCardTypeName);

				}
			}


		}

		protected void onCardTypeCursorChanged (object sender, EventArgs e)
		{
			TreeSelection selection = (sender as TreeView).Selection;
			TreeModel model;
			TreeIter iter;

			String selID = null;
			if (selection.CountSelectedRows() == 1)
			{
				if(selection.GetSelected(out model, out iter)){
					selID = model.GetValue (iter, 0).ToString();
				}
			}

			SelectCardType(selID);
		}

		private void SelectCardType(String strID)
		{
			Gtk.ListStore lsf1 = (Gtk.ListStore)treeviewFace1.Model;
			Gtk.ListStore lsf2 = (Gtk.ListStore)treeviewFace2.Model;
			lsf1.Clear();
			lsf2.Clear();
			_SelectedCardTypeName = strID;

			if (strID != null)
			{
				Dictionary<String, String> cardDefinition = CardGeneratorDB.Instance.getRowFromTable(CardGeneratorDB.TABLECardDefinitions, "FaceCardName", strID);
				List<CardDefinitions.CardDefClass> face1 = CardDefinitions.faceCardStructureFromJSONString(cardDefinition["Face1"]);
				List<CardDefinitions.CardDefClass> face2 = CardDefinitions.faceCardStructureFromJSONString(cardDefinition["Face2"]);

				PopulateFaceTable(treeviewFace1, face1);
				PopulateFaceTable(treeviewFace2, face2);
			}
		}

		private void PopulateFaceTable(TreeView tv, List<CardDefinitions.CardDefClass> faceDef)
		{
			Gtk.ListStore treemodel = (Gtk.ListStore)tv.Model;
			treemodel.Clear();

			foreach (CardDefinitions.CardDefClass item in faceDef)
			{
				String[] line = {item.COLUMNSOURCE, item.LANGUAGECODE, item.TYPE?"Voice":"Text"};
				treemodel.AppendValues(line);
			}
		}
	}
}

