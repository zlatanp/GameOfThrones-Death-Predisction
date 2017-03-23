using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Statistics.Models.Regression.Linear;
using ComputationalGraph;
using System.IO;

namespace Proba
{
    public partial class Form1 : Form
    {
        private NeuralNetwork network = new NeuralNetwork();

        // proba
        private int[] Jon = { 1, 1, 1, 1, 1, 1, 1, 5, 1 };
        private int[] Deneris = {0, 1, 1, 1, 1, 1, 1, 15, 1 };
        private int[] Arja = { 0, 1, 1, 1, 1, 1, 1, 8, 1 };
        private int[] Tirion = { 1, 1, 1, 1, 1, 1, 1, 12, 1 };
        private int[] Tomen = { 1, 0, 0, 0, 0, 0, 0, 5, 1 };

        private double temp, Darja, Djon, Ddeneris, Dtirion, Dbran, Dfrej, Dpiter, Dsansa;
        // PROBA

        private List<double> godineSmrti = new List<double>();
        private int brojMrtvih = 0;

        public Form1()
        {
            InitializeComponent();

            

            Image myimage = new Bitmap(@"./data/slika.jpg");
            this.BackgroundImage = myimage;

            console.Enabled = true;
            console.ReadOnly = true;
            console.BackColor = Color.White;
            console.AcceptsReturn = true;

            cmbLikovi.Enabled = false;
            button5.Enabled = false;
            button3.Enabled = false;
            
            this.network.Add(new NeuralLayer(9, 5, "sigmoid"));
            this.network.Add(new NeuralLayer(5, 1, "sigmoid"));

            double[] x1 = { 0.0, 0.0 };//xor
            double[] x2 = { 0.0, 1.0 };
            double[] x3 = { 1.0, 0.0 };
            double[] x4 = { 1.0, 1.0 };
            List<List<double>> X = new List<List<double>>() { x1.ToList(), x2.ToList(), x3.ToList(), x4.ToList() };

            double[] y1 = { 0.0 };
            double[] y2 = { 1.0 };
            double[] y3 = { 1.0 };
            double[] y4 = { 0.0 };
            List<List<double>> Y = new List<List<double>>() { y1.ToList(), y2.ToList(), y3.ToList(), y4.ToList() };
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // iscitati iz fajlova prve dve knjige
            string[] lines = File.ReadAllLines(@"./data/train1.csv");
            

            List<double[]> listaNizovaUlaza = new List<double[]>();
            List<double> ulaz2 = new List<double>();
            List<double> izlazi = new List<double>();

            MultipleLinearRegression regresija = new MultipleLinearRegression(9, true);
            int i = 0;
            foreach (String line in lines)
            {
                if (i != 0)
                {
                    double[] pom = new double[9];
                    pom[8] = Double.Parse(line.Split(',')[2]);
                    pom[0] = Double.Parse(line.Split(',')[3]);
                    pom[1] = Double.Parse(line.Split(',')[4]);
                    pom[2] = Double.Parse(line.Split(',')[5]);
                    pom[3] = Double.Parse(line.Split(',')[6]);
                    pom[4] = Double.Parse(line.Split(',')[7]);
                    pom[5] = Double.Parse(line.Split(',')[8]);
                    pom[6] = Double.Parse(line.Split(',')[9]);
                    pom[7] = Double.Parse(line.Split(',')[13]);
                    listaNizovaUlaza.Add(pom);

                    izlazi.Add(Double.Parse(line.Split(',')[11]));  //mrtav ili ne   

                    
                }
                i++;
            }

           

            double[][] ulazi = listaNizovaUlaza.ToArray();
            
            double[] izlaz = izlazi.ToArray();

            // PROBLEM GRESKE?
            // PROBLEM POSTAVKE MODELA
            // AKO ZA SAMO DVA ULAZA DAJE OVOLIKU GRESKU KOLIKA JE GRESKA NA VISE ULAZA(9 recimo)
            // GRESKA ZA 100 ispitanih karaktera je 64 A ZA 898 KOLIKO IH JE U TESTNOM SKUPU 640

            double greska = regresija.Regress(ulazi, izlaz, true);
            double a = regresija.Coefficients[0];
            double b = regresija.Coefficients[1];
            double c = regresija.Coefficients[2];
            double d = regresija.Coefficients[3];
            double ee = regresija.Coefficients[4];
            double f = regresija.Coefficients[5];
            double g = regresija.Coefficients[6];
            double h = regresija.Coefficients[7];
            double ii = regresija.Coefficients[8];
            double slobodni = regresija.Coefficients[9];

            console.Text += Environment.NewLine;
            console.Text += "***********************" + Environment.NewLine;
            
            console.Text += "a=" + Math.Round(a,2) + Environment.NewLine;
            console.Text += "b=" + Math.Round(b,2) + Environment.NewLine;
            console.Text += "c=" + Math.Round(c,2) + Environment.NewLine;
            console.Text += "d=" + Math.Round(d,2) + Environment.NewLine;
            console.Text += "e=" + Math.Round(ee,2) + Environment.NewLine;
            console.Text += "f=" + Math.Round(f,2) + Environment.NewLine;
            console.Text += "g=" + Math.Round(g,2) + Environment.NewLine;
            console.Text += "h=" + Math.Round(h,2) + Environment.NewLine;
            console.Text += "i=" + Math.Round(ii,2) + Environment.NewLine;
            console.Text += "s=" + Math.Round(slobodni,2) + Environment.NewLine;
            console.Text += "Greska=" + Math.Round(greska,2) + Environment.NewLine;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {

            MultipleLinearRegression target = new MultipleLinearRegression(2, true);
            //ulazi 
            double[][] ulazi =
            {
                new double[] {0,0},
                new double[] {0,1},
                new double[] {1,0},
                new double[] {1,1}
             };

            double[] izlazi = { 1, 1, 1, 1 };

            double greska = target.Regress(ulazi, izlazi);
            double a = target.Coefficients[0];
            double b = target.Coefficients[1];
            double c = target.Coefficients[2];

            Console.WriteLine("Greska " + greska);
            Console.WriteLine("a " + a);
            Console.WriteLine("b " + b);
            Console.WriteLine("c " + c);
        }

        private void button2_Click(object sender, EventArgs e)  // obucavanje mreze
        {
            string[] lines = File.ReadAllLines(@"./data/train1.csv");
            List<List<Double>> x = new List<List<double>>();
            List<List<Double>> y = new List<List<double>>();

            int i = 0;

            

            foreach (String line in lines)
            {
                if (i != 0)
                {
                    List<Double> pomX = new List<double>();
                    List<Double> pomY = new List<double>();


                    pomX.Add(Double.Parse(line.Split(',')[2]));  //male
                    pomX.Add(Double.Parse(line.Split(',')[3]));  //b1
                    pomX.Add(Double.Parse(line.Split(',')[4]));  //b2
                    pomX.Add(Double.Parse(line.Split(',')[5]));  //b3
                    pomX.Add(Double.Parse(line.Split(',')[6]));  //b4
                    pomX.Add(Double.Parse(line.Split(',')[7]));  //b5

                    pomX.Add(Double.Parse(line.Split(',')[8]));   //noble
                    pomX.Add(Double.Parse(line.Split(',')[9]));   //numDeadrelations
                    pomX.Add(Double.Parse(line.Split(',')[13]));  //popularnost

                    pomY.Add(Double.Parse(line.Split(',')[11]));  //mrtav ili ne
                    if (pomY[0] == 0)
                    {
                        brojMrtvih++;
                    }

                    if (line.Split(',')[12].Length != 0)
                    {
                        godineSmrti.Add(Double.Parse(line.Split(',')[12]));
                    }
                    x.Add(pomX);
                    y.Add(pomY);
                }
                i++;

            }
            this.network.fit(x, y, 0.1, 0.9, 10);

            console.Text += "Network is trained." + Environment.NewLine;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int brojTacnih = 0;
            double ukupanBroj = 0.0;
            double T;
            int i = 0;
            string[] linesTest = File.ReadAllLines(@"./data/test1.csv");

            foreach (String line in linesTest)
            {
                if (i != 0)
                {
                    List<Double> pomX = new List<double>();
                    List<Double> pomY = new List<double>();


                    pomX.Add(Double.Parse(line.Split(',')[2]));
                    pomX.Add(Double.Parse(line.Split(',')[3]));
                    pomX.Add(Double.Parse(line.Split(',')[4]));
                    pomX.Add(Double.Parse(line.Split(',')[5]));
                    pomX.Add(Double.Parse(line.Split(',')[6]));
                    pomX.Add(Double.Parse(line.Split(',')[7]));

                    pomX.Add(Double.Parse(line.Split(',')[8]));
                    pomX.Add(Double.Parse(line.Split(',')[9]));
                    pomX.Add(Double.Parse(line.Split(',')[12]));

                    pomY.Add(Double.Parse(line.Split(',')[11]));

                    if (line.Split(',')[1].Contains("Arya Stark"))
                    {
                        Darja = network.predict(pomX)[0];
                    }

                    if (line.Split(',')[1].Contains("Jon Snow"))
                    {
                        Djon = network.predict(pomX)[0];
                    }

                    if (line.Split(',')[1].Contains("Daenerys Targaryen"))
                    {
                        Ddeneris = network.predict(pomX)[0];
                    }

                    if (line.Split(',')[1].Contains("Tyrion Lannister"))
                    {
                        Dtirion = network.predict(pomX)[0];
                    }

                    if (line.Split(',')[1].Contains("Bran Stark"))
                    {
                        Dbran = network.predict(pomX)[0];
                    }

                    if (line.Split(',')[1].Contains("Sansa Stark"))
                    {
                        Dsansa = network.predict(pomX)[0];
                    }
                    
                    if (line.Split(',')[1].Contains("Alys Frey"))
                    {
                        Dfrej = network.predict(pomX)[0];
                    }

                    if (line.Split(',')[1].Contains("Petyr Baelish"))
                    {
                        Dpiter = network.predict(pomX)[0];
                    }
                    

                    if (network.predict(pomX)[0] > 0.5)
                    {
                        T = 1;
                    }
                    else
                        T = 0;

                    if (T == pomY[0])
                        brojTacnih++;
                }
                i++;
                ukupanBroj++;
            }

            
            console.Text += "Procentage good calculated: " + Math.Round((brojTacnih / ukupanBroj), 2) + "%" + Environment.NewLine;
            cmbLikovi.Enabled = true  ;
            console.Text += "Average life span: " + Math.Round((godineSmrti.Sum() / brojMrtvih),2) + Environment.NewLine;

        }

        private void cmbLikovi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLikovi.Text.Contains("Jon"))
            {
                temp = Djon;
                button3.Enabled = true;
                return;
            }
            if (cmbLikovi.Text.Contains("Daenerys"))
            {
                temp = Ddeneris;
                button3.Enabled = true;
                return;
            }
            if (cmbLikovi.Text.Contains("Arya"))
            {
                temp = Darja;
                button3.Enabled = true;
                return;
            }
            if (cmbLikovi.Text.Contains("Tyrion"))
            {
                temp = Dtirion;
                button3.Enabled = true;
                return;
            }
            if (cmbLikovi.Text.Contains("Bran "))
            {
                temp = Dbran;
                button3.Enabled = true;
                return;
            }
            if (cmbLikovi.Text.Contains("Alys"))
            {
                temp = Dfrej;
                button3.Enabled = true;
                return;
            }
            if (cmbLikovi.Text.Contains("Petyr"))
            {
                temp = Dpiter;
                button3.Enabled = true;
                return;
            }
            if (cmbLikovi.Text.Contains("Sansa"))
            {
                temp = Dsansa;
                button3.Enabled = true;
                return;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            double temp1 = temp * 100;
            if (temp1 != 0)
            {
                String procenat = temp1.ToString();
                String ispis = procenat.Substring(0, 5);
                console.Text += "Procentage to be alive: " + ispis + "%" + Environment.NewLine;
            }
            else
            {
                console.Text += "Procentage to be alive: " + 0 + "%" + Environment.NewLine;
            }

        }
    }
}

