﻿namespace desktopapp1
{
    partial class Form3
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
            txtUserName = new TextBox();
            dtStart = new DateTimePicker();
            dtEnd = new DateTimePicker();
            btnFilter = new Button();
            btnGetAll = new Button();
            btnDelete = new Button();
            dataGridView1 = new DataGridView();
            btnExportToExcel = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(478, 36);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(209, 27);
            txtUserName.TabIndex = 0;
            // 
            // dtStart
            // 
            dtStart.Location = new Point(829, 36);
            dtStart.Name = "dtStart";
            dtStart.Size = new Size(250, 27);
            dtStart.TabIndex = 1;
            // 
            // dtEnd
            // 
            dtEnd.Location = new Point(1162, 36);
            dtEnd.Name = "dtEnd";
            dtEnd.Size = new Size(250, 27);
            dtEnd.TabIndex = 2;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(668, 129);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(94, 29);
            btnFilter.TabIndex = 3;
            btnFilter.Text = "Filtrele";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // btnGetAll
            // 
            btnGetAll.Location = new Point(810, 129);
            btnGetAll.Name = "btnGetAll";
            btnGetAll.Size = new Size(118, 29);
            btnGetAll.TabIndex = 4;
            btnGetAll.Text = "Hepsini Yazdır";
            btnGetAll.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(958, 129);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Sil";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(427, 164);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1189, 544);
            dataGridView1.TabIndex = 6;
            // 
            // btnExportToExcel
            // 
            btnExportToExcel.Location = new Point(1099, 129);
            btnExportToExcel.Name = "btnExportToExcel";
            btnExportToExcel.Size = new Size(123, 29);
            btnExportToExcel.TabIndex = 7;
            btnExportToExcel.Text = "Excel Çıktısı Al";
            btnExportToExcel.UseVisualStyleBackColor = true;
            btnExportToExcel.Click += btnExportToExcel_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1731, 862);
            Controls.Add(btnExportToExcel);
            Controls.Add(dataGridView1);
            Controls.Add(btnDelete);
            Controls.Add(btnGetAll);
            Controls.Add(btnFilter);
            Controls.Add(dtEnd);
            Controls.Add(dtStart);
            Controls.Add(txtUserName);
            Name = "Form3";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUserName;
        private DateTimePicker dtStart;
        private DateTimePicker dtEnd;
        private Button btnFilter;
        private Button btnGetAll;
        private Button btnDelete;
        private DataGridView dataGridView1;
        private Button btnExportToExcel;
    }
}