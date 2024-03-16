namespace Nik.Common;

public class RandomTextGenerator: IRandomTextGenerator
{
    private readonly Random random = new Random();
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";

    public string GenerateRandomText(int length)
    {
        StringBuilder builder = new();

        for (int i = 0; i < length; i++)
        {
            builder.Append(chars[random.Next(chars.Length)]);
        }

        return builder.ToString();
    }
}