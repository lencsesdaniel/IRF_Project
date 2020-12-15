using IRF_projekt.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IRF_projekt
{
    public partial class Form1 : Form
    {
        List<Gyerek> gyerekek = new List<Gyerek>();
        public Form1()
        {
            InitializeComponent();
            betoltes();
            dataGridView1.DataSource = gyerekek;
        }

        private void betoltes()
        {
            using (StreamReader sr = new StreamReader("nevsor.csv", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(';');

                    Gyerek gyerek = new Gyerek();
                    gyerek.Név = line[0];
                    gyerek.Kor = Convert.ToInt32(line[1]);
                    gyerek.Csoport = line[2];
                    gyerek.Kirándul = Convert.ToBoolean(line[3]);
                    gyerek.Ottalszik = Convert.ToBoolean(line[4]);
                    gyerek.Étkezések_száma = Convert.ToInt32(line[5]);
                    gyerekek.Add(gyerek);
                }
            }
        }
          
    }
}
