using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
            Console.WriteLine(item.ID + ' ' +item.Name+ ' '+ item.Address );
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
    public void DrawChart()
    {
        int margin = 50;
        Bitmap image = new(1920, 1080);

        int columnWidth = (int)((image.Width - (2 * margin)) / (rentals.Count * 1.5f));
        int spacingWidth = columnWidth / 2;

        using (Graphics g = Graphics.FromImage(image))
        {
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, image.Width, image.Height));

            //Rysowanie osi
            Pen axisPen = new Pen(Color.Black);
            g.DrawLine(axisPen, new Point(margin, margin), new Point(margin, image.Height - 150));
            g.DrawLine(axisPen, new Point(margin, image.Height - 150), new Point(image.Width - margin, image.Height - 150));

            //Zliczanie wypożyczeń
            Dictionary<string, int> rentalCounts = new();
            foreach (Rental rental in rentals)
            {
                if (!rentalCounts.ContainsKey(rental.Movie.Title))
                {
                    rentalCounts.Add(rental.Movie.Title, 0);
                }
                rentalCounts[rental.Movie.Title]++;
            }

            //Wstępne obliczenia
            int x = margin + spacingWidth / 2;
            int axisY = image.Height - 150;
            int maxRentals = rentalCounts.Max(x => x.Value);
            int heightPerRental = (image.Height - margin - 150 - 20) / maxRentals;

            //Podpisywanie osi
            for (int i = 0; i <= maxRentals; i++)
            {
                int y = axisY - (i * heightPerRental) - 10;
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far; ;
                stringFormat.LineAlignment = StringAlignment.Center;
                g.DrawString(i.ToString(), new Font("Arial", 14, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF(0, y, margin - 2, 20), stringFormat);
            }

            //Rysowanie słupków
            foreach (KeyValuePair<string, int> movie in rentalCounts)
            {
                int height = heightPerRental * movie.Value;

                g.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(x, axisY - height, columnWidth, height));

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                g.DrawString(movie.Key, new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF(x - spacingWidth / 2, axisY + 10, columnWidth + spacingWidth, 130), stringFormat);
                x += columnWidth + spacingWidth;

            }
        }

        //Zapisanie wykresu
        image.Save("chart.bmp");

        //Uruchomienie wykresu
        string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\chart.bmp";
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.FileName = "powershell.exe";
        startInfo.Arguments = "explorer.exe " + filepath;
        process.StartInfo = startInfo;
        process.Start();
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
