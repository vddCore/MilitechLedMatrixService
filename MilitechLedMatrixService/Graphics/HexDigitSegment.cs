namespace MilitechLedMatrixService.Graphics;

using Starlight.Framework.Graphics;

public class HexDigitSegment(HexDisplay hexDisplay, byte y)
{
    private const string Alphabet = "0123456789ABCDEF";
    
    private readonly HexDisplay _hexDisplay = hexDisplay;
    
    private byte _y = y;
    private char _digit = '0';

    public const byte Size = 9;
    
    public byte Y
    {
        get => _y;
        set
        {
            var height = _hexDisplay.Height;
            
            if (value > height - 10)
                value = (byte)(height - 10);

            _y = value;
        }
    }

    public char Digit
    {
        get => _digit;

        set
        {
            if (!Alphabet.Contains(value))
                value = '0';

            _digit = value;
        }
    }

    public byte Number
    {
        get => Convert.ToByte(Digit.ToString(), 16);
        set => Digit = value.ToString("X1")[0];
    }

    public bool Invert { get; set; }

    public void Draw(SingleDisplayRenderer renderer)
    {
        var bitmap = ServiceAssets.HexBitmapFont;
        var index = Alphabet.IndexOf(_digit);

        if (index < 0)
            return;

        var yOffset = index * Size;
        for (var ty = 0; ty < Size; ty++)
        {
            for (var tx = 0; tx < Size; tx++)
            {
                var pixel = bitmap.GetPixel(tx, yOffset + ty);
                
                var pixelValue = Invert 
                    ? pixel.R > 0 
                        ? (byte)0 
                        : (byte)255 
                    : pixel.R > 0 
                        ? (byte)255 
                        : (byte)0;

                renderer.DrawPixel(tx, _y + ty, pixelValue);
            }
        }
    }
}