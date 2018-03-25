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
    public partial class VerticesInput : Form
    {
        public int v_count = 1;
        private Point pos;
        public List<Point> Pos = new List<Point>();

        public VerticesInput()
        {
            InitializeComponent();
        }

        private void VerticesInput_Load(object sender, EventArgs e)
        {
            int Y = button1.Location.Y;
            int height = 20;

            tableLayoutPanel1.RowCount = v_count;

            for (int i = 0; i < v_count; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
                this.Height += height;
                Y += height;

                Label lblTitle = new Label();
                lblTitle.Text = string.Format("{0}:", i + 1);
                lblTitle.TabIndex = (i * 3);
                lblTitle.Margin = new Padding(0);
                lblTitle.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(lblTitle, 0, i);

                TextBox txtValue = new TextBox();
                txtValue.TabIndex = (i * 3) + 1;
                txtValue.Margin = new Padding(0);
                txtValue.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(txtValue, 1, i);

                TextBox txtValue2 = new TextBox();
                txtValue2.TabIndex = (i * 3) + 2;
                txtValue2.Margin = new Padding(0);
                txtValue2.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(txtValue2, 2, i);
            }

            pos = new Point(button1.Location.X, Y);
            button1.Location = pos;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            
            int i = 1, prev = 0;
            try
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    TextBox txtBox = control as TextBox;
                    if (txtBox != null)
                    {
                        if (i % 2 == 0)
                            Pos.Add(new Point(prev, Convert.ToInt32(txtBox.Text)));
                        else
                            prev = Convert.ToInt32(txtBox.Text);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
