namespace D4;

class Program
{
    static void Main(string[] args)
    {
        string input = File.ReadAllText("input.txt");
        string[] lines = input.Split("\n");
        string[][] mat = new string[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            mat[i] = new string[lines[i].Length];
            for (int j = 0; j < lines[i].Length; j++)
            {
                mat[i][j] = lines[i][j].ToString();
            }
        }

        Console.WriteLine($"Star1: {findWords(mat, "XMAS")}\nStar2: {findxMas(mat)}");
    }
    
//star1
    private static int findWords(string[][] mat, string word)
    {
        int ret = 0;
        for (int row = 0; row < mat.Length; row++)
        {
            for (int col = 0; col < mat[row].Length; col++)
            {
                if (mat[row][col] == word[0].ToString())
                {
                    ret += matchedDirections(mat, word, row, col);
                }
            }
        }

        return ret;
    }

    private static int matchedDirections(string[][] mat, string word, int row, int col)
    {
        int ret = 0;
        int[] xdir = { 1, 1, 0, -1, -1, -1, 0, 1 };
        int[] ydir = { 0, 1, 1, 1, 0, -1, -1, -1 };

        //Vsechny DIR
        for (int i = 0; i < xdir.Length; i++)
        {
            int matchChars = 0;

            //Vsechny Znaky
            for (int j = 0; j < word.Length; j++)
            {
                int newRow = row + ydir[i] * j;
                int newCol = col + xdir[i] * j;

                // Kontrola hranic
                if (newRow >= 0 && newRow < mat.Length && newCol >= 0 && newCol < mat[newRow].Length)
                {
                    if (mat[newRow][newCol] == word[j].ToString())
                    {
                        matchChars++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (matchChars == word.Length)
            {
                ret++;
            }
        }

        return ret;
    }
    
//star2
    private static int findxMas(string[][] mat)
    {
        int ret = 0;
        for (int row = 0; row < mat.Length; row++)
        {
            for (int col = 0; col < mat[row].Length; col++)
            {
                if (mat[row][col] == "A")
                {
                    ret += matchedXMas(mat, row, col);
                }
            }
        }

        return ret;
    }
    
    private static bool IsInBounds(string[][] mat, int row, int col)
    {
        return row >= 0 && row < mat.Length &&
               col >= 0 && col < mat[row].Length;
    }
    
    private static int matchedXMas(string[][] mat, int row, int col)
    {
        int ret = 0;
        int matchChars = 0;

        // Kontrola hranic
        if (IsInBounds(mat, row + 1, col + 1) && IsInBounds(mat, row - 1, col - 1) &&
            IsInBounds(mat, row + 1, col - 1) && IsInBounds(mat, row - 1, col + 1))
        {
            if (((mat[row + 1][col + 1] == "M" && mat[row - 1][col - 1] == "S") ||
                (mat[row + 1][col + 1] == "S" && mat[row - 1][col - 1] == "M")) &&
                ((mat[row + 1][col - 1] == "M" && mat[row - 1][col + 1] == "S") ||
                (mat[row + 1][col - 1] == "S" && mat[row - 1][col + 1] == "M")))
            {
                ret++;
            }
        }

        return ret;
    }
}