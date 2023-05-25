using System;
using System.Windows.Forms;

namespace Railway
{
    public partial class TrainForm : Form
    {
        private readonly Train Train;
        public TrainForm()
        {
            InitializeComponent();
            Train = new Train();
        }

        private void TrainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Train.All();
            dataGridView1.Columns[0].Visible = false;

            comboBox1.DataSource = Train.TrainTypes();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                dataGridView1.DataSource = Train.All();
            } else
            {
                dataGridView1.DataSource = Train.AllWhere(comboBox1.SelectedValue.ToString());
            }
        }
    }
}
