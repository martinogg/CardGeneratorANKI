// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using System.Collections.Generic;

namespace CardGenerator
{
	public partial class DialogRemoveColumn : Gtk.Dialog
	{
		public String Column{ get{return cbColumn.ActiveText;}}

		public DialogRemoveColumn ()
		{
			this.Build ();
			buildColumns();
		}

		private void buildColumns()
		{
			List<String> entries = CardGeneratorDB.Instance.GetColumnNames(CardGeneratorDB.TABLECardData);

			foreach(String s in entries)
			{
				cbColumn.AppendText(s);
			}
		}
	}
}

