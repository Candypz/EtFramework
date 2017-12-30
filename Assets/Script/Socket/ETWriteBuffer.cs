using System;
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
            var _size = System.BitConverter.GetBytes((short)(data.Length + 4));
            var _randomId = System.BitConverter.GetBytes(ETMemoryStream.randomId);
            Array.Copy(_size, m_data, _size.Length);
            Array.Copy(_randomId, 0, m_data, _size.Length, _randomId.Length);
            Array.Copy(data, 0, m_data, _size.Length + _randomId.Length, data.Length);
            return m_data;
        }
        
    }
}

