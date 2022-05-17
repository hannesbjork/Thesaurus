using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Thesaurus;

namespace Thesaurus
{

    public class CThesaurus : IThesaurus
    {

        // Collection of words, linked to synonym collection id
        internal IDictionary<string, int> Word_Dictionary = new Dictionary<string, int>();
        // Collection of IDs, linked to list of synonyms
        internal IDictionary<int, List<String>> Synonym_Dictionary = new Dictionary<int, List<String>>();
        
        // ID count
        internal int id = 0;


        public void AddSynonyms(IEnumerable<string> synonyms) {

            // Cleaning the input synonym strings
            IEnumerable<string> clean_synonyms = synonyms.Select(s => s.ToLower().Trim());
            clean_synonyms = clean_synonyms.Distinct();

            // Existing collection (of synonyms) index
            int collection_index = -1;
            
            // Refactoring variables
            Boolean collection_found_flag = false; // One synonym occurence found
            List<string> refactoring_list = new List<string>(); // Words in other than first synonym occurance

            // Loop over the new words to find existing occurances and refactor if needed
            foreach (string keyword in clean_synonyms)
            {

                if (Word_Dictionary.ContainsKey(keyword))
                {
                    if (!collection_found_flag)
                    {

                        // First existing synonym occurance found, using this as collection
                        collection_index = Word_Dictionary[keyword];
                        collection_found_flag = true;
                    }
                    else
                    {

                        // More than one existing synonym occurance found; refactor
                        int temp_index = Word_Dictionary[keyword];
                        foreach (string temp_word in Synonym_Dictionary[temp_index])
                        {
                            Word_Dictionary[temp_word] = temp_index;
                            refactoring_list.Add(temp_word);
                        }
                        Synonym_Dictionary.Remove(temp_index);
                    }
                }
            }

            // Handle total collection of synonyms
            if (collection_index == -1) 
            {
                collection_index = id;
                id++;
                Synonym_Dictionary.Add(collection_index, clean_synonyms.ToList());
            } 
            else 
            {
                List<string> newSynonyms = Synonym_Dictionary[collection_index];
                newSynonyms.AddRange(clean_synonyms.ToList());
                newSynonyms.AddRange(refactoring_list);

                Synonym_Dictionary[collection_index] = newSynonyms.Distinct().ToList();
            }

            // Add all new words to word dictionary
            foreach (string keyword in clean_synonyms)
            {

                if (!Word_Dictionary.ContainsKey(keyword))
                {

                    Word_Dictionary.Add(keyword, collection_index);
                }
            }

        }

        public IEnumerable<string> GetSynonyms(string word) {

            word = word.ToLower().Trim();

            if (Word_Dictionary.ContainsKey(word))
            {
                int collection_index = Word_Dictionary[word];
                return Synonym_Dictionary[collection_index];
            }
            throw new ArgumentException("Word not found in list of synonyms.");
        }

        public IEnumerable<string> GetWords() {
            
            // Return all words
            return Word_Dictionary.Keys;
        }
    }
}
