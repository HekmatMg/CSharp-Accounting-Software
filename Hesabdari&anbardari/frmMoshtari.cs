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
    public partial class frmMoshtari : Form
    {
        public frmMoshtari()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();



        private void groupPanel2_Click(object sender, EventArgs e)
        {

        }

        private void frmMoshtari_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Moshtari1 (NameMoshtari,NooMoshtari,Tel,Mobile,Bedehkar,Bestankar)values (@a,@b,@c,@d,@e,@f)";
                cmd.Parameters.AddWithValue("@a", txtNameMoshtari.Text); 
                cmd.Parameters.AddWithValue("@c", txtTel.Text);
                cmd.Parameters.AddWithValue("@d", txtMobile.Text);
                cmd.Parameters.AddWithValue("@e", txtBedehkar.Text);
                cmd.Parameters.AddWithValue("@f", txtBestankar.Text);
                cmd.Parameters.AddWithValue("@b", txtNooMoshtari.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("عملیات با موفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            new frmListMoshtari().ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "Update Moshtari1 set NameMoshtari='" + txtNameMoshtari.Text + "',NooMoshtari='" + txtNooMoshtari.Text + "',Tel='" + txtTel.Text + "',Mobile='" + txtMobile.Text + "',Bedehkar='" + txtBedehkar.Text + "',Bestankar='" + txtBestankar.Text + "' where IdMoshtari=" + txtIdMoshtari.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("عملیات با موفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
        }

        private void txtIdMoshtari_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
