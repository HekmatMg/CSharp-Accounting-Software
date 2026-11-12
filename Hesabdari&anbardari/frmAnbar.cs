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
    public partial class frmAnbar : Form
    {
        public frmAnbar()
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
            adp.SelectCommand.CommandText = "select * from anbar";
            adp.Fill(ds, "anbar");
            dgvAnbar.DataSource = ds;
            dgvAnbar.DataMember = "anbar";
        }
        private void frmAnbar_Load_1(object sender, EventArgs e)
        {
           
            Display();
            dgvAnbar.Columns[0].HeaderText = "کد انبار";
            dgvAnbar.Columns[1].HeaderText = "نام انبار";
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Anbar (NameAnbar)values(@a)";
                cmd.Parameters.AddWithValue("@a", txtNameAnbar.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ثبت با موفقیت انجام شد");
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
                int x = Convert.ToInt32(dgvAnbar.SelectedCells[0].Value);
                cmd.Parameters.Clear();
                cmd.CommandText = "Delete from Anbar where IdAnbar=@N";
                cmd.Parameters.AddWithValue("@n", txtId.Text);
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

        private void dgvAnbar_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvAnbar[0, dgvAnbar.CurrentRow.Index].Value.ToString();
            txtNameAnbar.Text = dgvAnbar[1, dgvAnbar.CurrentRow.Index].Value.ToString();
        }

        
    }
}
