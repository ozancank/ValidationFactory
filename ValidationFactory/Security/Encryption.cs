using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace ValidationFactory.Security;

public class Encryption : IEncryption, IDisposable
{
    private readonly string _privateKey = "2756661284931169";

    #region Utilty

    private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
    {
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, TripleDES.Create().CreateEncryptor(key, iv), CryptoStreamMode.Write))
        {
            var toEncrypt = Encoding.Unicode.GetBytes(data);
            cs.Write(toEncrypt, 0, toEncrypt.Length);
            cs.FlushFinalBlock();
        }

        return ms.ToArray();
    }

    private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
    {
        using var ms = new MemoryStream(data);
        using var cs = new CryptoStream(ms, TripleDES.Create().CreateDecryptor(key, iv), CryptoStreamMode.Read);
        using var sr = new StreamReader(cs, Encoding.Unicode);
        return sr.ReadToEnd();
    }

    #endregion Utilty

    public string EncryptText(string text, string privateKey = "")
    {
        if (string.IsNullOrEmpty(text) || text == "null") return string.Empty;
        if (string.IsNullOrEmpty(privateKey)) privateKey = _privateKey;

        using var provider = TripleDES.Create();
        provider.Key = Encoding.ASCII.GetBytes(privateKey[..16]);
        provider.IV = Encoding.ASCII.GetBytes(privateKey.Substring(8, 8));

        var encryptedBinary = EncryptTextToMemory(text, provider.Key, provider.IV);
        return Convert.ToBase64String(encryptedBinary);
    }

    public string DecryptText(string text, string privateKey = "")
    {
        try
        {
            if (string.IsNullOrEmpty(text) || text == "null")
                return string.Empty;

            if (string.IsNullOrEmpty(privateKey))
                privateKey = _privateKey;

            using var provider = TripleDES.Create();
            provider.Key = Encoding.ASCII.GetBytes(privateKey[..16]);
            provider.IV = Encoding.ASCII.GetBytes(privateKey.Substring(8, 8));

            var buffer = Convert.FromBase64String(text);
            return DecryptTextFromMemory(buffer, provider.Key, provider.IV);
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }

    public string HashCreate(string value, string salt)
    {
        var valueBytes = KeyDerivation.Pbkdf2(value, Encoding.UTF8.GetBytes(salt), KeyDerivationPrf.HMACSHA512, 10000, 256 / 8);
        return Convert.ToBase64String(valueBytes) + "æ" + salt;
    }

    public bool ValidateHash(string value, string salt, string hash)
        => HashCreate(value, salt).Split('æ')[0] == hash;

    public string GenerateSalt()
    {
        byte[] randomBytes = new byte[128 / 8];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private bool _disposed;
    ~Encryption()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if(disposing)
        {
            //
        }

        _disposed = true;
    }
}