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
    public partial class frmPardakht : Form
    {
        public frmPardakht()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                int str1;
                con.Open();
                SqlCommand sqlcmd = new SqlCommand("select Mablagh from Hesabha where ShomareHesab = '" + txtShomareHesab.Text + "'", con);
                object result = sqlcmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("شماره حساب مورد نظر یافت نشد", "پیغام",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    con.Close();
                    return;
                }
                str  = Convert.ToString((int)sqlcmd.ExecuteScalar());
                str1 = Convert.ToInt32(txtMablagh.Text);

                if (txtMablagh.Value > Convert.ToInt32(str))
                {
                    MessageBox.Show("موجودی حساب برای پرداخت این مبلغ کافی نمی باشد");
                }
                else
                {
                    int b = Int32.Parse(str) - str1;
                    string updatequery = "Update Hesabha set Mablagh ='" + b + "' where ShomareHesab = '" + txtShomareHesab.Text + "'";
                    SqlCommand com = new SqlCommand(updatequery, con);
                    com.ExecuteNonQuery();
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "insert into PardakhtAzHesab (ShomareHesab,NameHesab,NameMoshtari,Mablagh,TarikhPardakht,Tozih) values (@a,@b,@c,@d,@e,@f)";
                    cmd.Parameters.AddWithValue("@a", txtShomareHesab.Text);
                    cmd.Parameters.AddWithValue("@b", txtNameHesab.Text);
                    cmd.Parameters.AddWithValue("@c", txtNameMoshtari.Text);
                    cmd.Parameters.AddWithValue("@d", txtMablagh.Text);
                    cmd.Parameters.AddWithValue("@e", mskTarikh.Text);
                    cmd.Parameters.AddWithValue("@f", txtTozih.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("عملیات با موفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                con.Close();
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

        private void frmPardakht_Load(object sender, EventArgs e)
        {

        }
    }
}
