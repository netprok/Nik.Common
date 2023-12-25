namespace Nik.Common.Abstractions;

public interface IHashGenerator
{
    string Generate(string value);

    string Generate(string data, string key);
}
