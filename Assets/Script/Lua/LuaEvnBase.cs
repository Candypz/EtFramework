using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace ET {

    public class LuaEvnBase {
        private static LuaEvnBase m_instance;
        private static readonly object m_lock = new object();

        private LuaEnv m_luaEnv;

        private LuaEvnBase() {
            m_luaEnv = new LuaEnv();
            m_luaEnv.DoString("package.path = package.path..';" + "Assets/Resources/Lua/?.lua.bytes;" + "Assets/Lua/?.lua'");
            m_luaEnv.DoString(Utility.LoadLuaFile("init"));
        }

        public LuaEnv luaEnv {
            get {
                return m_luaEnv;
            }
        }

        public static LuaEvnBase GetInstance() {
            if (m_instance == null) {
                lock (m_lock) {
                    if (m_instance == null) {
                        m_instance = new LuaEvnBase();
                    }
                }
            }
            return m_instance;
        }
    }
}