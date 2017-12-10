using ET;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace ET {
    [CSharpCallLua]
    public class Luabehaviour : MonoBehaviour {
        public  delegate void LuaAction(object obj);
        
        private LuaAction m_luaStart;
        private LuaAction m_luaUpdate;
        private LuaAction m_luaOnDestroy;

        public string FilePath = "";

        private LuaTable m_luaTab;

        private void Awake() {
            initLuaFunc();
        }

        private void initLuaFunc() {
            object[] _v = LuaEvnBase.GetInstance().luaEnv.DoString(Utility.LoadLuaFile(FilePath), this.name);
            m_luaTab = _v[0] as LuaTable;
            m_luaTab.Set("gameObject", this.gameObject);
            m_luaTab.Get("start", out m_luaStart);
            m_luaTab.Get("update", out m_luaUpdate);
            m_luaTab.Get("onDestroy", out m_luaOnDestroy);
        }

        private void Start() {
            if (m_luaStart != null) {
#if UNITY_EDITOR
                LuaEvnBase.GetInstance().luaEnv.DoString(@"
                    local _proFiler = require 'perf.profiler'
                    _proFiler.start()");
                m_luaStart(m_luaTab);
                LuaEvnBase.GetInstance().luaEnv.DoString(@"
                    local _proFiler = require 'perf.profiler'
                    print(_proFiler.report())
                    _proFiler.stop()");
#else
                m_luaStart(m_luaTab);
#endif
            }

        }


        private void Update() {
            if (m_luaUpdate != null) {
                m_luaUpdate(m_luaTab);
            }
        }

        private void OnDestroy() {
            if (m_luaOnDestroy != null) {
                m_luaOnDestroy(m_luaTab);
            }
            m_luaStart = null;
            m_luaUpdate = null;
            m_luaOnDestroy = null;
            m_luaTab.Dispose();
        }

    }
}
