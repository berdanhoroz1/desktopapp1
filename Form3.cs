using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using desktopapp1.Models;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;


namespace desktopapp1
{
    public partial class Form3 : Form
    {
        private List<WorkModel> workList = new List<WorkModel>();

        public Form3()
        {
            InitializeComponent();

            btnFilter.Click += btnFilter_Click;
            btnGetAll.Click += btnGetAll_Click;
            btnDelete.Click += btnDelete_Click;
        }

        private async void btnGetAll_Click(object sender, EventArgs e)
        {
            await LoadAllWorks();
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            await LoadFilteredWorks();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int selectedId = (int)selectedRow.Cells["id"].Value;

                var confirm = MessageBox.Show("Seçili kaydı silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    bool result = await DeleteWork(selectedId);
                    if (result)
                    {
                        MessageBox.Show("Kayıt silindi.");
                        await LoadAllWorks();
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi başarısız.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.");
            }
        }


        private async Task LoadAllWorks()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:5085/api/Work/GetAllWorks");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    workList = JsonConvert.DeserializeObject<List<WorkModel>>(json);
                    dataGridView1.DataSource = workList;

                    ChangeColumnHeaders();
                }
            }
        }

        private async Task LoadFilteredWorks()
        {
            using (var client = new HttpClient())
            {

                var queryParams = new List<string>();

                if (!string.IsNullOrEmpty(txtUserName.Text))
                    queryParams.Add($"UserName={Uri.EscapeDataString(txtUserName.Text)}");

                if (dtStart.Checked)
                    queryParams.Add($"StartDate={dtStart.Value.ToString("yyyy-MM-dd")}");

                if (dtEnd.Checked)
                    queryParams.Add($"EndDate={dtEnd.Value.ToString("yyyy-MM-dd")}");

                string queryString = string.Join("&", queryParams);
                string url = "http://localhost:5085/api/Work/GetFilteredWorks";

                if (!string.IsNullOrEmpty(queryString))
                    url += "?" + queryString;

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    workList = JsonConvert.DeserializeObject<List<WorkModel>>(result);
                    dataGridView1.DataSource = workList;
                }
                else
                {
                    MessageBox.Show("Filtreleme isteği başarısız.");
                }
            }
        }


        private async Task<bool> DeleteWork(int id)
        {
            using (var client = new HttpClient())
            {

                var response = await client.DeleteAsync($"http://localhost:5085/api/Work/DeleteWork/{id}");

                return response.IsSuccessStatusCode;
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Aktarılacak veri yok.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.Title = "Excel Dosyasını Kaydet";
            saveFileDialog.FileName = "WorkListesi.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Works");

                    // Kolon başlıkları
                    for (int col = 0; col < dataGridView1.Columns.Count; col++)
                    {
                        worksheet.Cell(1, col + 1).Value = dataGridView1.Columns[col].HeaderText;
                    }

                    // Satır verileri
                    for (int row = 0; row < dataGridView1.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataGridView1.Columns.Count; col++)
                        {
                            var value = dataGridView1.Rows[row].Cells[col].Value;
                            worksheet.Cell(row + 2, col + 1).Value = value?.ToString();
                        }
                    }

                    try
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Excel dosyası başarıyla kaydedildi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }

        private void ChangeColumnHeaders()
        {
            
            dataGridView1.Columns["id"].HeaderText = "id";
            dataGridView1.Columns["userName"].HeaderText = "Kişi";
            dataGridView1.Columns["worklog"].HeaderText = "İş Detayı";
            dataGridView1.Columns["key"].HeaderText = "Kod";
            dataGridView1.Columns["startDate"].HeaderText = "Başlangıç Tarihi";
            dataGridView1.Columns["endDate"].HeaderText = "Bitiş Tarihi";
            dataGridView1.Columns["logged"].HeaderText = "Süre";
            dataGridView1.Columns["break"].HeaderText = "Mola";
            dataGridView1.Columns["conflict"].HeaderText = "Çakışma";

        }



    }
}
