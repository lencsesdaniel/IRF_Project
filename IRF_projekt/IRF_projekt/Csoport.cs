﻿using IRF_projekt.Classes;
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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace IRF_projekt
{
    
    public partial class Csoport : Form
    {
        private readonly string cs_;
        private readonly List<Gyerek> gyerekek_;
        List<Gyerek> kirándulók_ = new List<Gyerek>();
        List<Gyerek> csoport_ = new List<Gyerek>();
        Excel.Application xlApp; 
        Excel.Workbook xlWB; 
        Excel.Worksheet xlSheet;

        public Csoport(List<Gyerek>gyerekek , string cs)
        {
            InitializeComponent();
            cs_ = cs;
            gyerekek_ = gyerekek;
            List<Gyerek> csoport = (from x in gyerekek
                                    where x.Csoport == cs
                                    select x).ToList();
            dataGridView1.DataSource = csoport;




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
                g.FillEllipse(new SolidBrush(Color.Red), (panel1.Width / 4), 30, 100, 100);
                g.DrawLine(new Pen(Color.Brown, 10), (12 + panel1.Width / 2), 55, 12 + panel1.Width / 2, 12);
                g.FillEllipse(new SolidBrush(Color.Green), (12 + panel1.Width / 2), 12, 30, 10);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                List<Gyerek> kirándulók = (from x in gyerekek_
                                           where x.Csoport == cs_
                                           where x.Kirándul == true
                                           select x).ToList();
                dataGridView1.DataSource = kirándulók;
                kirándulók_ = kirándulók;
                
            }
            else
            {
                List<Gyerek> csoport = (from x in gyerekek_
                                        where x.Csoport == cs_
                                        select x).ToList();
                dataGridView1.DataSource = csoport;
                csoport_ = csoport;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                xlApp = new Excel.Application();

                
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                
                xlSheet = xlWB.ActiveSheet;

                
                string[] headers = new string[] { "Név", "Kor", "Csoport", "Kirándul", "Ottalszik", "Étkezések száma" };
                for (int i = 0; i < headers.Length; i++)
                {
                    xlSheet.Cells[1, i + 1] = headers[i];
                }
                if (checkBox1.Checked)
                {
                    object[,] values = new object[kirándulók_.Count, headers.Length];
                    int counter = 0;
                    foreach (Gyerek gy in kirándulók_)
                    {
                        values[counter, 0] = gy.Név;
                        values[counter, 1] = gy.Kor;
                        values[counter, 2] = gy.Csoport;
                        values[counter, 3] = gy.Kirándul;
                        values[counter, 4] = gy.Ottalszik;
                        values[counter, 5] = gy.Étkezések_száma;
                        counter++;
                    }
                    xlSheet.get_Range(
                        GetCell(2, 1),
                        GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;
                }
                else
                {
                    object[,] values = new object[csoport_.Count, headers.Length];
                    int counter = 0;
                    foreach (Gyerek gy in csoport_)
                    {
                        values[counter, 0] = gy.Név;
                        values[counter, 1] = gy.Kor;
                        values[counter, 2] = gy.Csoport;
                        values[counter, 3] = gy.Kirándul;
                        values[counter, 4] = gy.Ottalszik;
                        values[counter, 5] = gy.Étkezések_száma;
                        counter++;
                    }
                    xlSheet.get_Range(
                        GetCell(2, 1),
                        GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;
                }

                
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) 
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
            

        }
        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }
    }
}