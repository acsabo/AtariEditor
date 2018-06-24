using System;
using System.Reflection;
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

    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
