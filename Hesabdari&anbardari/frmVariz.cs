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
    public partial class frmVariz : Form
    {
        public frmVariz()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into VarizBeHesab (ShomareHesab,NameHesab,NameMoshtari,Mablagh,TarikhVariz,Tozih)values (@a,@b,@c,@d,@e,@f)";
                cmd.Parameters.AddWithValue("@a", txtShomareHesab.Text);
                cmd.Parameters.AddWithValue("@b", txtNameHesab.Text);
                cmd.Parameters.AddWithValue("@c", txtNameMoshtari.Text);
                cmd.Parameters.AddWithValue("@d", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@e", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@f", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();

                
                

                SqlCommand sqlcmd = new SqlCommand("select Mablagh from Hesabha where ShomareHesab=@sh",con);

                sqlcmd.Parameters.AddWithValue("@sh", txtShomareHesab.Text);

                object result = sqlcmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("شماره حساب یافت نشد.");
                    con.Close();
                    return;
                }

                int mojodi = Convert.ToInt32(result);
                int varizi = Convert.ToInt32(txtMablagh.Text);

                int b = mojodi + varizi;

                string updatequery = "update Hesabha set Mablagh ='" + b + "' where ShomareHesab='" + txtShomareHesab.Text + "'";
                SqlCommand com = new SqlCommand(updatequery, con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("عملیات با موفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new frmReportMoshtari().ShowDialog();
        }

        private void frmVariz_Load(object sender, EventArgs e)
        {

        }
    }
}
