namespace MilitechLedMatrixService.Graphics;

using Starlight.Framework;
using Starlight.Framework.Graphics;

public class HexDisplay
{
    private LedDisplay _ledDisplay = null!;
    private SingleDisplayRenderer _renderer = null!;
    private readonly HexDigitSegment[] _segments = new HexDigitSegment[4];
    
    public int Height { get; }

    public HexDigitSegment? this[byte index]
    {
        get
        {
            if (index >= _segments.Length)
                return null;

            return _segments[index];
        }
    }
    
    public HexDisplay()
    {
        Reset();

        Height = _ledDisplay.Height;

        _segments[0] = new HexDigitSegment(this, 0);
        _segments[1] = new HexDigitSegment(this, 8);
        _segments[2] = new HexDigitSegment(this, 17);
        _segments[3] = new HexDigitSegment(this, 25);
    }

    public void DrawHexString(string str, bool reverse)
    {
        try
        {
            _renderer.Clear();

            if (reverse)
            {
                _segments[3].Digit = str[0];
                _segments[2].Digit = str[1];
                _segments[1].Digit = str[2];
                _segments[0].Digit = str[3];
            }
            else
            {
                _segments[0].Digit = str[0];
                _segments[1].Digit = str[1];
                _segments[2].Digit = str[2];
                _segments[3].Digit = str[3];
            }
        }
        catch
        {
            Reset();
        }
    }

    public void DrawHexNumber(ushort number, bool reverse)
    {
        try
        {
            _renderer.Clear();

            var str = number.ToString("X4");
            DrawHexString(str, reverse);
        }
        catch
        {
            Reset();
        }
    }

    public void Render()
    {
        try
        {
            _segments[0].Draw(_renderer);
            _segments[1].Draw(_renderer);
            _segments[2].Draw(_renderer);
            _segments[3].Draw(_renderer);
            
            _renderer.PushFramebuffer();
        }
        catch
        {
            Reset();
        }
    }

    private void Reset()
    {
        var displays = LedDisplay.Enumerate();

        while (!displays.Any())
        {
            displays = LedDisplay.Enumerate();
        }

        _ledDisplay = displays.First();
        _ledDisplay.SetGlobalBrightness(64);
        _renderer = new SingleDisplayRenderer(_ledDisplay);
    }
}