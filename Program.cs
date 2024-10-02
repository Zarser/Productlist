using System;
using System.Collections.Generic;
using System.Linq;

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

// Klass för att hantera produktlistan och logik
class ProduktLista
{
    private List<Produkt> produkter = new List<Produkt>();

    // Lägg till en produkt i listan
    public void LäggTillProdukt(Produkt produkt)
    {
        produkter.Add(produkt);
    }

    // Sortera produkterna efter pris och skriv ut listan
    public void VisaProdukter()
    {
        if (produkter.Count == 0)
        {
            Console.WriteLine("Ingen produkt tillagd.");
            return;
        }

        // Sortera listan efter pris
        var sorteradeProdukter = produkter.OrderBy(p => p.Pris).ToList();

        Console.WriteLine("\nProdukter (sorterat efter pris):");
        decimal totalPris = 0;

        // Visa produkterna och beräkna totalpris
        foreach (var produkt in sorteradeProdukter)
        {
            Console.WriteLine(produkt);
            totalPris += produkt.Pris;
        }

        // Skriv ut totalpris
        Console.WriteLine($"\nTotal pris: {totalPris:C}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Skapa en instans av ProduktLista
        ProduktLista produktLista = new ProduktLista();

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
                // Skapa en ny produkt och lägg till den i produktlistan
                Produkt produkt = new Produkt(kategori, namn, pris);
                produktLista.LäggTillProdukt(produkt);
            }
            else
            {
                Console.WriteLine("Ogiltigt pris. Försök igen.");
            }

            Console.WriteLine();
        }

        // Visa listan med produkter, sorterat och med totalpris
        produktLista.VisaProdukter();
    }
}
