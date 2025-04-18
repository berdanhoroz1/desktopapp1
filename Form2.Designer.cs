namespace desktopapp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOpenExcel = new Button();
            label1 = new Label();
            dataGridView2 = new DataGridView();
            label5 = new Label();
            label6 = new Label();
            btnSaveToDatabase = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // btnOpenExcel
            // 
            btnOpenExcel.Location = new Point(699, 543);
            btnOpenExcel.Name = "btnOpenExcel";
            btnOpenExcel.Size = new Size(221, 52);
            btnOpenExcel.TabIndex = 1;
            btnOpenExcel.Text = "Excel Dosyası Seçin";
            btnOpenExcel.UseVisualStyleBackColor = true;
            btnOpenExcel.Click += btnOpenExcel_Click_1;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(610, 27);
            label1.Name = "label1";
            label1.Size = new Size(424, 36);
            label1.TabIndex = 2;
            label1.Text = "Yalnızca xlsx uzantılı dosya seçiniz";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(366, 66);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(894, 440);
            dataGridView2.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1373, 95);
            label5.Name = "label5";
            label5.Size = new Size(0, 20);
            label5.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1373, 209);
            label6.Name = "label6";
            label6.Size = new Size(0, 20);
            label6.TabIndex = 5;
            // 
            // btnSaveToDatabase
            // 
            btnSaveToDatabase.Location = new Point(699, 611);
            btnSaveToDatabase.Name = "btnSaveToDatabase";
            btnSaveToDatabase.Size = new Size(221, 55);
            btnSaveToDatabase.TabIndex = 6;
            btnSaveToDatabase.Text = "Veritabanına Kaydet";
            btnSaveToDatabase.UseVisualStyleBackColor = true;
            btnSaveToDatabase.Click += btnSaveToDatabase_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1575, 783);
            Controls.Add(btnSaveToDatabase);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(dataGridView2);
            Controls.Add(label1);
            Controls.Add(btnOpenExcel);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnOpenExcel;
        private Label label1;
        private DataGridView dataGridView2;
        private Label label5;
        private Label label6;
        private Button btnSaveToDatabase;
    }
}