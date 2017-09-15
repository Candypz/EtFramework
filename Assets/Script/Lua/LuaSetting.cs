using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public static class LuaSetting {
    [LuaCallCSharp]
    public static List<Type> luaCallCsList = new List<Type>() {
            typeof(Action),
            typeof(object),
            typeof(GameObject),

            typeof(ET.UGUIEventListen),
            typeof(ET.Luabehaviour),
    };

    [CSharpCallLua]
    public static List<Type> csharpCallLua = new List<Type>() {
            typeof(System.Action),
            typeof(ET.Luabehaviour.LuaAction),
        };

}
