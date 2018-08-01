using CatLib.API.Network;
using System;

namespace ET {
    public static class CSharpCallLuaAction {
        public delegate void LuaAction(object obj);
        public delegate void ChannelAction(INetworkChannel channel);
        public delegate void ChannelActionEx(INetworkChannel channel, Exception ex);
        public delegate void ChannelActionObj(INetworkChannel channel, object obj);
        public delegate void ChannelActionInt(INetworkChannel channel, int count);
        public delegate void ChannelActionByte(INetworkChannel channel, byte[] bytes);
    }
}
