using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atari2600Editor
{

    public partial class Form1 : Form
    {
        //int mouseX, mouseY;

        bool canDraw;

        //HashSet<Point> clickedCells = new HashSet<Point>();
        Dictionary<Point, int> clickedCells = new Dictionary<Point, int>();
        //SortedDictionary<Point, int> clickedCells = new SortedDictionary<Point, int>();
        //SortedDictionary<int, RowData> rolls = new SortedDictionary<int, RowData>();
               

        bool fixedLine;
        bool mirrored;

        private Point initPoint;
        private const int ROW_HEIGHT = 4;
        private Color backColor = Color.Black;
        private Color frontColor = Color.Red;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clickedCells.Clear();

            dataGridView1.DoubleBuffered(true);

            dataGridView1.Rows[0].Height = ROW_HEIGHT;
/*
            // Connect the virtual-mode events to event handlers. 
            this.dataGridView1.CellValueNeeded += new
                DataGridViewCellValueEventHandler(dataGridView1_CellValueNeeded);
            this.dataGridView1.CellValuePushed += new
                DataGridViewCellValueEventHandler(dataGridView1_CellValuePushed);
            this.dataGridView1.NewRowNeeded += new
                DataGridViewRowEventHandler(dataGridView1_NewRowNeeded);
            this.dataGridView1.RowValidated += new
                DataGridViewCellEventHandler(dataGridView1_RowValidated);
            this.dataGridView1.RowDirtyStateNeeded += new
                QuestionEventHandler(dataGridView1_RowDirtyStateNeeded);
            this.dataGridView1.CancelRowEdit += new
                QuestionEventHandler(dataGridView1_CancelRowEdit);
            this.dataGridView1.UserDeletingRow += new
                DataGridViewRowCancelEventHandler(dataGridView1_UserDeletingRow);

            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing; //or even better .DisableResizing. Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders

            // set it to false if not needed
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
*/
            for (int l = 0; l < 192; l++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.DefaultCellStyle.BackColor = Color.Black;
                dataGridView1.Rows.Add(row);
            }

//            dataGridView1.RowHeadersVisible = true;
//            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //remove last row
            //dataGridView1.Rows.RemoveAt(0);

            /*
            tableLayoutPanel1.RowCount = 40;// 192;
            tableLayoutPanel1.ColumnCount = 40;

            tableLayoutPanel1.ColumnStyles.Clear();

            for (int l = 0; l < 40; l++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize, 3));

                for (int c = 0; c < 40; c++)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 5));
                    Label label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.Text = "x";
                    label.Size = new Size(new Point(15, 15));

                    if (l % 2 == 0)
                        label.BackColor = Color.Red;
                    else
                        label.BackColor = Color.Gray;

                    label.ForeColor = Color.Blue;

                    //label.Padding = new Padding(0);
                    //label.Margin = new Padding(0);

                                       
                    tableLayoutPanel1.Controls.Add(label, c, l);
                }
            }           

            tableLayoutPanel1.Invalidate();
            */
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void dataGridView1_CancelRowEdit(object sender, QuestionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void dataGridView1_RowDirtyStateNeeded(object sender, QuestionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void dataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void dataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //throw new NotImplementedException();
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
/*
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
*/
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            /*
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
            */
        }

        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
/*
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
*/
        }

        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
         
        }

        private void tableLayoutPanel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mirrored = !mirrored;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Control control = tableLayoutPanel1.GetControlFromPosition(0, 0);
            //tableLayoutPanel1.Controls.Remove(control);
            //tableLayoutPanel1.RowStyles[0].Height = 20;

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = frontColor;
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {            
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {            
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canDraw)
            {
                var info = this.dataGridView1.HitTest(e.X, e.Y);
                if (info.RowIndex > 0 && info.ColumnIndex >= 0)
                {
                    dataGridView1.Rows[info.RowIndex].Cells[info.ColumnIndex].Style.BackColor = frontColor;
                }
                //Console.Write(info);
            }
        }



        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
        }

        //--------------------
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            canDraw = true;
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            canDraw = false;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = (DataGridViewRow)dataGridView1.CurrentRow;
            row.Height += ROW_HEIGHT;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.CurrentRow;
            if (row.Height > 0)
            {
                row.Height -= ROW_HEIGHT;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            frontColor = colorDialog1.Color;
            labelFGColor.BackColor = frontColor;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            backColor = colorDialog1.Color;
            labelBKColor.BackColor = backColor;
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = backColor;
        }
    }
}
