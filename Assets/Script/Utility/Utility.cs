using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET {
    public static class Utility {
        public static string LoadLuaFile(string fileName) {
            string _filePath;
#if UNITY_EDITOR
            _filePath = System.IO.Path.Combine(Application.dataPath + "/Lua/", fileName + ".lua");
            FileInfo _info = new FileInfo(_filePath);
            string _data = "";
            if (_info.Exists) {
                StreamReader r = new StreamReader(_filePath);
                _data = r.ReadToEnd();
                return _data;
            }
#else
            _filePath = "Lua/" + fileName + ".lua";
            TextAsset _s = Resources.Load(_filePath) as TextAsset;
            return _s.text;
#endif
            return null;
        }
    }
}

