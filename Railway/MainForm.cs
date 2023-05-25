using System;
using System.Windows.Forms;

namespace Railway
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool toAuth = false;

        private void MainForm_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = "ФИО: " + User.CurrentUser.fullName;
            materialLabel2.Text = "Должность: " + User.CurrentUser.position;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!toAuth)
                Application.Exit();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            new DirectionsForm().ShowDialog();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            User.CurrentUser = null;
            toAuth = true;
            this.Close();
            (Owner).Show();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            TrainForm form = new TrainForm();
            form.ShowDialog();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            StuffInfo form = new StuffInfo();
            form.ShowDialog();
        }
    }
}
