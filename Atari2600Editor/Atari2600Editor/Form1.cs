using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atari2600Editor
{
    public partial class Form1 : Form
    {
        int mouseX, mouseY;

        bool canDraw;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
     
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            decimal x_Rate = (decimal) pictureBox1.Image.Width / pictureBox1.Size.Width;
            decimal y_Rate = (decimal) pictureBox1.Image.Height / pictureBox1.Size.Height;

            mouseX = (int)(e.X * x_Rate);
            mouseY = (int)(e.Y * y_Rate);

            if (canDraw)
            {
                Graphics g = Graphics.FromImage(pictureBox1.Image);

                Pen p = new Pen(Color.Red, 2);


                g.DrawRectangle(p, mouseX, mouseY, 1, 1);
                pictureBox1.Refresh();
            }
            label1.Text = "X: " + mouseX;
            label2.Text = "Y: " + mouseY;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            canDraw = false;
            Cursor.Show();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Hide();
            canDraw = true;
        }
    }
}
