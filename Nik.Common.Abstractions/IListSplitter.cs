namespace Nik.Common.Abstractions;

public interface IListSplitter
{
    List<List<T>> SplittByCount<T>(List<T> source, int count);
}