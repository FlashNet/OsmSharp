﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OsmSharp.WinForms.UI.Sample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            OsmSharp.Logging.Log.RegisterConsoleListener();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            ////Application.Run(new SampleControlForm());
            Application.Run(new MapControlForm());
        }
    }
}