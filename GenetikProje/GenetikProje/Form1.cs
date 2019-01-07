/* SAVAŞ HASÇELİK
 * Genetik Algoritmalar
 * Yüksek Lisans -- Hesaplamalı Bilimler
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenetikProje
{
    public partial class Form1 : Form
    {
        DialogResult result;
        public static string[] ilknesil;
        public static string[] caprazlama_sonucu;
        public static double[] uygunluk_sira;
        public static Microsoft.Office.Interop.Excel.Worksheet verimesafe;
        public static Microsoft.Office.Interop.Excel.Worksheet veritasima;
        public static string[] makineler = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };
        public static string[] makinesira = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
        public static string[] sonrakinesil;
        double[,] mesafeMatrisi = new double[14, 14];
        double[,] tasimaMatrisi = new double[14, 14];
        public static string[] uygunluklar;






        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            Excel.Application excell = new Microsoft.Office.Interop.Excel.Application();
            string yol = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            object misValue = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Workbook kitap = excell.Workbooks.Open(yol + @"\makine/makineverileri.xlsx");
            verimesafe = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
            veritasima = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[2];
            //MessageBox.Show((mesafe.UsedRange.Cells[9,14].Value * tasima.UsedRange.Cells[9, 14].Value).ToString());
            //durum.Text += "Excell bağlantısı gerçekleşti veriler çekiliyor";


            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {


                    mesafeMatrisi[i, j] = verimesafe.UsedRange.Cells[(i + 1), (j + 1)].Value;

                }

            }


            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {

                    tasimaMatrisi[i, j] = veritasima.UsedRange.Cells[(i + 1), (j + 1)].Value;

                }

            }

            kitap.Close(true, misValue, misValue);
            excell.Quit();






        }



        private void basla_Click(object sender, EventArgs e)
        {



            if ((radioButton1.Checked || radioButton2.Checked || radioButton3.Checked) & (radioButton4.Checked) & (radioButton5.Checked || radioButton6.Checked) & (radioButton7.Checked || radioButton8.Checked) & (radioButton9.Checked || radioButton10.Checked || radioButton11.Checked))
            {
                baslaa();

            }
            else
            {
                MessageBox.Show("Lütfen ayarlamaları düzgün şekilde yapın");
            }

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

                populasyon_uret(10);
            }

        }




        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {

                populasyon_uret(20);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {

                populasyon_uret(30);
            }
        }



        //pupulasyon başla

        public int populasyon_uret(int nekadar)
        {
            ilknesil = new string[nekadar];


            Random rnd = new Random();
            string[] populasyondizi = new string[10];

            string[] maklst = new string[ilknesil.Length];

            for (int i = 0; i < ilknesil.Length; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    string a = makinesira[rnd.Next(4, 14)];
                    if (populasyondizi.Contains(a) == false)
                    {

                        populasyondizi[j] = a;



                    }
                    else
                    {
                        j = j - 1;


                    }

                }




                maklst[i] = (makinesira[0] + "," + makinesira[1] + "," + makinesira[2] + "," + makinesira[3] + "," + populasyondizi[0] + "," + populasyondizi[1] + "," + populasyondizi[2] + "," + populasyondizi[3] + "," + populasyondizi[4] + "," + populasyondizi[5] + "," + populasyondizi[6] + "," + populasyondizi[7] + "," + populasyondizi[8] + "," + populasyondizi[9]);

                populasyondizi = new string[10];



            }
            ilknesil = maklst;


            return 0;

        }
        //populasyon bitir



        //uygunluk başla

        public void uygunlukFonk()

        {
            int i = 0;
            string[] uygunlukson = new string[ilknesil.Length];
            double toplam = 0;
            double[] uygunluklst = new double[14];
            string[] mk = new string[14];

            for (i = 0; i < ilknesil.Length; i++)
            {
                uygunlukson[i] = "";
                toplam = 0;

                mk = ilknesil[i].Split(',');

                for (int j = 0; j < 14; j++)

                {

                    int makines = int.Parse(mk[j]);
                    uygunluklst = new double[14];
                    for (int k = j; k < 14; k++)
                    {

                        uygunluklst[k] = mesafeMatrisi[j, k] * tasimaMatrisi[makines, int.Parse(mk[k])];


                    }


                    foreach (double a in uygunluklst)
                    {


                        toplam += a;

                    }


                }

                uygunlukson[i] = toplam.ToString();




            }



            uygunluklar = new string[ilknesil.Length];
            int iter = 0;
            foreach (string a in uygunlukson)
            {




                uygunluklar[iter] = uygunlukson[iter];
                iter++;
            }

            uygunluk_sira = new double[ilknesil.Length];
            i = 0;
            foreach (string b in uygunluklar)
            {

                uygunluk_sira[i] = double.Parse(b);
                i++;

            }


        }




        public void Secim()
        {
            int i = 0;

            double[] uygunlukRankkopya = uygunluk_sira;
            double[] yuzdeler = new double[ilknesil.Length];
            sonrakinesil = new string[ilknesil.Length];


            Array.Sort(uygunlukRankkopya);
            Array.Reverse(uygunlukRankkopya);
            double siralamatoplam = 0;
            for (i = 1; i <= ilknesil.Length; i++)
            {
                siralamatoplam += i;

            }





            double sira = 0;
            i = 0;
            foreach (double b in uygunlukRankkopya)
            {
                sira++;

                if (i == 0)
                {
                    yuzdeler[i] = Math.Round(sira / siralamatoplam, 4);
                }
                else
                {
                    yuzdeler[i] = Math.Round(sira / siralamatoplam, 4) + yuzdeler[i - 1];

                }




                i++;


            }

            i = 0;
            Random rnd = new Random();


            for (int say = 0; say < uygunlukRankkopya.Length; say++)
            {
                double rastdeger = rnd.NextDouble();

                if (rastdeger <= yuzdeler[say])
                {
                    sonrakinesil[i] = ilknesil[Array.IndexOf(uygunluklar, uygunlukRankkopya[say].ToString())];

                }
                else
                {
                    for (int sayy = 0; sayy < yuzdeler.Length; sayy++)
                    {
                        if (rastdeger <= yuzdeler[sayy])
                        {
                            sonrakinesil[i] = ilknesil[Array.IndexOf(uygunluklar, uygunlukRankkopya[sayy].ToString())];
                        }



                    }


                }



                i++;
            }













        }



        public void caprazlama()
        {

            int combocarp;

            if (radioButton5.Checked)
            {
                combocarp = 1;

            }

            else
            {
                combocarp = 2;

            }



            Random rnd = new Random();
            caprazlama_sonucu = new string[ilknesil.Length];


            int[] secilenindex = new int[2];








            switch (combocarp)
            {
                case 1:


                    for (int cap = 0; cap < (sonrakinesil.Length / 2); cap++)//caprazlama döngü başla



                    {
                        secilenindex = new int[2];

                        string[] secilenA = new string[14];
                        string[] secilenB = new string[14];
                        string[] cocuk1 = new string[14];
                        string[] cocuk2 = new string[14];
                        int secilenelemanA = rnd.Next(0, ilknesil.Length);
                        int secilenelemanB = rnd.Next(0, ilknesil.Length);



                        for (int j = 0; j < 14; j++)
                        {
                            secilenA = sonrakinesil[secilenelemanA].Split(',');
                            secilenB = sonrakinesil[secilenelemanB].Split(',');


                        }


                        int kacsecilsin = rnd.Next(1, 10);
                        int[] pozisyonsec = new int[kacsecilsin];
                        int[] nerdeB = new int[kacsecilsin];
                        int[] nerdeA = new int[kacsecilsin];

                        int sayac = 0;
                        while (sayac < kacsecilsin)
                        {
                            int pozisyon = rnd.Next(4, 13);
                            if (pozisyonsec.Contains(pozisyon))
                                continue;
                            pozisyonsec[sayac] = pozisyon;

                            nerdeB[sayac] = Array.IndexOf(secilenB, secilenA[pozisyonsec[sayac]].ToString());
                            sayac++;
                        }

                        //Array.Sort(pozisyonsec);
                        Array.Sort(nerdeB);

                        for (int i = 0; i < kacsecilsin; i++)
                        {
                            cocuk1[nerdeB[i]] = secilenA[pozisyonsec[i]];

                        }



                        for (int i = 0; i < 14; i++)
                        {
                            if (cocuk1[i] == null)
                            {
                                secilenB[i] = secilenB[i];


                            }
                            else
                            {
                                secilenB[i] = cocuk1[i];

                            }
                        }



                        string birlestirB = "";

                        for (int birlestir = 0; birlestir < 14; birlestir++)
                        {
                            if (birlestir == 13)
                            {

                                birlestirB += secilenB[birlestir];

                            }
                            else
                            {

                                birlestirB += secilenB[birlestir] + ",";


                            }

                        }


                        secilenindex[1] = secilenelemanB;


                        caprazlama_sonucu[secilenindex[1]] = birlestirB;




                    }//caprazlama döngü bitti

                    for (int aktar = 0; aktar < ilknesil.Length; aktar++)
                    {
                        if (caprazlama_sonucu[aktar] == null)
                        {
                            caprazlama_sonucu[aktar] = sonrakinesil[aktar];

                        }






                    }







                    break;


                case 2://sezgisel

                    for (int cap = 0; cap < (sonrakinesil.Length / 2); cap++)//caprazlama döngü başla



                    {

                        string[] secilenA = new string[14];
                        string[] secilenB = new string[14];
                        string[] cocuk1 = new string[14];
                        string[] cocuk2 = new string[14];
                        int secilenelemanA = rnd.Next(0, ilknesil.Length);
                        int secilenelemanB = rnd.Next(0, ilknesil.Length);


                        for (int j = 0; j < 14; j++)
                        {
                            secilenA = sonrakinesil[secilenelemanA].Split(',');
                            secilenB = sonrakinesil[secilenelemanB].Split(',');


                        }


                        int kacsecilsin = rnd.Next(2, 10);
                        int[] makinesec = new int[kacsecilsin];
                        int[] nerdeB = new int[kacsecilsin];
                        int[] nerdeA = new int[kacsecilsin];

                        for (int i = 0; i < kacsecilsin; i++)

                        {
                            int makinesecc = rnd.Next(4, 13);
                            if (makinesec.Contains(makinesecc))
                                continue;
                            makinesec[i] = makinesecc;
                            nerdeA[i] = Array.IndexOf(secilenA, makinesec[i].ToString());
                            nerdeB[i] = Array.IndexOf(secilenB, makinesec[i].ToString());
                        }


                        int sayac = 0;
                        foreach (int say in nerdeA)
                        {
                            cocuk1[nerdeA[sayac]] = secilenB[nerdeB[sayac]];
                            cocuk2[nerdeB[sayac]] = secilenA[nerdeA[sayac]];
                            sayac++;
                        }


                        for (int i = 0; i < 14; i++)
                        {
                            if (cocuk1[i] == null)
                            {
                                secilenA[i] = secilenA[i];


                            }
                            else
                            {
                                secilenA[i] = cocuk1[i];

                            }
                        }




                        for (int i = 0; i < 14; i++)
                        {
                            if (cocuk2[i] == null)
                            {
                                secilenB[i] = secilenB[i];


                            }
                            else
                            {
                                secilenB[i] = cocuk2[i];

                            }
                        }



                        string birlestirA = "";
                        string birlestirB = "";

                        for (int birlestir = 0; birlestir < 14; birlestir++)
                        {
                            if (birlestir == 13)
                            {
                                birlestirA += secilenA[birlestir];
                                birlestirB += secilenB[birlestir];

                            }
                            else
                            {
                                birlestirA += secilenA[birlestir] + ",";
                                birlestirB += secilenB[birlestir] + ",";


                            }

                        }

                        secilenindex[0] = secilenelemanA;
                        secilenindex[1] = secilenelemanB;
                        caprazlama_sonucu[secilenindex[0]] = birlestirA;

                        caprazlama_sonucu[secilenindex[1]] = birlestirB;




                    }


                    for (int aktar = 0; aktar < ilknesil.Length; aktar++)
                    {
                        if (caprazlama_sonucu[aktar] == null)
                        {
                            caprazlama_sonucu[aktar] = sonrakinesil[aktar];

                        }






                    }

                    break;



            }





        }



        //çaprazlama bitir



        //mutasyon başla


        public void mutasyon()
        {
            int combocarp;

            if (radioButton7.Checked)
            {
                combocarp = 1;

            }

            else
            {
                combocarp = 2;

            }

            //değer olarak 0,1 verildiğinden üretilen popülasyonun % 1 'i mutasyona uğrayacaktır 10 sa 1 20 ise 2 30 ise 3
            Random rnd = new Random();
            string[] mutasyonsonucu = caprazlama_sonucu;


            int[] secilenindex = new int[ilknesil.Length / 2];

            switch (combocarp)
            {
                case 2: //karsılıklı

                    for (int mutas = 0; mutas < ilknesil.Length * 0.01; mutas++)//mutasyon dongu başladı
                    {

                        string[] secilenA = new string[14];

                        int secilenelemanA = rnd.Next(0, ilknesil.Length);

                        secilenindex[mutas] = secilenelemanA;


                        for (int j = 0; j < 14; j++)
                        {
                            secilenA = caprazlama_sonucu[secilenelemanA].Split(',');

                        }

                        string bul1 = secilenA[rnd.Next(4, 14)];
                        string bul2 = secilenA[rnd.Next(4, 14)];


                        int a = Array.IndexOf(secilenA, bul1);
                        int b = Array.IndexOf(secilenA, bul2);


                        secilenA[a] = bul2;
                        secilenA[b] = bul1;

                        int say = 0;
                        string birlestir = "";
                        foreach (string yazdir in secilenA)
                        {

                            if (say == 13)
                            {
                                birlestir += yazdir;

                            }
                            else
                            {

                                birlestir += yazdir + ",";
                            }



                            say++;
                        }

                        mutasyonsonucu[secilenindex[mutas]] = birlestir;



                    }//mutasyon dongu bitti





                    break;


                case 1:
                    for (int mutas = 0; mutas < ilknesil.Length * 0.01; mutas++)//mutasyon dongu başladı
                    {

                        string[] secilenA = new string[14];

                        int secilenelemanA = rnd.Next(0, ilknesil.Length);

                        secilenindex[mutas] = secilenelemanA;


                        for (int j = 0; j < 14; j++)
                        {
                            secilenA = caprazlama_sonucu[secilenelemanA].Split(',');

                        }
                        int sec = rnd.Next(4, 12);
                        int sec2 = rnd.Next(sec + 1, 13);

                        string[] aralik = new string[(sec2 - sec)];
                        List<string> dizikopya = new List<string>(secilenA);
                        int sayac = 0;


                        for (int i = sec; i < sec2; i++)
                        {
                            aralik[sayac] = secilenA[i];
                            dizikopya.Remove(aralik[sayac]);
                            sayac++;

                        }


                        int dongusayi = dizikopya.Count - 1;
                        int neredenkopyala = rnd.Next(4, dongusayi);
                        string[] kalankopyala = new string[dizikopya.Count - neredenkopyala];
                        sayac = 0;
                        int sayac2 = neredenkopyala;
                        int dongudeger = dizikopya.Count;
                        for (int i = neredenkopyala; i < dongudeger; i++)
                        {

                            kalankopyala[sayac] = dizikopya[sayac2];

                            sayac2++;
                            sayac++;

                        }

                        for (int i = 0; i < kalankopyala.Length; i++)

                        {
                            dizikopya.Remove(kalankopyala[i]);

                        }

                        sayac = 0;
                        foreach (string aktar in aralik)
                        {

                            //secilenA[neredenkopyala] = aktar;
                            dizikopya.Add(aktar);



                            neredenkopyala++;


                        }

                        int basla = neredenkopyala;
                        sayac = 0;
                        for (int i = basla; i < secilenA.Length; i++)
                        {

                            //secilenA[i] = kalankopyala[sayac];
                            dizikopya.Add(kalankopyala[sayac]);

                            sayac++;


                        }


                        secilenA = dizikopya.ToArray();



                        int say = 0;
                        string birlestir = "";
                        foreach (string yazdir in secilenA)
                        {

                            if (say == 13)
                            {
                                birlestir += yazdir;

                            }
                            else
                            {

                                birlestir += yazdir + ",";
                            }



                            say++;
                        }

                        mutasyonsonucu[secilenindex[mutas]] = birlestir;




                    }//döngü bitti



                    break;



            }



            if (checkBox1.Checked == true)
            {
                int say = 0;




                foreach (int b in uygunluk_sira)
                {
                    if (say <= ilknesil.Length / 2)
                    {
                        ilknesil[say] = mutasyonsonucu[say];

                    }
                    else
                    {
                        ilknesil[say] = sonrakinesil[say];
                    }
                    say++;

                }





            }

            else
            {

                ilknesil = mutasyonsonucu;
            }


        }


        //mutasyon bitir

        //baslangıc fonfsıyonu



        public void baslaa()
        {
            int iterasyon = 0;
            if (radioButton9.Checked)
            {
                iterasyon = 500;

            }
            else if (radioButton10.Checked)
            {
                iterasyon = 1000;
            }

            else if (radioButton11.Checked)
            {
                iterasyon = 2000;

            }


            progressBar1.Maximum = iterasyon;

            double[] sonuclar = new double[iterasyon];
            string[] dizilisler = new string[iterasyon];
            double[] kopya = new double[iterasyon];

            for (int dongubasla = 0; dongubasla < iterasyon; dongubasla++)
            {




                uygunlukFonk();


                Array.Sort(uygunluk_sira);
                double eniyi = uygunluk_sira[0];





                ///
                string[] bul = new string[14];
                string bulunan = "";
                for (int j = 0; j < 14; j++)
                {
                    bul = ilknesil[Array.IndexOf(uygunluklar, eniyi.ToString())].Split(',');


                    if (j != 13)
                    {
                        bulunan += makineler[int.Parse(bul[j])] + ",";

                    }
                    else
                    {
                        bulunan += makineler[int.Parse(bul[j])];

                    }
                }



                foreach (double uygunluksayisi in uygunluk_sira)
                {

                    if (uygunluksayisi == 3111)
                    {

                        result = MessageBox.Show(dongubasla.ToString() + ". İterasyon da  uygunluğu " + uygunluksayisi.ToString() + "   dizilişi  =" + bulunan, "BULUNDU!!!! ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


                        if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Cancel)
                        {
                            MessageBox.Show("İterasyon Sonlandırılıyor...");

                            dongubasla = iterasyon - 1;


                        }
                    }



                }





                sonuclar[dongubasla] = eniyi;
                kopya[dongubasla] = eniyi;
                dizilisler[dongubasla] = ilknesil[Array.IndexOf(uygunluklar, eniyi.ToString())];
                Secim();


                caprazlama();
                mutasyon();


                if (dongubasla + 1 == iterasyon)
                {
                    sonuc.Text = "Bulabildiğimiz en Düşük uygunluk " + eniyi + "  <<<< Dizilişi>>>>  " + bulunan;
                }

                progressBar1.Value = dongubasla;






            }



        }
        //başlangıç fonsıyonu bitir


        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void sonuc_TextChanged(object sender, EventArgs e)
        {
            sonuc.Select(sonuc.TextLength, 0);
            sonuc.ScrollToCaret();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
