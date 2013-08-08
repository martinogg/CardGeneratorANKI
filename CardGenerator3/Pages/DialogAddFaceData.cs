// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using Gtk;
using System.Collections.Generic;

namespace CardGenerator
{
	public partial class DialogAddFaceData : Gtk.Dialog
	{
		public String Column{ get{return cbColumn.ActiveText;}}
		public String LanguageCode{ get{return cbLangCode.ActiveText;}}
		public bool Voice{ get{return (rbVoice.Active == true);}}

		public DialogAddFaceData ()
		{
			this.Build ();
			PopulateColumns();
		}

		private void PopulateColumns()
		{
			List<String> entries = CardGeneratorDB.Instance.GetColumnNames(CardGeneratorDB.TABLECardData);

			foreach(String s in entries)
			{
				cbColumn.AppendText(s);
			}
		}
	}
}

