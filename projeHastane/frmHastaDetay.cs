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
    public partial class frmHastaDetay : Form
    {
        public frmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;

        sqlConnect bgl = new sqlConnect();

        private void frmHastaDetay_Load(object sender, EventArgs e)
        {

            lblTC.Text = tc;

            // Ad Soyad Çekme
                SqlCommand komut = new SqlCommand("Select hastaAd,hastaSoyad From tbl_hastalar Where hastaTC=@p1",bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", lblTC.Text);

                SqlDataReader dr = komut.ExecuteReader();

                        while (dr.Read()){

                            lblAdSoyad.Text = dr[0] + " " + dr[1];

                        }

                bgl.baglanti().Close();

            //Randevu Geçmişi
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where hastaTC = " + tc , bgl.baglanti());

                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
            // Branşları Çekme

                    SqlCommand komut2 = new SqlCommand("Select bransAd From tbl_branslar" , bgl.baglanti());

                    SqlDataReader dr2 = komut2.ExecuteReader();

                    while (dr2.Read())
                    {
                        cmbBrans.Items.Add(dr2[0]);
                    }

                    bgl.baglanti().Close();



        }

        // Doktor Ad Soyad Çekme
        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbHekim.Items.Clear();
            SqlCommand komut3 = new SqlCommand ("Select doktorAd,doktorSoyad From tbl_doktorlar where doktorBrans=@p1", bgl.baglanti());

            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);

            SqlDataReader dr3 = komut3.ExecuteReader();

            while (dr3.Read())
            {
                cmbHekim.Items.Add(dr3[0] + " " + dr3[1]);

            }

            bgl.baglanti().Close();

        }
        // Randevu Detay Ekranında Branş Görme
        private void cmbHekim_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From tbl_randevular where randevuBrans ='" + cmbBrans.Text + "'" +"and randevuDoktor ='"+ cmbHekim.Text + "'and randevuDurum=0", bgl.baglanti() );
            da.Fill(dt);
            dataGridView2.DataSource = dt;


        }
        //Bilgileri Düzenle Ekranı
        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmBilgiDuzenle fr = new frmBilgiDuzenle();
            fr.tcNo = lblTC.Text;
            fr.Show();


        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tbl_randevular set randevuDurum=1,hastaTC=@p1,hastaSikayet=@p2 where randevuID=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", rchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtId);
            komut.ExecuteNonQuery();

            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
