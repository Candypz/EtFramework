using System.Collections;
using System.Collections.Generic;

namespace ET {
    public class ETReadBuffer {
        private static readonly ETReadBuffer m_instance = new ETReadBuffer();
        private byte[] m_data = new byte[ETMemoryStream.BUFFER_MAX];
        private byte[] m_readData = new byte[ETMemoryStream.BUFFER_MAX];
        private int m_readPos = 0;

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
        }

        public static ETReadBuffer getInstance() {
            return m_instance;
        }
    }
}
