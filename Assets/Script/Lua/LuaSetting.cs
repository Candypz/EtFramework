using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public static class LuaSetting {
    [CSharpCallLua]
    public static List<Type> luaCallCsList = new List<Type>() {
            typeof(System.Action),
            typeof(GameObject),

            typeof(ET.UGUIEventListen),
            typeof(ET.Luabehaviour),
    };

}
