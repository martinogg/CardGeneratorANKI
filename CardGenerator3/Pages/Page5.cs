// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using Gtk;

namespace CardGenerator
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class Page5 : Gtk.Bin
	{
		public Gtk.Window parentGtkWindow = null;

		public Page5 ()
		{
			this.Build ();
		}

		protected void onBnExportANKI (object sender, EventArgs e)
		{
			// create a new database and load it
			Gtk.FileChooserDialog fc=
				new Gtk.FileChooserDialog("Choose the file to open",
				                          parentGtkWindow,
				                          FileChooserAction.Save,
				                          "Cancel",ResponseType.Cancel,
				                          "Save",ResponseType.Accept);

			if (fc.Run() == (int)ResponseType.Accept) 
			{
				String fn = fc.Filename;
				CardOutput.saveToAnkiFile(fn);
			}
			fc.Destroy();
		}

		protected void onBnExportKLEIO (object sender, EventArgs e)
		{
			{
				DialogExportKLEIO d = new DialogExportKLEIO ();
				d.Run ();
				d.Destroy ();
			}

		}
	}
}

