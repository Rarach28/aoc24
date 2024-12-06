namespace D3;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string input = File.ReadAllText("input.txt");
        
        string pattern = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)";
        Regex regex = new Regex(pattern);

        MatchCollection matches = regex.Matches(input);

        int result1 = 0;
        int result2 = 0;
        bool enabled = true;
        
        foreach (Match match in matches)
        {
            if (match.Value == "do()")
            {
                enabled = true;
            } else if (match.Value == "don't()")
            {
                enabled = false;
            }
            else
            {
                string[] parts = match.Value.Split(new [] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                int num1 = int.Parse(parts[1]);
                int num2 = int.Parse(parts[2]);
                result1 += num1 * num2;
                if (enabled)
                {
                    result2 += num1 * num2;
                }
            }
            
        }

        Console.WriteLine($"star1: {result1}\nstar2: {result2}");
    }
}