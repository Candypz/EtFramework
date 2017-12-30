using System.Collections;
using System.Collections.Generic;

namespace ET {
    public class ETMemoryStream {
        public const int BUFFER_MAX = 1460 * 4;
        public struct PackageHead {
            public int size;
            public int serverPid;
        }
        public static short randomId = 2819;
    }
}
