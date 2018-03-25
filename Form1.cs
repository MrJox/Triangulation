using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangulation
{
    public partial class Form1 : Form
    {
        private bool is_drawOn, isClear = true;
        private Bitmap bmp = new Bitmap(782, 400);
        public List<Point> pos = new List<Point>();
        private List<Triangle> T = new List<Triangle>();
        private Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

            using (var form = new VerticesInput())
            {
                form.v_count = Convert.ToInt32(textBox2.Text);
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pos = form.Pos;
                }
            }
            
            draw(pos);

            isClear = false;
            button4.Enabled = true;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (!isClear)
                {
                    MessageBox.Show("Press CLEAR first!!!");
                    radioButton2.Checked = true;
                }
                else
                {
                    textBox1.Enabled = textBox2.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = button4.Enabled = false;
                }
            }
            else
            {
                if (!isClear)
                {
                    MessageBox.Show("Press CLEAR first!!!");
                    radioButton1.Checked = true;
                }
                else
                {
                    textBox1.Enabled = textBox2.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isClear)
            {
                if (!is_drawOn)
                {
                    is_drawOn = true;
                    button3.BackColor = SystemColors.ActiveCaption;
                    radioButton1.Enabled = radioButton2.Enabled = false;
                    button1.Enabled = button4.Enabled = false;
                    button3.Text = "Draw";
                }
                else
                {
                    is_drawOn = false;
                    button3.BackColor = SystemColors.Control;
                    radioButton1.Enabled = radioButton2.Enabled = true;
                    button1.Enabled = button4.Enabled = true;
                    button3.Text = "Set vertices";
                    draw(pos);
                }
            }
            else
            {
                MessageBox.Show("Press CLEAR first!!!");
            }
        }

        private void draw(List<Point> pos)
        {
            try
            {
                g.DrawPolygon(new Pen(new SolidBrush(Color.Red)), pos.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                isClear = true;
            }

            isClear = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (is_drawOn)
            {
                pos.Add(new Point(e.X, e.Y));
                bmp.SetPixel(e.X, e.Y, Color.Black);
                bmp.SetPixel(e.X + 1, e.Y + 1, Color.Black);
                bmp.SetPixel(e.X - 1, e.Y - 1, Color.Black);
                bmp.SetPixel(e.X + 1, e.Y - 1, Color.Black);
                bmp.SetPixel(e.X - 1, e.Y + 1, Color.Black);
                pictureBox1.Image = bmp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Triangulate calculate = new Triangulate(pos);
            calculate.EarTrimming(g);
            calculate.DrawTriangles(g);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = null;
            isClear = true;
            pos = new List<Point>();
        }
    }
}
