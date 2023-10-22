namespace Nik.Common.Abstractions;

public interface IHashGenerator
{
    string GetHashCode(string value);

    string GetHashCode(string data, string key);
}
