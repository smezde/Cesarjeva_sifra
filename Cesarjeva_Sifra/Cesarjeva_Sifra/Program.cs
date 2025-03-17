using System;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Vnesite sporočilo: ");
        string sporocilo = Console.ReadLine();

        Console.Write("Vnesite vrednost premika: ");
        int premik = int.Parse(Console.ReadLine());

        // šifriranje sporočila
        string sifrirano = CezarSifra.Sifriraj(sporocilo, premik);
        Console.WriteLine("Šifrirano sporočilo: " + sifrirano);

        // dešifriranje sporočila
        string desifrirano = CezarSifra.Desifriraj(sifrirano, premik);
        Console.WriteLine("Dešifrirano sporočilo: " + desifrirano);

        // dodatna funkcija: dinamični premik, izračunan iz vsote Unicode vrednosti vhodnega sporočila
        int dinamicniPremik = CezarSifra.IzracunajDinamicniPremik(sporocilo);
        Console.WriteLine("Dinamični premik (izračunano iz sporočila): " + dinamicniPremik);
    }
}

public static class CezarSifra
{
    // definicija slovenske abecede (male in velike črke)
    private static readonly string malaAbeceda = "abcčdefghijklmnoprsštuvzž";
    private static readonly string velikaAbeceda = "ABCČDEFGHIJKLMNOPRSŠTUVZŽ";

    // funkcija za šifriranje sporočila
    public static string Sifriraj(string besedilo, int premik)
    {
        return ObdelajBesedilo(besedilo, premik);
    }

    // funkcija za dešifriranje sporočila
    public static string Desifriraj(string besedilo, int premik)
    {
        return ObdelajBesedilo(besedilo, -premik);
    }

    // skupna metoda za šifriranje in dešifriranje
    private static string ObdelajBesedilo(string besedilo, int premik)
    {
        string rezultat = "";
        foreach (char znak in besedilo)
        {
            // preveri, če je znak črka
            if (char.IsLetter(znak))
            {
                if (char.IsLower(znak))
                {
                    int indeks = malaAbeceda.IndexOf(znak);
                    if (indeks != -1)
                    {
                        // izračuna nov indeks z modulo operacijo za prelivanje
                        int novIndeks = (indeks + premik) % malaAbeceda.Length;
                        if (novIndeks < 0)
                        {
                            novIndeks += malaAbeceda.Length;
                        }
                        rezultat += malaAbeceda[novIndeks];
                    }
                    else
                    {
                        // če črka ni v naši definiciji, jo pustimo nespremenjeno
                        rezultat += znak;
                    }
                }
                else // velike črke
                {
                    int indeks = velikaAbeceda.IndexOf(znak);
                    if (indeks != -1)
                    {
                        int novIndeks = (indeks + premik) % velikaAbeceda.Length;
                        if (novIndeks < 0)
                        {
                            novIndeks += velikaAbeceda.Length;
                        }
                        rezultat += velikaAbeceda[novIndeks];
                    }
                    else
                    {
                        rezultat += znak;
                    }
                }
            }
            else
            {
                // presledki, ločila in ostali ne-črkovni znaki ostanejo nespremenjeni
                rezultat += znak;
            }
        }
        return rezultat;
    }

    // dodatna funkcija: izračun dinamičnega premika glede na vsoto Unicode vrednosti znakov v sporočilu
    public static int IzracunajDinamicniPremik(string vnos)
    {
        int vsota = 0;
        foreach (char znak in vnos)
        {
            vsota += (int)znak;
        }
        return vsota % malaAbeceda.Length;
    }
}
