using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OfficeOpenXml;

namespace desktopapp1
{
    public partial class Form2 : Form
    {
        private DataTable dataTable;
        private DateTime firstStartTime;
        private DateTime lastEndTime;

        public Form2()
        {
            InitializeComponent();
            this.Text = "Excel Tarama";
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            InitializeDataTable();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
        }

        private void btnOpenExcel_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                ReadExcelFile(filePath);
            }
        }

        private void InitializeDataTable()
        {
            dataTable = new DataTable();

            dataTable.Columns.Add("Satır", typeof(int));
            dataTable.Columns.Add("Başlangıç", typeof(string));
            dataTable.Columns.Add("Bitiş", typeof(string));
            dataTable.Columns.Add("Süre", typeof(string));
            dataTable.Columns.Add("Çakışma", typeof(bool)); 
        }

        private void InitializeDataGridView()
        {
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.DataSource = dataTable;


        }

        private void ReadExcelFile(string filePath)
        {
            try
            {
                using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int row = 3;

                    DateTime previousEndTime = DateTime.MinValue;
                    StringBuilder conflictMessages = new StringBuilder();

                    dataTable.Clear();

                    firstStartTime = DateTime.MaxValue;
                    lastEndTime = DateTime.MinValue;

                    

                    while (worksheet.Cells[row, 6].Value != null && worksheet.Cells[row, 6].Value.ToString() != "")
                    {
                        string durationText = worksheet.Cells[row, 5].Value.ToString().Trim();
                        TimeSpan duration = ParseDuration(durationText);

                        string startTimeText = worksheet.Cells[row, 6].Value.ToString().Trim();
                        DateTime startTime = ParseStartTime(startTimeText);

                        DateTime endTime = startTime.Add(duration);
                        bool isConflict = false;

                        if (previousEndTime > DateTime.MinValue && startTime < previousEndTime)
                        {
                            conflictMessages.AppendLine($"Çakışma! Satır {row} - Başlangıç zamanı ({startTime:yyyy-MM-dd HH:mm}) önceki görevin bitişinden ({previousEndTime:yyyy-MM-dd HH:mm}) önce.");
                            isConflict = true;
                        }

                        previousEndTime = endTime;

                        if (startTime < firstStartTime)
                            firstStartTime = startTime;

                        if (endTime > lastEndTime)
                            lastEndTime = endTime;

                        
                        DataRow newRow = dataTable.NewRow();
                        newRow["Satır"] = row;
                        newRow["Başlangıç"] = startTime.ToString("yyyy-MM-dd HH:mm");
                        newRow["Bitiş"] = endTime.ToString("yyyy-MM-dd HH:mm");
                        newRow["Süre"] = durationText;
                        newRow["Çakışma"] = isConflict; 

                        dataTable.Rows.Add(newRow);
                        row++;
                    }

                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = dataTable;
                    dataGridView2.Refresh();

                    if (conflictMessages.Length > 0)
                        MessageBox.Show(conflictMessages.ToString());
                    else
                        MessageBox.Show("Çakışma yoktur.");

                    label5.Text = $"İlk Başlangıç Zamanı: \n{firstStartTime:yyyy-MM-dd HH:mm}";
                    label6.Text = $"Son Bitiş Zamanı: \n{lastEndTime:yyyy-MM-dd HH:mm}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        private TimeSpan ParseDuration(string durationText)
        {
            var regex = new Regex(@"(?:(\d+)\s*h)?\s*(?:(\d+)\s*m)?");
            var match = regex.Match(durationText.Trim().ToLower());

            if (match.Success)
            {
                int hours = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 0;
                int minutes = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
                return new TimeSpan(hours, minutes, 0);
            }
            else
            {
                throw new FormatException("Bilinmeyen süre formatı.");
            }
        }

        private DateTime ParseStartTime(string dateTimeStr)
        {
            try
            {
                string[] dateAndTime = dateTimeStr.Split(new string[] { " at " }, StringSplitOptions.None);

                if (dateAndTime.Length != 2)
                    throw new FormatException("Beklenmeyen tarih ve saat formatı.");

                string[] dateParts = dateAndTime[0].Split('/');
                if (dateParts.Length != 3)
                    throw new FormatException("Tarih formatı hatalı.");

                int day = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int year = int.Parse(dateParts[2]);

                if (year < 50)
                    year += 2000;
                else
                    year += 1900;

                string[] timeParts = dateAndTime[1].Split(':');
                if (timeParts.Length != 2)
                    throw new FormatException("Saat formatı hatalı.");

                int hour = int.Parse(timeParts[0]);
                int minute = int.Parse(timeParts[1]);

                return new DateTime(year, month, day, hour, minute, 0);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Tarih veya saat dönüştürme hatası: {ex.Message}");
            }
        }

        
        
    }
}
