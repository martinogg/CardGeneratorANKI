// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;
using Gtk;
using CardGenerator;

public partial class MainWindow: Gtk.Window
{	
	private int i_CurrentPage = 0;
	private Widget currentPage = null;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		showPage (0);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	// shows the relevant widget/page on screen
	private void showPage(int iPage)
	{
		Widget newPage = null;

		if (iPage == 0)
		{
			Page0 pp = new Page0 ();
			newPage = pp;
			pp.parentGtkWindow = this;
		}
		if (iPage == 1)
			newPage = new Page1 ();
		if (iPage == 2)
			newPage = new Page2 ();
		if (iPage == 3)
		{
			Page3 pp = new Page3 ();
			newPage = pp;
			pp.parentGtkWindow = this;
		}
		if (iPage == 4)
			newPage = new Page4 ();
		if (iPage == 5)
		{
			Page5 pp = new Page5 ();
			newPage = pp;
			pp.parentGtkWindow = this;
		}

		if (newPage != null)
		{
			//widgetSpace.Destroy ();
			if (currentPage != null)
			{
				eventBox.Remove (currentPage);
				currentPage.Destroy ();
			}

			eventBox.Add (newPage);
			newPage.Show ();

			labelPage.Text = "Page " + iPage + "/5";

			currentPage = newPage;
			i_CurrentPage = iPage;
		}

	}

	protected void onBnNextPage (object sender, EventArgs e)
	{
		showPage (i_CurrentPage + 1);
	}

	protected void onBnPrevPage (object sender, EventArgs e)
	{
		showPage (i_CurrentPage - 1);
	}
}
