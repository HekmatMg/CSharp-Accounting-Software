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
    public partial class frmControlKala : Form
    {
        public frmControlKala()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source=(local);initial catalog=Hesabdaridb;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        void Display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Kala where Tedad between '" + txtTedad1.Text + "' And '" + txtTedad2.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Kala");
            dgvKala.DataSource = ds.Tables["Kala"].DefaultView;
            con.Close();
        }

        void Display1()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Kala where Tedad = '" + 0 + "' ", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Kala");
            dgvKala.DataSource = ds.Tables["Kala"].DefaultView;
            con.Close();
        }

        private void frmControlKala_Load_1(object sender, EventArgs e)
        {
            Display1();
            dgvKala.Columns[0].HeaderText = "کد کالا";
            dgvKala.Columns[1].HeaderText = "گروه کالا";
            dgvKala.Columns[2].HeaderText = "نام کالا";
            dgvKala.Columns[3].HeaderText = "قیمت خرید";
            dgvKala.Columns[4].HeaderText = "قیمت فروش";
            dgvKala.Columns[5].HeaderText = "تعداد";
            dgvKala.Columns[6].HeaderText = "واحد";

        }

      

        private void btnControl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTedad1.Text) ||string.IsNullOrWhiteSpace(txtTedad2.Text))
            {
                MessageBox.Show("لطفاً تعداد کالا را وارد کنید",
                                "پیغام",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            int tedad1, tedad2;

            if (!int.TryParse(txtTedad1.Text, out tedad1) || !int.TryParse(txtTedad2.Text, out tedad2))
            {
                MessageBox.Show("لطفاً تعداد را به صورت عدد وارد کنید");
                return;
            }

            Display();
        }

        
    }
}
