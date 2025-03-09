using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AES_demonstration
{
    private static readonly string key = "1234567890123456"; 
    private static readonly string iv = "6543210987654321"; 
    public static string encrypt(string plainText)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            using (var msEncrypt = new MemoryStream())
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }
    public static string decrypt(string cipherText)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
    public static void Main()
    {
        Console.WriteLine("Enter a message to encrypt it: ")
        string ogtext = Console.ReadLine();
        Console.WriteLine($"Original: {ogtext}");
        string crypttext = encrypt(ogtext);
        Console.WriteLine($"Encrypted: {crypttext}");
        string decrypttext = decrypt(crypttext);
        Console.WriteLine($"Decrypted: {decrypttext}");
        Console.ReadKey();
    }
}
