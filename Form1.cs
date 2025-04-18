using System.Reflection;
using System.Windows.Forms;

namespace desktopapp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Anasayfa";
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "desktopapp1.Images.horoz1.jpg";


            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {

                pictureBox1.Image = System.Drawing.Image.FromStream(stream);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            Form2 openForm2 = null;

            foreach (Form childForm in this.MdiChildren)
            {
                if (childForm is Form2)
                {
                    openForm2 = (Form2)childForm;
                    break;
                }
            }

            if (openForm2 != null)
            {

                openForm2.Activate();
            }
            else
            {

                Form2 form2 = new Form2();
                form2.MdiParent = this;
                form2.FormBorderStyle = FormBorderStyle.None;
                form2.Dock = DockStyle.Fill;
                form2.Show();
            }

        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);


            var result = MessageBox.Show("Uygulamadan ��kmak istedi�inizden emin misiniz?",
                                          "��k�� Onay�",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

            }
            else
            {

                e.Cancel = true;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form3 openForm3 = null;

            foreach (Form childForm in this.MdiChildren)
            {
                if (childForm is Form3)
                {
                    openForm3 = (Form3)childForm;
                    break;
                }
            }

            if (openForm3 != null)
            {

                openForm3.Activate();
            }
            else
            {

                Form3 form3 = new Form3();
                form3.MdiParent = this;
                form3.FormBorderStyle = FormBorderStyle.None;
                form3.Dock = DockStyle.Fill;
                form3.Show();
            }
        }
    }
}
