

public class Movie
{
    static string GenerateRandomString()
    {
        Random rand = new Random();
        int length = 5;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] randomChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            randomChars[i] = chars[rand.Next(chars.Length)];
        }

        return new string(randomChars);
    }

    public string ID { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }

    public Movie(string title, string genre, string id = null)
    {
        Random rand = new Random();
        ID = id ?? GenerateRandomString();
        Title = title;
        Genre = genre;
    }
}
