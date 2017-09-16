using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace ET {
    public class EtEditorLuaFile {
        private static string m_luaFilePath = Application.dataPath + "/Resources/Lua";
        private static string m_assetLuaFilePath = Application.dataPath + "/Lua";

        [MenuItem("Et/Luafile/CopyLuaFile")]
        static void CopyLuaFile() {
            copyDir(m_assetLuaFilePath, m_luaFilePath);
            Debug.Log("copy lua file over");
        }

        [MenuItem("Et/Luafile/Clear")]
        static void clear() {
            if (Directory.Exists(m_luaFilePath)) {
                Directory.Delete(m_luaFilePath, true);
                AssetDatabase.DeleteAsset(m_luaFilePath.Substring(m_luaFilePath.IndexOf("Assets") + "Assets".Length));

                AssetDatabase.Refresh();
            }
        }

        private static void copyDir(string fromDir, string toDir) {
            if (Directory.Exists(toDir)) {
                Directory.Delete(toDir, true);
                AssetDatabase.DeleteAsset(toDir.Substring(toDir.IndexOf("Assets") + "Assets".Length));
            }
            Directory.CreateDirectory(toDir);

            if (!Directory.Exists(fromDir)) {
                Directory.CreateDirectory(fromDir);
            }

            string[] files = Directory.GetFiles(fromDir);
            foreach (string formFileName in files) {
                string fileName = Path.GetFileName(formFileName);
                string toFileName = Path.Combine(toDir, fileName);
                toFileName = toFileName.Replace(".lua", ".lua.bytes");
                File.Copy(formFileName, toFileName);
            }
            string[] fromDirs = Directory.GetDirectories(fromDir);
            foreach (string fromDirName in fromDirs) {
                string dirName = Path.GetFileName(fromDirName);
                string toDirName = Path.Combine(toDir, dirName);
                copyDir(fromDirName, toDirName);
            }
            AssetDatabase.Refresh();
        }
    }
}
