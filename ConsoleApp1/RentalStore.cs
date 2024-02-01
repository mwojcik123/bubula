using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
//using System.Text.Json;
public class RentalStore
{
    private List<Customer> customers;
    private List<Movie> movies;
    private List<Rental> rentals;




    public RentalStore()
    {
        customers = LoadDataFromJson<List<Customer>>("customers.json"); ;
        movies = LoadDataFromJson<List<Movie>>("movies.json"); ;
        rentals = LoadDataFromJson<List<Rental>>("rentals.json");
    }

    public void AddCustomer()
    {
        Console.Write("Podaj imię klienta: ");
        string name = Console.ReadLine();
        Console.Write("Podaj adres klienta: ");
        string address = Console.ReadLine();

        customers.Add(new Customer(name, address));
        SaveDataToJson();
        Console.WriteLine("Dodano klienta.");
    }
    public void ShowCustomer()
    {
        Console.Write("Nasi Klienci:\n ");
        foreach (var item in customers)
        {
            Console.WriteLine(item.ID + ' ' +item.Name+ ' '+ item.Address + '\n');
        }
        
    }

    public void AddMovie()
    {
        Console.Write("Podaj tytuł filmu: ");
        string title = Console.ReadLine();
        Console.Write("Podaj gatunek filmu: ");
        string genre = Console.ReadLine();

        movies.Add(new Movie(title, genre));
        SaveDataToJson();
        Console.WriteLine("Dodano film.");
    }
    public void ShowMovies()
    {
        Console.Write("Nasze FILMY:\n ");
        foreach (var item in movies)
        {
            Console.WriteLine(item.ID + ' ' + item.Title + ' ' + item.Genre + '\n');
        }

    }

    public void RentMovie()
    {
        Console.Write("Podaj imię klienta: ");
        string customerID = Console.ReadLine();
        Console.Write("Podaj tytuł filmu do wypożyczenia: ");
        string movieID = Console.ReadLine();

        Customer customer = customers.Find(c => c.ID == customerID);
        Movie movie = movies.Find(m => m.ID == movieID);

        if (customer != null && movie != null)
        {
            rentals.Add(new Rental(customer, movie));
            SaveDataToJson();
            Console.WriteLine($"{customer.Name} wypożyczył(a) film: {movie.Title}");
        }
        else
        {
            Console.WriteLine("Nie można znaleźć klienta lub filmu.");
        }
    }
    public void DeployMovie()
    {
        Console.Write("Podaj id Wyportzyczenia: ");
        string rentalID = Console.ReadLine();
        Rental rental = rentals.Find(c => c.ID == rentalID);


        if (rental != null)
        {
            rental.Deploy = true;
    
            SaveDataToJson();
            Console.WriteLine($"{rental.Customer.Name} oddał(a) film: {rental.Movie.Title}");
        }
        else
        {
            Console.WriteLine("Nie można znaleźć wyporzyczenia o danym id.");
        }
    }

    public void DisplayRentals()
    {
        Console.WriteLine("\nAktualne wypożyczenia:");
        foreach (var rental in rentals)
        {
            Console.WriteLine($"IDWYP:{rental.ID} Klient: {rental.Customer.Name}, Film: {rental.Movie.Title}, Data wypożyczenia: {rental.RentalDate}");
        }
    }
    public void DisplayDeployRentals()
    {
        Console.WriteLine("\nOddane Wyporzyczenia:");
        foreach (var rental in rentals)
        {
            if (rental.Deploy == true)
            {
                Console.WriteLine($"IDWYP:{rental.ID} Klient: {rental.Customer.Name}, Film: {rental.Movie.Title}, Data wypożyczenia: {rental.RentalDate}");
            }
        }
    }
    public void SaveDataToJson()
    {
        SaveDataToJson("customers.json", customers);
        SaveDataToJson("movies.json", movies);
        SaveDataToJson("rentals.json", rentals);
    }

    private void SaveDataToJson<T>(string fileName, T data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(fileName, jsonData);
    }

    private T LoadDataFromJson<T>(string fileName)
    {
        string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + '\\'+ fileName ;
        if (File.Exists(filepath))
        {
            string jsonData = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filepath));
        }
        return default(T);
    }
}
