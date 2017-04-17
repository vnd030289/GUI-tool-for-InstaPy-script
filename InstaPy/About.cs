using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InstaPy
{
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Specify that the link was visited.
			this.linkLabel1.LinkVisited = true;

			// Navigate to a URL.
			System.Diagnostics.Process.Start("https://github.com/Nemixalone/GUI-tool-for-InstaPy-script");
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// Specify that the link was visited.
			this.linkLabel1.LinkVisited = true;

			// Navigate to a URL.
			System.Diagnostics.Process.Start("https://github.com/timgrossmann/InstaPy");
			
		}
	}
}
