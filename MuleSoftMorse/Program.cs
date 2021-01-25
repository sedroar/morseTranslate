using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
class Solution
{

    static Dictionary<char, string> morseCode = new Dictionary<char, string>();
    static List<ContextEntry> context = new List<ContextEntry>();

    class ContextEntry
    {
        private readonly string morseCode;
        private readonly string word;

        public ContextEntry(string morseCode, string word)
        {
            this.morseCode = morseCode;
            this.word = word;
        }

        public string MorseCode
        {
            get
            {
                return morseCode;
            }
        }

        public string Word
        {
            get
            {
                return word;
            }
        }
    }

    static void loadMorseCode(string[] input, int morseCodeLastElement)
    {
        for (int i = 0; i <= morseCodeLastElement; i++)
        {
            string line = input[i].ToUpperInvariant().Trim();
            morseCode.Add(line.First(), line.Substring(line.LastIndexOf(" ") + 1));
        }
    }

    static void loadContext(string[] input, int contextFirstElementIndex, int contextLastElementIndex)
    {
        string line = "";
        try
        {
            for (int i = contextFirstElementIndex; i <= contextLastElementIndex; i++)
            {
                line = input[i].Trim().ToUpperInvariant();
                StringBuilder contextWordMorseCodeBuilder = new StringBuilder();
                for (int j = 0; j < line.Length; j++)
                {
                    contextWordMorseCodeBuilder.Append(morseCode[line[j]]);
                }
                context.Add(new ContextEntry(contextWordMorseCodeBuilder.ToString(), line));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(line);
            Console.WriteLine(ex.Message);
        }
    }

    static string findMatchingWord(string morseCode)
    {
        IEnumerable<ContextEntry> matchingWords = context.Where(ce => ce.MorseCode.Equals(morseCode));

        int matchingWordsCount = matchingWords.Count();

        if (matchingWordsCount == 1)
        {
            return matchingWords.First().Word;
        }

        if (matchingWordsCount > 1)
        {
            IEnumerable<ContextEntry> matchingWordsOrderedByLength = matchingWords.OrderBy(ce => ce.Word.Length);

            return matchingWordsOrderedByLength
                .First().Word + "!";

        }

        IEnumerable<ContextEntry> morseCodesWithSameStart = context.Where(ce => ce.MorseCode.StartsWith(morseCode));

        if (morseCodesWithSameStart.Count() >= 1)
        {
            return context.Where(ce => ce.MorseCode.StartsWith(morseCode)).FirstOrDefault()?.Word + "?";
        }

        return null;
    }

    static void translateCode(string[] input, int startIndex, int endIndex)
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            string line = input[i];
            string[] lineWords = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < lineWords.Length; j++)
            {
                string matchingWord = findMatchingWord(lineWords[j]);
                if (!string.IsNullOrEmpty(matchingWord))
                {
                    Console.WriteLine(matchingWord);
                }                
            }
        }
    }

    /*
     * Complete the function below.
     */
    static void doIt(string[] input)
    {
        int morseCodeLastElement = Array.IndexOf(input, "*") - 1;
        loadMorseCode(input, morseCodeLastElement);
        int lastContextElementIndex = Array.IndexOf(input, "*", morseCodeLastElement + 2) - 1;
        loadContext(input, morseCodeLastElement + 2, lastContextElementIndex);
        translateCode(input, lastContextElementIndex + 2, input.Length - 2);
        Console.ReadLine();
    }

    static void Main(String[] args)
    {

        int _input_size = 0;
        _input_size = Convert.ToInt32(Console.ReadLine());
        string[] _input = new string[_input_size];
        string _input_item;
        for (int _input_i = 0; _input_i < _input_size; _input_i++)
        {
            _input_item = Console.ReadLine();
            _input[_input_i] = _input_item;
        }

        doIt(_input);

    }
}