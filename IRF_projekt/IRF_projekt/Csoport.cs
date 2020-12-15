using IRF_projekt.Classes;
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

namespace IRF_projekt
{
    
    public partial class Csoport : Form
    {
        
        Pen p = new Pen(Color.Red, 3);
        public Csoport(List<Gyerek>gyerekek , string cs)
        {
            InitializeComponent();
            List<Gyerek> csoport = (from x in gyerekek
                                             where x.Csoport == cs
                                             select x).ToList();

            dataGridView1.DataSource = csoport;
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(p,panel1.Width/2,panel1.Bottom,panel1.Width/2,panel1.Height);
            
        }

        
    }
}
