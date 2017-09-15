using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class EtCopyLuaFile {
    [MenuItem("Et/Luafile/CopyLuaFile")]
    static void CopyLuaFile() {
        copyDir(Application.dataPath + "/Lua", Application.dataPath + "/Resources/Lua");
        Debug.Log("copy lua file over");
    }

    [MenuItem("Et/Luafile/RemoveLuaFile")]
    static void RemoveLuaFile() {
        if (Directory.Exists(Application.dataPath + "/Resources/Lua")) {
            Directory.Delete(Application.dataPath + "/Resources/Lua", true);
        }
    }

    private static void copyDir(string fromDir, string toDir) {
        if (Directory.Exists(toDir)) {
            Directory.Delete(toDir, true);
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
    }
}
