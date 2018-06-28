using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        bool canDraw;
        Dictionary<Point, int> clickedCells = new Dictionary<Point, int>();

        private const int ROW_HEIGHT = 4;

        private enum RowType { Asymmetric, Symmetric, Repeatable };

        private class ScanlineInfo
        {
            public Color backColor;
            public byte backColorIndex;

            public Color frontColor;
            public byte frontColorIndex;

            public byte repeat = 2;
            public RowType type;
        }

        private Color defaultBackColor = Color.Black;
        private byte defaultBackColorIndex;

        private Color defaultFrontColor = Color.Red;        
        private byte defaultFrontColorIndex;

        //------------------------------
        #region Constants

        private const int DefaultX = 6;
        private const int DefaultY = 6;
        private const int Spacing = 6;
        private const int CellSize = 24;

        #endregion

        private List<Color> _loadedPalette;

        public bool erasure { get; private set; }

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

        private int ReadInt16(Stream stream)
        {
            return (stream.ReadByte() << 8) | (stream.ReadByte() << 0);
        }
        
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
            _loadedPalette = this.ReadPhotoShopSwatchFile("data/Atari 128 (NTSC).aco");

            InitializeComponent();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            clickedCells.Clear();

            dataGridView1.DoubleBuffered(true);
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Blue;

            dataGridView1.Rows[0].Height = ROW_HEIGHT;

            for (int l = 0; l < 192; l++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                addScanlineInfoTo(row);
                dataGridView1.Rows.Add(row);
                row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                row.HeaderCell.Style.Font = new Font("Arial", 6F, FontStyle.Bold, GraphicsUnit.Pixel);
                row.HeaderCell.Value = "<- <-";
            }
        }

        private void addScanlineInfoTo(DataGridViewRow row)
        {
            //store line info
            ScanlineInfo scanLine = new ScanlineInfo();
            scanLine.frontColor = Color.Empty;// Color.Red;
            scanLine.backColor = Color.Empty;
            scanLine.repeat = 1;
            scanLine.type = RowType.Asymmetric;
            row.Tag = scanLine;
            row.DefaultCellStyle.BackColor = scanLine.backColor;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                ScanlineInfo scanlineInfo = (ScanlineInfo)row.Tag;
                if (scanlineInfo != null)
                {
                    byte pf0 = extractBitsFrom(row.Cells, 0, 3);
                    byte pf1 = extractBitsFrom(row.Cells, 4, 11);
                    byte pf2 = extractBitsFrom(row.Cells, 12, 19);
                    //second half if needed

                    byte bkColor = scanlineInfo.backColorIndex;
                    byte fgColor = scanlineInfo.frontColorIndex;
                    byte height = scanlineInfo.repeat;
                    string prefix = ToHex(height) + "," + ToHex(bkColor) + "," + ToHex(fgColor);

                    // Convert integer 182 as a hex in a string variable
                    string hexValue1 = ".byte " + ToHex(pf0) + "," + ToHex(pf1) + "," + ToHex(pf2) + ";";
                    string binValue1 = ".byte " + ToBin(pf0) + "," + ToBin(pf1) + "," + ToBin(pf2) + ";";
                    string hexPlainValue1 = "hex  " + ToHex2(pf0) + "" + ToHex2(pf1) + "" + ToHex2(pf2) + "";

                    if (true) //RowType.Asymmetric == scanlineInfo.type)
                    {
                        pf0 = extractBitsFrom(row.Cells, 20, 23);
                        pf1 = extractBitsFrom(row.Cells, 24, 31);
                        pf2 = extractBitsFrom(row.Cells, 32, 39);

                        string hexValue2 = "," + ToHex(pf0) + "," + ToHex(pf1) + "," + ToHex(pf2) + ";";
                        string binValue2 = "," + ToBin(pf0) + "," + ToBin(pf1) + "," + ToBin(pf2) + ";";
                        string hexPlainValue2 = "" + ToHex2(pf0) + "" + ToHex2(pf1) + "" + ToHex2(pf2) + "";
                        Debug.WriteLine("hexValue = " + hexValue1 + hexValue2 + " --- binValue = " + binValue1 + binValue2 + "hexPlainValue = " + hexPlainValue1 + hexPlainValue2);
                    }
                    else
                    {
                        Debug.WriteLine("hexValue = " + hexValue1 + " --- binValue = " + binValue1 + "hexPlainValue = " + hexPlainValue1);
                    }

                    Debug.WriteLine(prefix);
                }

            }

        }

        private byte extractBitsFrom(DataGridViewCellCollection cells, int v1, int v2)
        {
            byte data = 0;
            string bits = "";
            for (int i = v1; i <= v2; i++)
            {
                if (cells[i].Tag != null && ((int)cells[i].Tag == 1))
                {
                    bits += "1";
                }
                else
                {
                    bits += "0";
                }
            }
            //Debug.WriteLine("bits = " + bits + "["+v1+","+v2+"]");
            data = Convert.ToByte(bits, 2);
            return data;
        }

        /**
         * Plot pixel at a given cell coordinate
         */
        private void drawPixelAt(int line, int row)
        {
            ScanlineInfo scanlineInfo = (ScanlineInfo)dataGridView1.Rows[line].Tag;
            if (scanlineInfo != null)
            {
                if (scanlineInfo.frontColor == Color.Empty)
                {
                    scanlineInfo.frontColor = defaultFrontColor;
                }
                
                if (erasure)
                {
                    dataGridView1.Rows[line].Cells[row].Tag = 0;
                    dataGridView1.Rows[line].Cells[row].Style.BackColor = scanlineInfo.backColor;
                }
                else { 
                    dataGridView1.Rows[line].Cells[row].Tag = 1;
                    dataGridView1.Rows[line].Cells[row].Style.BackColor = scanlineInfo.frontColor;
                }
            }
        }

        private void drawPixelAt(DataGridViewCell cell)
        {
            ScanlineInfo scanlineInfo = (ScanlineInfo)dataGridView1.Rows[cell.RowIndex].Tag;
            if (scanlineInfo != null)
            {
                if (scanlineInfo.frontColor == Color.Empty)
                {
                    scanlineInfo.frontColor = defaultFrontColor;
                }
                cell.Style.BackColor = scanlineInfo.frontColor;

                if (erasure)
                {
                    cell.Tag = 0;
                    cell.Style.BackColor = scanlineInfo.backColor;
                }
                else
                {
                    cell.Tag = 1;
                    cell.Style.BackColor = scanlineInfo.frontColor;
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex >= 0)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        drawPixelAt(e.RowIndex, e.ColumnIndex);
                        break;
                    case MouseButtons.Right:
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        drawBackground(e.RowIndex);
                        break;
                }
            }
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canDraw)
            {
                var info = this.dataGridView1.HitTest(e.X, e.Y);
                if (info.RowIndex > 0 && info.ColumnIndex >= 0)
                {
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            drawPixelAt(info.RowIndex, info.ColumnIndex);
                            break;
                        case MouseButtons.Right:
                            dataGridView1.CurrentCell = dataGridView1.Rows[info.RowIndex].Cells[info.ColumnIndex];
                            drawBackground(info.RowIndex);
                            break;
                    }
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            canDraw = true;
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            canDraw = false;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '+')
            {
                //DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Clone();
                //row.DefaultCellStyle.BackColor = Color.Black;
                dataGridView1.Rows.Insert(dataGridView1.CurrentRow.Index + 1);
                addScanlineInfoTo(dataGridView1.CurrentRow);
            }
            if (e.KeyChar == '-')
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.CurrentRow;
                row.DefaultCellStyle.BackColor = Color.Black;
                dataGridView1.Rows.Remove(row);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left))
            {
                drawPixelAt(dataGridView1.CurrentCell);
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            ScanlineInfo scanlineInfo = (ScanlineInfo)row.Tag;
            scanlineInfo.type++;
            if (scanlineInfo.type > RowType.Repeatable)
            {
                scanlineInfo.type = RowType.Asymmetric;
            }
            Debug.WriteLine("scanlineInfo.type = " + scanlineInfo.type);
        }

        private void buttonIncRowHeight_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.CurrentRow;
            ScanlineInfo scanlineInfo = (ScanlineInfo)row.Tag;
            if (scanlineInfo != null)
            {
                scanlineInfo.repeat++;
                row.Height += ROW_HEIGHT;
            }
        }

        private void buttonDecRowHeight_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.CurrentRow;
            ScanlineInfo scanlineInfo = (ScanlineInfo)row.Tag;
            if (scanlineInfo != null && row.Height > 0)
            {
                scanlineInfo.repeat--;
                row.Height -= ROW_HEIGHT;
            }
        }

        private void bufferedPanelColorPalette_Paint(object sender, PaintEventArgs e)
        {
            if (_loadedPalette != null)
            {
                int x;
                int y;

                x = DefaultX;
                y = DefaultY;

                e.Graphics.Clear(bufferedPanel1.BackColor);

                byte index = 0;
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

                    //store the color index
                    label.Tag = index++;

                    label.MouseClick += new MouseEventHandler(labelColor_MouseClick);
                    bufferedPanel1.Controls.Add(label);

                    using (Brush brush = new SolidBrush(color))
                        e.Graphics.FillRectangle(brush, bounds);

                    e.Graphics.DrawRectangle(Pens.Black, bounds);

                    x += (CellSize + Spacing);
                }
            }
        }

        private string ToHex(byte value)
        {
            //return String.Format("0x{0:X2}", value);
            return String.Format("#${0:X2}", value);
        }

        private string ToHex2(byte value)
        {
            return String.Format("{0:X2}", value);
        }

        private string ToBin(byte value)
        {
            //return String.Format("0x{0:X2}", value);
            return "#%" + Convert.ToString(value, 2).PadLeft(8, '0');
        }

        private void labelColor_MouseClick(object sender, MouseEventArgs e)
        {
            Label labelColor = (Label)sender;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        labelFGColor.BackColor = labelColor.BackColor;

                        //default color
                        defaultFrontColor = labelFGColor.BackColor;
                        defaultFrontColorIndex = (byte)labelColor.Tag;

                        if (dataGridView1.CurrentCell != null)
                        {
                            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                            ScanlineInfo scanlineInfo = (ScanlineInfo)row.Tag;
                            scanlineInfo.frontColor = labelColor.BackColor;
                            //save color index
                            scanlineInfo.frontColorIndex = (byte)labelColor.Tag;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.Tag != null && (int)cell.Tag == 1)
                                {
                                    drawPixelAt(cell);
                                }
                            }
                        }
                        break;
                    }

                case MouseButtons.Right:
                    {
                        labelBKColor.BackColor = labelColor.BackColor;
                        //default color
                        defaultBackColor = labelBKColor.BackColor;
                        defaultBackColorIndex = (byte)labelColor.Tag;
                        drawBackground(dataGridView1.CurrentCell.RowIndex);
                        break;
                    }
            }
            Debug.WriteLine(" = " + labelColor.BackColor);
        }

        private void drawBackground(int rowIndex)
        {
            if (dataGridView1.CurrentCell != null)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                ScanlineInfo scanlineInfo = (ScanlineInfo)row.Tag;
                scanlineInfo.backColor = defaultBackColor;                                                          
                scanlineInfo.backColorIndex = defaultBackColorIndex;
                dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = scanlineInfo.backColor;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                erasure = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                erasure = false;
        }
    }
}
