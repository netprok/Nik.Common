namespace Nik.Common;

public class AesEncryptor : IAesEncryptor
{
    public byte[] Encrypt(string plainText, AesOptions aesOptions)
    {
        if (string.IsNullOrEmpty(plainText))
            throw new Exception("Nothing to encrypt.");

        var (key, iv) = GetKeyAndIv(aesOptions);

        byte[] encrypted;

        using (Aes aes = Aes.Create())
        {
            aes.Padding = PaddingMode.ISO10126;
            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

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

    public string Decrypt(byte[] cipherText, AesOptions aesOptions)
    {
        if (cipherText == null || cipherText.Length <= 0)
            throw new Exception("Nothing to decrypt.");

        var (key, iv) = GetKeyAndIv(aesOptions);

        using (Aes aes = Aes.Create())
        {
            aes.Padding = PaddingMode.ISO10126;
            ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

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

    private (byte[], byte[]) GetKeyAndIv(AesOptions aesOptions)
    {
        if (string.IsNullOrWhiteSpace(aesOptions.Key))
            throw new Exception("The encryption key not found.");
        if (string.IsNullOrWhiteSpace(aesOptions.IV))
            throw new Exception("The encryption initialization vector not found.");

        return (Encoding.ASCII.GetBytes(aesOptions.Key), Encoding.ASCII.GetBytes(aesOptions.IV));
    }
}