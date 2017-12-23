using System.Collections;
using System.Collections.Generic;

namespace ET {
    public class ETWriteBuffer {
        private struct PackageHead {
            short size;
            short serverPid;
        }

        private static readonly ETWriteBuffer m_instance = new ETWriteBuffer();
        public byte[] m_data = new byte[ETMemoryStream.BUFFER_MAX];


        public static ETWriteBuffer getInstance() {
            return m_instance;
        }
        
    }
}

