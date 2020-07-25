using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        int N = 6, mit = 2000, klm = 2;
        int L, i, k1, Ne, Ngr;
        double Eps, h, dxmax, al, bl;
        double[] Yg = new double[800];
        double[] Xg = new double[800];
        double[] Yg1 = new double[800];
        double[] f = new double[9];
        double[] K = new double[9];
        double[] dx = new double[9];
        double[,] ia = new double[9, 9];

        double[] Xe = new double[] {8.51602E+01, 8.735245E+01,8.929174E+01,9.114671E+01,9.274874E+01,
              9.401349E+01,9.544688E+01,9.578415E+01,9.738617E+01,9.806071E+01,9.881956E+01,
           1.0008432E+02,1.0160202E+02,1.039629E+02,1.0581788E+02,1.0834739E+02,1.118887E+02,
           1.1416526E+02,1.1585160E+02,1.1661046E+02,1.1770658E+02,1.1956155E+02,1.2082631E+02,
           1.2158516E+02,1.2352445E+02,1.2546374E+02,1.2698145E+02,1.2917369E+02,1.3102867E+02,
           1.3229342E+02,1.3465430E+02,1.3591906E+02,1.3844857E+02,1.4072513E+02,1.4401349E+02,
           1.4721754E+02,1.5286678E+02,1.5868465E+02,1.6627319E+02,1.7841484E+02,1.8701518E+02,
           1.9915683E+02,2.1576728E+02,2.3473862E+02,2.6087690E+02,2.9519393E+02};
        double[] Ye = new double[]{2.409070E+00,3.448280E+00,5.999060E+00,9.541800E+00,1.388758E+01,1.761927E+01,
          2.281530E+01,2.678318E+01,3.278224E+01,3.684459E+01,3.930090E+01,4.034010E+01,4.100142E+01,
          4.095418E+01,4.038734E+01,4.152102E+01,4.633916E+01,5.040151E+01,5.592820E+01,6.079358E+01,
          6.447803E+01,7.482286E+01,7.789325E+01,7.987718E+01,8.134152E+01,8.049126E+01,7.817667E+01,
          7.057156E+01,6.017950E+01,4.657534E+01,3.401039E+01,2.616911E+01,1.880019E+01,1.341521E+01,
          8.786020E+00,6.188000E+00,4.251300E+00,3.070380E+00,2.786960E+00,2.172890E+00,1.700520E+00,
          1.794990E+00,1.747760E+00,1.653280E+00,1.417100E+00,1.322630E+00 };
        double[] Xe1 = new double[] {8.51602E+01, 8.735245E+01,8.929174E+01,9.114671E+01,9.274874E+01,9.401349E+01,9.544688E+01,
                                     9.578415E+01,9.738617E+01,9.806071E+01,9.881956E+01,1.0008432E+02,1.0160202E+02,1.039629E+02,
                                     1.0581788E+02,1.0834739E+02,1.118887E+02,1.1416526E+02,1.1585160E+02,1.1661046E+02,1.1770658E+02,
                                     1.1956155E+02,1.2082631E+02,1.2158516E+02,1.2352445E+02,1.2546374E+02,1.2698145E+02,1.2917369E+02,
                                     1.3102867E+02,1.3229342E+02,1.3465430E+02,1.3591906E+02,1.3844857E+02,1.4072513E+02,1.4401349E+02,
                                     1.4721754E+02,1.5286678E+02,1.5868465E+02,1.6627319E+02,1.7841484E+02,1.8701518E+02,
                                     1.9915683E+02,2.1576728E+02,2.3473862E+02,2.6087690E+02,2.9519393E+02};
        double[] Ye1 = new double[]{17.4181e+00, 22.1803e+00, 26.6138, 30.8242, 34.2575, 40.7334, 39.2018, 39.7226, 41.8524,   42.5726, 43.2565,
                             44.1099, 44.7109, 45.0047, 45.0637, 45.6569, 49.092, 53.5122, 57.8549, 60.0425,   63.3757, 67.1886, 73.0045,
                             77.1352, 79.7062, 82.5624, 83.3644, 82.2558,79.5044, 72.9412, 71.4549,    68.5322 ,60.5878, 60.6974,58.9252,
                             57.97, 55.3792, 32.3226 ,18.2429, 0.393753, 0.013452, 10.52591e-005, 2.68013e-010, 9.72552e-018, 4.61836e-031, 5.83547e-054};
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            double dxmax = 0;
            int choise = Convert.ToInt32(numericUpDown1.Value);
            Ne = Convert.ToInt32(textBox1.Text);
            Ngr = Convert.ToInt32(textBox3.Text);
            
            if (choise == 2)
            {
                K = new double[7];
                al = Xe[0];
                bl = Xe[Ne - 1];
                L = 120;
                dataGridView1.RowCount = Ne;
                for (int i = 0; i <= Ne - 1; i++)
                {
                    dataGridView1[0, i].Value = Xe[i];
                    dataGridView1[1, i].Value = Ye[i];
                }

                do
                {
                    Eps = 1E-7;
                    h = 0.0000001 * Eps;
                    K[0] = 79.0;
                    K[1] = 0.005;
                    K[2] = 127.0;
                    K[3] = 38.0;
                    K[4] = 0.004;
                    K[5] = 102.0;
                    k1 = 0;
                    do
                    {
                        FM(Xe, Ye, K, out f);
                        JAC(6, Ne, out ia, K);
                        gauss(ia, f, out dx, 6);
                        k1 = k1 + 1;
                        dxmax = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            K[i] = K[i] - 0.9 * dx[i];

                            if (Math.Abs(dx[i]) > dxmax) dxmax = Math.Abs(dx[i]);
                        }
                        if (dxmax < Eps) { for (int i = 0; i < N; i++); }
                    } while (k1 > mit);
                    L = L - 1;
                } while (L > klm);
                h = (Xe[Ne - 1] - Xe[0]) / Ngr;
                Xg[0] = Xe[0];
                for (int i = 0; i < Ngr; i++)
                {
                    Yg[i] = K[0] * Math.Exp(-K[1] * Math.Pow(Xg[i] - K[2], 2)) + K[3] * Math.Exp(-K[4] * Math.Pow(Xg[i] - K[5], 2));
                    Xg[i + 1] = Xg[i] + h;
                }
                for (int i = 0; i < 6; i++)
                    dataGridView2[0, i].Value = Math.Round(K[i], 5);
                Graphic(pictureBox1, Ne);
            }
            else
            {
                N = 8;
                K = new double[9];
                al = Xe[0];
                bl = Xe[Ne - 1];
                L = 120;
                dataGridView1.RowCount = Ne;
                for (int i = 0; i <= Ne - 1; i++)
                {
                    dataGridView1[0, i].Value = Xe1[i];
                    dataGridView1[1, i].Value = Ye1[i];
                }

                do
                {
                    Eps = 1E-7;
                    h = 0.0000001 * Eps;
                    K[0] = 42;
                    K[1] = 0.004;
                    K[2] = 100;
                    K[3] = 79;
                    K[4] = 0.005;
                    K[5] = 127;
                    K[6] = 50;
                    K[7] = 0.006;
                    K[8] = 150;
                    k1 = 0;
                    do
                    {
                        FM1(Xe1, Ye1, K, out f);
                        JAC1(N, Ne, out ia, K);
                        gauss(ia, f, out dx, N);
                        k1 = k1 + 1;
                        dxmax = 0;
                        for (int i = 0; i < N; i++)
                        {
                            K[i] = K[i] - 0.9 * dx[i];

                            if (Math.Abs(dx[i]) > dxmax) dxmax = Math.Abs(dx[i]);
                        }
                        if (dxmax < Eps) { for (int i = 0; i < N; i++); }
                    } while (k1 > mit);
                    L = L - 1;
                } while (L > klm);
                h = (Xe1[Ne - 1] - Xe1[0]) / Ngr;
                Xg[0] = Xe1[0];
                
                for (int i = 0; i < Ngr; i++)
                {
                    Yg1[i] = K[0] * Math.Exp(-K[1] * Math.Pow(Xg[i] - K[2], 2)) + K[3] * Math.Exp(-K[4] * Math.Pow(Xg[i] - K[5], 2))
                          + K[6] * Math.Exp(-K[7] * Math.Pow(Xg[i] - K[8], 2));
                    Xg[i + 1] = Xg[i] + h;
                }
                dataGridView2.RowCount = 9;
                dataGridView2.Height = 230;
                for (int i = 0; i <= N; i++)
                    dataGridView2[0, i].Value = Math.Round(K[i], 5);

                Graphic1(pictureBox1, Ne);
            }
        }
        public void gauss(double[,] A, double[] B, out double[] X, int N)
        {
            int i, j, k;
            double R;
            X = new double[N];
            if (N == 1)
                if (Math.Abs(A[0, 0]) < 1E-9)
                {

                    return;
                }
                else
                {
                    X[0] = B[0] / A[0, 0];
                    MessageBox.Show("Матриця вироджена");
                    return;
                }
            for (i = 0; i < N - 1; i++)
            {

                k = i;
                R = Math.Abs(A[i, i]);
                for (j = i + 1; j < N; j++)
                    if (Math.Abs(A[j, i]) >= R)
                    {
                        k = j;
                        R = Math.Abs(A[j, i]);
                    }
                if (R <= 1E-9)
                {
                    MessageBox.Show("Матриця вироджена");
                    return;
                }

                if (k != i)
                {
                    R = B[k];
                    B[k] = B[i];
                    B[i] = R;
                    for (j = i; j < N; j++)
                    {
                        R = A[k, j];
                        A[k, j] = A[i, j];
                        A[i, j] = R;
                    }
                }
                R = A[i, i];
                B[i] = B[i] / R;
                for (j = 0; j < N; j++)
                    A[i, j] = A[i, j] / R;
                for (k = i + 1; k < N; k++)
                {
                    R = A[k, i];
                    B[k] = B[k] - R * B[i];
                    A[k, i] = 0;
                    for (j = i + 1; j < N; j++)
                        A[k, j] = A[k, j] - R * A[i, j];
                }

            }
            if (Math.Abs(A[N - 1, N - 1]) <= 1E-9)
            {
                MessageBox.Show("Матриця вироджена");
                return;
            }

            X[N - 1] = B[N - 1] / A[N - 1, N - 1];
            for (i = N - 2; i >= 0; i--)
            {
                R = B[i];
                for (j = i + 1; j < N; j++)
                    R = R - A[i, j] * X[j];
                X[i] = R;
            }
        }
        private void FM(double[] x, double[] y, double[] K, out double[] f)
        {
            f = new double[6];
            double s1 = 0, s2 = 0, s3 = 0, s4 = 0, s5 = 0, s6 = 0, r1 = 0, r2 = 0, R = 0;
            for (int i = 0; i < Ne; i++)
            {
                r1 = Math.Exp(-K[1] * Math.Pow(x[i] - K[2], 2));
                r2 = Math.Exp(-K[4] * Math.Pow(x[i] - K[5], 2));
                R = K[0] * r1 + K[3] * r2 - y[i];
                s1 = s1 + R * r1;
                s2 = s2 + R * r1 * K[0] * (-Math.Pow(x[i] - K[2], 2));
                s3 = s3 + R * 2 * K[0] * K[1] * (x[i] - K[2]) * r1;
                s4 = s4 + R * r2;
                s5 = s5 + R * r2 * K[3] * (-Math.Pow(x[i] - K[5], 2));
                s6 = s6 + R * 2 * K[3] * K[4] * (x[i] - K[5]) * r2;
                f[0] = s1; f[1] = s2; f[2] = s3;
                f[3] = s4; f[4] = s5; f[5] = s6;
            }
        }
        private void FM1(double[] x, double[] y, double[] K, out double[] f)
        {
            f = new double[9];
            double s1 = 0, s2 = 0, s3 = 0, s4 = 0, s5 = 0, s6 = 0, r1 = 0, r2 = 0, R = 0,s7=0,s8=0,s9=0,r3=0;
            for (int i = 0; i < Ne; i++)
            {
                r1 = Math.Exp(-K[1] * Math.Pow(x[i] - K[2], 2));
                r2 = Math.Exp(-K[4] * Math.Pow(x[i] - K[5], 2));
                r3 = Math.Exp(-K[7] * Math.Pow(x[i] - K[8], 2));
                R = K[0] * r1 + K[3] * r2 - y[i]+K[6] * r3;
                s1 = s1 + R * r1;
                s2 = s2 + R * r1 * K[0] * (-Math.Pow(x[i] - K[2], 2));
                s3 = s3 + R * 2 * K[0] * K[1] * (x[i] - K[2]) * r1;
                s4 = s4 + R * r2;
                s5 = s5 + R * r2 * K[3] * (-Math.Pow(x[i] - K[5], 2));
                s6 = s6 + R * 2 * K[3] * K[4] * (x[i] - K[5]) * r2;
                s7 = s7 + R * r3;
                s8 = s8 + R * r3 * K[6] * (-Math.Pow(x[i] - K[7], 2));
                s9 = s9 + R * 2 * K[6] * K[7] * (x[i] - K[8]) * r2;
                f[0] = s1; f[1] = s2; f[2] = s3;
                f[3] = s4; f[4] = s5; f[5] = s6;
                f[6] = s7; f[7] = s8; f[8] = s9;
            }
        }
        private void JAC(int N, int Ne, out double[,] Ja, double[] z)
        {
            Ja = new double[6, 6];
            double[] Fp;
            FM(Xe, Ye, z, out f);
            for (int j = 0; j < N; j++)
            {
                z[j] = z[j] + h;
                FM(Xe, Ye, z, out Fp);
                for (int i = 0; i < N; i++)
                    Ja[i, j] = (Fp[i] - f[i]) / h;
                z[j] = z[j] - h;
            }

        }
        private void JAC1(int N, int Ne, out double[,] Ja, double[] z)
        {
            Ja = new double[N, N];
            double[] Fp;
            FM1(Xe, Ye, z, out f);
            for (int j = 0; j < N; j++)
            {
                z[j] = z[j] + h;
                FM1(Xe, Ye, z, out Fp);
                for (int i = 0; i < N; i++)
                    Ja[i, j] = (Fp[i] - f[i]) / h;
                z[j] = z[j] - h;
            }

        }
        private void Graphic(PictureBox pictureBox1, int Ne)
        {
            double MaxX, MaxY, MaxYg, MinX, MinY, MinYg;
            double Kx, Ky, Zx, Zy, h, Krx, Kry, xx, yy, Gx, Gy;
            int i = 0, j, KrokX, KrokY, l;

            Gx = 0;
            Gy = 0;
            L = 40;
            MinX = Xe[0];
            MaxX = Xe[Ne - 1];
            MinY = Ye[0];
            MaxY = Ye[0];
            MinYg = Yg[0];
            MaxYg = Yg[0];
            for (i = 1; i <= Ne - 1; i++)
            {
                if (MaxY < Ye[i]) MaxY = Ye[i];
                if (MinY > Ye[i]) MinY = Ye[i];
            }
            for (i = 1; i <= Ngr - 1; i++)
            {
                if (MaxYg < Yg[i]) MaxYg = Yg[i];
                if (MinYg > Yg[i]) MinYg = Yg[i];
            }
            if (MaxYg > MaxY) MaxY = MaxYg;
            if (MinYg < MinY) MinY = MinYg;
            Kx = (pictureBox1.Width - 2 * L) / (MaxX - MinX);
            Ky = (pictureBox1.Height - 2 * L) / (MinY - MaxY);
            Zx = (pictureBox1.Width * MinX - L * (MinX + MaxX)) / (MinX - MaxX);
            Zy = (pictureBox1.Height * MaxY - L * (MinY + MaxY)) / (MaxY - MinY);
            if (MinX * MaxX <= 0) Gx = 0;
            if (MinX * MaxX > 0) Gx = MinX;
            if (MinX * MaxX > 0 && MinX < 0) Gx = MaxX;
            if (MinY * MaxY <= 0) Gy = 0;
            if (MinY * MaxY > 0 && MinY > 0) Gy = MinY;
            if (MinY * MaxY > 0 && MinY < 0) Gy = MaxY;

            Graphics pb1 = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Black);
            p.DashStyle = DashStyle.Solid;
            p.Color = Color.Black;
            p.Width = 1;
            pb1.DrawLine(p, L, Round(Ky * Gy + Zy), pictureBox1.Width - L, Round(Ky * Gy + Zy));
            pb1.DrawLine(p, Round(Kx * Gx + Zx), L, Round(Kx * Gx + Zx), pictureBox1.Height - L);
            KrokX = (pictureBox1.Width - 2 * L) / 10;
            KrokY = (pictureBox1.Height - 2 * L) / 10;

            p.Color = Color.Green;
            for (i = 0; i <= 10; i++)
            {
                pb1.DrawLine(p, L, L + i * KrokY, pictureBox1.Width - L, L + i * KrokY);
                pb1.DrawLine(p, L + i * KrokX, L, L + i * KrokX, pictureBox1.Height - L);
            }
            xx = MinX;
            yy = MaxY;
            Krx = (MaxX - MinX) / 10;
            Kry = (MaxY - MinY) / 10;
            for (i = 0; i <= 10; i++)
            {
                pb1.DrawString(Convert.ToString(Math.Round(xx, 2)),
                           new Font(new FontFamily("Verdana"), 8), Brushes.Black, new Point(L + i * KrokX, pictureBox1.Height - L + 5));
                pb1.DrawString(Convert.ToString(Math.Round(yy, 2)),
                            new Font(new FontFamily("Verdana"), 8), Brushes.Black, new Point(5, L + i * KrokY));
                xx += Krx;
                yy -= Kry;
            }
            p.Color = Color.Blue;
            p.Width = 2;
            for (i = 0; i <= Ne; i++)
                pb1.DrawRectangle(p, Round(Kx * Xe[i] + Zx) - 5, Round(Ky * Ye[i] + Zy) - 5, 8, 8);

            p.Color = Color.Red;
            p.Width = 4;
            for (i = 1; i < Ngr - 1; i++)
                pb1.DrawLine(p, Round(Kx * Xg[i - 1] + Zx), Round(Ky * Yg[i - 1] + Zy), Round(Kx * Xg[i] + Zx), Round(Ky * Yg[i] + Zy));
            p.Width = 2;
            p.Color = Color.Blue;
            for (i = 0; i < Ngr - 1; i++)
                Yg1[i] = K[0] * Math.Exp(-K[1] * Math.Pow(Xg[i] - K[2], 2));
            for (i = 1; i < Ngr - 1; i++)
                pb1.DrawLine(p, Round(Kx * Xg[i - 1] + Zx), Round(Ky * Yg1[i - 1] + Zy), Round(Kx * Xg[i] + Zx), Round(Ky * Yg1[i] + Zy));
            p.Color = Color.Black;
            for (i = 0; i < Ngr - 1; i++)
                Yg1[i] = K[3] * Math.Exp(-K[4] * Math.Pow(Xg[i] - K[5], 2));
            for (i = 1; i <= Ngr - 1; i++)
                pb1.DrawLine(p, Round(Kx * Xg[i - 1] + Zx), Round(Ky * Yg1[i - 1] + Zy), Round(Kx * Xg[i] + Zx), Round(Ky * Yg1[i] + Zy));
            if (Convert.ToInt32(numericUpDown1.Value) == 3)
            {
                p.Color = Color.Green;
                for (i = 0; i < Ngr - 1; i++)
                    Yg1[i] = K[6] * Math.Exp(-K[7] * Math.Pow(Xg[i] - K[8], 2));
                for (i = 1; i <= Ngr - 1; i++)
                    pb1.DrawLine(p, Round(Kx * Xg[i - 1] + Zx), Round(Ky * Yg1[i - 1] + Zy), Round(Kx * Xg[i] + Zx), Round(Ky * Yg1[i] + Zy));
            }

        }
        private void Graphic1(PictureBox pictureBox1, int Ne)
        {
            double MaxX, MaxY, MaxYg, MinX, MinY, MinYg;
            double Kx, Ky, Zx, Zy, h, Krx, Kry, xx, yy, Gx, Gy;
            int i = 0, j, KrokX, KrokY, l;

            Gx = 0;
            Gy = 0;
            L = 40;
            MinX = Xe1[0];
            MaxX = Xe1[Ne - 1];
            MinY = Ye1[0];
            MaxY = Ye1[0];
            MinYg = Yg1[0];
            MaxYg = Yg1[0];
            for (i = 1; i <= Ne - 1; i++)
            {
                if (MaxY < Ye1[i]) MaxY = Ye1[i];
                if (MinY > Ye1[i]) MinY = Ye1[i];
            }
            for (i = 1; i <= Ngr - 1; i++)
            {
                if (MaxYg < Yg1[i]) MaxYg = Yg1[i];
                if (MinYg > Yg1[i]) MinYg = Yg1[i];
            }
            if (MaxYg > MaxY) MaxY = MaxYg;
            if (MinYg < MinY) MinY = MinYg;
            Kx = (pictureBox1.Width - 2 * L) / (MaxX - MinX);
            Ky = (pictureBox1.Height - 2 * L) / (MinY - MaxY);
            Zx = (pictureBox1.Width * MinX - L * (MinX + MaxX)) / (MinX - MaxX);
            Zy = (pictureBox1.Height * MaxY - L * (MinY + MaxY)) / (MaxY - MinY);
            if (MinX * MaxX <= 0) Gx = 0;
            if (MinX * MaxX > 0) Gx = MinX;
            if (MinX * MaxX > 0 && MinX < 0) Gx = MaxX;
            if (MinY * MaxY <= 0) Gy = 0;
            if (MinY * MaxY > 0 && MinY > 0) Gy = MinY;
            if (MinY * MaxY > 0 && MinY < 0) Gy = MaxY;

            Graphics pb1 = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Black);
            p.DashStyle = DashStyle.Solid;
            p.Color = Color.Black;
            p.Width = 1;
            pb1.DrawLine(p, L, Round(Ky * Gy + Zy), pictureBox1.Width - L, Round(Ky * Gy + Zy));
            pb1.DrawLine(p, Round(Kx * Gx + Zx), L, Round(Kx * Gx + Zx), pictureBox1.Height - L);
            KrokX = (pictureBox1.Width - 2 * L) / 10;
            KrokY = (pictureBox1.Height - 2 * L) / 10;

            p.Color = Color.Green;
            for (i = 0; i <= 10; i++)
            {
                pb1.DrawLine(p, L, L + i * KrokY, pictureBox1.Width - L, L + i * KrokY);
                pb1.DrawLine(p, L + i * KrokX, L, L + i * KrokX, pictureBox1.Height - L);
            }
            xx = MinX;
            yy = MaxY;
            Krx = (MaxX - MinX) / 10;
            Kry = (MaxY - MinY) / 10;
            for (i = 0; i <= 10; i++)
            {
                pb1.DrawString(Convert.ToString(Math.Round(xx, 2)),
                           new Font(new FontFamily("Verdana"), 8), Brushes.Black, new Point(L + i * KrokX, pictureBox1.Height - L + 5));
                pb1.DrawString(Convert.ToString(Math.Round(yy, 2)),
                            new Font(new FontFamily("Verdana"), 8), Brushes.Black, new Point(5, L + i * KrokY));
                xx += Krx;
                yy -= Kry;
            }
            p.Color = Color.Blue;
            p.Width = 2;
            for (i = 0; i <= Ne; i++)
                pb1.DrawRectangle(p, Round(Kx * Xe1[i] + Zx) - 5, Round(Ky * Ye1[i] + Zy) - 5, 8, 8);

            p.Color = Color.Red;
            p.Width = 4;
            for (i = 1; i < Ngr - 1; i++)
                pb1.DrawLine(p, Round(Kx * Xg[i - 1] + Zx), Round(Ky * Yg1[i - 1] + Zy), Round(Kx * Xg[i] + Zx), Round(Ky * Yg1[i] + Zy));
            p.Width = 2;
            p.Color = Color.Blue;
            for (i = 0; i < Ngr - 1; i++)
                Yg1[i] = K[0] * Math.Exp(-K[1] * Math.Pow(Xg[i] - K[2], 2));
            for (i = 1; i < Ngr - 1; i++)
                pb1.DrawLine(p, Round(Kx * Xg[i - 1] + Zx), Round(Ky * Yg1[i - 1] + Zy), Round(Kx * Xg[i] + Zx), Round(Ky * Yg1[i] + Zy));
            p.Color = Color.Black;
            for (i = 0; i < Ngr - 1; i++)
                Yg1[i] = K[3] * Math.Exp(-K[4] * Math.Pow(Xg[i] - K[5], 2));
            for (i = 1; i <= Ngr - 1; i++)
                pb1.DrawLine(p, Round(Kx * Xg[i - 1] + Zx), Round(Ky * Yg1[i - 1] + Zy), Round(Kx * Xg[i] + Zx), Round(Ky * Yg1[i] + Zy));
            if (Convert.ToInt32(numericUpDown1.Value) == 3)
            {
                p.Color = Color.Green;
                for (i = 0; i < Ngr - 1; i++)
                    Yg1[i] = K[6] * Math.Exp(-K[7] * Math.Pow(Xg[i] - K[8], 2));
                for (i = 1; i <= Ngr - 1; i++)
                    pb1.DrawLine(p, Round(Kx * Xg[i - 1] + Zx), Round(Ky * Yg1[i - 1] + Zy), Round(Kx * Xg[i] + Zx), Round(Ky * Yg1[i] + Zy));
            }

        }
        public int Round(double n)
        {
            int r = (int)n;
            if (n % r >= 0.5) return r + 1; else return r;

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView2.RowCount = 6;
            dataGridView1.RowCount = 46;
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 80;
            dataGridView2.Columns[0].Width = 100;
            dataGridView1.Height = 800;
            dataGridView2.Height = 150;
            dataGridView2.Width = 105;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            if (numericUpDown1.Value == 3) textBox1.Text = "42";
            if (numericUpDown1.Value == 2) textBox1.Text = "38";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
       

    }
}
