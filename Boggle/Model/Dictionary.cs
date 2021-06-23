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
                var line = file.ReadLine();
                wordList.Add(line);
            }

            return wordList;
        }
    }
}