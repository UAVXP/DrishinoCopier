//#define PUBLIC

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VMFPassThrough
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
#if !PUBLIC
		//	if (!Security.IsMachineAllowed())
			if (!NewSecurity.IsMachineAllowed())
		//	if(true)
			{
				MessageBox.Show("You are not on devteam. Bye.\r\n\r\n" + NewSecurity.GetMachineID(), "NutBucket");
				Environment.Exit(100500);
				return;
			}
#endif

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		//	Application.Run(new frmCode());
		}
	}
}
