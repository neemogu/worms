using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

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
        
        public string Generate() {
            return _names[_rand.Next(_names.Count)];
        }
    }
}
