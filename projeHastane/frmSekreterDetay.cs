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
    public partial class frmSekreterDetay : Form
    {
        public frmSekreterDetay()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string TCnumara;
        sqlConnect bgl = new sqlConnect();

        private void frmSekreterDetay_Load(object sender, EventArgs e)
        {

            LblTc.Text = TCnumara;

            // Sekreter Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("Select sekreterAdSoyad from tbl_sekreter where sekreterTC =@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1" , LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read()){

                LblAdSoyad.Text = dr[0].ToString();

            }
            bgl.baglanti().Close();


            // Branşları Çekme

            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;


            // Doktorları Listeye Aktarma

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (doktorAd+ ' ' +doktorSoyad) as 'Hekim Adı',doktorBrans from tbl_doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            // Branşa Comboboxa Aktarma
            SqlCommand komut2 = new SqlCommand("Select bransAd from tbl_branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }

            

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("insert into tbl_Randevular (randevuTarih,randevuSaat,randevuBrans,randevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
            komutKaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutKaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komutKaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            komutKaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            // Branşa Göre Hekim Seçme

            SqlCommand komut3 = new SqlCommand("Select doktorAd,doktorSoyad from tbl_doktorlar where doktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while(dr3.Read()){

                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);

            }
            bgl.baglanti().Close();


        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("insert into tbl_duyurular (duyuru) values(@d1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", rchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");

            
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {

            frmDoktorPanel drp = new frmDoktorPanel();
            drp.Show();

        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            frmBrans frb = new frmBrans();
            frb.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            frmRandevuListesi frmrl = new frmRandevuListesi();
            frmrl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDuyurular frd = new frmDuyurular();
            frd.Show();
        }
    }
}
