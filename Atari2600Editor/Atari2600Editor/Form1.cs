using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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

        //------------------------------

        #region Constants

        private const int DefaultX = 6;

        private const int DefaultY = 6;

        private const int Spacing = 6;

        private const int CellSize = 24;

        #endregion

        private List<Color> _loadedPalette;

        private enum ColorSpace
        {
            Rgb = 0,

            Hsb = 1,

            Cmyk = 2,

            Lab = 7,

            Grayscale = 8
        }

        private enum FileVersion
        {
            Version1 = 1,

            Version2
        }

        /// <summary>
        /// Reads a 16bit unsigned integer in big-endian format.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>The unsigned 16bit integer cast to an <c>Int32</c>.</returns>
        private int ReadInt16(Stream stream)
        {
            return (stream.ReadByte() << 8) | (stream.ReadByte() << 0);
        }

        /// <summary>
        /// Reads a 32bit unsigned integer in big-endian format.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>The unsigned 32bit integer cast to an <c>Int32</c>.</returns>
        private int ReadInt32(Stream stream)
        {
            return ((byte)stream.ReadByte() << 24) | ((byte)stream.ReadByte() << 16) | ((byte)stream.ReadByte() << 8) | ((byte)stream.ReadByte() << 0);
        }

        private List<Color> ReadPhotoShopSwatchFile(string fileName)
        {
            List<Color> colorPalette;

            using (Stream stream = File.OpenRead(fileName))
            {
                FileVersion version;

                // read the version, which occupies two bytes
                version = (FileVersion)this.ReadInt16(stream);

                if (version != FileVersion.Version1 && version != FileVersion.Version2)
                    throw new InvalidDataException("Invalid version information.");

                // the specification states that a version2 palette follows a version1
                // the only difference between version1 and version2 is the inclusion 
                // of a name property. Perhaps there's addtional color spaces as well
                // but we can't support them all anyway
                // I noticed some files no longer include a version 1 palette

                colorPalette = this.ReadSwatches(stream, version);
                if (version == FileVersion.Version1)
                {
                    version = (FileVersion)this.ReadInt16(stream);
                    if (version == FileVersion.Version2)
                        colorPalette = this.ReadSwatches(stream, version);
                }
            }

            return colorPalette;
        }

        /// <summary>
        /// Reads a unicode string of the specified length.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <param name="length">The number of characters in the string.</param>
        /// <returns>The string read from the stream.</returns>
        private string ReadString(Stream stream, int length)
        {
            byte[] buffer;

            buffer = new byte[length * 2];

            stream.Read(buffer, 0, buffer.Length);

            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        private List<Color> ReadSwatches(Stream stream, FileVersion version)
        {
            int colorCount;
            List<Color> results;

            results = new List<Color>();

            // read the number of colors, which also occupies two bytes
            colorCount = this.ReadInt16(stream);

            for (int i = 0; i < colorCount; i++)
            {
                ColorSpace colorSpace;
                int value1;
                int value2;
                int value3;
                int value4;

                // again, two bytes for the color space
                colorSpace = (ColorSpace)(this.ReadInt16(stream));

                value1 = this.ReadInt16(stream);
                value2 = this.ReadInt16(stream);
                value3 = this.ReadInt16(stream);
                value4 = this.ReadInt16(stream);

                if (version == FileVersion.Version2)
                {
                    int length;

                    // need to read the name even though currently our colour collection doesn't support names
                    length = ReadInt32(stream);
                    this.ReadString(stream, length);
                }

                switch (colorSpace)
                {
                    case ColorSpace.Rgb:
                        int red;
                        int green;
                        int blue;

                        // RGB.
                        // The first three values in the color data are red , green , and blue . They are full unsigned
                        //  16-bit values as in Apple's RGBColor data structure. Pure red = 65535, 0, 0.

                        red = value1 / 256; // 0-255
                        green = value2 / 256; // 0-255
                        blue = value3 / 256; // 0-255

                        results.Add(Color.FromArgb(red, green, blue));
                        break;

                    case ColorSpace.Hsb:
                        double hue;
                        double saturation;
                        double brightness;

                        // HSB.
                        // The first three values in the color data are hue , saturation , and brightness . They are full 
                        // unsigned 16-bit values as in Apple's HSVColor data structure. Pure red = 0,65535, 65535.

                        hue = value1 / 182.04; // 0-359
                        saturation = value2 / 655.35; // 0-100
                        brightness = value3 / 655.35; // 0-100

                        throw new InvalidDataException(string.Format("Color space '{0}' not supported.", colorSpace));

                    case ColorSpace.Grayscale:

                        int gray;

                        // Grayscale.
                        // The first value in the color data is the gray value, from 0...10000.

                        gray = (int)(value1 / 39.0625); // 0-255

                        results.Add(Color.FromArgb(gray, gray, gray));
                        break;

                    default:
                        throw new InvalidDataException(string.Format("Color space '{0}' not supported.", colorSpace));
                }
            }

            return results;
        }



        //------------------------------
        public Form1()
        {
            //---
            _loadedPalette = this.ReadPhotoShopSwatchFile("data/Atari 128 (NTSC).aco");

            //---------------------------------------------------
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

        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void bufferedPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (_loadedPalette != null)
            {
                int x;
                int y;

                x = DefaultX;
                y = DefaultY;

                e.Graphics.Clear(bufferedPanel1.BackColor);

                foreach (Color color in _loadedPalette)
                {
                    Rectangle bounds;

                    if (x > bufferedPanel1.Width - (CellSize + DefaultX))
                    {
                        x = DefaultX;
                        y += DefaultY + CellSize + Spacing;
                    }

                    bounds = new Rectangle(x, y, CellSize, CellSize);
                    Label label = new Label();
                    label.Location = new Point(x, y);
                    label.Size = new Size(new Point((CellSize + DefaultX), (CellSize + DefaultX)));
                    label.BackColor = color;
                    label.MouseClick += new MouseEventHandler(labelColor_MouseClick);
                    bufferedPanel1.Controls.Add(label);

                    using (Brush brush = new SolidBrush(color))
                        e.Graphics.FillRectangle(brush, bounds);

                    e.Graphics.DrawRectangle(Pens.Black, bounds);

                    x += (CellSize + Spacing);
                }
            }
        }

        private void labelColor_MouseClick(object sender, MouseEventArgs e)
        {
            Label labelColor = (Label)sender;

            switch (e.Button)
            {

                case MouseButtons.Left:
                    frontColor = labelColor.BackColor;
                    labelFGColor.BackColor = frontColor;
                    break;

                case MouseButtons.Right:
                    backColor = labelColor.BackColor;
                    labelBKColor.BackColor = backColor;
                    if (dataGridView1.CurrentCell != null) { 
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = backColor;
                    }
                    break;
            }

        }

        private void bufferedPanel1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void labelFGColor_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
