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
    public void VisaProdukter(List<string> highlights = null)
    {
        if (!produkter.Any())
        {
            Console.WriteLine("Ingen produkt tillagd.");
            return;
        }

        // Sortera listan efter pris med LINQ och visa
        var sorteradeProdukter = produkter.OrderBy(p => p.Pris).ToList();

        Console.WriteLine("\nProdukter (sorterat efter pris):");
        decimal totalPris = sorteradeProdukter.Sum(p => p.Pris);  // LINQ för att summera pris

        // Visa produkterna
        foreach (var produkt in sorteradeProdukter)
        {
            // Om det finns matchande produkter, markera dem
            if (highlights != null && highlights.Contains(produkt.Namn, StringComparer.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;  // Highlight in yellow
            }
            Console.WriteLine(produkt);
            Console.ResetColor();
        }

        // Skriv ut totalpris
        Console.WriteLine($"\nTotal pris: {totalPris:C}");
    }

    // Kontrollera om användaren vill fortsätta lägga till produkter
    public bool FortsättLäggaTill()
    {
        Console.Write("\nVill du lägga till fler produkter? (J/N): ");
        string input = Console.ReadLine();
        return input.Equals("J", StringComparison.OrdinalIgnoreCase);
    }

    // Sök efter produkter baserat på namn och returnera matchande namn
    public List<string> SökProdukter(string sökTerm)
    {
        var resultat = produkter
            .Where(p => p.Namn.Equals(sökTerm, StringComparison.OrdinalIgnoreCase))
            .Select(p => p.Namn)
            .ToList();

        return resultat;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Skapa en instans av ProduktLista
        ProduktLista produktLista = new ProduktLista();

        // Huvudloopen för att lägga till, söka och visa produkter
        do
        {
            LäggTillProdukter(produktLista);
            produktLista.VisaProdukter();

            // Fråga om användaren vill söka i listan
            if (VillSöka())
            {
                SökIProdukter(produktLista);
            }

        } while (produktLista.FortsättLäggaTill());

        Console.WriteLine("Programmet avslutades.");
    }

    // Funktion för att lägga till produkter
    static void LäggTillProdukter(ProduktLista produktLista)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\nSkriv in produktdetaljer eller 'Q' för att avsluta:");

                // Hämta kategori
                Console.Write("Kategori: ");
                string kategori = Console.ReadLine();
                if (Avsluta(kategori)) break;

                // Hämta produktnamn
                Console.Write("Namn: ");
                string namn = Console.ReadLine();
                if (Avsluta(namn)) break;

                // Hämta pris
                Console.Write("Pris: ");
                string prisInput = Console.ReadLine();
                if (Avsluta(prisInput)) break;

                // Försök att konvertera priset till decimal och hantera fel
                if (!decimal.TryParse(prisInput, out decimal pris))
                {
                    throw new ArgumentException("Ogiltigt pris. Ange ett giltigt numeriskt värde.");
                }

                // Skapa och lägg till produkten i listan
                Produkt produkt = new Produkt(kategori, namn, pris);
                produktLista.LäggTillProdukt(produkt);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Ett oväntat fel inträffade. Försök igen.");
            }

            Console.WriteLine();
        }
    }

    // Funktion för att kontrollera om användaren vill avsluta
    static bool Avsluta(string input)
    {
        return input.Equals("Q", StringComparison.OrdinalIgnoreCase);
    }

    // Funktion för att fråga om användaren vill söka
    static bool VillSöka()
    {
        Console.Write("\nVill du söka efter en produkt i listan? (J/N): ");
        string input = Console.ReadLine();
        return input.Equals("J", StringComparison.OrdinalIgnoreCase);
    }

    // Funktion för att söka efter produkter
    static void SökIProdukter(ProduktLista produktLista)
    {
        Console.Write("\nAnge produktnamn att söka efter: ");
        string sökTerm = Console.ReadLine();

        // Hämta matchande produkter från listan
        var resultat = produktLista.SökProdukter(sökTerm);

        if (resultat.Any())
        {
            // Om det finns matchande produkter, visa dem och markera dem
            Console.WriteLine($"\n{resultat.Count} match(er) hittades:");
            produktLista.VisaProdukter(resultat);
        }
        else
        {
            Console.WriteLine("Inga produkter matchade din sökning.");
        }
    }
}
