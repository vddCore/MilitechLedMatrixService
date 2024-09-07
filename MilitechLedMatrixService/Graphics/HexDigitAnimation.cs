namespace MilitechLedMatrixService.Graphics;

public class HexDigitAnimation
{
    public enum State
    {
        Stationary,
        Rolling
    }

    private readonly HexDisplay _display;

    private readonly byte[] _displayDigits = new byte[4];
    private readonly State[] _animationStates = new State[4];
    private readonly DateTime[] _animationTriggerTimes = new DateTime[4];

    public HexDigitAnimation(HexDisplay display)
    {
        _display = display;
    }

    private async Task RollDigit(byte digit, Func<int> countFunc)
    {
        _animationStates[digit] = State.Rolling;
        var count = countFunc();

        if (digit == 0 || digit == 1)
        {
            _display[0]!.Invert = false;
            _display[1]!.Invert = false;
            await Task.Delay(125);
        }
        else if (digit == 2 || digit == 3)
        {
            _display[2]!.Invert = false;
            _display[3]!.Invert = false;
            await Task.Delay(125);
        }

        for (var i = 0; i < count; i++)
        {
            _displayDigits[digit] = (byte)Random.Shared.Next();
            _display[digit]!.Number = _displayDigits[digit];
            await Task.Delay(3 * i + 24);
        }

        _animationStates[digit] = State.Stationary;

        if (digit == 0 || digit == 1)
        {
            var animateExit = _animationStates[0] == State.Stationary
                           && _animationStates[1] == State.Stationary;

            if (animateExit)
            {
                _display[0]!.Invert = _display[1]!.Invert = false;
                await Task.Delay(70);

                _display[0]!.Invert = _display[1]!.Invert = true;
                await Task.Delay(100);

                _display[0]!.Invert = _display[1]!.Invert = false;
                await Task.Delay(100);

                _display[0]!.Invert = _display[1]!.Invert = true;
                await Task.Delay(120);
            }
        }
        else if (digit == 2 || digit == 3)
        {
            var animateExit = _animationStates[2] == State.Stationary
                           && _animationStates[3] == State.Stationary;

            if (animateExit)
            {
                _display[2]!.Invert = _display[3]!.Invert = false;
                await Task.Delay(70);

                _display[2]!.Invert = _display[3]!.Invert = true;
                await Task.Delay(100);

                _display[2]!.Invert = _display[3]!.Invert = false;
                await Task.Delay(100);

                _display[2]!.Invert = _display[3]!.Invert = true;
                await Task.Delay(120);
            }
        }
    }

    public void Update()
    {
        for (var i = 0; i < _animationStates.Length; i++)
        {
            var state = _animationStates[i];
            var triggerTime = _animationTriggerTimes[i];

            if (state == State.Stationary)
            {
                if (DateTime.Now > triggerTime)
                {
                    var taskIndex = i;
                    Task.Run(() =>
                    {
                        _ = RollDigit((byte)taskIndex, () => Random.Shared.Next(8, 48));

                        _animationTriggerTimes[taskIndex] = DateTime.Now + TimeSpan.FromSeconds(
                            ServiceConfig.IntervalBase * Random.Shared.Next(
                                ServiceConfig.IntervalMinimumFactor,
                                ServiceConfig.IntervalMaximumFactor
                            )
                        );
                    });
                }
            }
        }
    }

    public void Draw()
    {
        _display.Render();
    }
}