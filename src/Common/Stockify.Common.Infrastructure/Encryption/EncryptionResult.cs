namespace Stockify.Common.Infrastructure.Encryption;

public class EncryptionResult
{
    public string EncryptedData { get; set; }
    public string Key { get; set; }

    public static EncryptionResult CreateEncryptedData(byte[] data, byte[] iv, string key)
    {
        byte[] combined = new byte[iv.Length + data.Length];
        Array.Copy(iv, 0, combined, 0, iv.Length);
        Array.Copy(data, 0, combined, iv.Length, data.Length);

        return new EncryptionResult
        {
            EncryptedData = Convert.ToBase64String(combined),
            Key = key
        };
    }

    public (byte[] iv, byte[] encryptedData) GetIVAndEncryptedData()
    {
        byte[] combined = Convert.FromBase64String(EncryptedData);
        
        byte[] iv = new byte[16]; // AES block size is 16 bytes (128 / 8)
        byte[] encryptedData = new byte[combined.Length - 16];

        Array.Copy(combined, 0, iv, 0, 16);
        Array.Copy(combined, 16, encryptedData, 0, encryptedData.Length);

        return (iv, encryptedData);
    }
}
