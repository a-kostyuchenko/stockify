using System.Security.Cryptography;

namespace Stockify.Common.Infrastructure.Encryption;

public static class Decryptor
{
    private const int KeySize = 256;
    private const int BlockSize = 128;

    public static string Decrypt(EncryptionResult encryptionResult)
    {
        byte[] key = Convert.FromBase64String(encryptionResult.Key);
        (byte[] iv, byte[] encryptedData) = encryptionResult.GetIVAndEncryptedData();

        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.BlockSize = BlockSize;
        aes.Key = key;
        aes.IV = iv;

        using ICryptoTransform decryptor = aes.CreateDecryptor();
        using var msDecrypt = new MemoryStream(encryptedData);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        try
        {
            return srDecrypt.ReadToEnd();
        }
        catch (CryptographicException ex)
        {
            throw new CryptographicException("Decryption failed", ex);
        }
    }
}
