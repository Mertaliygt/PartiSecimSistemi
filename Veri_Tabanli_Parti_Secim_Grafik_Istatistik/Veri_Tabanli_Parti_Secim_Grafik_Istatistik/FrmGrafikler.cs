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

namespace Veri_Tabanli_Parti_Secim_Grafik_Istatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-O581UK8\SQLEXPRESS;Initial Catalog=DBSECİMPROJE;Integrated Security=True");


        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //İlçe Adlarını Comboboxa çek!
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ILCEAD from TBLILCE", baglanti);
            SqlDataReader dr = komut.ExecuteReader();  //VERİ TABANINI OKUYOR
            while(dr.Read())
            {

                comboBox1.Items.Add(dr[0]);
            }
            baglanti.Close();

            //SONUÇ GETİRME KISMI
            // SUM KULLAN- O SÜTÜNDA Kİ BÜTÜN VERİLERİ TOPLAR
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) " +
                "FROM TBLILCE", baglanti);
            SqlDataReader dr2= komut2.ExecuteReader();
            while(dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A PARTİ", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", dr2[4]);
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLILCE where ILCEAD=@P1",baglanti);
            komut.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                string partiad;
                progressBar1.Value = int.Parse(dr[2].ToString());
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                LblA.Text= dr[2].ToString();
                LblB.Text = dr[3].ToString();
                LblC.Text = dr[4].ToString();
                LblD.Text = dr[5].ToString();
                LblE.Text = dr[6].ToString();

                int[] dizi = { progressBar1.Value, progressBar2.Value,
                progressBar3.Value,progressBar4.Value, progressBar5.Value};
                int enBuyuk = dizi[0];
                for (int i = 0; i < dizi.Length; i++)
                    
                {
                    if (dizi[i] > enBuyuk)
                    {
                        enBuyuk = dizi[i];
                        
                    }
                }

               
                    MessageBox.Show("EN YÜKSEK OY:" + enBuyuk);
              

            }
            baglanti.Close();
        }
    }
}
