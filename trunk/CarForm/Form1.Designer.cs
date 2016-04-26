namespace CarForm
{
    partial class Form1
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
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Table1 = new System.Windows.Forms.DataGridView();
            this.Manufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Power = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Trancemission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Table1)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(12, 12);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(122, 12);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Table1
            // 
            this.Table1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Table1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Table1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Table1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Table1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Manufacturer,
            this.Model,
            this.Dat,
            this.Volume,
            this.Power,
            this.Trancemission});
            this.Table1.Location = new System.Drawing.Point(0, 41);
            this.Table1.Name = "Table1";
            this.Table1.Size = new System.Drawing.Size(484, 345);
            this.Table1.TabIndex = 3;
            this.Table1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Table1_CellContentClick);
            // 
            // Manufacturer
            // 
            this.Manufacturer.HeaderText = "Manufacturer";
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Manufacturer.Width = 95;
            // 
            // Model
            // 
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.Width = 61;
            // 
            // Dat
            // 
            this.Dat.HeaderText = "Date";
            this.Dat.Name = "Dat";
            this.Dat.Width = 55;
            // 
            // Volume
            // 
            this.Volume.HeaderText = "Volume";
            this.Volume.Name = "Volume";
            this.Volume.Width = 67;
            // 
            // Power
            // 
            this.Power.HeaderText = "Power";
            this.Power.Name = "Power";
            this.Power.Width = 62;
            // 
            // Trancemission
            // 
            this.Trancemission.HeaderText = "Trancemission";
            this.Trancemission.Name = "Trancemission";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 386);
            this.Controls.Add(this.Table1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LoadButton);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "CarShop";
            ((System.ComponentModel.ISupportInitialize)(this.Table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView Table1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Manufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn Power;
        private System.Windows.Forms.DataGridViewTextBoxColumn Trancemission;
    }
}

