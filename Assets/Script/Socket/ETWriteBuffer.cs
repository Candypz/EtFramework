using System.Collections;
using System.Collections.Generic;

namespace ET {
    public class ETWriteBuffer {
        private static readonly ETWriteBuffer m_instance = new ETWriteBuffer();
        public byte[] m_data = new byte[ETMemoryStream.BUFFER_MAX];


        public static ETWriteBuffer getInstance() {
            return m_instance;
        }

        public byte[] creatWriteBuffer(byte[] data) {
            var _size = System.BitConverter.GetBytes((short)(data.Length + 8));
            var _randomId = System.BitConverter.GetBytes(ETMemoryStream.randomId);
            for (int i = 0; i < _size.Length; ++i){
                m_data[i] = _size[i];
            }
            for (int i = 0; i< _randomId.Length; ++i) {
                m_data[i + _randomId.Length] = _randomId[i];
            }
            for (int i = 0; i < data.Length; ++i) {
                m_data[i + _size.Length + _randomId.Length] = data[i];
            }
            return m_data;
        }
        
    }
}

