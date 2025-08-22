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
using System.Data.Common;

namespace Proje_Hastane
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;


            ///Ad Soyad Çekme 
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTc=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0] + " " + dr[1]; //yukaridaki select ikikolonlu spnuç döndüreceği için bu şekilde yazıldı
                bgl.baglanti().Close();
                //RANDEVU GEÇMİŞİ  DATAGRIDVIEW I DOLDURMA  datagrid dataadapter ile doluyor
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTC=" + tc, bgl.baglanti());///tc yerine lbltc olur
                da.Fill(dt);
                dataGridView1.DataSource = dt;



                ///BRANŞLARI ÇEKME

                SqlCommand komut2 = new SqlCommand("select * from Tbl_Branslar", bgl.baglanti());
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read()) {

                    cmbbrans.Items.Add(dr2[1]);


        }
    }
    }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbdoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("p1",cmbbrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbdoktor.Items.Add(dr3[0] + " " + dr3[1]);
            
            }

            bgl.baglanti().Close();

        }

        private void cmbdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans='" + cmbbrans.Text + "'" + "and RandevuDoktor='"  + cmbdoktor.Text + "' and RandevuDurum=0",bgl.baglanti());
         //   SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans='" + cmbbrans.Text + "'", bgl.baglanti());

            da.Fill(dt);
            dataGridView2.DataSource = dt;



            {

            }
        }

        private void lnkbilgiduzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCNo = lblTC.Text;
            fr.Show();
        }
      
        private void lblTC_Click(object sender, EventArgs e)
        {
            
        }

        private void btnrandevual_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Randevular set RandevuDurum=1,HastaTC=@P1,HastaSikayet=@p2 where RandevuID=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", TxtRandevuID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);



        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            TxtRandevuID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();


        }
    }
}