namespace Nik.Common;

public sealed class SHA256HashGenerator : IHashGenerator
{
    public string Generate(string data)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(data));

        return Convert.ToHexString(hash);
    }

    public string Generate(string data, string key)
    {
        HMACSHA256 sha256 = new(Encoding.ASCII.GetBytes(key));
        MemoryStream stream = new(Encoding.ASCII.GetBytes(data));
        byte[] hash = sha256.ComputeHash(stream);
        return Convert.ToHexString(hash);
    }
}