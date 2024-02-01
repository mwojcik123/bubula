using System;

public class Customer
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
    public string Name { get; set; }
    public string Address { get; set; }

    // Konstruktor klasy Customer
    public Customer(string name, string address, string id = null)
    {
        ID = id ?? GenerateRandomString(); // Jeœli id jest null, generuj nowy losowy identyfikator
        Name = name;
        Address = address;
    }
}