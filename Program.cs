using System;
using System.Collections.Generic;

// Klass för att representera en produkt
class Produkt
{
    public string Kategori { get; set; }
    public string Namn { get; set; }
    public decimal Pris { get; set; }

    // Konstruktor för att initiera produktens detaljer
    public Produkt(string kategori, string namn, decimal pris)
    {
        Kategori = kategori;
        Namn = namn;
        Pris = pris;
    }

    // Överskriver ToString för att visa produktinformation
    public override string ToString()
    {
        return $"Kategori: {Kategori}, Namn: {Namn}, Pris: {Pris:C}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Lista för att lagra produkter
        List<Produkt> produktLista = new List<Produkt>();

        while (true)
        {
            Console.WriteLine("Skriv in produktdetaljer eller 'Q' för att avsluta:");

            // Hämta kategori
            Console.Write("Kategori: ");
            string kategori = Console.ReadLine();
            if (kategori.Equals("Q", StringComparison.OrdinalIgnoreCase)) break;

            // Hämta produktnamn
            Console.Write("Namn: ");
            string namn = Console.ReadLine();
            if (namn.Equals("Q", StringComparison.OrdinalIgnoreCase)) break;

            // Hämta pris
            Console.Write("Pris: ");
            string prisInput = Console.ReadLine();
            if (prisInput.Equals("Q", StringComparison.OrdinalIgnoreCase)) break;

            // Försök att konvertera priset till decimal
            if (decimal.TryParse(prisInput, out decimal pris))
            {
                // Lägg till produkten i listan
                Produkt produkt = new Produkt(kategori, namn, pris);
                produktLista.Add(produkt);
            }
            else
            {
                Console.WriteLine("Ogiltigt pris. Försök igen.");
            }

            Console.WriteLine();
        }

        // Visa alla tillagda produkter
        Console.WriteLine("\nProdukter:");
        foreach (var produkt in produktLista)
        {
            Console.WriteLine(produkt);
        }
    }
}
