using Thesaurus;

namespace ThesaurusTests
{
    [TestClass]
    public class ThesaurusTests
    {
        [TestMethod]
        public void synonyms_HalfInserts()
        {
            List<string> first_half = new List<string>() { "fable", "allegory", "metaphor", "simile" };
            List<string> second_half = new List<string>() { "story", "tale", "fable" };
           
            List<string> expected = new List<string>() { "fable", "allegory", "metaphor", "simile", "story", "tale"};

            CThesaurus the = new CThesaurus();

            the.AddSynonyms(first_half);
            the.AddSynonyms(second_half);

            List<string> answer = the.GetSynonyms("fable").ToList();

            CollectionAssert.AreEquivalent(expected, answer);
        }

        [TestMethod]
        public void synonyms_NoDoubles()
        {
            List<string> double_words = new List<string>() { "fable", "allegory", "metaphor", "simile", "simile", "allegory"};

            List<string> expected = new List<string>() { "fable", "allegory", "metaphor", "simile" };

            CThesaurus the = new CThesaurus();

            the.AddSynonyms(double_words);

            List<string> answer = the.GetSynonyms("metaphor").ToList();

            CollectionAssert.AreEquivalent(expected, answer);
        }

        [TestMethod]
        public void synonyms_CapitalsAndSpaces()
        {
            List<string> misspelled_words = new List<string>() { " fable ", "aLlegORy", "metaphor", "simile"};

            List<string> expected = new List<string>() { "fable", "allegory", "metaphor", "simile" };

            CThesaurus the = new CThesaurus();

            the.AddSynonyms(misspelled_words);

            List<string> answer = the.GetSynonyms(" siMilE   ").ToList();

            CollectionAssert.AreEquivalent(expected, answer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Word not found in list of synonyms.")]
        public void synonyms_WordDoesNotExist()
        {
            List<string> words = new List<string>() { "fable", "allegory", "metaphor", "simile" };

            CThesaurus the = new CThesaurus();

            the.AddSynonyms(words);

            the.GetSynonyms("park");

        }

        [TestMethod]
        public void synonyms_Refactoring()
        {
            List<string> first_half = new List<string>() { "fable", "allegory", "tale" };
            List<string> second_half = new List<string>() { "metaphor", "simile", "story" };
            List<string> bridge = new List<string>() { "fable", "metaphor" };

            List<string> expected = new List<string>() { "fable", "allegory", "tale", "metaphor", "simile", "story" };

            CThesaurus the = new CThesaurus();

            the.AddSynonyms(first_half);
            the.AddSynonyms(second_half);

            List<string> ans1 = the.GetSynonyms("allegory").ToList();
            CollectionAssert.AreEquivalent(first_half, ans1);

            List<string> ans2 = the.GetSynonyms("story").ToList();
            CollectionAssert.AreEquivalent(second_half, ans2);


            the.AddSynonyms(bridge);
            
            List<string> ans3 = the.GetSynonyms("fable").ToList();
            CollectionAssert.AreEquivalent(expected, ans3);
        }

        [TestMethod]
        public void synonyms_Multiple()
        {

            List<string> syn1 = new List<string>() { "fable", "allegory", "tale", "metaphor", "simile", "story" };
            List<string> syn2 = new List<string>() { "prowess", "valor", "bravery", "courage" };
            List<string> syn3 = new List<string>() { "easy", "trivial", "fast", "soft", "relaxed" };

            CThesaurus the = new CThesaurus();

            the.AddSynonyms(syn1);
            the.AddSynonyms(syn2);
            the.AddSynonyms(syn3);

            List<string> ans1 = the.GetSynonyms("fable").ToList();
            List<string> ans2 = the.GetSynonyms("valor").ToList();
            List<string> ans3 = the.GetSynonyms("soft").ToList();

            CollectionAssert.AreEquivalent(syn1, ans1);
            CollectionAssert.AreEquivalent(syn2, ans2);
            CollectionAssert.AreEquivalent(syn3, ans3);
        }

    }
}