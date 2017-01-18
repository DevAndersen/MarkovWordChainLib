using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovWordChainLib
{
    [Serializable]
    public class MarkovGen
    {
        private List<Word> words = new List<Word>();

        /// <summary>
        /// The Random object used in generating random sentences.
        /// </summary>
        public Random Rand { get; set; }

        /// <summary>
        /// New MarkovGen object, with a specified Random object and an empty word list.
        /// </summary>
        /// <param name="rand"></param>
        public MarkovGen(Random rand)
        {
            Rand = rand;
        }

        /// <summary>
        /// New MarkovGen object, with a random Random object and an empty word list.
        /// </summary>
        public MarkovGen() : this(new Random())
        {

        }

        /// <summary>
        /// New MarkovGen object, with a specified Random object and a word list generated from the IEnumerable of strings.
        /// </summary>
        public MarkovGen(Random rand, IEnumerable<string> wordStringCollection) : this(rand)
        {
            AddWords(wordStringCollection);
        }

        /// <summary>
        /// New MarkovGen object, with a specified Random object and a word list generated from the IEnumerable of strings.
        /// </summary>
        /// <param name="wordStringCollection"></param>
        public MarkovGen(IEnumerable<string> wordStringCollection) : this(new Random(), wordStringCollection)
        {

        }
        
        /// <summary>
        /// Add a word to the word list. If the word already exists, it will not be re-added, but the word's "Occurrences" variable will go up by one.
        /// </summary>
        /// <param name="wordString"></param>
        private Word AddWordFromString(string wordString)
        {
            Word word;
            Word foundWord = words.Find(x => x.TheWord == wordString);
            if (foundWord == null)
            {
                word = new Word(this, wordString);
                words.Add(word);
            }
            else
            {
                word = foundWord;
            }
            word.AddOccurrence();
            return word;
        }

        /// <summary>
        /// Adds strings as words to the word list.
        /// </summary>
        /// <param name="wordStringCollection"></param>
        public void AddWord(string wordString)
        {
            Word prevWord = words.Count > 0 ? words[words.Count - 1] : null;
            Word newWord = AddWordFromString(wordString);
            prevWord?.AddChildWord(newWord);
        }

        /// <summary>
        /// Adds an IEnumerable of strings as words to the word list.
        /// </summary>
        /// <param name="wordStringCollection"></param>
        public void AddWords(IEnumerable<string> wordStringCollection)
        {
            Word prevWord = words.Count == 0 ? null : words[words.Count - 1];
            wordStringCollection.ToList().ForEach(x =>
            {
                Word newWord = AddWordFromString(x);
                prevWord?.AddChildWord(newWord);
                prevWord = newWord;
            });
        }

        /// <summary>
        /// Returns a random Word from the word list. If no words exists, returns null.
        /// </summary>
        /// <returns></returns>
        public Word GetRandomWord()
        {
            if (words.Count == 0)
                return null;
            return words[Rand.Next(words.Count)];
        }

        /// <summary>
        /// Returns a copy of the word list, as an array. Note, that this is a copy, so editing it will not edit the actual word list.
        /// </summary>
        /// <returns></returns>
        public Word[] GetWords()
        {
            Word[] wordArray = new Word[words.Count];
            words.CopyTo(wordArray);
            return wordArray;
        }
    }
}