namespace Nik.Common;

public sealed class EnglishStringFormatter : IStringFormatter
{
    public string ToDoubleString(double value) => value.ToString(CultureInfo.InvariantCulture);

    public string ToShortDateString(DateTime value) => value.ToString("yyyy-MM-dd");
}