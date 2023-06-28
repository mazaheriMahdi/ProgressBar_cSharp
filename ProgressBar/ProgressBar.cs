namespace ProgressBar;

public enum ProgressBarStyle
{
    Block,
    Arrow,
    EqualSign,
    Hash,
    PlusMinus,
    Star,
    Circle,
    Dot,

}

public enum SpinnerStyle
{
    Line,
    Circle,
    Dot,
    PlusMinus,
    Star,
    Arrow,
    Triangle,
    Squares,
    Brackets,
    HorizontalBars,
    CircleQuarter,
    CircleArc
}

public class ProgressBar
{
    private int _length;
    private int _maxValue;
    private int _minValue;
    private string _completedColor;
    private string _incompleteColor;
    private char[] _completedChars;
    private char[] _incompleteChars;
    private char[] _spinners;
    private int _spinnerIndex;
    private DateTime _startTime;

    public ProgressBar(int length, int maxValue, int minValue, string completedColor = "\u001b[42m", string incompleteColor = "\u001b[41m",
        ProgressBarStyle completedStyle = ProgressBarStyle.Block, ProgressBarStyle incompleteStyle = ProgressBarStyle.Block,
        SpinnerStyle spinnerStyle = SpinnerStyle.Line)
    {
        _length = length;
        _maxValue = maxValue;
        _minValue = minValue;
        _completedColor = completedColor;
        _incompleteColor = incompleteColor;
        _completedChars = GetCharacterStyle(completedStyle);
        _incompleteChars = GetCharacterStyle(incompleteStyle);
        _spinners = GetSpinnerStyle(spinnerStyle);
        _spinnerIndex = 0;
        _startTime = DateTime.Now;
    }

    private char[] GetCharacterStyle(ProgressBarStyle style)
    {
        switch (style)
        {
            case ProgressBarStyle.Block:
                return new char[] { '█' };
            case ProgressBarStyle.Arrow:
                return new char[] { '←', '↖', '↑', '↗', '→', '↘', '↓', '↙' };
            case ProgressBarStyle.EqualSign:
                return new char[] { '=' };
            case ProgressBarStyle.Hash:
                return new char[] { '#' };
            case ProgressBarStyle.PlusMinus:
                return new char[] { '+', '-' };
            case ProgressBarStyle.Star:
                return new char[] { '★' };
            case ProgressBarStyle.Circle:
                return new char[] { '●' };
            case ProgressBarStyle.Dot:
                return new char[] { '•' }; 
            default:
                return new char[] { '█' }; 
        }
    }

    private char[] GetSpinnerStyle(SpinnerStyle style)
    {
        switch (style)
        {
            case SpinnerStyle.Line:
                return new char[] { '|', '/', '-', '\\' };
            case SpinnerStyle.Circle:
                return new char[] { '◐', '◓', '◑', '◒' };
            case SpinnerStyle.Dot:
                return new char[] { '.', 'o', 'O', '0' };
            case SpinnerStyle.PlusMinus:
                return new char[] { '+', '×', '÷', '-' };
            case SpinnerStyle.Star:
                return new char[] { '✶', '✹', '✷', '✸' };
            case SpinnerStyle.Arrow:
                return new char[] { '←', '↖', '↑', '↗', '→', '↘', '↓', '↙' };
            case SpinnerStyle.Triangle:
                return new char[] { '▲', '►', '▼', '◄' };
            case SpinnerStyle.Squares:
                return new char[] { '■', '□', '▪', '▫' };
            case SpinnerStyle.Brackets:
                return new char[] { '┤', '┘', '┴', '└', '├', '┌', '┬', '┐' };
            case SpinnerStyle.HorizontalBars:
                return new char[] { '▌', '▄', '▀', '▐' };
            case SpinnerStyle.CircleQuarter:
                return new char[] { '◜', '◝', '◞', '◟' };
            case SpinnerStyle.CircleArc:
                return new char[] { '◷', '◶', '◵', '◴' };
            default:
                return new char[] { '|', '/', '-', '\\' }; // Default to line theme
        }
    }

    public void Update(int currentValue)
    {
        if (currentValue < _minValue || currentValue > _maxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(currentValue), $"The value must be between {_minValue} and {_maxValue}.");
        }

        float percentage = (float)(currentValue - _minValue) / (_maxValue - _minValue);
        int completedWidth = (int)(_length * percentage);
        int incompleteWidth = _length - completedWidth;

        string completedProgressBar = new string(_completedChars[0], completedWidth);
        string incompleteProgressBar = new string(_incompleteChars[0], incompleteWidth);

        string progressBar = $"{_completedColor}{completedProgressBar}\u001b[0m{_incompleteColor}{incompleteProgressBar}\u001b[0m";
        char spinner = _spinners[_spinnerIndex];

        _spinnerIndex = (_spinnerIndex + 1) % _spinners.Length;

        TimeSpan elapsedTime = DateTime.Now - _startTime;
        string elapsedTimeString = elapsedTime.ToString(@"hh\:mm\:ss");

        Console.Write("\r[{0}] {1}% {2} Elapsed Time: {3}", progressBar, (int)(percentage * 100), spinner, elapsedTimeString);
    }
}