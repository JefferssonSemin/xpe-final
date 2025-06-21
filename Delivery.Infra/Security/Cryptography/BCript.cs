using Delivery.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace Delivery.Infra.Security.Cryptography;

public class BCript: IPasswordEncripter
{
    public string Encrypt(string password) => BC.HashPassword(password);
    
    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}