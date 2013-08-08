
// This file has been generated by the GUI designer. Do not modify.
namespace CardGenerator
{
	public partial class DialogAddCardType
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.Frame frame1;
		private global::Gtk.Alignment GtkAlignment2;
		private global::Gtk.Entry entry1;
		private global::Gtk.Label labeltext;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget CardGenerator.DialogAddCardType
			this.Name = "CardGenerator.DialogAddCardType";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child CardGenerator.DialogAddCardType.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.entry1 = new global::Gtk.Entry ();
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '●';
			this.GtkAlignment2.Add (this.entry1);
			this.frame1.Add (this.GtkAlignment2);
			this.labeltext = new global::Gtk.Label ();
			this.labeltext.Name = "labeltext";
			this.labeltext.LabelProp = global.Mono.Unix.Catalog.GetString ("Enter New Card Definition Name");
			this.labeltext.UseMarkup = true;
			this.frame1.LabelWidget = this.labeltext;
			this.vbox2.Add (this.frame1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.frame1]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			w1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox2]));
			w5.Position = 0;
			// Internal child CardGenerator.DialogAddCardType.ActionArea
			global::Gtk.HButtonBox w6 = this.ActionArea;
			w6.Name = "dialog1_ActionArea";
			w6.Spacing = 10;
			w6.BorderWidth = ((uint)(5));
			w6.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w6 [this.buttonCancel]));
			w7.Expand = false;
			w7.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w8 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w6 [this.buttonOk]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show ();
		}
	}
}
