namespace Nik.Common.Tests;

public sealed class HashTests
{
    [Fact]
    public void TestHash()
    {
        string data = "TestHashData";
        string key = Guid.NewGuid().ToString("N");

        Blake3HashGenerator blake3HashGenerator = new();
        SHA256HashGenerator sha256HashGenerator = new();

        var blake1 = blake3HashGenerator.Generate(data);
        var blake2 = blake3HashGenerator.Generate(data, key);
        var sha = sha256HashGenerator.Generate(data, key);
        blake1.Should().NotBeEmpty();
        sha.Should().NotBeEmpty();
        blake1.Should().NotBe(blake2);
    }
}