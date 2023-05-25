using System;
using System.Windows.Forms;

namespace Railway
{
    public partial class CreateStuffInfo : Form
    {
        private readonly Stuff Stuff;
        public CreateStuffInfo()
        {
            InitializeComponent();
            Stuff = new Stuff();
        }

        private void CreateStuffInfo_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 1;

            comboBox1.DataSource = new Direction().Trains();
            comboBox1.DisplayMember = "number";
            comboBox1.ValueMember = "id";

            comboBox3.DataSource = Stuff.Employees();
            comboBox3.DisplayMember = "full_name";
            comboBox3.ValueMember = "id";
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string startDate = $"{dateTimePicker1.Value.ToString("yyyy-MM-dd")} {dateTimePicker2.Value.ToString("HH:mm:ss")}";
                string endlDate = $"{dateTimePicker3.Value.ToString("yyyy-MM-dd")} {dateTimePicker4.Value.ToString("HH:mm:ss")}";
                Stuff.CreateStuffInfo(
                    comboBox1.SelectedValue.ToString(),
                    comboBox2.SelectedItem.ToString(),
                    comboBox3.SelectedValue.ToString(),
                    startDate,
                    endlDate,
                    richTextBox1.Text
                );
                MessageBox.Show("Добавление прошло успешно");
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
