namespace MilitechLedMatrixService;

using System.Drawing;
using System.Reflection;

public class ServiceAssets
{
    private static Bitmap? _hexBitmapFont;

    private static Assembly ThisAssembly => Assembly.GetExecutingAssembly();
    
    public static Bitmap HexBitmapFont => _hexBitmapFont ?? LoadBitmapFont(
        "MilitechLedMatrixService.Resources.hex_glyphs.bmp", 
        ref _hexBitmapFont
    )!;

    private static Bitmap? LoadBitmapFont(string resourceKey, ref Bitmap? bitmapFont)
    {
        if (bitmapFont == null)
        {
            using (var stream = ThisAssembly.GetManifestResourceStream(resourceKey)!) 
                bitmapFont = new(stream, false);
        }
        
        return bitmapFont;
    }
}