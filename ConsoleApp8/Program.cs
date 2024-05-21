using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Obliczenia logarytmiczne");
            Console.WriteLine("2. Obliczenia ciągu");
            Console.WriteLine("3. Opuszczenie programu");
            int opcja;

            if (!int.TryParse(Console.ReadLine(), out opcja))
            {
                Console.WriteLine("Błąd.");
                continue;
            }

            switch (opcja)
            {
                case 1:
                    MenuLogarytmu();
                    break;
                case 2:
                    Test5();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Błąd");
                    break;
            }
        }
    }

    static void MenuLogarytmu()
    {
        Console.Clear();
        Console.WriteLine("Wybierz rodzaj obliczeń logarytmicznych:");
        Console.WriteLine("1. Obliczenia prostych logarytmów");
        Console.WriteLine("2. Obliczenia na logarytmach");
        Console.WriteLine("3. Obliczanie x z równania logarytmicznego");
        Console.WriteLine("4. Obliczanie x z logarytmu o podstawie x");

        int opcja2;
        if (!int.TryParse(Console.ReadLine(), out opcja2))
        {
            Console.WriteLine("Błąd");
            return;
        }

        switch (opcja2)
        {
            case 1:
                ObliczeniaLogarytmiczne();
                break;
            case 2:
                ObliczeniaCiagu();
                break;
            case 3:
                ObliczX();
                break;
            case 4:
                ObliczX2();
                break;
            default:
                Console.WriteLine("Błąd.");
                break;
        }
    }

    static void ObliczeniaLogarytmiczne()
    {
        Console.Clear();
        Console.WriteLine("Podaj działanie np. log_10(1000)");
        string input = Console.ReadLine().Trim();


        double wynik;
        if (!TryParseLogarithm(input, out wynik))
        {
            Console.WriteLine("Błąd");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Wynik {wynik}");
        Console.ReadLine();
    }

    static void ObliczeniaCiagu()
    {
        Console.Clear();
        Console.WriteLine("Podaj działanie np. log_10(1000)+log_10(100)");
        string input = Console.ReadLine().Trim();

        double wynik;
        if (!TryParseLogarithmOperation(input, out wynik))
        {
            Console.WriteLine("Błąd");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Wynik: {wynik}");
        Console.ReadLine();
    }

    static void ObliczX()
    {
        Console.Clear();
        Console.WriteLine("Podaj równanie np. log_7(x)=2");
        string input = Console.ReadLine().Trim();

        string[] parts = input.Split(new[] { "log_", "(", ")", "=" }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 3 && double.TryParse(parts[0], out double baseNumber) && double.TryParse(parts[2], out double result))
        {
            double x = Math.Pow(baseNumber, result);
            Console.WriteLine($"x = {x}");
        }
        else
        {
            Console.WriteLine("Błąd.");
        }
        Console.ReadLine();
    }

    static void ObliczX2()
    {
        Console.Clear();
        Console.WriteLine("Podaj równanie np. log_x(64)=2");
        string input = Console.ReadLine().Trim();

        string[] parts = input.Split(new[] { "log_", "(", ")", "=" }, StringSplitOptions.RemoveEmptyEntries);  //dzieli wzgeledem log itd

        if (parts.Length == 3 && double.TryParse(parts[1], out double number) && double.TryParse(parts[2], out double result))
        {
            double x = Math.Pow(number, 1 / result); // logika do sprawdzania
            Console.WriteLine($"x = {x}");
        }
        else
        {
            Console.WriteLine("Błąd.");
        }
        Console.ReadLine();
    }
    static bool TryParseLogarithm(string expression, out double result)
    {
        result = 0.0;
        string[] parts = expression.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries); //dzielimy wyrazenie zeby wyszedl logarytm  

        if (parts.Length != 2) // poprawnosc logarytmu
            return false;

        if (double.TryParse(parts[1], out double number)) // sprawdzenie czy 2 czesc log moze byc number
        {
            if (parts[0].StartsWith("log_", StringComparison.OrdinalIgnoreCase)) // sprawdzenie czy zaczyna sie od log
            {
                string[] baseAndNumber = parts[0].Split('_'); //dzieli na podstawe i liczbe
                if (baseAndNumber.Length != 2 || !double.TryParse(baseAndNumber[1], out double baseNumber)) // sprawdzenie czy jest 2 czesci
                    return false;

                result = Math.Log(number, baseNumber); // logika do sprawdzania
                return true;
            }

            if (parts[0].StartsWith("log", StringComparison.OrdinalIgnoreCase)) // sprawdzenie czy zaczyna sie od log
            {
                result = Math.Log10(number); // logika do sprawdzania
                return true;
            }
            }
        return false;
        }

    static bool TryParseLogarithmOperation(string expression, out double result)
    {
        result = 0.0;

        string[] parts = expression.Split(new[] { "+", "-", "*", "/" }, StringSplitOptions.RemoveEmptyEntries); //dzieli wyrazenie na czesci
        if (parts.Length != 2)
            return false;

        if (TryParseLogarithm(parts[0], out double first) && TryParseLogarithm(parts[1], out double second)) // sprawdzenie czy mozna zrobic logarytm
        {
            if (expression.Contains("+"))
                result = first + second;
            else if (expression.Contains("-"))
                result = first - second;
            else if (expression.Contains("*"))
                result = first * second;
            else if (expression.Contains("/"))
                result = first / second;

            return true;
        }
        return false;
        }


    static void Test5()
    {
        Console.Clear();
        Console.WriteLine("Podaj długosc ciągu:");

        if (!int.TryParse(Console.ReadLine(), out int length) || length < 1)
        {
            Console.WriteLine("Błąd");
            Console.ReadLine();
            return;
        }


        List<double> elements = new List<double>();
        for (int i = 0; i < length; i++)
        {
            Console.WriteLine($"Podaj {i + 1} element ciągu:");
            if (!double.TryParse(Console.ReadLine(), out double element))
            {
                Console.WriteLine("Błąd");
                Console.ReadLine();
                return;
            }

            elements.Add(element);
        }

        SequencePrint(elements);
        Console.ReadLine();

        }


    static void SequencePrint(List<double> elements)
    {
        string sequenceType = GetSequenceType(elements);
        string monotonicity = GetMonotonicity(elements);

        Console.WriteLine($"Typ ciągu: {sequenceType}");
        Console.WriteLine($"Monotoniczność: {monotonicity}");

        if (sequenceType == "arytmetyczny")
        {
            double commonDiff = CommonDiff(elements);
            Console.WriteLine($"Różnica: {commonDiff}");
        }
        else if (sequenceType == "geometryczny")
        {
            double commonRatio = CommonRatio(elements);
            Console.WriteLine($"Stała iloraz: {commonRatio}");
        }

        Console.WriteLine("Podaj numer elementu do obliczenia:");
        if (!int.TryParse(Console.ReadLine(), out int i) || i < 1 || i > elements.Count)
        {
            Console.WriteLine("Błąd");
            return;
        }

        double result = Elements(elements, i, sequenceType);
        Console.WriteLine($"Wynik: {result}");
    }

    static double Elements(List<double> elements, int i, string sequenceType)
    {
        if (sequenceType == "arytmetyczny")
        {
            double commonDiff = CommonDiff(elements);
            return CalculateArithmetic(elements[0], commonDiff, i);
        }
        else if (sequenceType == "geometryczny")
        {
            double commonRatio = CommonRatio(elements);
            return CalculateGeometric(elements[0], commonRatio, i);
        }
        else
        {
            return 0.0;
        }
    }

    static double CommonDiff(List<double> elements)
    {
        if (elements.Count < 2)
            return 0.0;

        return elements[1] - elements[0];
    }

    static double CommonRatio(List<double> elements)
    {
        if (elements.Count < 2)
            return 0.0;

        return elements[1] / elements[0];
    }

    static string GetSequenceType(List<double> elements)
    {

if (CheckArithmSeq(elements))
            return "arytmetyczny";
        else if (CheckGeom(elements))
            return "geometryczny";
        else
            return "nieokreślony";
    }

    static double CalculateGeometric(double firstTerm, double commonRatio, int n)
    {
            return firstTerm * Math.Pow(commonRatio, n - 1);
    }

    static double CalculateArithmetic(double firstTerm, double commonDiff, int n) // pierwsza liczba , roznica , index 
    {
        return firstTerm + commonDiff * (n - 1);
    }

    static bool CheckArithmSeq(List<double> elements)
    {
        double commonDiff = CommonDiff(elements);

        for (int i = 2; i <= elements.Count; i++)
        {
            if (elements[i - 1] != CalculateArithmetic(elements[0], commonDiff, i)) // porownanie wszystkiego z arytmetycznoscia
                return false;
        }

        return true;

    }

    static bool CheckGeom(List<double> elements)
    {
        double commonRatio = CommonRatio(elements);

        for (int i = 2; i <= elements.Count; i++)
        {
            if (elements[i - 1] != CalculateGeometric(elements[0], commonRatio, i)) // porownanie wszystkiego z geometrycznoscia
                return false;
        }

        return true;
    }

    static string GetMonotonicity(List<double> elements)
    {
        if (CheckArithmSeq(elements))
            return "rosnący";
        else if (CheckArithmSeq(elements))
            return "malejący";
        else
            return "nieokreślony";
    }

}