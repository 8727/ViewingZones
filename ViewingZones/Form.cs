using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Data.SQLite;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ViewingZones
{

    public partial class Ui : Form
    {
        public Ui()
        {
            InitializeComponent();
        }

        class CarFilePoint
        {
            public string file;
            public Int16 x;
            public Int16 y;
            public Int16 width;
            public Int16 height;
        }

        class CarTrackPoint
        {
            public Int16 x;
            public Int16 y;
            //public Int16 width;
            //public Int16 height;
        }

        class Carfile
        {
            public string channelId;
            public string patchfile;
            public CarTrackPoint[] point { get; set; } = new CarTrackPoint[200];
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
        Hashtable imagesCar = new Hashtable(); // найденые картинки проезда
        Hashtable imageNames = new Hashtable(); // имена найденых картинок проезда

        CommonOpenFileDialog dialog = new CommonOpenFileDialog();

        string installDir = @"C:\Vocord\Vocord.Traffic Crossroads\";
        string screenshotDir = @"E:\Screenshots";

        void HashImagesNames()
        {
            imageNames.Add("VideoDetection/ObjectBeginImage", "Video Begin"); // первая фиксация
            imageNames.Add("VideoDetection/ObjectImage", "Video Best"); // первая фиксация
            imageNames.Add("VideoDetection/ObjectEndImage", "Video End");     // последняя фиксация

            imageNames.Add("WrongWay/Episodes/Episode/DetectionBeginning", "Wrong Way Begin");
            imageNames.Add("WrongWay/Episodes/Episode/DetectionEnd", "Wrong Way End");

            imageNames.Add("VideoDetection/ObjectStopLineImage", "Stop Line");
            imageNames.Add("RedLightBeforeLine/Episodes/Episode/DetectionBeginning", "Red Light Before Line");
            imageNames.Add("RedLightAfterLine/Episodes/Episode/DetectionBeginning", "Red Light After Line Begin");
            imageNames.Add("RedLightAfterLine/Episodes/Episode/DetectionEnd", "Red Light After Line End");

            imageNames.Add("RedLightBeforeLineLeft/Episodes/Episode/DetectionBeginning", "Red Light Before Line Left Begin");
            imageNames.Add("RedLightBeforeLineRight/Episodes/Episode/DetectionBeginning", "Red Light Before Line Right Begin");

            imageNames.Add("RedLightCross/Episodes/Episode/DetectionBeginning", "Red Light Cross Begin");
            imageNames.Add("RedLightCross/Episodes/Episode/DetectionEnd", "Red Light Cross End");

            imageNames.Add("WrongCross/Episodes/Episode/DetectionBeginning", "Wrong Cross Begin");
            imageNames.Add("WrongCross/Episodes/Episode/DetectionEnd", "Wrong Cross End");

            imageNames.Add("BeforeZebraWithPedestrian/Episodes/Episode/RedLightState", "Zebra Begin");
            imageNames.Add("AfterZebraWithPedestrian/Episodes/Episode/DetectionEnd", "Zebra End");
        }

        string NameCreation(string name)
        {
            Regex regex = new Regex(@"-\d{1}$");
            if (regex.IsMatch(name))
            {
                int number = (int.Parse(name.Substring(name.IndexOf("-") + 1))) + 1;
                name = name.Remove(name.LastIndexOf("-") + 1) + number.ToString("0");
            }
            else
            {
                name = name + "-2";
            }

            if (imagesCar.ContainsKey(name))
            {
                name = NameCreation(name);
            }
            return name;
        }

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
            HashImagesNames();
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
                Carfile carfile = (Carfile)cars[carsBox.SelectedItem.ToString()];
                CarFilePoint imgCar = (CarFilePoint)imagesCar[imagesBox.SelectedItem.ToString()];

                if (imgCar.file != "")
                {
                    if (File.Exists(screenshotDir + "\\" + carfile.patchfile + imgCar.file)) {
                        imageBox.Image = Image.FromFile(screenshotDir + "\\" + carfile.patchfile + imgCar.file);

                        Graphics imageBoximg = Graphics.FromImage(imageBox.Image);
                        Pen pen = new Pen(Color.Red, 5);
                        imageBoximg.DrawRectangle(pen, (imgCar.x - imgCar.width / 2), (imgCar.y - imgCar.height / 2), imgCar.width, imgCar.height);
                        imageBoximg.DrawRectangle(pen, imgCar.x, imgCar.y, 5, 5);

                        for (Int16 indexPoints = 0; indexPoints < carfile.point.Length; indexPoints++)
                        {
                            if (carfile.point[indexPoints] != null)
                            {
                                if (carfile.point[indexPoints].x != 0 & carfile.point[indexPoints].y != 0)
                                { 
                                    imageBoximg.DrawEllipse(pen, carfile.point[indexPoints].x - 10, carfile.point[indexPoints].y - 10, 20, 20);
                                    if (indexPoints > 0)
                                    {
                                        if (carfile.point[indexPoints - 1].x != 0 & carfile.point[indexPoints - 1].y != 0)
                                        {
                                            imageBoximg.DrawLine(pen, carfile.point[indexPoints - 1].x, carfile.point[indexPoints - 1].y, carfile.point[indexPoints].x, carfile.point[indexPoints].y);
                                        }
                                    }
                                }
                            }
                        }

                        var color = Color.LightGray;

                        ChannelNameZone channelZones = (ChannelNameZone)channel[carfile.channelId];

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
                    else
                    {
                        MessageBox.Show($"In Data.xml \n {screenshotDir + "\\" + carfile.patchfile + imgCar.file} \n\nthere is a link to the file, but there is no file itself.", "No file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        void readXmlfile()
        {
            XmlDocument dataXmlFile = new XmlDocument();
            if (carsBox.Items.Count > 0)
            {
                imagesBox.Items.Clear();
                imagesCar.Clear();

                Carfile carfile = (Carfile)cars[carsBox.SelectedItem.ToString()];

                if (File.Exists(screenshotDir + "\\" + carfile.patchfile + "Data.xml"))
                {
                    dataXmlFile.Load(screenshotDir + "\\" + carfile.patchfile + "Data.xml");

                    ICollection keys = imageNames.Keys;
                    foreach (String nameImages in keys)
                    {
                        XmlNodeList nodeList = dataXmlFile.SelectNodes($"//{nameImages}");
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

                                if (imagesXml.file != "" || imagesXml.file == null)
                                {
                                    string names = (string)imageNames[nameImages];
                                    if (imagesCar.ContainsKey(names))
                                    {
                                        names = NameCreation(names);
                                    }
                                    imagesBox.Items.Add(names);
                                    imagesCar.Add(names, imagesXml);
                                }
                            }
                        }
                    }

                    XmlNodeList statusPoints = dataXmlFile.GetElementsByTagName($"Interpolated");
                    if (statusPoints.Count > 0)
                    {
                        string xmlPoint = null;
                        if (statusPoints[0].InnerText == "true" | statusPoints[0].InnerText == "True")
                        {
                            xmlPoint = $"//TrackPoints/TrackPoint/NumberArea";
                        }
                        else
                        {
                            xmlPoint = $"//TrackPoints/TrackPoint/RecognitionNumber";
                        }

                        XmlNodeList nodePoints = dataXmlFile.SelectNodes(xmlPoint);
                        if (nodePoints != null)
                        {
                            int indexPoint = 0;
                            foreach (XmlNode xnode in nodePoints)
                            {
                                CarTrackPoint trackPoint = new CarTrackPoint();
                                foreach (XmlNode vavueNode in xnode.ChildNodes)
                                {
                                    if (vavueNode.Name == "X") { trackPoint.x = Int16.Parse(vavueNode.InnerText); }
                                    if (vavueNode.Name == "Y") { trackPoint.y = Int16.Parse(vavueNode.InnerText); }
                                    //if (vavueNode.Name == "Width") { trackPoint.width = Int16.Parse(vavueNode.InnerText); }
                                    //if (vavueNode.Name == "Height") { trackPoint.height = Int16.Parse(vavueNode.InnerText); }
                                }
                                carfile.point[indexPoint++] = trackPoint;
                            }
                        }
                    }
                    if (imagesBox.Items.Count > 0)
                    {
                        imagesBox.SelectedIndex = 0;
                        save.Enabled = true;
                        saveAll.Enabled = true;
                        drawingPolygons();
                    }
                }
                else
                {
                    MessageBox.Show($"There is a record with number {numberBox.Text} in the database, but there is no Data.xml file on the path \n{screenshotDir + "\\" + carfile.patchfile} Data.xml", "No file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            save.Enabled = false;
            saveAll.Enabled = false;

            if (File.Exists(installDir + @"Database\vtvehicledb.sqlite"))
            {
                string sqlcar = $"SELECT CHECKTIME, CHANNEL_ID, SCREENSHOT FROM CARS WHERE FULLGRNNUMBER LIKE \"{numberBox.Text.Replace('*', '_').ToUpper()}\"";

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
                                Carfile carfile = new Carfile();
                                ChannelNameZone channelName = (ChannelNameZone)channel[reader.GetString(1)];
                                datetime = DateTime.FromFileTime(reader.GetInt64(0)).ToString() + " - " + channelName.channelName;
                                carfile.channelId = reader.GetString(1);
                                carfile.patchfile = reader.GetString(2).Remove(reader.GetString(2).LastIndexOf("\\") + 1);
                                carsBox.Items.Add(datetime);
                                cars.Add(datetime, carfile);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"There are no driveways with number {numberBox.Text} in the database.", "Number not found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            dialog.InitialDirectory = Application.StartupPath.ToString();
            dialog.AllowNonFileSystemItems = false;
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                imageBox.Image.Save(dialog.FileName + "\\" + carsBox.SelectedItem.ToString().Replace(':', '.') + " - " + numberBox.Text.Replace('*', '.') + " - " + imagesBox.SelectedItem.ToString() + ".jpg", ImageFormat.Jpeg);
            }

        }

        private void saveAll_Click(object sender, EventArgs e)
        {
            dialog.InitialDirectory = Application.StartupPath.ToString();
            dialog.AllowNonFileSystemItems = false;
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string fileName = carsBox.SelectedItem.ToString().Replace(':', '.') + " - " + numberBox.Text.Replace('*', '.') + " - ";
                Carfile carfile = (Carfile)cars[carsBox.SelectedItem.ToString()];
                ICollection keys = imagesCar.Keys;
                foreach (String key in keys)
                {
                    CarFilePoint imgCar = (CarFilePoint)imagesCar[key];
                    if (imgCar.file != "")
                    {
                        imageBox.Image = Image.FromFile(screenshotDir + "\\" + carfile.patchfile + imgCar.file);

                        Graphics imageBoximg = Graphics.FromImage(imageBox.Image);
                        Pen pen = new Pen(Color.Red, 5);
                        imageBoximg.DrawRectangle(pen, (imgCar.x - imgCar.width / 2), (imgCar.y - imgCar.height / 2), imgCar.width, imgCar.height);
                        imageBoximg.DrawEllipse(pen, imgCar.x, imgCar.y, 5, 5);

                        for (Int16 indexPoints = 0; indexPoints < carfile.point.Length; indexPoints++)
                        {
                            if (carfile.point[indexPoints] != null)
                            {
                                if (carfile.point[indexPoints].x != 0 & carfile.point[indexPoints].y != 0)
                                {
                                    imageBoximg.DrawEllipse(pen, carfile.point[indexPoints].x - 10, carfile.point[indexPoints].y - 10, 20, 20);
                                    if (indexPoints > 0)
                                    {
                                        if (carfile.point[indexPoints - 1].x != 0 & carfile.point[indexPoints - 1].y != 0)
                                        {
                                            imageBoximg.DrawLine(pen, carfile.point[indexPoints - 1].x, carfile.point[indexPoints - 1].y, carfile.point[indexPoints].x, carfile.point[indexPoints].y);
                                        }
                                    }
                                }
                            }
                        }

                        var color = Color.LightGray;

                        ChannelNameZone channelZones = (ChannelNameZone)channel[carfile.channelId];

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
                    imageBox.Image.Save(dialog.FileName + "\\" + fileName + key + ".jpg", ImageFormat.Jpeg);
                }
            }
        }

        private void Ui_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == (char)Keys.Enter)
            { 
                //search_Click(search, null);
                search.PerformClick();  
            }
        }

        private void imageBox_Click(object sender, EventArgs e)
        {
            if (carsBox.Items.Count > 0)
            {
                Carfile carfile = (Carfile)cars[carsBox.SelectedItem.ToString()];
                Process.Start("explorer.exe", screenshotDir + "\\" + carfile.patchfile);
            }
        }
    }
    //Releases v0.3
}
