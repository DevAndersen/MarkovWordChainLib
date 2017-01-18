using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovWordChainLib
{
    [Serializable]
    public class Word
    {
        private List<Word> childWords = new List<Word>();
        private MarkovGen gen;
        private int occurrences = 0;

        /// <summary>
        /// The actual word.
        /// </summary>
        public string TheWord { get; }

        /// <summary>
        /// Number of occurrences of this word.
        /// </summary>
        public int Occurrences
        {
            get
            {
                return occurrences;
            }
        }

        protected internal Word(MarkovGen gen, string wordString)
        {
            TheWord = wordString;
            this.gen = gen;
        }

        /// <summary>
        /// Increases the number of occurrences by one.
        /// </summary>
        protected internal void AddOccurrence()
        {
            occurrences++;
        }

        /// <summary>
        /// Add a word (childWord) to the list of child words for this word.
        /// </summary>
        /// <param name="childWord"></param>
        /// <returns></returns>
        protected internal Word AddChildWord(Word childWord)
        {
            childWords.Add(childWord);
            return childWord;
        }

        /// <summary>
        /// Clear all child words.
        /// </summary>
        protected internal void ClearChildWords()
        {
            childWords.RemoveAll(x => true);
        }

        /// <summary>
        /// Returns a random child word. If no child words exists, returns null.
        /// </summary>
        /// <returns></returns>
        public Word GetRandomChildWord()
        {
            if (childWords.Count == 0)
                return null;
            return childWords[gen.Rand.Next(childWords.Count)];
        }

        /// <summary>
        /// Returns a copy of the child word list, as an array. Note, that this is a copy, so editing it will not edit the actual child word list.
        /// </summary>
        /// <returns></returns>
        public Word[] GetChildWords()
        {
            Word[] childWordArray = new Word[childWords.Count];
            childWords.CopyTo(childWordArray);
            return childWordArray;
        }

        /// <summary>
        /// Returns false if the list of child words is empty. Returns true if not.
        /// </summary>
        /// <returns></returns>
        public bool HasChildWords()
        {
            return childWords.Count > 0;
        }
    }
}