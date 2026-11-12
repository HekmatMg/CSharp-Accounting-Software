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
    public partial class frmMaliyat : Form
    {
        public frmMaliyat()
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
                cmd.CommandText = "insert into Maliyat(Maliyatkharid,MaliyatFroosh) values (@a,@b)";
                cmd.Parameters.AddWithValue("@a", dblKharid.Text);
                cmd.Parameters.AddWithValue("@b", dblFroosh.Text);
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

        private void frmMaliyat_Load(object sender, EventArgs e)
        {

        }
    }
}
