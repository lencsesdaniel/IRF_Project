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
        private readonly string cs_;
        public Csoport(List<Gyerek>gyerekek , string cs)
        {
            InitializeComponent();
            List<Gyerek> csoport = (from x in gyerekek
                                             where x.Csoport == cs
                                             select x).ToList();

            dataGridView1.DataSource = csoport;
            
            cs_ = cs;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (cs_ == "pillangó")
            {
                Graphics g = panel1.CreateGraphics();

                g.DrawLine(new Pen(Color.Brown, 10), 12 + panel1.Width / 2, 12 + panel1.Height, 12 + panel1.Width / 2, 12);
                g.FillEllipse(new SolidBrush(Color.Blue), 35, 10, 50, 50);
                g.FillEllipse(new SolidBrush(Color.Blue), 93, 10, 50, 50);
                g.FillEllipse(new SolidBrush(Color.Blue), 23, 65, 65, 65);
                g.FillEllipse(new SolidBrush(Color.Blue), 90, 65, 65, 65);
                g.DrawEllipse(new Pen(Color.Orange,3), 35, 10, 50, 50);
                g.DrawEllipse(new Pen(Color.Orange, 3), 93, 10, 50, 50);
                g.DrawEllipse(new Pen(Color.Orange, 3), 23, 65, 63, 63);
                g.DrawEllipse(new Pen(Color.Orange, 3), 90, 65, 64, 64);
            }
            else if (cs_ == "alma")
            {
                Graphics g = panel1.CreateGraphics();
                g.FillEllipse(new SolidBrush(Color.Red), panel1.Width / 4, 30, 100, 100);
                g.DrawLine(new Pen(Color.Brown, 10), 12 + panel1.Width / 2, 55, 12 + panel1.Width / 2, 12);
            }
            else if (cs_ == "napocska")
            {
                Graphics g = panel1.CreateGraphics();
                Image imageFile = Image.FromFile("napocska.jpg");
                g.DrawImage(imageFile, new Rectangle(0, 0, 155, 155));
            }
            else if (cs_ == "virág")
            {
                Graphics g = panel1.CreateGraphics();
                Image imageFile = Image.FromFile("virág.png");
                g.DrawImage(imageFile, new Rectangle(0, 0, 155, 155));

            }
            else if (cs_ == "autó")
            {
                Graphics g = panel1.CreateGraphics();
                Image imageFile = Image.FromFile("car.png");
                g.DrawImage(imageFile, new Rectangle(0, 0, 155, 155));
            }
        }
    }
}
