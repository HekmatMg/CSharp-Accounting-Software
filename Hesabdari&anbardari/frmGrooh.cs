using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesabdari_anbardari
{
    public partial class frmGrooh : Form
    {
        public frmGrooh()
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
            adp.SelectCommand.CommandText = "select * from Grooh";
            adp.Fill(ds, "Grooh");
            dgvGrooh.DataSource = ds;
            dgvGrooh.DataMember = "Grooh";
        }
        private void frmGrooh_Load_1(object sender, EventArgs e)
        {
            try
            {
                Display();
                dgvGrooh.Columns[0].HeaderText = "کد گروه کالا";
                dgvGrooh.Columns[1].HeaderText = "نام گروه کال";

            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Grooh (NameGrooh)values(@a)";
                cmd.Parameters.AddWithValue("@a", txtNameGrooh.Text);
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

                int x = Convert.ToInt32(dgvGrooh.SelectedCells[0].Value);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Grooh where IdGrooh = @N";
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
        }

        private void dgvGrooh_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvGrooh[0, dgvGrooh.CurrentRow.Index].Value.ToString();
            txtNameGrooh.Text = dgvGrooh[1, dgvGrooh.CurrentRow.Index].Value.ToString();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
