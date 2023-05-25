using System;
using System.Windows.Forms;

namespace Railway
{
    public partial class DirectionsForm : Form
    {
        private readonly Direction Direction;
        public DirectionsForm()
        {
            InitializeComponent();
            Direction = new Direction();
        }

        private void DirectionsForm_Load(object sender, EventArgs e)
        {
            if (User.CurrentUser.position == "Администратор")
            {
                materialRaisedButton2.Hide();
                materialFlatButton1.Hide();
            }
            if (User.CurrentUser.position == "Сотрудник")
            {
                materialFlatButton1.Hide();
                materialRaisedButton2.Hide();
                materialFlatButton1.Hide();
            }
            dataGridView1.DataSource = Direction.All();
        }

        public void RefreshForm()
        {
            dataGridView1.DataSource = Direction.All();
            dataGridView1.Columns[0].Visible = false;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            CreateDirectionForm form = new CreateDirectionForm();
            form.ShowDialog(this);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            ChangeDirectionForm form = new ChangeDirectionForm();
            form.ShowDialog(this);
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Direction.Delete(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                RefreshForm();
                MessageBox.Show("Удаление прошло успешно.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
