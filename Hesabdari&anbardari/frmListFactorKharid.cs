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
using Stimulsoft.Report;


namespace Hesabdari_anbardari
{
    public partial class frmListFactorKharid : Form
    {
        public frmListFactorKharid()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        void Display()
        {
            SqlDataAdapter da = new SqlDataAdapter(" SELECT DISTINCT CodeFactor,TarikhFactor,NameMoshtari,JameFactor From FactorKharid where TarikhFactor  between '" + mskTarikh1.Text + "' And '" + mskTarikh2.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "FactorKharid");
            dgvFactor.DataSource = ds.Tables["FactorKharid"].DefaultView;
            con.Close();
        }
        private void frmListFactorKharid_Load_1(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikh2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            Display();

            dgvFactor.Columns[0].HeaderText = "شماره فاکتور";
            dgvFactor.Columns[1].HeaderText = "تاریخ ثبت";
            dgvFactor.Columns[2].HeaderText = "نام مشتری";
            dgvFactor.Columns[3].HeaderText = "مبلغ فاکتور";


            dgvFactor.Columns[2].Width = 250;
            dgvFactor.Columns[3].Width = 150;

        }
        private void mskTarikh2_TextChanged_1(object sender, EventArgs e)
        {
            Display();
        }

        private void mskTarikh1_TextChanged_1(object sender, EventArgs e)
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
                cmd.CommandText = "delete from FactorKharid where CodeFactor =@s";
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
            Report.Load("Report/rptListKharid.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskTarikh1.Text;
            Report["strTarikh2"] = mskTarikh2.Text;
            Report.ShowWithRibbonGUI();
        }

        private void dgvFactor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }

        private void mskTarikh2_Click(object sender, EventArgs e)
        {

        }

        
    }
}
