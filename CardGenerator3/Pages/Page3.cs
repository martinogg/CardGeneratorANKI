// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using System.IO;
using System.Collections.Generic;
using Gtk;
using System.Threading;
using System.Net;

namespace CardGenerator
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class Page3 : Gtk.Bin
	{
		class AudioFileOnScreen
		{
			public AudioFiles.AudioEntry audioEntry { get; set; }

			public String fullPath { get; set; }

			public TreeIter ti { get; set; }
		};

		public Gtk.Window parentGtkWindow = null;
		private String audioFolder = null;
		private List<AudioFileOnScreen> audioFileListOnScreen = new List<AudioFileOnScreen> ();

		public Page3 ()
		{
			this.Build ();
			setupTreeView ();
		}

		private void setupTreeView ()
		{
			Gtk.TreeViewColumn Column = new Gtk.TreeViewColumn ();
			Column.Title = "File";

			// Create the text cell that will display the artist name
			Gtk.CellRendererText NameCell = new Gtk.CellRendererText ();

			// Add the cell to the column
			Column.PackStart (NameCell, true);

			// Create a column for the song title
			treeview1.AppendColumn (Column);

			Column.AddAttribute (NameCell, "text", 0);

			//

			Gtk.ListStore listStore = new Gtk.ListStore (typeof(string));

			// Add some data to the store
			listStore.AppendValues ("Select Folder then Press Refresh");
			treeview1.Model = listStore;

		}

		protected void onBnFolder (object sender, EventArgs e)
		{
			Gtk.FileChooserDialog fc =
				new Gtk.FileChooserDialog ("Choose the file to open",
			                           parentGtkWindow,
			                           FileChooserAction.SelectFolder,
			                           "Cancel", ResponseType.Cancel,
			                           "Open", ResponseType.Accept);

			if (fc.Run () == (int)ResponseType.Accept) {
				buttonFolder.Label = "Audio Folder: " + fc.Filename;
				audioFolder = fc.Filename;
			}
			fc.Destroy ();
		
		}

		protected void onBnRefresh (object sender, EventArgs e)
		{
			GenerateFileList ();
		}

		private void GenerateFileList ()
		{
			// Steps
			// if the folder is selected
			if (audioFolder != null) {
				// Remove list items.
				Gtk.ListStore ls = (Gtk.ListStore)treeview1.Model;
				if (ls != null) {
					ls.Clear ();
				}

				// Get List of Audiofiles required.
				List<AudioFiles.AudioEntry> audioFileList = AudioFiles.generateAudioFileNames ();
				refreshAudioFileOnScreenListFromAudioFileList (audioFileList);

				// Add them to the screenlist
				addAudioFileListToTreeView ();


				//   Show whether they already exist or not
			}

		}

		private void addAudioFileListToTreeView ()
		{
			Gtk.ListStore listStore = (Gtk.ListStore)treeview1.Model;

			// Add some data to the store
			foreach (AudioFileOnScreen item in audioFileListOnScreen) {
				item.ti = listStore.AppendValues (item.audioEntry.filename);
			}
		}

		private void refreshAudioFileOnScreenListFromAudioFileList (List<AudioFiles.AudioEntry> audioFileList)
		{
			audioFileListOnScreen.Clear ();
			foreach (AudioFiles.AudioEntry item in audioFileList) {
				AudioFileOnScreen newFileOnScreen = new AudioFileOnScreen ();
				newFileOnScreen.audioEntry = item;
				newFileOnScreen.fullPath = System.IO.Path.Combine (audioFolder, item.filename);
				audioFileListOnScreen.Add (newFileOnScreen);
			}
		}

		private void ChangeEntryText (TreeIter it, String str)
		{
			Gtk.Application.Invoke (delegate {
				Gtk.ListStore ls = (Gtk.ListStore)treeview1.Model;

				ls.SetValue (it, 0, str);
			});
		}

		protected void onBnDownload (object sender, EventArgs e)
		{
			Thread o = new Thread (new ThreadStart (threadedDownloader));
			o.Start();
		}

		private void threadedDownloader ()
		{
			foreach (AudioFileOnScreen item in audioFileListOnScreen) {
				if (File.Exists (item.fullPath)) {
					ChangeEntryText (item.ti, "ALREADYEXISTS: " + item.audioEntry.filename);
				} else {
					ChangeEntryText (item.ti, "DOWNLOADING: " + item.audioEntry.filename);
					var cli = new WebClient ();
					String urlpath = "http://translate.google.com/translate_tts?ie=UTF-8&tl=" + item.audioEntry.languageCode.ToLower () + "&q=" + item.audioEntry.text;

					try {
						cli.DownloadFile (urlpath, item.fullPath);
						ChangeEntryText (item.ti, "Downloaded " + item.audioEntry.filename);
					} catch (Exception _e) {
						ChangeEntryText (item.ti, "ERROR: " + item.audioEntry.filename + ", :" + _e.ToString ());
					}
				}
			}
		}
	}
}

