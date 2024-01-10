namespace Nik.Common.Abstractions;

public interface IAesEncryptor
{
    string Decrypt(byte[] cipherText, AesConfig aesOptions);

    byte[] Encrypt(string plainText, AesConfig aesOptions);
}