namespace Nik.Common.Abstractions;

public interface IAesEncryptor
{
    string Decrypt(byte[] cipherText, AESOptions aesSettings);

    byte[] Encrypt(string plainText, AESOptions aesSettings);
}