using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using FasonTakip;
using System.IO;
using System.Data.SqlClient;
using System.Collections;

namespace FcResimDekubeProgrami
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }
        // Fatih Çetinkaya - www.fcyazilim.com - info@fcyazilim.com
        private static float BoyutG = 0;
        private static float BoyutY = 0;
        private static string YapilacakIslem;
        private static int LogoY;
        private static int LogoG;
        private static float LogoYerlesimx;
        private static float LogoYerlesimy;
        private static float YaziYerlesimx;
        private static float YaziYerlesimy;
        private static string Fontum = "Verdana";
        private static Color Rengim = Color.Black;
        private static float FontBoyut = 14;
        private static string ResimYol, AdresLink, Logo;
        ArrayList diziListe = new ArrayList();
        string[] dizi;
        string[] Yenidizi;
        private void YaziliResimBoyutlandır(string Yenisimimiz)
        {
            string yol = Application.StartupPath + "\\" + "Resimler";
            if (Directory.Exists(yol) == false)
            {
                string Klasor = "Resimler";
                Directory.CreateDirectory(Application.StartupPath + "\\" + Klasor);
            }
            Bitmap OrjinalResim = new Bitmap(ResimYol);
            Bitmap ResimiBoyutlandir = new Bitmap(OrjinalResim);
            Graphics YaziYaz = Graphics.FromImage(ResimiBoyutlandir);
            SolidBrush fircamiz = new SolidBrush(Rengim);
            Font Fontumuz = new Font(Fontum, FontBoyut);
            PointF coor = new PointF(YaziYerlesimx, YaziYerlesimy);
            YaziYaz.DrawString(yazibox.Text, Fontumuz, fircamiz, coor);
            ResimiBoyutlandir.Save(Application.StartupPath + "\\Resimler\\" + Yenisimimiz, ImageFormat.Jpeg);
            OrjinalResim.Dispose();
            System.GC.SuppressFinalize(OrjinalResim);
            System.GC.SuppressFinalize(ResimiBoyutlandir);

        }
        private void LogoluResimBoyutlandır(string Yenisimimiz)
        {
            string yol = Application.StartupPath + "\\" + "Resimler";
            if (Directory.Exists(yol) == false)
            {
                string Klasor = "Resimler";
                Directory.CreateDirectory(Application.StartupPath + "\\" + Klasor);
            }
            Bitmap OrjinalResim = new Bitmap(ResimYol);
            Bitmap ResimiBoyutlandir = new Bitmap(OrjinalResim);
            Graphics g = Graphics.FromImage(ResimiBoyutlandir);
            g.SmoothingMode = SmoothingMode.Default;
            Image img = Image.FromFile(Logo);
            g.DrawImage(img, LogoYerlesimx, LogoYerlesimy, LogoG, LogoY);
            ResimiBoyutlandir.Save(Application.StartupPath + "\\Resimler\\" + Yenisimimiz, ImageFormat.Jpeg);
            OrjinalResim.Dispose();
            System.GC.SuppressFinalize(OrjinalResim);
            System.GC.SuppressFinalize(ResimiBoyutlandir);
        }
        private void SadeceBoyutResimBoyutlandır(string Yenisimimiz)
        {
            string yol = Application.StartupPath + "\\" + "Resimler";
            if (Directory.Exists(yol) == false)
            {
                string Klasor = "Resimler";
                Directory.CreateDirectory(Application.StartupPath + "\\" + Klasor);
            }
            float ResimGenislik = 0;
            float ResimYukseklik = 0;
            Bitmap OrjinalResim = new Bitmap(ResimYol);
            ResimGenislik = BoyutG;
            ResimYukseklik = BoyutY;
            Size YeniResimBoyut = new Size(Convert.ToInt32(ResimGenislik), Convert.ToInt32(ResimYukseklik));
            Bitmap ResimiBoyutlandir = new Bitmap(OrjinalResim, YeniResimBoyut);
            ResimiBoyutlandir.Save(Application.StartupPath + "\\Resimler\\" + Yenisimimiz, ImageFormat.Jpeg);
            OrjinalResim.Dispose();
            System.GC.SuppressFinalize(OrjinalResim);
            System.GC.SuppressFinalize(ResimiBoyutlandir);
        }
        private void ResimDuzenleme()
        {
            try
            {
                if (DekubeIslemComboBox.Text == "Lütfen İşlem Seçiniz")
                {
                    MessageBox.Show("Lütfen İşlem Seçiniz.", "İşlem Seçimi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (YapilacakIslem == "boyut")
                {
                    for (int i = 0; i < ResimSecFileDialog.FileNames.Length; i++)
                    {
                        string Yeniisim = BoyutG + "x" + BoyutY + "_" + ResimSecFileDialog.SafeFileNames[i].ToString();
                        ResimYol = ResimSecFileDialog.FileNames[i].ToString();
                        SadeceBoyutResimBoyutlandır(Yeniisim);
                        listBox2.Items.Add(ResimYol);
                    }
                    MessageBox.Show("Resimler düzenlenleme işlemi tamamlandı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (YapilacakIslem == "logo")
                {
                    for (int i = 0; i < ResimSecFileDialog.FileNames.Length; i++)
                    {
                        string Yeniisim = BoyutG + "x" + BoyutY + "_" + ResimSecFileDialog.SafeFileNames[i].ToString();
                        ResimYol = ResimSecFileDialog.FileNames[i].ToString();
                        PictureBox pb = new PictureBox();
                        Image loadedImage = Image.FromFile(ResimYol);
                        pb.Height = loadedImage.Height;
                        pb.Width = loadedImage.Width;
                        if (LogoIslemleriComboBox.Text == "Sol Üstte Görünsün")
                        {
                            LogoYerlesimx = 0;
                            LogoYerlesimy = 0;
                        }
                        else if (LogoIslemleriComboBox.Text == "Sol Altda Görünsün")
                        {
                            LogoYerlesimx = 0;
                            LogoYerlesimy = (float)pb.Height - LogoY;
                        }
                        else if (LogoIslemleriComboBox.Text == "Sağ Üstte Görünsün")
                        {
                            LogoYerlesimx = (float)pb.Width - LogoG;
                            LogoYerlesimy = 0;
                        }
                        else if (LogoIslemleriComboBox.Text == "Sağ Altda Görünsün")
                        {
                            LogoYerlesimy = (float)pb.Height - LogoY;
                            LogoYerlesimx = (float)pb.Width - LogoG;
                        }
                        else
                        {
                            LogoYerlesimx = (float)pb.Width / 2 - (LogoG / 2);
                            LogoYerlesimy = (float)pb.Height / 2 - (LogoY / 2);

                        }
                        LogoluResimBoyutlandır(Yeniisim);
                        listBox2.Items.Add(ResimYol);
                    }
                    MessageBox.Show("Resimler düzenlenleme işlemi tamamlandı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (YapilacakIslem == "yazı")
                {
                    for (int i = 0; i < ResimSecFileDialog.FileNames.Length; i++)
                    {
                        string Yeniisim = BoyutG + "x" + BoyutY + "_" + ResimSecFileDialog.SafeFileNames[i].ToString();
                        ResimYol = ResimSecFileDialog.FileNames[i].ToString();
                        PictureBox pb = new PictureBox();
                        Image loadedImage = Image.FromFile(ResimYol);
                        pb.Height = loadedImage.Height;
                        pb.Width = loadedImage.Width;
                        if (YaziIslemlericomboBox.Text == "Sol Üstte Görünsün")
                        {
                            YaziYerlesimx = 0;
                            YaziYerlesimy = 0;
                        }
                        else if (YaziIslemlericomboBox.Text == "Sol Altda Görünsün")
                        {
                            YaziYerlesimx = 0;
                            YaziYerlesimy = pb.Height - (yazibox.TextLength * FontBoyut);
                        }
                        else if (YaziIslemlericomboBox.Text == "Sağ Üstte Görünsün")
                        {
                            YaziYerlesimx = pb.Width - (yazibox.TextLength * FontBoyut);
                            YaziYerlesimy = 0;
                        }
                        else if (YaziIslemlericomboBox.Text == "Sağ Altda Görünsün")
                        {
                            YaziYerlesimx = pb.Width - (yazibox.TextLength * FontBoyut);
                            YaziYerlesimy = pb.Height - 40;
                        }
                        else
                        {
                            YaziYerlesimx = pb.Width / 2 - (yazibox.TextLength * 2);
                            YaziYerlesimy = pb.Height / 2;
                        }
                        YaziliResimBoyutlandır(Yeniisim);
                        listBox2.Items.Add(ResimYol);
                    }
                    MessageBox.Show("Resimler düzenlenleme işlemi tamamlandı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void programHakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            tabControl1.Enabled = false;
            baslabtn.Text = "Lütfen Bekleyiniz...";
            ResimDuzenleme();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogoImg.ImageLocation = null;
            yazibox.Clear();
            listBox1.Items.Clear();
            logonumeG.Value = 0;
            logonumeY.Value = 0;
            LogoYerlesimx = 0;
            LogoYerlesimy = 0;
            YaziYerlesimx = 0;
            YaziYerlesimy = 0;
            BoyutG = 0;
            BoyutY = 0;
            LogoY = 0;
            LogoG = 0;
            this.Refresh();
            baslabtn.Text = "BAŞLA";
            tabControl1.Enabled = true;
            listBox2.Items.Clear();
            tabPage2.Enabled = false;
            tabPage3.Enabled = false;
            tabControl1.SelectTab("tabPage1");
            string myPath = Application.StartupPath + "\\Resimler";
            ResimImg.ImageLocation = "";
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = myPath;
            prc.Start();
        }
        private void AnaMenu_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            tabPage2.Enabled = false;
            tabPage3.Enabled = false;
        }
        private void AnaMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
        private void gozatbtnresimbutton_Click(object sender, EventArgs e)
        {
            try
            {
                ResimSecFileDialog.Title = "Lütfen Resim Seçiniz";
                ResimSecFileDialog.Filter = " (*.jpg)|*.jpg|(*.png)|*.png";
                DialogResult soru = ResimSecFileDialog.ShowDialog();
                if (soru == DialogResult.OK)
                {
                    dizi = new string[ResimSecFileDialog.FileNames.Length];
                    for (int i = 0; i < ResimSecFileDialog.FileNames.Length; i++)
                    {
                        dizi[i] = ResimSecFileDialog.FileNames[i].ToString();
                        SeciliResimCikartBtn.Enabled = true;
                    }
                    listBox1.Items.AddRange(dizi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count != 0)
                {
                    ResimImg.ImageLocation = listBox1.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lütfen Resim Seçiniz.");
            }
        }
        private void DekubeIslemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (DekubeIslemComboBox.SelectedIndex)
                {
                    case 0: // Boyutlandırma İşlemi
                        boyutgroup.Location = new Point(240, 95);
                        logogroup.Visible = false;
                        yazigroup.Visible = false;
                        boyutgroup.Visible = true;
                        break;
                    case 1: //Logo İşlemi
                        logogroup.Location = new Point(240, 95);
                        logogroup.Visible = true;
                        boyutgroup.Visible = false;
                        yazigroup.Visible = false;
                        break;
                    case 2: //Yazı İşlemi
                        yazigroup.Location = new Point(240, 95);
                        logogroup.Visible = false;
                        boyutgroup.Visible = false;
                        yazigroup.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void SeciliResimCikartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                diziListe.AddRange(dizi);
                int sayi = Convert.ToInt32(listBox1.SelectedIndex.ToString());
                diziListe.RemoveAt(sayi);
                Yenidizi = diziListe.ToArray(Type.GetType("System.String")) as string[];
                listBox1.Items.Clear();
                listBox1.Items.AddRange(Yenidizi);
                dizi = Yenidizi;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ResimSecDevamEtBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count != 0)
                {
                    tabPage2.Enabled = true;
                    tabControl1.SelectTab("tabPage2");
                }
                else
                {
                    MessageBox.Show("Lütfen Resim Seçiniz.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void BoyutIslemleriDevamBtn_Click(object sender, EventArgs e)
        {
            if (BoyutNumericGenislik.Value > 0 && BoyutNumericYukseklik.Value > 0)
            {
                BoyutG = (float)BoyutNumericGenislik.Value;
                BoyutY = (float)BoyutNumericYukseklik.Value;
                YapilacakIslem = "boyut";
                tabPage3.Enabled = true;
                tabControl1.SelectTab("tabPage3");
            }
            else
            {
                MessageBox.Show("Lütfen Genişlik ve Yükselik Ayarlarını Seçiniz.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void YaziIslemleriDevamBtn_Click(object sender, EventArgs e)
        {
            //            private void yazisolustradio_Click(object sender, EventArgs e)
            //{
            //    if (BoyutG == 0 && BoyutY == 0)
            //    {
            //        MessageBox.Show("Boyut Seçiniz.", "Boyut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        yazisolustradio.Checked = false;
            //    }
            //    else
            //    {
            //        YaziYerlesimx = 0;
            //        YaziYerlesimy = 0;
            //    }
            //}
            //private void yazisolaltradio_Click(object sender, EventArgs e)
            //{
            //    YaziYerlesimx = 0;
            //    YaziYerlesimy = 0;
            //    if (BoyutG == 0 && BoyutY == 0)
            //    {
            //        MessageBox.Show("Boyut Seçiniz.", "Boyut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        yazisolaltradio.Checked = false;
            //    }
            //    else
            //    {
            //        if (boyut320radio.Checked == true)
            //        {
            //            YaziYerlesimx = 0;
            //            YaziYerlesimy = 240 - (yazibox.TextLength * FontBoyut);
            //        }
            //        else if (boyut640radio.Checked == true)
            //        {
            //            YaziYerlesimx = 0;
            //            YaziYerlesimy = 480 - (yazibox.TextLength * FontBoyut);
            //        }
            //        else if (boyut800radio.Checked == true)
            //        {
            //            YaziYerlesimx = 0;
            //            YaziYerlesimy = 600 - (yazibox.TextLength * FontBoyut);
            //        }
            //        else if (boyut1024radio.Checked == true)
            //        {
            //            YaziYerlesimx = 0;
            //            YaziYerlesimy = 768 - (yazibox.TextLength * FontBoyut);
            //        }
            //    }
            //}
            //private void yazisagaltradio_Click(object sender, EventArgs e)
            //{
            //    YaziYerlesimx = 0;
            //    YaziYerlesimy = 0;
            //    if (BoyutG == 0 && BoyutY == 0)
            //    {
            //        MessageBox.Show("Boyut Seçiniz.", "Boyut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        yazisagaltradio.Checked = false;
            //    }
            //    else
            //    {
            //        if (boyut320radio.Checked == true)
            //        {
            //            YaziYerlesimx = 320 - (yazibox.TextLength * FontBoyut);
            //            YaziYerlesimy = 200;
            //        }
            //        else if (boyut640radio.Checked == true)
            //        {
            //            YaziYerlesimx = 640 - (yazibox.TextLength * FontBoyut);
            //            YaziYerlesimy = 440;
            //        }
            //        else if (boyut800radio.Checked == true)
            //        {
            //            YaziYerlesimx = 800 - (yazibox.TextLength * FontBoyut);
            //            YaziYerlesimy = 560;
            //        }
            //        else if (boyut1024radio.Checked == true)
            //        {
            //            YaziYerlesimx = 1024 - (yazibox.TextLength * FontBoyut);
            //            YaziYerlesimy = 728;
            //        }
            //    }
            //}
            //private void yaziortaradio_Click(object sender, EventArgs e)
            //{
            //    YaziYerlesimx = 0;
            //    YaziYerlesimy = 0;
            //    if (BoyutG == 0 && BoyutY == 0)
            //    {
            //        MessageBox.Show("Boyut Seçiniz.", "Boyut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        yaziortaradio.Checked = false;
            //    }
            //    else
            //    {
            //        if (boyut320radio.Checked == true)
            //        {
            //            YaziYerlesimx = 160 - (yazibox.TextLength * 2);
            //            YaziYerlesimy = 120;
            //        }
            //        else if (boyut640radio.Checked == true)
            //        {
            //            YaziYerlesimx = 320 - (yazibox.TextLength * 2);
            //            YaziYerlesimy = 240;
            //        }
            //        else if (boyut800radio.Checked == true)
            //        {
            //            YaziYerlesimx = 400 - (yazibox.TextLength * 2);
            //            YaziYerlesimy = 300;
            //        }
            //        else if (boyut1024radio.Checked == true)
            //        {
            //            YaziYerlesimx = 512 - (yazibox.TextLength * 2);
            //            YaziYerlesimy = 384;
            //        }
            //    }
            //}
            //private void yazisagustradio_Click(object sender, EventArgs e)
            //{
            //    YaziYerlesimx = 0;
            //    YaziYerlesimy = 0;
            //    if (BoyutG == 0 && BoyutY == 0)
            //    {
            //        MessageBox.Show("Boyut Seçiniz.", "Boyut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        yazisagustradio.Checked = false;
            //    }
            //    else
            //    {
            //        if (boyut320radio.Checked == true)
            //        {
            //            YaziYerlesimx = 320 - (yazibox.TextLength * FontBoyut);
            //            YaziYerlesimy = 0;
            //        }
            //        else if (boyut640radio.Checked == true)
            //        {
            //            YaziYerlesimx = 640 - (yazibox.TextLength * FontBoyut);
            //            YaziYerlesimy = 0;
            //        }
            //        else if (boyut800radio.Checked == true)
            //        {
            //            YaziYerlesimx = 800 - (yazibox.TextLength * FontBoyut);
            //            YaziYerlesimy = 0;
            //        }
            //        else if (boyut1024radio.Checked == true)
            //        {
            //            YaziYerlesimx = 1024 - (yazibox.TextLength * FontBoyut);
            //            YaziYerlesimy = 0;
            //        }
            //    }
            //}





            if (!string.IsNullOrEmpty(yazibox.Text) && YaziIslemlericomboBox.Text != "Lütfen Seçiniz")
            {
                YapilacakIslem = "yazı";
                tabPage3.Enabled = true;
                tabControl1.SelectTab("tabPage3");
            }
            else
            {
                MessageBox.Show("Lütfen İşlem Seçiniz.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void LogoIslemleriDevamBtn_Click(object sender, EventArgs e)
        {
            if (LogoImg.ImageLocation != null && LogoIslemleriComboBox.Text != "Lütfen Seçiniz")
            {
                YapilacakIslem = "logo";
                tabPage3.Enabled = true;
                tabControl1.SelectTab("tabPage3");

            }
            else
            {
                MessageBox.Show("Lütfen İşlem Seçiniz.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                if (listBox1.Items.Count != 0)
                {
                    tabPage2.Enabled = true;
                    tabControl1.SelectTab("tabPage2");
                }
                else
                {
                    MessageBox.Show("Lütfen Resim Seçiniz.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    tabPage2.Enabled = false;
                    tabControl1.SelectTab("tabPage1");
                }
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                if (!string.IsNullOrEmpty(YapilacakIslem))
                {
                    tabPage3.Enabled = true;
                    tabControl1.SelectTab("tabPage3");
                }
                else
                {
                    MessageBox.Show("Lütfen İşlem Seçiniz.!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (listBox1.Items.Count == 0)
                    {
                        tabControl1.SelectTab("tabPage1");
                    }
                    else
                    {
                        tabPage3.Enabled = false;
                        tabControl1.SelectTab("tabPage2");
                    }
                }
            }
        }
        private void gozatbtnlogo_Click(object sender, EventArgs e)
        {
            LogoSecFileDialog.Title = "Lütfen Logo Seçiniz";
            LogoSecFileDialog.Filter = " (*.jpg)|*.jpg|(*.png)|*.png";
            DialogResult sorc = LogoSecFileDialog.ShowDialog();
            if (sorc == DialogResult.OK)
            {
                Bitmap orjinalResim = new Bitmap(LogoSecFileDialog.FileName);
                if (logonumeG.Maximum < orjinalResim.Width || logonumeY.Maximum < orjinalResim.Height)
                {
                    MessageBox.Show("Lütfen logonun Yükseklik / Genişlik ölçülerini küçültünüz. ", "Logo Boyut Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    logonumeG.Value = orjinalResim.Width;
                    logonumeY.Value = orjinalResim.Height;
                    LogoImg.ImageLocation = LogoSecFileDialog.FileName;
                    Logo = LogoSecFileDialog.FileName.ToString();
                    LogoG = Convert.ToInt32(logonumeG.Value);
                    LogoY = Convert.ToInt32(logonumeY.Value);
                }
            }
        }
        private void renkbtn_Click(object sender, EventArgs e)
        {
            DialogResult soru = colorDialog1.ShowDialog();
            if (soru == DialogResult.OK)
            {
                Rengim = colorDialog1.Color;
                yazibox.ForeColor = colorDialog1.Color;
            }
        }
        private void fontbtn_Click(object sender, EventArgs e)
        {
            DialogResult sor = fontDialog1.ShowDialog();
            if (sor == DialogResult.OK)
            {
                Fontum = fontDialog1.Font.Name.ToString();
                FontBoyut = fontDialog1.Font.Size;
                yazibox.Font = fontDialog1.Font;
            }
        }
        private void baslabtn_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
