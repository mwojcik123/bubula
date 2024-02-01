using System;

public class Rental

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
    public bool Deploy { get; set; }
    public Customer Customer { get; set; }
    public Movie Movie { get; set; }
    public DateTime RentalDate { get; set; }

    public Rental(Customer customer, Movie movie, bool deploy = false, string id = null)
    {
        ID = id ?? GenerateRandomString();
        Deploy = deploy;
        Customer = customer;
        Movie = movie;
        RentalDate = DateTime.Now;
    }
}
