using System;
using Gtk;

namespace SupAdv
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Label label1 = new Label();
			label1.Text = "label_Test";
			win.Add (label1);

			Application.Run ();
		}
	}
}
