namespace Nik.Common.Abstractions;

public interface IEncryptor
{
    string Decrypt(byte[] cipherText);

    byte[] Encrypt(string plainText);
}