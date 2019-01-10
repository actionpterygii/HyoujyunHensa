using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //saikoro wo futte 1 no deru kakuritu ga 1/6 dearutoki no n kai saikorowo
        //futte 1 no deru wariai wo kanngaeru
        //nansyuruikano n no atai ni tuite 1 no deru wariai x to
        //sono wariai ga x tonaru kakuritu p toko kannkei wo
        //keisann shi hyou ni matome x-p no gurafu wo kaku


        //textbox kara
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                double d;
                if (double.TryParse(textBox1.Text, out d))
                {
                    d = Convert.ToDouble(textBox1.Text);
                    SG(d);
                    if (d <= 100 && d >= 0)
                    {
                        trackBar1.Value = Convert.ToInt32(d);
                    }
                }
            }
        }


        //atai hyouji to gurafu byouga
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
            double n = trackBar1.Value;

            double iti = 0.1666666667;
            double igi = 0.8333333334;

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            Series HHg = new Series();
            HHg.ChartType = SeriesChartType.Spline;
            ChartArea area1 = new ChartArea();
            HHg.Color = Color.FromArgb(255, 0, 0, 220);

            area1.AxisX.Maximum = 1;
            area1.AxisX.Minimum = 0;
            area1.AxisX.Interval = 0.2;
            area1.AxisX.MajorGrid.LineColor = Color.LightGray;
            area1.AxisX.MajorGrid.Interval = 0.05;

            area1.AxisY.Maximum = 1;
            area1.AxisY.Minimum = 0;
            area1.AxisY.Interval = 0.2;
            area1.AxisY.MajorGrid.LineColor = Color.LightGray;
            area1.AxisY.MajorGrid.Interval = 0.05;

            for (double m = 0; m <= n; m++)
            {
                double X = Convert.ToDouble(SG((m / n) * 1000.0) / 1000.0);
                double nCm = (KJ(n, 1)) / (KJ(m, 1) * KJ(n - m, 1));
                double drk = (Math.Pow(iti, m));
                double dnk = (Math.Pow(igi, n - m));
                double Y = nCm * drk * dnk;
                HHg.Points.Add(new DataPoint(X, Y));
            }
            chart1.ChartAreas.Add(area1);
            chart1.Series.Add(HHg);
        }


        //kaijo suru yatu
        double KJ(double a, double i)
        {
            if (a == 0)
            {
                return i;
            }
            else
            {
                return KJ(a - 1, a * i);
            }
        }


        //shisyagonyu suru yatu
        int SG(double b)
        {
            return (int)(b < 0.0 ? b - 0.5 : b + 0.5);
        }


        //hozon suru yatu
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Bmp;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                }
                chart1.SaveImage(sfd.FileName, format);
            }
        }
    }
}
