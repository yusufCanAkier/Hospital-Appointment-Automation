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
    public partial class frmBilgiDuzenle : Form
    {
        public frmBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string tcNo;
        sqlConnect bgl = new sqlConnect();

        private void frmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            msk_tc.Text = tcNo;
            SqlCommand komut = new SqlCommand("Select * from tbl_hastalar Where hastaTC = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("p1", msk_tc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                mskTel.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                cmbGender.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();


        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {

            SqlCommand komut2 = new SqlCommand("Update tbl_hastalar set hastaAd =@p1 , hastaSoyad = @p2 , hastaTelefon = @p3 , hastaSifre = @p4 , hastaCinsiyet = @p5 where hastaTC=@p6",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtAd.Text);
            komut2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", mskTel.Text);
            komut2.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", cmbGender.Text);
            komut2.Parameters.AddWithValue("@p6", msk_tc.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }
    }
}
