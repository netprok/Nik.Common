namespace Nik.Common.Abstractions;

public interface IStringFormatter
{
    string ToShortDateString(DateTime value);

    string ToDoubleString(double value);

    string GetSortableNow() => DateTime.Now.ToString("s").Replace(":", "-");

    string GetFolderViewDateNow() => DateTime.Now.ToString("u")[..10].Replace("-", "/");
}