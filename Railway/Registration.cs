using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Railway
{
    public partial class Registration : Form
    {
        private readonly User User;
        public Registration()
        {
            InitializeComponent();
            User = new User();
        }
        private bool Validation()
        {
            if (materialSingleLineTextField1.Text.Length < 1)
            {
                return false;
            }
            if (materialSingleLineTextField2.Text.Length < 1)
            {
                return false;
            }
            if (materialSingleLineTextField3.Text.Length < 1)
            {
                return false;
            }
            if (materialSingleLineTextField4.Text.Length < 1)
            {
                return false;
            }
            if (materialSingleLineTextField5.Text.Length < 1)
            {
                return false;
            }
            return true;
        }
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if(!Validation())
            {
                MessageBox.Show("Введите данные корректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MD5 md5Hasher = MD5.Create();
            string password = Convert.ToString(User.GEtMd5Harh(md5Hasher, materialSingleLineTextField5.Text));
            string fullName = $"{materialSingleLineTextField1.Text} {materialSingleLineTextField2.Text} {materialSingleLineTextField3.Text}";
            try
            {
                User.RegistrationEmployee(fullName, comboBox1.SelectedValue.ToString(), materialSingleLineTextField4.Text, password);
                MessageBox.Show("Регистрация выполнен успешна. Авторизуйтесь");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = User.Positions();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }
    }
}
