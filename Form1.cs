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

            
            var result = MessageBox.Show("Uygulamadan çýkmak istediðinizden emin misiniz?",
                                          "Çýkýþ Onayý",
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


    }
}
