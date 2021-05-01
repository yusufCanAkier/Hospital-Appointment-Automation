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

namespace projeHastane
{
    public partial class doktorGiris : Form
    {
        public doktorGiris()
        {
            InitializeComponent();
        }

        sqlConnect bgl = new sqlConnect();

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from tbl_doktorlar where doktorTC=@p1 and doktorSifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",mskTC.Text);
            komut.Parameters.AddWithValue("@p2",txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read()){
                doktorDetay dot = new doktorDetay();
                dot.tc_no = mskTC.Text;
                dot.Show();
                this.Hide();
                

            }
            else
            {
                MessageBox.Show("Hatalı Giris","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();


        }
    }
}
