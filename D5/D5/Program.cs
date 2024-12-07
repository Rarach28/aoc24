namespace D5;

class Program
{
    static void Main(string[] args)
    {
        string data = File.ReadAllText("input.txt");
        string[] split = data.Split("\n\n");
        string[] rules = split[0].Split('\n');
        string[] pages = split[1].Split('\n');
        int star1 = 0, star2 = 0;
        
        Dictionary<int, List<int>> ruleDictionary = new Dictionary<int, List<int>>();
        foreach (var rule in rules)
        {
            string[] tmp = rule.Split('|');
            if (!ruleDictionary.ContainsKey(int.Parse(tmp[0])))
            {
                ruleDictionary.Add(int.Parse(tmp[0]), new List<int>());
            }
            ruleDictionary[int.Parse(tmp[0])].Add(int.Parse(tmp[1]));
        }

        foreach (var page in pages)
        {
            List<int> numbers = page.Split(',').Select(int.Parse).ToList();
            List<int> visited = new List<int>();
            bool correct = true;
            foreach (var num in numbers) // pro kazde cislo z toho řadku
            {
                
                // kontroluju jestli je v ruleDictionary cisel pred nim
                foreach (var vis in visited)
                {
                    if (ruleDictionary.ContainsKey(num) && ruleDictionary[num].Contains(vis))
                    {
                        correct = false;
                    }
                }
                visited.Add(num);
            }

            if (correct)
            {
                // Console.WriteLine($"Correct: {numbers} {numbers.Count-1}");
                // Console.WriteLine($"Correct: {numbers[(numbers.Count - 1) / 2 + (numbers.Count-1) % 2]}");
                star1 += numbers[(numbers.Count - 1) / 2 + (numbers.Count-1) % 2];
            }
            else
            {
                List<int> sorted = reorder(numbers, ruleDictionary);
                star2 += sorted[(sorted.Count - 1) / 2];
            }
        }

        Console.WriteLine($"Star 1: {star1}\nStar 2: {star2}");
    }
    
    static List<int> reorder(List<int> numbers, Dictionary<int, List<int>> rules)
    {
        // 0 = nenavstiveno, 1 = aktivni (v rekurzi), 2 = navstiveno
        Dictionary<int, int> visitStatus = numbers.ToDictionary(num => num, num => 0);
        
        List<int> result = new List<int>();
    
        // rekurzivniMagic do hloubky
        void rekurzivniMagic(int num)
        {
            if (visitStatus[num] == 2 || visitStatus[num] == 1) return;
            visitStatus[num] = 1;

            if (rules.ContainsKey(num))
            {
                foreach (var neighbor in rules[num])
                {
                    if (numbers.Contains(neighbor))
                    {
                        rekurzivniMagic(neighbor);
                    }
                }
            }
            
            visitStatus[num] = 2;
            result.Add(num);
        }

        foreach (var num in numbers)
        {
            if (visitStatus[num] == 0) // Not visited
            {
                rekurzivniMagic(num);
            }
        }

        //List je naopak tak ho otocim, ale nemusel bych
        result.Reverse();
        return result;
    }
}