using System.Collections;
using System.Collections.Generic;

namespace ET {
    public class Localize {
        private static readonly Localize m_instance = new Localize();

        private string m_language = "";
        private Dictionary<string, Dictionary<string, string>> m_dictionary;

        public Localize() {
            m_dictionary = new Dictionary<string, Dictionary<string, string>> ();
        }

        public static Localize getInstance() {
            return m_instance;
        }

        public string language {
            set {
                if (m_language != value) {
                    m_language = value;
                }
            }
            get {
                return m_language;
            }
        }

        public string value(string key) {
            if (m_dictionary[key] != null) {
                if (string.IsNullOrEmpty(m_dictionary[key][m_language])) {
                    return m_dictionary[key][m_language];
                }
            }
            return key;
        }

    }
}