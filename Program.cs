using WordTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace WordTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<string> allWords = WordOccurence();
            ConcatWords(allWords);
            Console.ReadLine();
        }

        static IList<string> WordOccurence()
        {
            //Read in words from textfile
            string txt = File.ReadAllText("fileinput.txt");

            //Replaces all special characters with spaces
            txt = Regex.Replace(txt, "[^0-9A-Za-z]+", " ");

            //Splits all words by spaces and puts into list
            IList<string> allWords = txt.Split(' ');

            //Gets all words in list and pairs them with their occurence in list
            var output = allWords
                .GroupBy(x => x)
                .Select(y => new WordModel { Word = y.Key, WordOccurence = y.Count() })
                .OrderByDescending(z => z.WordOccurence)
                .ThenBy(z => z.Word);

            Console.WriteLine("List of Words In File:");

            //Prints all words 
            foreach (var word in output)
            {
                Console.WriteLine("Word: " + word.Word + $"({word.WordOccurence})");

            }
            //Returns list for next function to use
            return allWords;
        }

        static void ConcatWords(IList<string> allWords)
        {
            //Initialises new list
            List<string> shorterThan6 = new List<string>();

            foreach (var word in allWords)
            {
                //Puts word in list if less than 6 characters and more than 1
                if (word.Length < 6 && word.Length > 1)
                {
                    //Adds to list if not already contained, avoiding duplicates
                    if (!shorterThan6.Contains(word))
                        shorterThan6.Add(word);
                }
            }

            Console.WriteLine("Concatenated Words: ");

            foreach (var containingWord in shorterThan6)
            {
                List<bool> timesPresentList = new List<bool>();

                //Checks each word in list against every word in list
                foreach (var check in shorterThan6)
                {
                    //If word contains another word but not itself
                    if (check != containingWord)
                        timesPresentList.Add(containingWord.Contains(check));

                }
                //Counts no words contained in testing word
                var timesPresent = timesPresentList.Where(x => x).Count();

                //If it occurs more than once, write to console
                if (timesPresent > 1)
                    Console.WriteLine(containingWord);

            }


        }


    }
}
