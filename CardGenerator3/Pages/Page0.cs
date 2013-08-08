// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using System.IO;
using System.Data;
using Gtk;
using System.Threading;
using System.Net;


namespace CardGenerator
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class Page0 : Gtk.Bin
	{
		public Gtk.Window parentGtkWindow = null;

		public Page0 ()
		{
				this.Build ();
				
			String path = CardGeneratorDB.Instance.DBPath;
			if (path == "")
				path = "none.";
			labelFile.Text = labelFile.Text = "File Selected: "+path;
		}

		protected void onBNLoadDB (object sender, EventArgs e)
		{
			// select a database and load it

				Gtk.FileChooserDialog fc=
					new Gtk.FileChooserDialog("Choose the file to open",
					                          parentGtkWindow,
					                          FileChooserAction.Open,
					                          "Cancel",ResponseType.Cancel,
					                          "Open",ResponseType.Accept);

			if (fc.Run() == (int)ResponseType.Accept) 
			{
				if (CGError.OK == CardGeneratorDB.Instance.loadDBAtPath (fc.Filename))
				{

					labelFile.Text = "File Selected: "+fc.Filename;
				}
				else
				{
					labelFile.Text = "File Selected: none";
					MessageDialog md = new MessageDialog (parentGtkWindow,
					                                      Gtk.DialogFlags.Modal,
					                                      MessageType.Info,
					                                      ButtonsType.Ok,
					                                      "Error in DB File load");
					md.Run ();
					md.Destroy();

				}
				//System.IO.FileStream file=System.IO.File.OpenRead(fc.Filename);
				//file.Close();
			}
			//Don't forget to call Destroy() or the FileChooserDialog window won't get closed.
			fc.Destroy();

		}

		protected void onBNNewDB (object sender, EventArgs e)
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
				if (CGError.OK == CardGeneratorDB.Instance.createDBAtPath (fc.Filename))
				{
					labelFile.Text = "File Selected: "+fc.Filename;
				}
				else
				{
					labelFile.Text = "File Selected: none";
					MessageDialog md = new MessageDialog (parentGtkWindow,
					                                      Gtk.DialogFlags.Modal,
					                                      MessageType.Info,
					                                      ButtonsType.Ok,
					                                      "Error in DB File creation");
					md.Run ();
					md.Destroy();
				}
			}
			fc.Destroy();



		}
	}
}

