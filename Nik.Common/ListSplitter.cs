namespace Nik.Common;

public sealed class ListSplitter : IListSplitter
{
    public List<List<T>> SplittByCount<T>(List<T> source, int count)
    {
        return source
           .Select((x, i) => new { Index = i, Value = x })
           .GroupBy(x => x.Index / count)
           .Select(x => x.Select(v => v.Value).ToList())
           .ToList();
    }
}