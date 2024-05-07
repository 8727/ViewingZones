using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewingZones
{
    public partial class Ui : Form
    {
        string InstallDir = "C:\\Vocord\\Vocord.Traffic Crossroads\\";
        string ScreenshotDir = "E:\\Screenshots";
        public Ui()
        {
            InitializeComponent();
        }

        private void Ui_Load(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Vocord\VOCORD Traffic CrossRoads Server"))
            {
                InstallDir = key?.GetValue("InstallDir")?.ToString();
                ScreenshotDir = key?.GetValue("ScreenshotDir")?.ToString();
            }





        }

        private void search_Click(object sender, EventArgs e)
        {

        }


    }
}
