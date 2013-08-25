using System;

namespace CardGenerator
{
	public partial class DialogExportKLEIO : Gtk.Dialog
	{
		public Gtk.Window parentGtkWindow = null;

		public DialogExportKLEIO ()
		{
			this.Build ();
			textview1.Buffer.Text = CardOutput.textFromKLEIOOutput ();
		}

		protected void onBnExportKLEIO (object sender, EventArgs e)
		{
			// create a new database and load it


			Gtk.FileChooserDialog fc=
				new Gtk.FileChooserDialog("Choose the file to save to",
				                          parentGtkWindow,
					                          Gtk.FileChooserAction.Save,
				                          "Cancel", Gtk.ResponseType.Cancel,
				                          "Save", Gtk.ResponseType.Accept);

			if (fc.Run() == (int)Gtk.ResponseType.Accept) 
			{
				String fn = fc.Filename;
				CardOutput.saveToKLEIOFile(fn);

			}
			fc.Destroy();
		}
	}
}

