using ET;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace ET {
    public class Luabehaviour : MonoBehaviour {
        [CSharpCallLua]
        public delegate void LuaAction();

        private LuaAction m_luaStart;
        private LuaAction m_luaUpdate;
        private LuaAction m_luaOnDestroy;

        

        public string FilePath = "view/first";

        private LuaTable m_luaTab;

        private void Awake() {
            initLuaFunc();
        }

        private void initLuaFunc() {
            m_luaTab = LuaEvnBase.GetInstance().luaEnv.NewTable();
            LuaTable meta = LuaEvnBase.GetInstance().luaEnv.NewTable();
            meta.Set("__index", LuaEvnBase.GetInstance().luaEnv.Global);
            m_luaTab.SetMetaTable(meta);
            meta.Dispose();
            m_luaTab.Set("self", this);
            LuaEvnBase.GetInstance().luaEnv.DoString(Utility.LoadLuaFile(FilePath), this.name, m_luaTab);
            //LuaEvnBase.GetInstance().luaEnv.DoString("require 'view/first'", this.name, m_luaTab);
            m_luaTab.Get("start", out m_luaStart);
            m_luaTab.Get("update", out m_luaUpdate);
            m_luaTab.Get("onDestroy", out m_luaOnDestroy);
        }

        private void Start() {
            if (m_luaStart != null) {
                LuaEvnBase.GetInstance().luaEnv.DoString(@"
                    local _proFiler = require 'perf.profiler'
                    _proFiler.start()", this.name, m_luaTab);
                m_luaStart();
                LuaEvnBase.GetInstance().luaEnv.DoString(@"
                    local _proFiler = require 'perf.profiler'
                    print(_proFiler.report())
                    _proFiler.stop()", this.name, m_luaTab);
            }
        }


        private void Update() {
            if (m_luaUpdate != null) {
                m_luaUpdate();
            }
        }

        private void OnDestroy() {
            if (m_luaOnDestroy != null) {
                m_luaOnDestroy();
            }
            m_luaStart = null;
            m_luaUpdate = null;
            m_luaOnDestroy = null;
            m_luaTab.Dispose();
        }

    }
}
