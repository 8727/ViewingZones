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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ui));
            this.searchNumber = new System.Windows.Forms.GroupBox();
            this.search = new System.Windows.Forms.Button();
            this.numberBox = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchNumber.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchNumber
            // 
            this.searchNumber.Controls.Add(this.search);
            this.searchNumber.Controls.Add(this.numberBox);
            this.searchNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchNumber.Location = new System.Drawing.Point(10, 2);
            this.searchNumber.Name = "searchNumber";
            this.searchNumber.Size = new System.Drawing.Size(466, 79);
            this.searchNumber.TabIndex = 0;
            this.searchNumber.TabStop = false;
            this.searchNumber.Text = "Number";
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(332, 28);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(97, 37);
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
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(370, 221);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(656, 28);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 429);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1457, 808);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.searchNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ui";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.Ui_Load);
            this.searchNumber.ResumeLayout(false);
            this.searchNumber.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox numberBox;
        private System.Windows.Forms.GroupBox searchNumber;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}

