// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using Gtk;

namespace CardGenerator
{
	public partial class DialogAddCardType : Gtk.Dialog
	{
		public String CardName{ get{return entry1.Text;}}
		public Gtk.Label LabelText{get{return labeltext;}}

		public DialogAddCardType ()
		{
			this.Build ();
		}
	}
}

