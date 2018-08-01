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
            typeof(ET.Utility),
            typeof(ET.EtSocket),
    };

    [CSharpCallLua]
    public static List<Type> csharpCallLua = new List<Type>() {
            typeof(Action),
            typeof(ET.CSharpCallLuaAction.LuaAction),
            typeof(ET.CSharpCallLuaAction.ChannelAction),
            typeof(ET.CSharpCallLuaAction.ChannelActionByte),
            typeof(ET.CSharpCallLuaAction.ChannelActionEx),
            typeof(ET.CSharpCallLuaAction.ChannelActionInt),
            typeof(ET.CSharpCallLuaAction.ChannelActionObj),
        };

}
