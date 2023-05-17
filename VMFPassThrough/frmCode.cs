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
	public partial class frmCode : Form
	{
		public frmCode()
		{
			InitializeComponent();
		}

		private void frmCode_Load(object sender, EventArgs e)
		{
#if !PUBLIC
		//	textBox1.Text = Security.GetMachineID();
			textBox1.Text = NewSecurity.GetMachineID();
			textBox1.SelectAll();
#else
			textBox1.ReadOnly = true;
			textBox1.Text = "Not available: Build is public.";
#endif
		}
	}
}
