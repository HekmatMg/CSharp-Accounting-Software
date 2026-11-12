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
    public partial class frmKarbar : Form
    {
        public frmKarbar()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        void Display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Karbar";
            adp.Fill(ds, "Karbar");
            dgvKarbar.DataSource = ds;
            dgvKarbar.DataMember = "Karbar";
        }

        private void frmKarbar_Load(object sender, EventArgs e)
        {
            Display();
            dgvKarbar.Columns[0].HeaderText = "پسورد";
            dgvKarbar.Columns[0].HeaderText = " نام کاربری ";
            dgvKarbar.Columns[0].HeaderText = " کد کاربری ";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Karbar (UserName,Password)values(@a,@b)";
                cmd.Parameters.AddWithValue("@a", txtUserName.Text);
                cmd.Parameters.AddWithValue("@b", txtPassword.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Display();
                MessageBox.Show("عملیات با موفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "Delete from karbar where IdKarbar = @N";
                cmd.Parameters.AddWithValue("@N", txtId.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Display();
                MessageBox.Show("عملیات با موفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            int x = Convert.ToInt32(dgvKarbar.SelectedCells[0].Value);
        }

        private void dgvKarbar_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvKarbar[0, dgvKarbar.CurrentRow.Index].Value.ToString();
        }

        
    }
}
