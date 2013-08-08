// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using System.Collections.Generic;

namespace CardGenerator
{
	public partial class DialogAddRows : Gtk.Dialog
	{
		public DialogAddRows ()
		{
			this.Build ();
		}

		public List<List<String>> getRowData()
		{
			List<List<String>> ret = new List<List<String>>();

			String inText = textview1.Buffer.Text;
			List<String> allLines = new List<String>(inText.Split('\n'));

			foreach (String singleLine in allLines)
			{
				List<String> outLine = new List<String>(singleLine.Split('\t'));
				ret.Add(outLine);
			}

			return ret;

		}
	}
}

