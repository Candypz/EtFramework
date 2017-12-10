using System.Collections;
using System.Collections.Generic;

namespace ET {
    public class ETWriteBuffer {
        private static readonly ETWriteBuffer m_instance = new ETWriteBuffer();
        public byte[] m_data = new byte[ETMemoryStream.BUFFER_MAX];

        public byte[] data {
            set {
                m_data = value;
                setCb();
            }
        }

        private void setCb() {

        }

        public static ETWriteBuffer getInstance() {
            return m_instance;
        }
        
    }
}

