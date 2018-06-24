using System.Windows.Forms;

// Reading PhotoShop Color Swatch (aco) files using C#
// http://cyotek.com/blog/reading-photoshop-color-swatch-aco-files-using-csharp

namespace PhotoShopColorSwatchLoader
{
  internal class BufferedPanel : Panel
  {
    #region Public Constructors

    public BufferedPanel()
    {
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
    }

    #endregion
  }
}
