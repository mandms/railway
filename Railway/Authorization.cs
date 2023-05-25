using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Railway
{
    public partial class Authorization : Form
    {
        private readonly User User;
        public Authorization()
        {
            InitializeComponent();
            User = new User();
        }

        private bool validate()
        {
            if (materialSingleLineTextField1.Text.Length > 8)
            {
                errorProvider1.SetError(materialSingleLineTextField1, "Имя пользователя должно быть больше 8 символов");
                return false;
            }
            if (materialSingleLineTextField1.Text.Length < 4)
            {
                errorProvider1.SetError(materialSingleLineTextField1, "Имя пользователя должно быть меньше 4 символов");
                return false;
            }
            if (materialSingleLineTextField2.Text.Length > 11)
            {
                errorProvider1.SetError(materialSingleLineTextField2, "Пароль должен быть больше 11 символов");
                return false;
            }
            if (materialSingleLineTextField2.Text.Length < 4)
            {
                errorProvider1.SetError(materialSingleLineTextField2, "Пароль должен быть меньше 8 символов");
                return false;
            }
            return true;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (!validate())
                return;
            MD5 md5Hasher = MD5.Create();
            string password = Convert.ToString(User.GEtMd5Harh(md5Hasher, materialSingleLineTextField2.Text));
            try
            {
                User.CurrentUser = User.AuthorizationEmployee(materialSingleLineTextField1.Text, password);
                MessageBox.Show("Вход выполнен успешно!");
                this.Hide();
                materialSingleLineTextField1.Text = "";
                materialSingleLineTextField2.Text = "";
                MainForm form = new MainForm();
                form.ShowDialog(this);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            form.ShowDialog();
        }
    }
}
