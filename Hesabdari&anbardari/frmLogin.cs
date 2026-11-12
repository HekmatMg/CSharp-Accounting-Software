using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hesabdari_anbardari
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();



        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                cmd = new SqlCommand("select count(*) from karbar where UserName=@N And Password=@F", con);
                cmd.Parameters.AddWithValue("@N", txtUserName.Text);
                cmd.Parameters.AddWithValue("@F", txtPassword.Text);
                con.Open();
                i = (int)cmd.ExecuteScalar();
                con.Close();

                if (i > 0)
                {
                    new Form1().ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("کاربر یافت نشد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }

        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
