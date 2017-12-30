using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

namespace ET {

    public class LuaEvnBase {
        private static readonly LuaEvnBase m_instance = new LuaEvnBase();

        private LuaEnv m_luaEnv;

        private LuaEvnBase() {
            m_luaEnv = new LuaEnv();
            m_luaEnv.AddBuildin("protobuf.c", XLua.LuaDLL.Lua.LoadProtobufC);
#if UNITY_EDITOR
            m_luaEnv.AddLoader((ref string filepath) => {
                filepath = Application.dataPath + "/Lua/" + filepath.Replace('.', '/') + ".lua";
                if (File.Exists(filepath)) {
                    return File.ReadAllBytes(filepath);
                }
                else {
                    return null;
                }
            });
#else
            m_luaEnv.AddLoader((ref string filepath) => {
                filepath = "Lua/" + filepath.Replace('.', '/') + ".lua";
                TextAsset file = (TextAsset)Resources.Load(filepath);
                if (file != null) {
                    return file.bytes;
                }
                else {
                    return null;
                }
            });
#endif
            m_luaEnv.DoString(Utility.LoadLuaFile("init"));
        }

        public LuaEnv luaEnv {
            get {
                return m_luaEnv;
            }
        }

        public static LuaEvnBase GetInstance() {
            return m_instance;
        }
    }
}