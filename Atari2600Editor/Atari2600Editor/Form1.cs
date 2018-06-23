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
    /*
    partial class RowData
    {
        private Color color;
        private int height;

        public bool IsEmpty { get => Columns.Any(); }
        public int Height { get => height; set => height = value; }
        public Color Color { get => color; set => color = value; }
        public SortedDictionary<int, object> Columns { get => columns; set => columns = value; }

        private SortedDictionary<int, Object> columns;

        public RowData(Color color)
        {
            this.Color = color;
        }
    }
    */
    public partial class Form1 : Form
    {
        int mouseX, mouseY;

        bool canDraw;

        //HashSet<Point> clickedCells = new HashSet<Point>();
        Dictionary<Point, int> clickedCells = new Dictionary<Point, int>();
        //SortedDictionary<Point, int> clickedCells = new SortedDictionary<Point, int>();
        //SortedDictionary<int, RowData> rolls = new SortedDictionary<int, RowData>();
               

        bool fixedLine;
        bool mirrored;

        private Point initPoint;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clickedCells.Clear();
            tableLayoutPanel1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            canDraw = true;

            var pt = new Point(e.X, e.Y);

            var colWidths = this.tableLayoutPanel1.GetColumnWidths();
            var rowHeights = this.tableLayoutPanel1.GetRowHeights();

            int col = -1, row = -1;
            int offset = 0;
            for (int iCol = 0; iCol < this.tableLayoutPanel1.ColumnCount; ++iCol)
            {
                if (pt.X >= offset && pt.X <= (offset + colWidths[iCol]))
                {
                    col = iCol;
                    break;
                }

                offset += colWidths[iCol];
            }

            offset = 0;
            for (int iRow = 0; iRow < this.tableLayoutPanel1.RowCount; ++iRow)
            {
                if (pt.Y >= offset && pt.Y <= (offset + rowHeights[iRow]))
                {
                    row = iRow;
                    break;
                }

                offset += rowHeights[iRow];
            }

            //MessageBox.Show(String.Format("row = {0}, col = {1}", row, col));
            var cell = new Point(col, row);
            clickedCells[cell] = 1;

            if (mirrored)
            {
                cell = new Point(39 - col, row);
                clickedCells[cell] = 1;
            }
            //if (!clickedCells.Contains(pt)) clickedCells.Add(pt);
            tableLayoutPanel1.Invalidate();

            initPoint = cell;
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (clickedCells.Any())
            //if (clickedCells.Contains(new Point(e.Column, e.Row)))
             //   if (e.Column == 10 && e.Row == 10)
            {
                //if (clickedCells[0].X == e.Column && clickedCells[0].Y == e.Row)
                if (clickedCells.ContainsKey(new Point(e.Column, e.Row)))
                {
                    e.Graphics.FillRectangle(Brushes.Red, e.CellBounds);
                }
            }
        }

        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!canDraw) return;

            var pt = new Point(e.X, e.Y);

            var colWidths = this.tableLayoutPanel1.GetColumnWidths();
            var rowHeights = this.tableLayoutPanel1.GetRowHeights();

            int col = -1, row = -1;
            int offset = 0;
            for (int iCol = 0; iCol < this.tableLayoutPanel1.ColumnCount; ++iCol)
            {
                if (pt.X >= offset && pt.X <= (offset + colWidths[iCol]))
                {
                    col = iCol;
                    break;
                }

                offset += colWidths[iCol];
            }

            offset = 0;
            for (int iRow = 0; iRow < this.tableLayoutPanel1.RowCount; ++iRow)
            {
                if (pt.Y >= offset && pt.Y <= (offset + rowHeights[iRow]))
                {
                    row = iRow;
                    break;
                }

                offset += rowHeights[iRow];
            }

            //MessageBox.Show(String.Format("row = {0}, col = {1}", row, col));
            var cell = new Point(col, row);
           // if (!clickedCells.Contains(cell)) clickedCells.Add(cell);

            if (fixedLine)
            {
                cell.Y = initPoint.Y;
            }

            clickedCells[cell] = 1;

            if (mirrored)
            {
                cell = new Point(39-col, cell.Y);
                clickedCells[cell] = 1;
            }
            //if (!clickedCells.Contains(pt)) clickedCells.Add(pt);
            tableLayoutPanel1.Invalidate();
            label3.Text = "total: " + clickedCells.Count;
        }

        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            canDraw = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            fixedLine = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            fixedLine = false;
            
        }

        private void tableLayoutPanel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            fixedLine = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mirrored = !mirrored;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Control control = tableLayoutPanel1.GetControlFromPosition(0, 0);
            //tableLayoutPanel1.Controls.Remove(control);
            tableLayoutPanel1.RowStyles[0].Height = 20;

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}
