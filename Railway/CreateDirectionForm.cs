using System;
using System.Windows.Forms;

namespace Railway
{
    public partial class CreateDirectionForm : Form
    {
        private readonly Direction Direction;
        public CreateDirectionForm()
        {
            InitializeComponent();
            Direction = new Direction();
        }

        private bool Validation()
        {
            if(materialSingleLineTextField1.Text.Length < 1)
            {
                return false;
            }
            if (materialSingleLineTextField2.Text.Length < 1)
            {
                return false;
            }
            return true;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                MessageBox.Show("Заполните данные корректно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string departureDate = $"{dateTimePicker1.Value.ToString("yyyy-MM-dd")} {dateTimePicker2.Value.ToString("HH:mm:ss")}";
                string arrivalDate = $"{dateTimePicker3.Value.ToString("yyyy-MM-dd")} {dateTimePicker4.Value.ToString("HH:mm:ss")}";
                Direction.Create(
                    comboBox1.SelectedValue.ToString(),
                    materialSingleLineTextField1.Text,
                    materialSingleLineTextField2.Text,
                    departureDate,
                    arrivalDate
                );
                MessageBox.Show("Добавление прошло успешно");
                (Owner as DirectionsForm).RefreshForm();
                this.DialogResult = DialogResult.OK;
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDirectionForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Direction.Trains();
            comboBox1.DisplayMember = "number";
            comboBox1.ValueMember = "id";
        }
    }
}
