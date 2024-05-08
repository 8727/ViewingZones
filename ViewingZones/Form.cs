using Microsoft.Win32;
using System;
using System.Data.SQLite;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace ViewingZones
{
    public partial class Ui : Form
    {
        Hashtable Channel = new Hashtable();
        string InstallDir = @"C:\Vocord\Vocord.Traffic Crossroads\";
        string ScreenshotDir = @"E:\Screenshots";

        public Ui()
        {
            InitializeComponent();
        }

        void Ui_Load(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Vocord\VOCORD Traffic CrossRoads Server"))
            {
                if(key != null)
                {
                    if(key.GetValue("InstallDir") != null)
                    {
                        InstallDir = key.GetValue("InstallDir").ToString();
                    }
                    if (key.GetValue("ScreenshotDir") != null)
                    {
                        ScreenshotDir = key.GetValue("ScreenshotDir").ToString();
                    }
                }
            }
            if (File.Exists( InstallDir + @"Database\vtsettingsdb.sqlite"))
            {
                string sqlChannel = "SELECT CHANNEL_ID, NAME FROM CHANNELS";
                using (var connection = new SQLiteConnection($@"URI=file:{InstallDir}Database\vtsettingsdb.sqlite"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(sqlChannel, connection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //Channel.Add(reader.GetString(0), reader.GetStream(1));
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show($"There is no database file \n{InstallDir}Database\\vtsettingsdb.sqlite \nor it is in a different folder.", "No database file", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
            }

            string sqlzones = "SELECT ChannelId, Type, Name, X1, Y1, X2, Y2, X3, Y3, X4, Y4 FROM Zone WHERE Type = 2 or Type = 3 or Type = 4";

/*            using (var connection = new SQLiteConnection($@"URI=file:{InstallDir}Database\bpm.db"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sqlzones, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }*/




        }



        void search_Click(object sender, EventArgs e)
        {
            string sqlcar = $"SELECT CHECKTIME, CHANNEL_ID, SCREENSHOT FROM CARS WHERE GRNNUMBER LIKE \"{numberBox.Text.Replace('*', '_').ToUpper()}\"";

            label1.Text = sqlcar;  
            
            timeAndDateBox.Items.Clear();    

            using (var connection = new SQLiteConnection($@"URI=file:{InstallDir}Database\vtvehicledb.sqlite"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sqlcar, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            
                            string test = DateTime.FromFileTime(reader.GetInt64(0)).ToString();
                            timeAndDateBox.Items.Add(test + " " + reader.GetString(1));
                        }
                    }
                }
            }






/*            XmlDocument xFile = new XmlDocument();
            xFile.Load(InstallDir + "configmanager.dll.config");

            XmlNodeList _fnames = xFile.GetElementsByTagName("FirstName");*/
        }


    }
}
