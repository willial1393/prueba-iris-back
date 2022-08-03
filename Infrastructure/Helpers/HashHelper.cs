using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Helpers;

public static class HashHelper
{
    public static string MD5Hash(string input)
    {
        var bytes = Encoding.ASCII.GetBytes(input);
        using var md5 = MD5.Create();
        var hash = string.Join("", md5.ComputeHash(bytes).Select(x => x.ToString("X2")));
        return hash.ToLower();
    }
}