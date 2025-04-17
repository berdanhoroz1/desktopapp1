using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
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
            dataTable.Columns.Add("Kişi", typeof(string));
            dataTable.Columns.Add("İş Detayı", typeof(string));
            dataTable.Columns.Add("Kod", typeof(string));
            dataTable.Columns.Add("Başlangıç", typeof(DateTime));
            dataTable.Columns.Add("Bitiş", typeof(DateTime));
            dataTable.Columns.Add("Süre", typeof(string));
            dataTable.Columns.Add("Mola", typeof(string));
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

                    Dictionary<string, DateTime> lastEndTimePerPerson = new Dictionary<string, DateTime>(); // kişi-bitiş zamanı

                    while (worksheet.Cells[row, 6].Value != null && worksheet.Cells[row, 6].Value.ToString() != "")
                    {
                        string durationText = worksheet.Cells[row, 5].Value.ToString().Trim();
                        TimeSpan duration = ParseDuration(durationText);

                        string startTimeText = worksheet.Cells[row, 6].Value.ToString().Trim();
                        DateTime startTime = ParseStartTime(startTimeText);
                        DateTime endTime = startTime.Add(duration);

                        string person = worksheet.Cells[row, 2].Value.ToString().Trim();
                        string worklog = worksheet.Cells[row, 3].Value.ToString().Trim();
                        string key = worksheet.Cells[row, 4].Value.ToString().Trim();
                        string updatedKey = "https://horozlojistik.atlassian.net/browse/" + key;

                        bool isConflict = false;

                        if (previousEndTime > DateTime.MinValue && startTime < previousEndTime)
                        {
                            conflictMessages.AppendLine($"Çakışma! Satır {row} - Başlangıç zamanı ({startTime:yyyy-MM-ddTHH:mm:ss}) önceki görevin bitişinden ({previousEndTime:yyyy-MM-ddTHH:mm:ss}) önce.");
                            isConflict = true;
                        }

                        previousEndTime = endTime;

                        if (startTime < firstStartTime)
                            firstStartTime = startTime;

                        if (endTime > lastEndTime)
                            lastEndTime = endTime;

                        
                        TimeSpan breakDuration = TimeSpan.Zero;
                        if (lastEndTimePerPerson.ContainsKey(person))
                        {
                            DateTime lastPersonEnd = lastEndTimePerPerson[person];
                            if (startTime > lastPersonEnd)
                            {
                                breakDuration = startTime - lastPersonEnd;
                            }
                        }

                        lastEndTimePerPerson[person] = endTime;

                        DataRow newRow = dataTable.NewRow();
                        newRow["Satır"] = row;
                        newRow["Kişi"] = person;
                        newRow["İş Detayı"] = worklog;
                        newRow["Kod"] = updatedKey;
                        newRow["Başlangıç"] = startTime;
                        newRow["Bitiş"] = endTime;
                        newRow["Süre"] = durationText;
                        newRow["Mola"] = breakDuration.TotalMinutes > 0
                            ? $"{(int)breakDuration.TotalHours}h {(int)breakDuration.Minutes}m"
                            : "0m";

                        dataTable.Rows.Add(newRow);
                        newRow["Çakışma"] = isConflict;
                        
                        row++;
                    }

                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = dataTable;
                    dataGridView2.Refresh();

                    if (conflictMessages.Length > 0)
                        MessageBox.Show(conflictMessages.ToString());
                    else
                        MessageBox.Show("Çakışma yoktur.");

                    label5.Text = $"İlk Başlangıç Zamanı: \n{firstStartTime:yyyy-MM-ddTHH:mm:ss}";
                    label6.Text = $"Son Bitiş Zamanı: \n{lastEndTime:yyyy-MM-ddTHH:mm:ss}";
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



        private async void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                bool allSuccess = true; 

                foreach (DataRow row in dataTable.Rows)
                {
                    var data = new Dictionary<string, object>();

                    data["userName"] = row["Kişi"].ToString();
                    data["worklog"] = row["İş Detayı"].ToString();
                    data["key"] = row["Kod"].ToString();
                    data["logged"] = row["Süre"].ToString();
                    data["break"] = row["Mola"].ToString();

                    
                    data["startDate"] = (DateTime)row["Başlangıç"];
                    data["endDate"] = (DateTime)row["Bitiş"];

                    data["conflict"] = (bool)row["Çakışma"];


                    var success = await SendDataToApi(data);

                    if (!success)
                    {
                        allSuccess = false; 
                        MessageBox.Show("Veritabanına kaydetme işlemi başarısız oldu.");
                        break; 
                    }
                }

                
                if (allSuccess)
                {
                    MessageBox.Show("Veriler başarıyla veritabanına kaydedildi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }





        private async Task<bool> SendDataToApi(object data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    
                    string apiUrl = "http://localhost:5085/api/Work/CreateWork";

                    
                    string json = JsonConvert.SerializeObject(data);

                    

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    
                    var response = await client.PostAsync(apiUrl, content);

                    
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


    }
}