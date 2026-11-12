using Stimulsoft.Report;
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
using System.Data.SqlClient;
using Stimulsoft.Report;

namespace Hesabdari_anbardari
{
    public partial class frmReportMoshtari : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Hesabdaridb;Integrated Security=True");
        public frmReportMoshtari()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PardakhtAzHesab WHERE NameMoshtari = @name", con);

            cmd.Parameters.AddWithValue("@name", txtNameMoshtari2.Text.Trim());

            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            con.Close();

            if (count == 0)
            {
                MessageBox.Show("مشتری با این نام یافت نشد.",
                                "پیغام",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            StiReport Report = new StiReport();
            Report.Load("Report/rptDaryaftiMoshtari.mrt");
            Report.Compile();
            
            Report["NameMoshtari"] = txtNameMoshtari2.Text.Trim();
            Report.ShowWithRibbonGUI();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VarizBeHesab WHERE NameMoshtari = @name", con);

            cmd.Parameters.AddWithValue("@name", txtNameMoshtari1.Text.Trim());

            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            con.Close();

            if (count == 0)
            {
                MessageBox.Show("مشتری با این نام یافت نشد.",
                                "پیغام",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            StiReport Report = new StiReport();
            Report.Load("Report/rptPardakhtimoshtariha.mrt");
            Report.Compile();

            Report["NameMoshtari"] = txtNameMoshtari1.Text.Trim();

            Report.ShowWithRibbonGUI(); 
            /* StiReport Report = new StiReport();
             Report.Load("Report/rptPardakhtimoshtariha.mrt");
             Report.Compile();
              //Report["CodeFactor"] = Convert.ToInt32(txtCodeFactor.Text);
             Report["NameMoshtari"] = txtNameMoshtari1.Text;
             Report.ShowWithRibbonGUI();*/
        }

        private void frmReportMoshtari_Load(object sender, EventArgs e)
        {

        }
    }
}
