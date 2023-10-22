namespace Nik.Common;

public sealed class SHA256HashGenerator : IHashGenerator
{
    public string GetHashCode(string data)
    {
        byte[] hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(data));

        return Convert.ToHexString(hashValue);
    }

    public string GetHashCode(string data, string key)
    {
        HMACSHA256 sha256 = new(Encoding.ASCII.GetBytes(key));
        MemoryStream stream = new(Encoding.ASCII.GetBytes(data));
        byte[] hash = sha256.ComputeHash(stream);
        return ConvertToString(hash);
    }

    private static string ConvertToString(byte[] hash)
    {
        return hash.Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
    }
}