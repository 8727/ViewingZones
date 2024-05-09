using Microsoft.Win32;
using System;
using System.Data.SQLite;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Drawing;
using System.Reflection;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;

namespace ViewingZones
{

    public partial class Ui : Form
    {
        public Ui()
        {
            InitializeComponent();
        }
        string[] timeCar = {"ObjectImage", "ObjectBeginImage", "ObjectEndImage" };


        class CarFilePoint
        {
            public string file;
            public Int16 x;
            public Int16 y;
            public Int16 width;
            public Int16 height;
        }

        class ChannelZone
        {
            public string name;
            public Int16 type;
            public Int16 x1;
            public Int16 y1;
            public Int16 x2;
            public Int16 y2;
            public Int16 x3;
            public Int16 y3;
            public Int16 x4;
            public Int16 y4;
        }

        class ChannelNameZone
        {
            public string channelName;
            public Int16 count;
            public ChannelZone[] zones { get; set; } = new ChannelZone[100];
        }

        Hashtable channel = new Hashtable(); // камеры на съемнике 
        Hashtable cars = new Hashtable(); // найденые проезды
        Hashtable imagesCarcs = new Hashtable(); // найденые картинки в проезде

        string installDir = @"C:\Vocord\Vocord.Traffic Crossroads\";
        string screenshotDir = @"E:\Screenshots";

        void Ui_Load(object sender, EventArgs e)
        {
            carsBox.Enabled = false;
            imagesBox.Enabled = false;
            save.Enabled = false;
            saveAll.Enabled = false;

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Vocord\VOCORD Traffic CrossRoads Server"))
            {
                if(key != null)
                {
                    if(key.GetValue("InstallDir") != null)
                    {
                        installDir = key.GetValue("InstallDir").ToString();
                    }
                    if (key.GetValue("ScreenshotDir") != null)
                    {
                        screenshotDir = key.GetValue("ScreenshotDir").ToString();
                    }
                }
            }
            if (File.Exists(installDir + @"Database\vtsettingsdb.sqlite"))
            {
                string sqlChannel = "SELECT CHANNEL_ID, NAME FROM CHANNELS";
                using (var connection = new SQLiteConnection($@"URI=file:{installDir}Database\vtsettingsdb.sqlite"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(sqlChannel, connection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                LoadZone(reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show($"There is no database file \n{installDir}Database\\vtsettingsdb.sqlite \nor it is in a different folder.", "No database file", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
            }
        }

        void LoadZone(string id, string name)
        {
            if (File.Exists(installDir + @"Database\bpm.db"))
            {
                string sqlzones = $"SELECT Type, Name, X1, Y1, X2, Y2, X3, Y3, X4, Y4 FROM Zone WHERE ChannelId = \"{id}\" AND Type < 11";
                using (var connection = new SQLiteConnection($@"URI=file:{installDir}Database\bpm.db"))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(sqlzones, connection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int indexZona = 0;
                            ChannelNameZone nameZone = new ChannelNameZone();
                            while (reader.Read())
                            {
                                ChannelZone zone = new ChannelZone();
                                zone.name = reader.GetString(1);
                                zone.type = reader.GetInt16(0);
                                zone.x1 = Convert.ToInt16(reader.GetFloat(2));
                                zone.y1 = Convert.ToInt16(reader.GetFloat(3));
                                zone.x2 = Convert.ToInt16(reader.GetFloat(4));
                                zone.y2 = Convert.ToInt16(reader.GetFloat(5));
                                zone.x3 = Convert.ToInt16(reader.GetFloat(6));
                                zone.y3 = Convert.ToInt16(reader.GetFloat(7));
                                zone.x4 = Convert.ToInt16(reader.GetFloat(8));
                                zone.y4 = Convert.ToInt16(reader.GetFloat(9));

                                nameZone.zones[indexZona++] = zone;
                            }
                            nameZone.channelName = name;
                            nameZone.count = Convert.ToInt16(indexZona);
                            channel.Add(id, nameZone);
                        }
                    }
                }
            }
        }

        void drawingPolygons()
        {
            if (imagesBox.Items.Count > 0)
            {
                CarFilePoint imgCar = (CarFilePoint)imagesCarcs[imagesBox.SelectedItem.ToString()];

                if (imgCar.file != "")
                {
                    imageBox.Image = Image.FromFile("C:\\vocord\\Vocord.Traffic Crossroads\\11394 09-41-04 @257068@\\" + imgCar.file);
                    Graphics imageBoximg = Graphics.FromImage(imageBox.Image);
                    Pen pen = new Pen(Color.Red, 5);
                    imageBoximg.DrawRectangle(pen, (imgCar.x - imgCar.width / 2), (imgCar.y - imgCar.height / 2), imgCar.width, imgCar.height);
                    imageBoximg.DrawEllipse(pen, imgCar.x, imgCar.y, 5, 5);

                    var color = Color.LightGray;

                    ChannelNameZone channelZones = (ChannelNameZone)channel["289D333B-6E88-404E-B492-E4AA89183C81"];

                    label1.Text = channelZones.count.ToString();

                    for (Int16 indexZone = 0; indexZone < channelZones.count; indexZone++)
                    {
                        if (channelZones.zones[indexZone].type == 0) { color = Color.Green; } // Зона поиска встречного движения
                        if (channelZones.zones[indexZone].type == 2) { color = Color.SkyBlue; } // Зона до стоп-линии
                        if (channelZones.zones[indexZone].type == 3) { color = Color.Blue; } // Зона после стоп-линии
                        if (channelZones.zones[indexZone].type == 4) { color = Color.Red; } // Зона проезда перекрестка на красный свет
                        if (channelZones.zones[indexZone].type == 10) { color = Color.Pink; } // Зона распознавания номеров


                        Point point1 = new Point(channelZones.zones[indexZone].x1, channelZones.zones[indexZone].y1);
                        Point point2 = new Point(channelZones.zones[indexZone].x2, channelZones.zones[indexZone].y2);
                        Point point3 = new Point(channelZones.zones[indexZone].x3, channelZones.zones[indexZone].y3);
                        Point point4 = new Point(channelZones.zones[indexZone].x4, channelZones.zones[indexZone].y4);
                        Point[] curvePoints = { point1, point2, point3, point4 };
                        pen = new Pen(color, 2);
                        SolidBrush brush = new SolidBrush(Color.FromArgb(50, color));
                        imageBoximg.FillPolygon(brush, curvePoints);
                        imageBoximg.DrawPolygon(pen, curvePoints);
                    }
                    imageBoximg.Dispose();
                    imageBox.Refresh();
                }
            }
        }

        void readXmlfile()
        {
            XmlDocument dataXmlFile = new XmlDocument();
            if (carsBox.Items.Count > 0)
            {
                imagesBox.Items.Clear();
                imagesCarcs.Clear();
                string[] data = (string[])cars[carsBox.SelectedItem.ToString()];

                //dataXmlFile.Load(screenshotDir + "\\" + data[1] + "data.xml");
                dataXmlFile.Load("C:\\vocord\\Vocord.Traffic Crossroads\\11394 09-41-04 @257068@\\Data.xml");

                foreach (string nameImages in timeCar)
                {
                    XmlNodeList nodeList = dataXmlFile.GetElementsByTagName(nameImages);
                    if (nodeList != null)
                    {
                        CarFilePoint imagesXml = new CarFilePoint();
                        foreach (XmlNode xnode in nodeList)
                        {
                            foreach (XmlNode vavueNode in xnode.ChildNodes)
                            {
                                if (vavueNode.Name == "Image") { imagesXml.file = vavueNode.InnerText; }
                                if (vavueNode.Name == "X") { imagesXml.x = Int16.Parse(vavueNode.InnerText); }
                                if (vavueNode.Name == "Y") { imagesXml.y = Int16.Parse(vavueNode.InnerText); }
                                if (vavueNode.Name == "Width") { imagesXml.width = Int16.Parse(vavueNode.InnerText); }
                                if (vavueNode.Name == "Height") { imagesXml.height = Int16.Parse(vavueNode.InnerText); }
                            }
                        }
                        if (imagesXml.file != "")
                        {
                            imagesBox.Items.Add(nameImages);
                            imagesCarcs.Add(nameImages, imagesXml);
                        }
                    }
                }
                if (imagesBox.Items.Count > 0)
                {
                    imagesBox.SelectedIndex = 0;
                    drawingPolygons();
                }
                else
                {

                }
            }
        }

        void search_Click(object sender, EventArgs e)
        {
            carsBox.Items.Clear();
            imagesBox.Items.Clear();
            cars.Clear();
            carsBox.Enabled = false;
            imagesBox.Enabled = false;
            carsBox.Text = string.Empty;
            imagesBox.Text = string.Empty;

            if (File.Exists(installDir + @"Database\vtvehicledb.sqlite"))
            {
                string sqlcar = $"SELECT CHECKTIME, CHANNEL_ID, SCREENSHOT FROM CARS WHERE GRNNUMBER LIKE \"{numberBox.Text.Replace('*', '_').ToUpper()}\"";

                using (var connection = new SQLiteConnection($@"URI=file:{installDir}Database\vtvehicledb.sqlite"))
                {
                    connection.Open();

                    SQLiteCommand command = new SQLiteCommand(sqlcar, connection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            string datetime;
                            while (reader.Read())
                            {
                                ChannelNameZone channelName = (ChannelNameZone)channel[reader.GetString(1)];
                                datetime = DateTime.FromFileTime(reader.GetInt64(0)).ToString() + " --- " + channelName.channelName;
                                carsBox.Items.Add(datetime);
                                string[] date = { reader.GetString(1), reader.GetString(2).Remove(reader.GetString(2).LastIndexOf("\\") + 1) };
                                cars.Add(datetime, date);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show($"There is no database file \n{installDir}Database\\vtvehicledb.sqlite \nor it is in a different folder.", "No database file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (carsBox.Items.Count > 0)
            {
                carsBox.SelectedIndex = 0;
                carsBox.Enabled = true;
                imagesBox.Enabled = true;
                readXmlfile();
            }
        }

        void dateAndTimeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            readXmlfile();
        }

        void imagesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawingPolygons();
        }

        private void save_Click(object sender, EventArgs e)
        {
            imageBox.Image.Save("test.jpg", ImageFormat.Jpeg);
        }

        private void saveAll_Click(object sender, EventArgs e)
        {

        }

        private void Ui_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == (char)Keys.Enter)
            { 
                //search_Click(search, null);
                search.PerformClick();  
            }
        }
    }
}
