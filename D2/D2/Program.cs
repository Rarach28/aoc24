namespace D2;

class Program
{
    static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using StreamReader sr = new StreamReader(fs);
        
        int safe1 = 0;
        int safe2 = 0;
        
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? string.Empty;
            string[] numbersStrings = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int[] numbers = Array.ConvertAll(numbersStrings, int.Parse);

            if (IsSafe(numbers))
            {
                safe1++;
                safe2++;
            }
            else if (CanBeSafe(numbers))
            {
                safe2++;
            }
        }

        Console.WriteLine($"Star1: {safe1}\nStar2: {safe2}");
    }
    
    private static bool IsSafe(int[] numbers)
    {
        bool descending = numbers[0] > numbers[1];

        for (int i = 0; i < numbers.Length - 1; i++)
        {
            if (descending)
            {
                if (numbers[i + 1] >= numbers[i] || (numbers[i] - numbers[i + 1] > 3))
                {
                    return false;
                }
            }
            else
            {
                if (numbers[i + 1] <= numbers[i] || (numbers[i + 1] - numbers[i] > 3))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool CanBeSafe(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            int[] modified = new int[numbers.Length - 1];
            int index = 0;

            for (int j = 0; j < numbers.Length; j++)
            {
                if (j != i) //skip 1
                {
                    modified[index] = numbers[j];
                    index++;
                }
            }

            if (IsSafe(modified))
            {
                return true;
            }
        }

        return false;
    }
}