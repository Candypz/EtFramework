using CatLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CatLib.API.Socket;
using CatLib.API.Network;

namespace ET {
    public class EtSocket {
        public string IpAddr = "tcp://127.0.0.1:7777";
        public string ConnectName = "myConnect";

        private INetworkManager m_networkManager;
        private INetworkChannel m_channel;

        public CSharpCallLuaAction.ChannelAction onConnected;
        public CSharpCallLuaAction.ChannelActionEx onError;
        public CSharpCallLuaAction.ChannelActionObj onMessage;
        public CSharpCallLuaAction.ChannelAction onDisconnect;
        public CSharpCallLuaAction.ChannelActionEx onClosed;
        public CSharpCallLuaAction.ChannelActionByte onSent;
        public CSharpCallLuaAction.ChannelActionInt onMissHeartBeat;


        public INetworkChannel getChannel() {
            return m_channel;
        }

        private void register() {
            m_networkManager = App.Make<INetworkManager>();
            m_channel = m_networkManager.Make(IpAddr, new EtPacker(), ConnectName);
        }

        public EtSocket() {
            register();
        }

        public EtSocket(string ipAddr, string connectName) {
            IpAddr = ipAddr;
            ConnectName = connectName;
            register();
        }

        public void init() {
            if (onConnected != null) {
                m_channel.OnConnected += (from) => {
                    onConnected(from);
                };
            }

            if (onError != null) {
                m_channel.OnError += (from, ex) => {
                    onError(from, ex);
                };
            }

            if (onMessage != null) {
                m_channel.OnMessage += (from, obj) => {
                    onMessage(from, obj);
                };
            }

            if (onDisconnect != null) {
                m_channel.OnDisconnect += (from) => {
                    onDisconnect(from);
                };
            }

            if (onClosed != null) {
                m_channel.OnClosed += (from, ex) => {
                    onClosed(from, ex);
                };
            }

            if (onSent != null) {
                m_channel.OnSent += (from, obj) => {
                    onSent(from, obj);
                };
            }

            if (onMissHeartBeat != null) {
                m_channel.OnMissHeartBeat += (from, count) => {
                    onMissHeartBeat(from, count);
                };
            }

        }
    }
}
