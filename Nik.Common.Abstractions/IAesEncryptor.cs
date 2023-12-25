namespace Nik.Common.Abstractions;

public interface IAesEncryptor
{
    string Decrypt(byte[] cipherText, AesOptions aesOptions);

    byte[] Encrypt(string plainText, AesOptions aesOptions);
}