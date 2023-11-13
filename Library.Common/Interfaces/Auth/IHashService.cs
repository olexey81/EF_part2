namespace Library.Common.Interfaces.Auth
{
    public interface IHashService
    {
        (byte[] hash, byte[] key) GetHash(string value, byte[]? key = null);
    }
}
