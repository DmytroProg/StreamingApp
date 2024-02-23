using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ScreenLib;

public static class ScreenHelper
{
    public static BitmapSource? GetScreenSource()
    {
        var bounds = Screen.PrimaryScreen.Bounds;
        using var bitmap = new Bitmap(bounds.Width, bounds.Height,
            System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        using var g = Graphics.FromImage(bitmap);
        g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(bitmap.Width, bitmap.Height));
        BitmapSource? bitmapSource = null;
        var hBitmap = bitmap.GetHbitmap();
        try
        {
            bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                            hBitmap,
                            IntPtr.Zero,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions());
        }
        catch(Exception)
        {
            bitmapSource = null;
        }
        finally
        {
            NativeMethods.DeleteObject(hBitmap);
        }

        return bitmapSource;
    }
}
