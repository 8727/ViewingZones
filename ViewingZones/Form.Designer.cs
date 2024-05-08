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
            this.searchNumber = new System.Windows.Forms.GroupBox();
            this.search = new System.Windows.Forms.Button();
            this.numberBox = new System.Windows.Forms.TextBox();
            this.timeAndDateBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeAndDate = new System.Windows.Forms.GroupBox();
            this.imagesBox = new System.Windows.Forms.ComboBox();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            images = new System.Windows.Forms.GroupBox();
            this.searchNumber.SuspendLayout();
            this.timeAndDate.SuspendLayout();
            images.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
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
            this.search.ForeColor = System.Drawing.Color.Black;
            this.search.Location = new System.Drawing.Point(230, 28);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(87, 37);
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
            this.numberBox.Text = "А001АА177";
            this.numberBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timeAndDateBox
            // 
            this.timeAndDateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeAndDateBox.FormattingEnabled = true;
            this.timeAndDateBox.Location = new System.Drawing.Point(14, 31);
            this.timeAndDateBox.Name = "timeAndDateBox";
            this.timeAndDateBox.Size = new System.Drawing.Size(315, 28);
            this.timeAndDateBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // timeAndDate
            // 
            this.timeAndDate.Controls.Add(this.timeAndDateBox);
            this.timeAndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeAndDate.ForeColor = System.Drawing.Color.White;
            this.timeAndDate.Location = new System.Drawing.Point(363, 2);
            this.timeAndDate.Name = "timeAndDate";
            this.timeAndDate.Size = new System.Drawing.Size(344, 79);
            this.timeAndDate.TabIndex = 3;
            this.timeAndDate.TabStop = false;
            this.timeAndDate.Text = "Time and Date";
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
            this.imagesBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imagesBox.FormattingEnabled = true;
            this.imagesBox.Location = new System.Drawing.Point(14, 31);
            this.imagesBox.Name = "imagesBox";
            this.imagesBox.Size = new System.Drawing.Size(315, 28);
            this.imagesBox.TabIndex = 1;
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.imageBox.Location = new System.Drawing.Point(10, 87);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(1280, 720);
            this.imageBox.TabIndex = 5;
            this.imageBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(1084, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 69);
            this.button1.TabIndex = 6;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(1195, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 69);
            this.button2.TabIndex = 7;
            this.button2.Text = "Save All";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1299, 815);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(images);
            this.Controls.Add(this.timeAndDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form";
            this.Load += new System.EventHandler(this.Ui_Load);
            this.searchNumber.ResumeLayout(false);
            this.searchNumber.PerformLayout();
            this.timeAndDate.ResumeLayout(false);
            images.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox numberBox;
        private System.Windows.Forms.GroupBox searchNumber;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.ComboBox timeAndDateBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox timeAndDate;
        private System.Windows.Forms.ComboBox imagesBox;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

