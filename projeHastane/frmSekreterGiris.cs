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
    public partial class frmSekreterGiris : Form
    {
        public frmSekreterGiris()
        {
            InitializeComponent();
        }

        sqlConnect bgl = new sqlConnect();

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from tbl_sekreter where sekreterTC = @p1 and sekreterSifre = @p2 " , bgl.baglanti());
            komut.Parameters.AddWithValue("@p1" ,mskTC.Text );
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                frmSekreterDetay frs = new frmSekreterDetay();
                frs.TCnumara = mskTC.Text;
                frs.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı Giris Yaptınız");
                
            }
            bgl.baglanti().Close();

        }
    }
}
