using System.Security.Cryptography;
using System.Text;
using Canvas.Application.Security;
using Konscious.Security.Cryptography;

namespace Canvas.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    private const int _saltSize = 16;
    private const int _hashSize = 32;
    private static readonly int _degreeOfParallelism =
        Math.Min(Environment.ProcessorCount, 4);
    private const int _iterations = 4;
    private const int _memorySize = 128 * 1024;

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_saltSize);
        var hash = HashInternal(password, salt);

        var combined = new byte[salt.Length + hash.Length];
        Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, combined, salt.Length, hash.Length);

        return $"argon2id$v1${Convert.ToBase64String(combined)}";
    }


    public bool Verify(string password, string storedHash)
    {
        var parts = storedHash.Split('$', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3 || parts[0] != "argon2id")
            throw new InvalidOperationException("Invalid password hash format");

        var bytes = Convert.FromBase64String(parts[2]);

        var salt = bytes[.._saltSize];
        var hash = bytes[_saltSize..];

        var computed = HashInternal(password, salt);

        return CryptographicOperations.FixedTimeEquals(hash, computed);
    }

    private byte[] HashInternal(string password, byte[] salt)
    {
        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = _degreeOfParallelism;
            argon2.Iterations = _iterations;
            argon2.MemorySize = _memorySize;
            return argon2.GetBytes(_hashSize);
        }
    }
}
