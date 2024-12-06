using System.Collections;

namespace D1;

class Program
{
    static void Main(string[] args)
    {
        Star1();
        Star2();
    }

    private static void Star1()
    {
        int diff = 0;
        List<int> c1 = new List<int>();
        List<int> c2 = new List<int>();
        


        using FileStream fs = File.OpenRead("input.txt");
        using StreamReader sr = new StreamReader(fs);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? string.Empty;
            string[] numbers = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int.TryParse(numbers[0], out int num1);
            int.TryParse(numbers[1], out int num2);
            c1.Add(num1);
            c2.Add(num2);
        }
        
        c1.Sort();
        c2.Sort();

        for (int i = 0; i < c1.Count; i++)
        {
            diff += Math.Abs(c1[i] - c2[i]);
        }

        Console.WriteLine(diff);
    }

    private static void Star2()
    {
        int diff = 0;
        List<int> c1 = new List<int>();
        List<int> c2 = new List<int>();
        


        using FileStream fs = File.OpenRead("input.txt");
        using StreamReader sr = new StreamReader(fs);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? string.Empty;
            string[] numbers = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int.TryParse(numbers[0], out int num1);
            int.TryParse(numbers[1], out int num2);
            c1.Add(num1);
            c2.Add(num2);
        }
        
        foreach (int number in c1)
        {
            int count = c2.Count(x => x == number);
            diff += number * count;
        }
        
        Console.WriteLine(diff);
    }
}