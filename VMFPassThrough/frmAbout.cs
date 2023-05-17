using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VMFPassThrough
{
	public partial class frmAbout : Form
	{
		public frmAbout()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FrmAbout_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.T)
			{
				frmCode codegen = new frmCode();
				codegen.ShowDialog();
			}
		/*	if (e.Modifiers == Keys.Control && e.KeyCode == Keys.T)
			{
				frmCode codegen = new frmCode();
				codegen.ShowDialog();
			}*/
		}
	}
}
