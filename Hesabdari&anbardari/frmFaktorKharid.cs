using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Stimulsoft.Report;

namespace Hesabdari_anbardari
{
    public partial class frmFaktorKharid : Form
    {
        
        public frmFaktorKharid()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();


        void Display1()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Moshtari1";
            adp.Fill(ds, "Moshtari1");
            dgvMoshtari.DataSource = ds;
            dgvMoshtari.DataMember = "Moshtari1";

        }

        void Display2()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Kala ";
            adp.Fill(ds, "Kala");
            dgvKala.DataSource = ds;
            dgvKala.DataMember = "Kala";
        }
        string Address;
        string Tel;

        private void frmFaktorKharid_Load_1(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(System.Globalization.CultureInfo.CreateSpecificCulture("fa-IR"));

            Display1();
            dgvMoshtari.Columns[0].HeaderText = "کد مشتری";
            dgvMoshtari.Columns[1].HeaderText = "نام مشتری";
            dgvMoshtari.Columns[2].HeaderText = "نوع مشتری";
            dgvMoshtari.Columns[3].HeaderText = "تلفن ثابت";
            dgvMoshtari.Columns[4].HeaderText = "موبایل";
            dgvMoshtari.Columns[5].HeaderText = "بدهکار";
            dgvMoshtari.Columns[6].HeaderText = "بستانکار";

            Display2();
            dgvKala.Columns[0].HeaderText = "کد کالا";
            dgvKala.Columns[1].HeaderText = "گروه کالا";
            dgvKala.Columns[2].HeaderText = "نام کالا";
            dgvKala.Columns[3].HeaderText = "قیمت خرید";
            dgvKala.Columns[4].HeaderText = "قیمت فروش";
            dgvKala.Columns[5].HeaderText = "تعداد";
            dgvKala.Columns[6].HeaderText = "واحد";

            cmd.Connection = con;
            cmd.CommandText = "select MaliyatKharid from Maliyat";
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dblKharid.Text = dr["MaliyatKharid"].ToString();
            }
            con.Close();

            cmd.CommandText = "select * from Tanzimat";
            SqlDataReader dr1;
            con.Open();
            dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                Address = dr1["Address"].ToString();
                Tel = dr1["Tel"].ToString();
            }
            con.Close();

            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }
        
        private void txtFilterKala_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Kala where NameKala Like '%' + @S +'%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtFilterKala.Text + "%");
            adp.Fill(ds, "Kala");
            dgvKala.DataSource = ds;
            dgvKala.DataMember = "Kala";
        }

        private void txtFilterMoshtari_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Moshtari1 where NameMoshtari Like @S ";
            adp.SelectCommand.Parameters.AddWithValue("@S","%" + txtFilterMoshtari.Text + "%");
            adp.Fill(ds, "Moshtari1");
            dgvMoshtari.DataSource = ds;
            dgvMoshtari.DataMember = "Moshtari1";
        }
            
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvFactor.Rows.Add(txtIdKala.Text, txtNameKala.Text, txtGeymat.Text, txtTedad.Text);

            txtIdKala.Text = "";
            txtNameKala.Text = "";
            txtGeymat.Text = "";
            txtTedad.Text = "";

            int JameKolfactor = 0;
            int JameKalaha = 0;
            int Maliyat = 0;

            for (int i = 0; i < dgvFactor.Rows.Count; i++)
            {
                JameKalaha = JameKalaha + (Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
                int GeymatKol;

                GeymatKol = (Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
                dgvFactor.Rows[i].Cells[4].Value = GeymatKol;
            }
            txtJameFactor.Text = JameKalaha.ToString();

            Maliyat = Convert.ToInt32(JameKalaha * dblKharid.Value);

            txtMaliyat.Value = Maliyat;

            JameKolfactor = (JameKalaha + Maliyat + txtHazineh.Value) - txtTakhfif.Value;
            txtJameKol.Value = JameKolfactor;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFactor.Rows.Count == 0)
                {
                    MessageBox.Show("کالایی انتخاب نشده است");
                }
                else
                {
                    dgvFactor.Rows.RemoveAt(dgvFactor.CurrentRow.Index);
                }
            }
            catch (Exception)
            {
               


            }
        }

        private void dgvMoshtari_MouseUp(object sender, MouseEventArgs e)
        {
            txtIdMoshtari.Text = dgvMoshtari[0, dgvMoshtari.CurrentRow.Index].Value.ToString();
            txtNameMoshtari.Text = dgvMoshtari[1, dgvMoshtari.CurrentRow.Index].Value.ToString();
            txtMobile.Text = dgvMoshtari[4, dgvMoshtari.CurrentRow.Index].Value.ToString();
        }

        private void dgvKala_MouseUp(object sender, MouseEventArgs e)
        {
            txtIdKala.Text = dgvKala[0, dgvKala.CurrentRow.Index].Value.ToString();
            txtNameKala.Text = dgvKala[2, dgvKala.CurrentRow.Index].Value.ToString();
            txtGeymat.Text = dgvKala[3, dgvKala.CurrentRow.Index].Value.ToString();
            lbltdad.Text = dgvKala[5, dgvKala.CurrentRow.Index].Value.ToString();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            
            int JameKolfactor = 0;
            int JameKalaha = 0;
            int Maliyat = 0;

            for (int i = 0; i < dgvFactor.Rows.Count; i++)
            {
                JameKalaha = JameKalaha + (Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
            }
            txtJameFactor.Text = JameKalaha.ToString();

            Maliyat = Convert.ToInt32(JameKalaha * dblKharid.Value);

            txtMaliyat.Value = Maliyat;

            JameKolfactor = (JameKalaha + Maliyat + txtHazineh.Value) - txtTakhfif.Value;
            txtJameKol.Value = JameKolfactor;
        }
          
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
               {

                      for (int i = 0; i < dgvFactor.Rows.Count-1; i++)
                      {
                       con.Close();
                       cmd.Connection = con;
                       cmd.Parameters.Clear();
                       cmd.CommandText = "insert into FactorKharid (CodeFactor,TarikhFactor,IdMoshtari,NameMoshtari,Mobile,IdKala,NameKala,GeymatKharid,Tedad,MablaghFactor,MaliyatKharid,HazinehFactor,TakhfifFactor,JameFactor,Tozih,GeymatKol) values (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p)";
                       cmd.Parameters.AddWithValue("@a", txtCodeFactor.Text);
                       cmd.Parameters.AddWithValue("@b", mskTarikh.Text);
                       cmd.Parameters.AddWithValue("@c", txtIdMoshtari.Text);
                       cmd.Parameters.AddWithValue("@d", txtNameMoshtari.Text);
                       cmd.Parameters.AddWithValue("@e", txtMobile.Text);

                       cmd.Parameters.AddWithValue("@f", Convert.ToInt32(dgvFactor.Rows[i].Cells[0].Value));
                       cmd.Parameters.AddWithValue("@g", dgvFactor.Rows[i].Cells[1].Value);
                       cmd.Parameters.AddWithValue("@h", Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value));
                       cmd.Parameters.AddWithValue("@i", Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
                       cmd.Parameters.AddWithValue("@p", Convert.ToInt32(dgvFactor.Rows[i].Cells[4].Value));

                       cmd.Parameters.AddWithValue("@j", txtJameFactor.Text);
                       cmd.Parameters.AddWithValue("@k", txtMaliyat.Text);
                       cmd.Parameters.AddWithValue("@l", txtHazineh.Text);
                       cmd.Parameters.AddWithValue("@m", txtTakhfif.Text);
                       cmd.Parameters.AddWithValue("@n", txtJameKol.Text);
                       cmd.Parameters.AddWithValue("@o", txtTozih.Text);
                       con.Open();
                       cmd.ExecuteNonQuery();
                       //con.Close();


                       /* string str;
                          SqlCommand sqlcmd = new SqlCommand("select Tedad from Kala where IdKala=@IdKala", con);
                          sqlcmd.Parameters.AddWithValue("@IdKala", Convert.ToInt32(dgvFactor.Rows[i].Cells[0].Value));
                          str = Convert.ToString((int)sqlcmd.ExecuteScalar());
                          int tedadMojood = Convert.ToInt32(sqlcmd.ExecuteScalar());*/



                         SqlCommand sqlcmd = new SqlCommand("select Tedad from Kala where IdKala=@IdKala", con);

                         sqlcmd.Parameters.AddWithValue("@IdKala", Convert.ToInt32(dgvFactor.Rows[i].Cells[0].Value));

                         int tedadMojood = Convert.ToInt32(sqlcmd.ExecuteScalar());
                         int tedadKharid = Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value);
                         int b = tedadMojood + tedadKharid;
                         SqlCommand com = new SqlCommand("update Kala set Tedad=@Tedad where IdKala=@IdKala",con);

                         com.Parameters.AddWithValue("@Tedad", b);
                         com.Parameters.AddWithValue("@IdKala", Convert.ToInt32(dgvFactor.Rows[i].Cells[0].Value));

                         com.ExecuteNonQuery();

                         Display2();

                         con.Close();
                         MessageBox.Show("عملیات با موفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                      }

               }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
        }

        private void btnPardakht_Click(object sender, EventArgs e)
        {
            frmPardakht frm = new frmPardakht();
            frm.txtTozih.Text = "پرداخت نقد مبلغ فاکتور خرید به شماره فاکتور" + txtCodeFactor.Text;
            frm.txtNameMoshtari.Text = txtNameMoshtari.Text;
            frm.txtMablagh.Text = txtJameKol.Text;
            frm.mskTarikh.Text = mskTarikh.Text;
            frm.ShowDialog();
        }

        private void btnSanad_Click(object sender, EventArgs e)
        {
           frmChekPardakhti frm = new frmChekPardakhti();
            frm.Tozih.Text = "پرداخت  مبلغ فاکتور خرید به صورت چک به شماره فاکتور" + txtCodeFactor.Text;
            frm.Moshtari.Text = txtNameMoshtari.Text;
            frm.Mablagh.Text = txtJameKol.Text;
            frm.Tarikh.Text = mskTarikh.Text;
            frm.ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new frmListFactorKharid().ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            StiReport Report = new StiReport();
            Report.Load("Report/rptFactorKharid.mrt");
            Report.Compile();
            Report["CodeFactor"] = Convert.ToInt32(txtCodeFactor.Text);
            Report["Address"] = Address;
            Report["Tel"] = Tel;
            
            Report.ShowWithRibbonGUI();

            
        }

        private void lbltdad_Click(object sender, EventArgs e)
        {

        }
    }
}
