using System.Collections.Generic;
using NUnit.Framework;
using WormsAdvanced;

namespace WormsTests {
    public class NameGeneratorTests {
        private NameGenerator _nameGenerator;
        
        [SetUp]
        public void Setup() {
            _nameGenerator = new NameGenerator();
        }

        [Test]
        public void UniqueNameTest() {
            const int totalNames = 10000;
            ISet<string> generatedNames = new HashSet<string>();
            for (var i = 0; i < totalNames; ++i) {
                generatedNames.Add(_nameGenerator.Generate(generatedNames));
            }
            Assert.AreEqual(totalNames, generatedNames.Count);
        }
    }
}