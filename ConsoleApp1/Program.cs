using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;

//using Newtonsoft.Json;
using System.Text.Json;
public class Program
{
    public static void Main()
	{
		RentalStore rs = new RentalStore();
        bool isWork = true;
        while (isWork)
        {
            Console.WriteLine("Proszê wybraæ opcjê:\n1 - dodaj klienta,\n2 - dodaj FILM,\n3 - Wyszukaj Klientów,\n4 - Wyszukaj film,\n5 - Wyporzycz Film,\n6 - Obecne Wyporzyczenia,\n7 - Oddane Wyporzyczenia,\n8 - Oddaj FILM.\n9 - WYKRES NAJCZÊŒCIEJ OGL¥DANYCH FILMÓW! \n10 - wyjdŸ.");
            string wybor = Console.ReadLine();
            switch (wybor)
            {
                case "1":
                    rs.AddCustomer();
                    break;
                case "2":
                    rs.AddMovie();
                    break;
                case "3":
                    rs.ShowCustomer();
                    break;
                case "4":
                    rs.ShowMovies();
                    break;
                case "5":
                    rs.RentMovie(); 
                    break;
                case "6":
                    rs.DisplayRentals(); 
                    break;
                case "7":
                    rs.DisplayDeployRentals();
                    break;
                case "8":
                    rs.DeployMovie();
                    break;
                case "9":
                    rs.DrawChart();
                    break;
                case "10":
                    isWork = false; 
                    break;
                default:
                    Console.WriteLine("\nProszê wybraæ poprawn¹ wartoœæ.\n");
                    break;
            }
        }


    }
    
}
