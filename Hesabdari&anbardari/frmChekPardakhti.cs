using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Hesabdari_anbardari
{
    public partial class frmChekPardakhti : Form
    {
        public frmChekPardakhti()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        
            
        private void frmChekPardakhti_Load_1(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            Tarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            SarResid.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into ChekPardakhti(ShomareHesab,NameHesab,ShomareSanad,Mablagh,TarikhSabt,SarResid,NameMoshtari,Vaziyat,Tozih) values (@a,@b,@c,@d,@e,@f,@g,@h,@i)";
                cmd.Parameters.AddWithValue("@a", ShomareHesab.Text);
                cmd.Parameters.AddWithValue("@b", NameHesab.Text);
                cmd.Parameters.AddWithValue("@c", ShomareSanad.Text);
                cmd.Parameters.AddWithValue("@d", Mablagh.Text);
                cmd.Parameters.AddWithValue("@e", Tarikh.Text);
                cmd.Parameters.AddWithValue("@f", SarResid.Text);
                cmd.Parameters.AddWithValue("@g", Moshtari.Text);
                cmd.Parameters.AddWithValue("@h", Vaziyat.Text);
                cmd.Parameters.AddWithValue("@i", Tozih.Text);
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new frmListChekP().ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "Update ChekPardakhti set ShomareHesab='" + txtShomareHesab.Text + "',NameHesab='" + txtNameHesab.Text + "',ShomareSanad='" + txtShomareSanad.Text + "',Mablagh='" + txtMablagh.Text + "',TarikhSabt='" + mskTarikh.Text + "',SarResid='" + mskSarResid.Text + "',NameMoshtari='" + txtMoshtari.Text + "',Vaziyat='" + cmbVaziyat.Text + "',Tozih='" + txtTozih.Text + "' where IdChekP=" + txtIdSanad.Text;
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

        
    }
}
