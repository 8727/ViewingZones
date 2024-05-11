namespace ViewingZones
{
    partial class Ui
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox images;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ui));
            this.imagesBox = new System.Windows.Forms.ComboBox();
            this.searchNumber = new System.Windows.Forms.GroupBox();
            this.search = new System.Windows.Forms.Button();
            this.numberBox = new System.Windows.Forms.TextBox();
            this.carsBox = new System.Windows.Forms.ComboBox();
            this.datetime = new System.Windows.Forms.GroupBox();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.save = new System.Windows.Forms.Button();
            this.saveAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            images = new System.Windows.Forms.GroupBox();
            images.SuspendLayout();
            this.searchNumber.SuspendLayout();
            this.datetime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // images
            // 
            images.Controls.Add(this.imagesBox);
            images.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            images.ForeColor = System.Drawing.Color.White;
            images.Location = new System.Drawing.Point(725, 2);
            images.Name = "images";
            images.Size = new System.Drawing.Size(344, 79);
            images.TabIndex = 4;
            images.TabStop = false;
            images.Text = "Images";
            // 
            // imagesBox
            // 
            this.imagesBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imagesBox.FormattingEnabled = true;
            this.imagesBox.Location = new System.Drawing.Point(14, 31);
            this.imagesBox.Name = "imagesBox";
            this.imagesBox.Size = new System.Drawing.Size(315, 32);
            this.imagesBox.TabIndex = 1;
            this.imagesBox.SelectedIndexChanged += new System.EventHandler(this.imagesBox_SelectedIndexChanged);
            // 
            // searchNumber
            // 
            this.searchNumber.Controls.Add(this.search);
            this.searchNumber.Controls.Add(this.numberBox);
            this.searchNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchNumber.ForeColor = System.Drawing.Color.White;
            this.searchNumber.Location = new System.Drawing.Point(10, 2);
            this.searchNumber.Name = "searchNumber";
            this.searchNumber.Size = new System.Drawing.Size(335, 79);
            this.searchNumber.TabIndex = 0;
            this.searchNumber.TabStop = false;
            this.searchNumber.Text = "Number";
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.ForeColor = System.Drawing.Color.Black;
            this.search.Location = new System.Drawing.Point(216, 20);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(113, 50);
            this.search.TabIndex = 1;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // numberBox
            // 
            this.numberBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberBox.Location = new System.Drawing.Point(15, 28);
            this.numberBox.Name = "numberBox";
            this.numberBox.Size = new System.Drawing.Size(195, 38);
            this.numberBox.TabIndex = 0;
            this.numberBox.Text = "В300ЕА19";
            this.numberBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // carsBox
            // 
            this.carsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carsBox.FormattingEnabled = true;
            this.carsBox.Location = new System.Drawing.Point(14, 31);
            this.carsBox.Name = "carsBox";
            this.carsBox.Size = new System.Drawing.Size(315, 32);
            this.carsBox.TabIndex = 1;
            this.carsBox.SelectedIndexChanged += new System.EventHandler(this.dateAndTimeBox_SelectedIndexChanged);
            // 
            // datetime
            // 
            this.datetime.Controls.Add(this.carsBox);
            this.datetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.datetime.ForeColor = System.Drawing.Color.White;
            this.datetime.Location = new System.Drawing.Point(363, 2);
            this.datetime.Name = "datetime";
            this.datetime.Size = new System.Drawing.Size(344, 79);
            this.datetime.TabIndex = 3;
            this.datetime.TabStop = false;
            this.datetime.Text = "Date and Time";
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.imageBox.Image = ((System.Drawing.Image)(resources.GetObject("imageBox.Image")));
            this.imageBox.InitialImage = null;
            this.imageBox.Location = new System.Drawing.Point(10, 87);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(1280, 720);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 5;
            this.imageBox.TabStop = false;
            // 
            // save
            // 
            this.save.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.save.ForeColor = System.Drawing.Color.Black;
            this.save.Location = new System.Drawing.Point(1084, 12);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(95, 69);
            this.save.TabIndex = 6;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // saveAll
            // 
            this.saveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveAll.ForeColor = System.Drawing.Color.Black;
            this.saveAll.Location = new System.Drawing.Point(1195, 11);
            this.saveAll.Name = "saveAll";
            this.saveAll.Size = new System.Drawing.Size(95, 69);
            this.saveAll.TabIndex = 7;
            this.saveAll.Text = "Save All";
            this.saveAll.UseVisualStyleBackColor = true;
            this.saveAll.Click += new System.EventHandler(this.saveAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // Ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1299, 815);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveAll);
            this.Controls.Add(this.save);
            this.Controls.Add(images);
            this.Controls.Add(this.datetime);
            this.Controls.Add(this.searchNumber);
            this.Controls.Add(this.imageBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Ui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewingZones";
            this.Load += new System.EventHandler(this.Ui_Load);
            images.ResumeLayout(false);
            this.searchNumber.ResumeLayout(false);
            this.searchNumber.PerformLayout();
            this.datetime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox numberBox;
        private System.Windows.Forms.GroupBox searchNumber;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.ComboBox carsBox;
        private System.Windows.Forms.GroupBox datetime;
        private System.Windows.Forms.ComboBox imagesBox;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button saveAll;
        private System.Windows.Forms.Label label1;
    }
}

