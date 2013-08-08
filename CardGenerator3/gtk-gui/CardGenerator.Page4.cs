
// This file has been generated by the GUI designer. Do not modify.
namespace CardGenerator
{
	public partial class Page4
	{
		private global::Gtk.VBox vbox1;
		private global::Gtk.Label label1;
		private global::Gtk.Frame frame1;
		private global::Gtk.Alignment GtkAlignment;
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		private global::Gtk.TreeView treeviewCardTypes;
		private global::Gtk.Label GtkLabel1;
		private global::Gtk.Frame frame2;
		private global::Gtk.Alignment GtkAlignment1;
		private global::Gtk.VBox vbox2;
		private global::Gtk.RadioButton rbOrderVocab;
		private global::Gtk.RadioButton rbOrderCardTypes;
		private global::Gtk.RadioButton rbOrderRandom;
		private global::Gtk.Label GtkLabel5;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Button buttonBuildOuput;
		private global::Gtk.Frame frame3;
		private global::Gtk.Alignment GtkAlignment2;
		private global::Gtk.Entry entryCardCount;
		private global::Gtk.Label GtkLabel6;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget CardGenerator.Page4
			global::Stetic.BinContainer.Attach (this);
			this.Name = "CardGenerator.Page4";
			// Container child CardGenerator.Page4.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global.Mono.Unix.Catalog.GetString ("Build Card Data");
			this.vbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeviewCardTypes = new global::Gtk.TreeView ();
			this.treeviewCardTypes.CanFocus = true;
			this.treeviewCardTypes.Name = "treeviewCardTypes";
			this.GtkScrolledWindow1.Add (this.treeviewCardTypes);
			this.GtkAlignment.Add (this.GtkScrolledWindow1);
			this.frame1.Add (this.GtkAlignment);
			this.GtkLabel1 = new global::Gtk.Label ();
			this.GtkLabel1.Name = "GtkLabel1";
			this.GtkLabel1.LabelProp = global.Mono.Unix.Catalog.GetString ("Card Type Selection");
			this.GtkLabel1.UseMarkup = true;
			this.frame1.LabelWidget = this.GtkLabel1;
			this.vbox1.Add (this.frame1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.frame1]));
			w5.Position = 1;
			// Container child vbox1.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment1 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment1.Name = "GtkAlignment1";
			this.GtkAlignment1.LeftPadding = ((uint)(12));
			// Container child GtkAlignment1.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.rbOrderVocab = new global::Gtk.RadioButton (global.Mono.Unix.Catalog.GetString ("Vocabulary"));
			this.rbOrderVocab.CanFocus = true;
			this.rbOrderVocab.Name = "rbOrderVocab";
			this.rbOrderVocab.Active = true;
			this.rbOrderVocab.DrawIndicator = true;
			this.rbOrderVocab.UseUnderline = true;
			this.rbOrderVocab.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.vbox2.Add (this.rbOrderVocab);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.rbOrderVocab]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.rbOrderCardTypes = new global::Gtk.RadioButton (global.Mono.Unix.Catalog.GetString ("Card Types"));
			this.rbOrderCardTypes.CanFocus = true;
			this.rbOrderCardTypes.Name = "rbOrderCardTypes";
			this.rbOrderCardTypes.DrawIndicator = true;
			this.rbOrderCardTypes.UseUnderline = true;
			this.rbOrderCardTypes.Group = this.rbOrderVocab.Group;
			this.vbox2.Add (this.rbOrderCardTypes);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.rbOrderCardTypes]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.rbOrderRandom = new global::Gtk.RadioButton (global.Mono.Unix.Catalog.GetString ("Random"));
			this.rbOrderRandom.CanFocus = true;
			this.rbOrderRandom.Name = "rbOrderRandom";
			this.rbOrderRandom.DrawIndicator = true;
			this.rbOrderRandom.UseUnderline = true;
			this.rbOrderRandom.Group = this.rbOrderVocab.Group;
			this.vbox2.Add (this.rbOrderRandom);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.rbOrderRandom]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			this.GtkAlignment1.Add (this.vbox2);
			this.frame2.Add (this.GtkAlignment1);
			this.GtkLabel5 = new global::Gtk.Label ();
			this.GtkLabel5.Name = "GtkLabel5";
			this.GtkLabel5.LabelProp = global.Mono.Unix.Catalog.GetString ("Order by");
			this.GtkLabel5.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel5;
			this.vbox1.Add (this.frame2);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.frame2]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonBuildOuput = new global::Gtk.Button ();
			this.buttonBuildOuput.CanFocus = true;
			this.buttonBuildOuput.Name = "buttonBuildOuput";
			this.buttonBuildOuput.UseUnderline = true;
			this.buttonBuildOuput.Label = global.Mono.Unix.Catalog.GetString ("Rebuild data");
			this.hbox1.Add (this.buttonBuildOuput);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonBuildOuput]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.frame3 = new global::Gtk.Frame ();
			this.frame3.Name = "frame3";
			this.frame3.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame3.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.entryCardCount = new global::Gtk.Entry ();
			this.entryCardCount.CanFocus = true;
			this.entryCardCount.Name = "entryCardCount";
			this.entryCardCount.IsEditable = false;
			this.entryCardCount.InvisibleChar = '●';
			this.GtkAlignment2.Add (this.entryCardCount);
			this.frame3.Add (this.GtkAlignment2);
			this.GtkLabel6 = new global::Gtk.Label ();
			this.GtkLabel6.Name = "GtkLabel6";
			this.GtkLabel6.LabelProp = global.Mono.Unix.Catalog.GetString ("Output Card Count");
			this.GtkLabel6.UseMarkup = true;
			this.frame3.LabelWidget = this.GtkLabel6;
			this.hbox1.Add (this.frame3);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.frame3]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w16.Position = 3;
			w16.Expand = false;
			w16.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonBuildOuput.Clicked += new global::System.EventHandler (this.onButtonBuildOuput);
		}
	}
}
