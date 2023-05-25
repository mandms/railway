using System;
using System.Windows.Forms;

namespace Railway
{
    public partial class ChangeDirectionForm : Form
    {
        private readonly Direction Direction;
        private string DirectionId;
        public ChangeDirectionForm()
        {
            InitializeComponent();
            Direction = new Direction();
        }
        private bool Validation()
        {
            if (materialSingleLineTextField1.Text.Length < 0)
            {
                return false;
            }
            if (materialSingleLineTextField2.Text.Length < 0)
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
            }
            try
            {
                string departureDate = $"{dateTimePicker1.Value.ToString("yyyy-MM-dd")} {dateTimePicker2.Value.ToString("HH:mm:ss")}";
                string arrivalDate = $"{dateTimePicker3.Value.ToString("yyyy-MM-dd")} {dateTimePicker4.Value.ToString("HH:mm:ss")}";
                Direction.Change(
                    DirectionId,
                    comboBox1.SelectedValue.ToString(),
                    materialSingleLineTextField1.Text,
                    materialSingleLineTextField2.Text,
                    departureDate,
                    arrivalDate
                );
                MessageBox.Show("Обновление прошло успешно");
                (Owner as DirectionsForm).RefreshForm();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeDirectionForm_Load(object sender, EventArgs e)
        {
            DirectionId = (Owner as DirectionsForm).dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            DirectionObject direction = Direction.GetById(DirectionId);
            comboBox1.SelectedValue = direction.trainID;

            materialSingleLineTextField1.Text = direction.from;
            materialSingleLineTextField2.Text = direction.to;
            dateTimePicker1.Value = direction.departureDate.Date;
            dateTimePicker3.Value = direction.arrivalDate.Date;
            dateTimePicker2.Text = direction.departureDate.ToShortTimeString();
            dateTimePicker4.Text = direction.arrivalDate.ToShortTimeString();

            comboBox1.DataSource = Direction.Trains();
            comboBox1.DisplayMember = "number";
            comboBox1.ValueMember = "id";
        }
    }
}
