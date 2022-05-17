using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus
{
    /// <summary>
    /// Represents a thesaurus.
    /// </summary>
    public interface IThesaurus
    {
        /// <summary>
        /// Adds the given synonyms to the thesaurus
        /// </summary>
        /// <param name="synonyms">The synonyms to add.</param>
        void AddSynonyms(IEnumerable<string> synonyms);

        /// <summary>
        /// Gets the synonyms for a given word.
        /// </summary>
        /// <param name="word">The word the synonyms of which to get.</param>
        /// <returns>A <see cref="string"/> with all synonyms for the given word.</returns>
        IEnumerable<string> GetSynonyms(string word);

        /// <summary>
        /// Gets all words from the thesaurus.
        /// </summary>
        /// <returns>An <see cref="IEnumerable<string>"/> containing
        /// all the words in the thesaurus.</returns>
        IEnumerable<string> GetWords();
    }
}
