namespace ValidationFactory.Security;

public interface IEncryption
{
    string EncryptText(string text, string privateKey = "");

    string DecryptText(string text, string privateKey = "");

    string HashCreate(string value, string salt);

    bool ValidateHash(string value, string salt, string hash);

    string GenerateSalt();
}