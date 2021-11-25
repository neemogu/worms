using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WormsBasic;

namespace WormsAdvanced {
    public class NameGenerator {
        class NameList
        {
            public string[] Names { get; set; }

            public NameList()
            {
                Names = Array.Empty<string>();
            }
        }

        private readonly Random _rand = new ();
        private List<string> _names;
        
        public NameGenerator()
        { 
            var list = new NameList();

            var serializer = new JsonSerializer();

            using (var reader = new StreamReader( ".\\names.json"))
            using (JsonReader jreader = new JsonTextReader(reader))
            {
                list = serializer.Deserialize<NameList>(jreader);
            }

            if (list != null) {
                _names = new List<string>(list.Names);
            }
        }
        
        public string Generate(ISet<string> existingNames) {
            var result = _names[_rand.Next(_names.Count)];
            if (existingNames.Count >= _names.Count) {
                var s = 1;
                while (existingNames.Contains(result)) {
                    result = result + "_" + s;
                    ++s;
                }
            } else {
                while (existingNames.Contains(result)) {
                    result = _names[_rand.Next(_names.Count)];
                }
            }
            return result;
        }
    }
}
