using System.Collections.Generic;
using System.IO;

namespace Boggle.Model
{
    public class Dictionary
    {
        public static HashSet<string> CreateDictionary(string wordListPath)
        {
            var wordList = new HashSet<string>();
            using (var file = new StreamReader(wordListPath))
            {
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine().Trim();
                    wordList.Add(line);
                }
            }

            return wordList;
        }
    }
}