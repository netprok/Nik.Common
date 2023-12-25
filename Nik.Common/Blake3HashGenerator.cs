namespace Nik.Common;

public sealed class Blake3HashGenerator : IHashGenerator
{
    public string Generate(string data)
    {
        var hash = Blake3.Hasher.Hash(Encoding.UTF8.GetBytes(data));

        return hash.ToString();
    }

    public string Generate(string data, string key)
    {
        using var blake3 = Hasher.NewKeyed(Encoding.UTF8.GetBytes(key));
        blake3.UpdateWithJoin(Encoding.ASCII.GetBytes(data));
        var hash = blake3.Finalize();
        return hash.ToString();
    }
}