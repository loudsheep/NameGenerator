namespace NameGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="Generator" />.
    /// </summary>
    internal class Generator
    {
        /// <summary>
        /// Defines the syllabes present in file with names.
        /// </summary>
        private Dictionary<string, int> syllabes = new Dictionary<string, int>();

        /// <summary>
        /// Defines the sorted syllabes.
        /// </summary>
        private Dictionary<string, int> sorted = new Dictionary<string, int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        public Generator()
        {
            //string[] names = File.ReadAllText("names.txt").Split("\n");
            Regex r = new Regex("[a-z][a-z]");

            string str;
            // for every name in file
            foreach (string name in File.ReadAllText("names.txt").Split("\n"))
            {
                // for every letter in name
                for (int i = 0; i < name.Length - 1; i++)
                {
                    // get pair of letters
                    str = (name[i].ToString() + name[i + 1]).ToLower();

                    // if pair doesn't fit into pattern continue
                    if (!r.IsMatch(str)) continue;

                    // if dict already contins this pair then increase count of this pair else add new pair to dict
                    if (syllabes.ContainsKey(str))
                    {
                        syllabes[str]++;
                    }
                    else
                    {
                        syllabes.Add(str, 1);
                    }
                }
            }

            // sort syllabes in ascending oreder and pack it to sorted dict
            sorted = syllabes.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Generates random name.
        /// </summary>
        /// <param name="minLen">The minLen<see cref="int"/>.</param>
        /// <param name="maxLen">The maxLen<see cref="int"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string RandomName(int minLen, int maxLen)
        {
            Random r = new Random();
            // Length of new name
            int len = r.Next(minLen - 1, maxLen - 1);

            // get random pair with last char as random vowel. vowel as second param makes name more pronounceable
            string res = GetCharLast(GetLetter());
            for (int i = 0; i < len - 1; i++)
            {
                // get random next letter
                res += GetCharFirst(res.Last());
            }
            return res;
        }

        /// <summary>
        /// Get random pair of letters from sorted with secified letter as first char
        /// </summary>
        /// <param name="first">The first<see cref="char"/>.</param>
        /// <returns>The <see cref="char"/>.</returns>
        private char GetCharFirst(char first)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < sorted.Count; i++)
            {
                if (sorted.ElementAt(i).Key[0] == first)
                {
                    dict.Add(sorted.ElementAt(i).Key, sorted.ElementAt(i).Value);
                }
            }

            int num = new Random().Next(dict.ElementAt(0).Value, dict.Last().Value);

            for (int i = 0; i < dict.Count; i++)
            {
                if (dict.ElementAt(i).Value > num)
                {
                    return dict.ElementAt(i).Key[1];
                }
            }

            return '\0';
        }

        /// <summary>
        /// Get random pair of letters from sorted with secified letter as last char
        /// </summary>
        /// <param name="last">The last<see cref="char"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetCharLast(char last)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < sorted.Count; i++)
            {
                if (sorted.ElementAt(i).Key[1] == last)
                {
                    dict.Add(sorted.ElementAt(i).Key, sorted.ElementAt(i).Value);
                }
            }

            int num = new Random().Next(dict.ElementAt(0).Value, dict.Last().Value);
            for (int i = 0; i < dict.Count; i++)
            {
                if (dict.ElementAt(i).Value > num)
                {
                    return dict.ElementAt(i).Key;
                }
            }

            return "a";
        }

        /// <summary>
        /// Get random vowel.
        /// </summary>
        /// <returns>The <see cref="char"/>.</returns>
        public char GetLetter()
        {
            string str = "aeiouy";
            int num = new Random().Next(0, str.Length);
            return str[num];
        }

        /// <summary>
        /// Noty uses. It's just for testing.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        internal static void Main(string[] args)
        {
            Generator g = new Generator();
            while (true)
            {
                Console.Write("Enter name size: ");
                try
                {
                    string[] arr = Console.ReadLine().Split(" ");
                    int num1 = Int32.Parse(arr[0]);
                    int num2 = Int32.Parse(arr[1]);
                    Console.WriteLine("Name: " + g.RandomName(num1, num2));
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect format");
                }
                Console.WriteLine();
            }
        }
    }
}
