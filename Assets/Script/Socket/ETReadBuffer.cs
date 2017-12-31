using System;
using System.Collections;
using System.Collections.Generic;
using XLua;

namespace ET {
    public class ETReadBuffer {
        private static readonly ETReadBuffer m_instance = new ETReadBuffer();
        private byte[] m_data = new byte[ETMemoryStream.BUFFER_MAX];
        private byte[] m_readData = new byte[ETMemoryStream.BUFFER_MAX];
        private int m_readPos = 0;

        [CSharpCallLua]
        public delegate Action ReadCallLua(byte[] data);
        private ReadCallLua m_callLua;

        public byte[] data {
            get {
                return m_data;
            }
        }

        public void read(int len) {
            var _pos = m_readPos;
            for (int i = 0; i < len; ++i) {
                m_readData[_pos + i] = m_data[i];
                m_readPos += 1;
            }
            if (m_readPos >= 4) {
                var _len = new byte[2];
                var _serverPId = new byte[2];
                var _headSize = _len.Length + _serverPId.Length;
                Array.Copy(m_readData, _len, _len.Length);
                Array.Copy(m_readData, _len.Length, _serverPId, 0, _serverPId.Length);
                var _dataSzie = BitConverter.ToInt16(_len, 0);
                if (_dataSzie >= m_readPos) {
                    if (BitConverter.ToInt16(_serverPId, 0) == ETMemoryStream.randomId) {
                        var _data = new byte[_dataSzie - _headSize];
                        Array.Copy(m_readData, _headSize, _data, 0, _dataSzie - _headSize);
                        if (m_callLua != null) {
                            m_callLua(_data);
                        }
                        else {
                            m_callLua = LuaEvnBase.GetInstance().luaEnv.Global.Get<ReadCallLua>("recvCallBack");
                            if (m_callLua != null) {
                                m_callLua(_data);
                            }
                        }
                        m_readPos = 0;
                    }
                    else {
                        m_readPos = 0;
                    }
                }
            }
        }

        public static ETReadBuffer getInstance() {
            return m_instance;
        }
    }
}
