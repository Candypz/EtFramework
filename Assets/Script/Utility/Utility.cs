using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET {
    public static class Utility {
        private static string m_luaFilePath = "/Lua/";

        public static string LoadLuaFile(string fileName) {
            string _filePath;
            _filePath = System.IO.Path.Combine(Application.dataPath + m_luaFilePath, fileName + ".lua");
            FileInfo _info = new FileInfo(_filePath);
            string _data = "";
            if (_info.Exists) {
                StreamReader r = new StreamReader(_filePath);
                _data = r.ReadToEnd();
                return _data;
            }
            return null;
        }
    }
}

