using System;
using System.Windows.Forms;

namespace Railway
{
    public partial class UpdateStuffInfo : Form
    {
        private readonly Stuff Stuff;
        private string StuffId;
        public UpdateStuffInfo()
        {
            InitializeComponent();
            Stuff = new Stuff();
        }

        private void UpdateStuffInfo_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 1;

            comboBox1.DataSource = new Direction().Trains();
            comboBox1.DisplayMember = "number";
            comboBox1.ValueMember = "id";

            comboBox3.DataSource = Stuff.Employees();
            comboBox3.DisplayMember = "full_name";
            comboBox3.ValueMember = "id";

            StuffId = (Owner as StuffInfo).dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            StuffObject stuff = Stuff.GetStuffInfoById(StuffId);
            comboBox1.SelectedValue = stuff.trainId;
            comboBox2.SelectedItem = stuff.shift;
            comboBox3.SelectedValue = stuff.stuffId;
            dateTimePicker2.Text = stuff.startDate.ToShortTimeString();
            dateTimePicker4.Text = stuff.endDate.ToShortTimeString();
            richTextBox1.Text = stuff.comment;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        { 
            try
            {
                string startDate = $"{dateTimePicker1.Value.ToString("yyyy-MM-dd")} {dateTimePicker2.Value.ToString("HH:mm:ss")}";
                string endDate = $"{dateTimePicker3.Value.ToString("yyyy-MM-dd")} {dateTimePicker4.Value.ToString("HH:mm:ss")}";
                Stuff.ChangeStuffInfo(
                    StuffId,
                    comboBox1.SelectedValue.ToString(),
                    comboBox2.SelectedItem.ToString(),
                    comboBox3.SelectedValue.ToString(),
                    startDate,
                    endDate,
                    richTextBox1.Text
                );
                MessageBox.Show("Обновление прошло успешно");
                (Owner as StuffInfo).RefreshForm();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
