namespace Nik.Common;

public class AESEncryptor : IEncryptor
{
    private byte[] Key { get; }
    private byte[] IV { get; }

    public AESEncryptor(IOptions<AESSettings> options)
    {
        AESSettings aesSettings = options.Value;

        if (string.IsNullOrWhiteSpace(aesSettings.Key))
            throw new Exception("The encryption key not found.");
        if (string.IsNullOrWhiteSpace(aesSettings.IV))
            throw new Exception("The encryption initialization vector not found.");

        Key = Encoding.ASCII.GetBytes(aesSettings.Key);
        IV = Encoding.ASCII.GetBytes(aesSettings.IV);
    }

    public byte[] Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            throw new Exception("Nothing to encrypt.");

        byte[] encrypted;

        using (Aes aes = Aes.Create())
        {
            aes.Padding = PaddingMode.ISO10126;
            ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }
                    encrypted = memoryStream.ToArray();
                }
            }
        }

        return encrypted;
    }

    public string Decrypt(byte[] cipherText)
    {
        if (cipherText == null || cipherText.Length <= 0)
            throw new Exception("Nothing to decrypt.");

        using (Aes aes = Aes.Create())
        {
            aes.Padding = PaddingMode.ISO10126;
            ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);

            using (MemoryStream memoryStream = new MemoryStream(cipherText))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}