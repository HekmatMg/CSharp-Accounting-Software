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
    public partial class frmListFactorFroosh : Form
    {
        public frmListFactorFroosh()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        void Display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select DISTINCT CodeFactor,TarikhFactor,NameMoshtari,JameFactor from FactorFroosh where TarikhFactor between '" + mskTarikh1.Text + "' And '" + mskTarikh2.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "FactorFroosh");
            dgvFactor.DataSource = ds.Tables["FactorFroosh"].DefaultView;
            con.Close();
        }

        private void frmListFactorFroosh_Load_1(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikh2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            Display();

            


            dgvFactor.Columns[0].HeaderText = "شماره فاکتور";
            dgvFactor.Columns[1].HeaderText = "تاریخ ثبت";
            dgvFactor.Columns[2].HeaderText = "نام مشتری";
            dgvFactor.Columns[3].HeaderText = "مبلغ فاکتور";
           
        }

        private void mskTarikh1_TextChanged(object sender, EventArgs e)
        {
            Display();
        }





        private void mskTarikh1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mskTarikh2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            Display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvFactor.CurrentRow.Cells[0].Value);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from FactorFroosh where CodeFactor =@s";
                cmd.Parameters.AddWithValue("@s", x);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("عملیات با موفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                Display();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptListFroosh.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskTarikh1.Text;
            Report["strTarikh2"] = mskTarikh2.Text;
            Report.ShowWithRibbonGUI();
        }

        
    }
}
