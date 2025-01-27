using System.Security.Cryptography;

namespace Stockify.Common.Infrastructure.Encryption;

public static class Encryptor
{
    private const int KeySize = 256;
    private const int BlockSize = 128;

    public static EncryptionResult Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.BlockSize = BlockSize;

        aes.GenerateKey();
        aes.GenerateIV();

        byte[] encryptedData;

        using (ICryptoTransform encryptor = aes.CreateEncryptor())
        using (var msEncrypt = new MemoryStream())
        {
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            encryptedData = msEncrypt.ToArray();
        }

        var result = EncryptionResult.CreateEncryptedData(
            encryptedData,
            aes.IV,
            Convert.ToBase64String(aes.Key)
        );

        return result;
    }
}
