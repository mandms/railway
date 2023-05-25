using System;
using System.Windows.Forms;

namespace Railway
{
    public partial class StuffInfo : Form
    {
        private readonly Stuff Stuff;
        public StuffInfo()
        {
            InitializeComponent();
            Stuff = new Stuff();
        }

        private void StuffInfo_Load(object sender, EventArgs e)
        {
            if (User.CurrentUser.position == "Сотрудник")
            {
                dataGridView1.DataSource = Stuff.AllWhere(User.CurrentUser.id);
                materialFlatButton1.Hide();
                materialRaisedButton2.Hide();
                materialFlatButton1.Hide();
            } else if (User.CurrentUser.position == "Администратор") {
                materialFlatButton1.Hide();
                materialRaisedButton2.Hide();
                dataGridView1.DataSource = Stuff.All();
            }
            else
            {
                dataGridView1.DataSource = Stuff.All();
            }
            dataGridView1.Columns[0].Visible = false;
        }

        public void RefreshForm()
        {
            dataGridView1.DataSource = Stuff.All();
            dataGridView1.Columns[0].Visible = false;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            CreateStuffInfo form = new CreateStuffInfo();
            form.ShowDialog(this);
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Stuff.DeleteStuffInfo(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                RefreshForm();
                MessageBox.Show("Удаление прошло успешно.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            UpdateStuffInfo form = new UpdateStuffInfo();
            form.ShowDialog(this);
        }
    }
}
