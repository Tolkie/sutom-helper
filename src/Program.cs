using System.Text;
using System.Text.RegularExpressions;

namespace SutomHelper;

internal class Program
{
    private const string FilePath = "C:\\Users\\teo_o\\Documents\\Dev\\sutom-helper\\words\\pli07.txt";


    private static void Main(string[] args)
    {
        var words = ReadFileLinesToList(FilePath);

        while (true)
        {
            var input = GetInputFromPlayer();

            if (input == "EXIT")
            {
                break;
            }

            // regex
            var regexPattern = input.Replace("*", "[A-Z]{1}");

            // must contain letters
            var mustContainLetters = GetMustContainLetters();
            
            // must not contain letters
            var mustNotContainLetters = GetMustNotContainLetters();
            
            // wildcards positions
            var wildcardPositions = GetWildcardPositions(input);
            
            // newlyPlacedLetters
            // var newlyPlacedLetters = GetNewlyPlacedLetters(input, wildcardPositions);
            
            
            var possibleWords = words.Where(w => w.Length == input.Length && 
                                                 Regex.IsMatch(w, regexPattern) && 
                                                 WordContainsLetters(w, mustContainLetters) && 
                                                 WordDoesNotContainLetters(w, mustNotContainLetters));
            
            
            Console.WriteLine("Voici les mots que nous avons trouvé: \n");
            
            foreach (var variable in possibleWords)
            {
                Console.WriteLine(variable);
            }
        }
    }

    private static bool WordContainsLetters(string word, List<char> lettersToContain)
    {
        return lettersToContain.All(word.Contains);
    }
    
    private static bool WordDoesNotContainLetters(string word, List<char> lettersToNotContain)
    {
        return !lettersToNotContain.Any(word.Contains);
    }
    
    private static string GetInputFromPlayer()
    {
        Console.WriteLine("Tapez le mot actuel, avec des '*' pour n'importe quelle lettre., ou 'exit' pour quitter :");

        return Console.ReadLine()!.ToUpper();
    }
    
    private static List<char> GetMustContainLetters()
    {
        Console.WriteLine("Tapez les lettres qui doivent être contenues dans le mot final et qui ne sont pas encore bien placées :");

        return Console.ReadLine()!.ToUpper().ToList();
    }
    
    private static List<char> GetMustNotContainLetters()
    {
        Console.WriteLine("Tapez les lettres qui ne doivent pas être contenues dans le mot final :");

        return Console.ReadLine()!.ToUpper().ToList();
    }

    private static List<int> GetWildcardPositions(string input)
    {
        return Enumerable.Range(0, input.Length).Where(i => input[i] == '*').ToList();
    }

    private static List<char> GetNewlyPlacedLetters(string originalWord, List<int> wildcardsPositions)
    {
        return wildcardsPositions.Select(pos => originalWord[pos]).ToList();
    }
    
    static List<string> ReadFileLinesToList(string filePath)
    {
        var lines = new List<string>();
        using var reader = new StreamReader(filePath);

        while (reader.ReadLine() is { } line)
        {
            lines.Add(line);
        }
        return lines;
    }
}