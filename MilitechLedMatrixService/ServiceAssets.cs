namespace MilitechLedMatrixService;

using IronSoftware.Drawing;
using System.Reflection;

public class ServiceAssets
{
    private static AnyBitmap? _hexBitmapFont;

    private static Assembly ThisAssembly => Assembly.GetExecutingAssembly();
    
    public static AnyBitmap HexBitmapFont => _hexBitmapFont ?? LoadBitmapFont(
        "MilitechLedMatrixService.Resources.hex_glyphs.bmp", 
        ref _hexBitmapFont
    )!;

    private static AnyBitmap? LoadBitmapFont(string resourceKey, ref AnyBitmap? bitmapFont)
    {
        if (bitmapFont == null)
        {
            using (var stream = ThisAssembly.GetManifestResourceStream(resourceKey)!) 
                bitmapFont = new(stream, false);
        }
        
        return bitmapFont;
    }
}