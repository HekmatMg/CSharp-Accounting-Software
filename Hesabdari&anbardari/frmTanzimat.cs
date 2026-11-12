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
    public partial class frmTanzimat : Form
    {
        public frmTanzimat()
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
            adp.SelectCommand.CommandText = "select * from tanzimat";
            adp.Fill(ds, "tanzimat");
            dgvTanzimat.DataSource = ds;
            dgvTanzimat.DataMember = "tanzimat";
        }

        private void frmTanzimat_Load_1(object sender, EventArgs e)
        {
            Display();
            dgvTanzimat.Columns[0].HeaderText = "کد تنظیمات";
            dgvTanzimat.Columns[1].HeaderText = "نام فروشگاه";
            dgvTanzimat.Columns[2].HeaderText = "تلفن ثابت";
            dgvTanzimat.Columns[3].HeaderText = "تلفن همراه";
            dgvTanzimat.Columns[4].HeaderText = "آدرس";
            dgvTanzimat.Columns[5].HeaderText = "توضیحات";

        }

        private void dgvTanzimat_MouseUp_1(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvTanzimat[0, dgvTanzimat.CurrentRow.Index].Value.ToString();
            textNamFroshgah.Text = dgvTanzimat[1, dgvTanzimat.CurrentRow.Index].Value.ToString();
            textTel.Text = dgvTanzimat[2, dgvTanzimat.CurrentRow.Index].Value.ToString();
            textMobile.Text = dgvTanzimat[3, dgvTanzimat.CurrentRow.Index].Value.ToString();
            textAddress.Text = dgvTanzimat[4, dgvTanzimat.CurrentRow.Index].Value.ToString();
            textTozih.Text = dgvTanzimat[5, dgvTanzimat.CurrentRow.Index].Value.ToString();
        }

        

        
      

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Tanzimat (NameFroshgah,Tel,Mobile,Address,Tozih)values(@a,@b,@c,@d,@e)";
            cmd.Parameters.AddWithValue("@a", textNamFroshgah.Text);
            cmd.Parameters.AddWithValue("@b", textTel.Text);
            cmd.Parameters.AddWithValue("@c", textMobile.Text);
            cmd.Parameters.AddWithValue("@d", textAddress.Text);
            cmd.Parameters.AddWithValue("@e", textTozih.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("اطلاعات با موفقیت ثبت شد");
            Display();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvTanzimat.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "delete from tanzimat where IdTanzimat=@N";
            cmd.Parameters.AddWithValue("@N", txtId.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("اطلاعات با موفقیت حذف شد");
            Display();
        }

       

        private void btnEdit_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "update Tanzimat set NameFroshgah='" + textNamFroshgah.Text + "',Tel='" + textTel.Text + "',Mobile='" + textMobile.Text + "',Address='" + textAddress.Text + "',Tozih='" + textTozih.Text + "' where IdTanzimat= " + txtId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("اطلاعات ویرایش شد");
            Display();
        }

       
    }
}
