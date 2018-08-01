using System;
using CatLib.API.Network;


public class EtPacker : IPacker {
    public object Decode(byte[] source, out Exception ex) {
        ex = null;
        return source;
    }

    public byte[] Encode(object packet, out Exception ex) {
        ex = null;
        return new byte[12];
    }

    public int Input(byte[] source, out Exception ex) {
        ex = null;
        return 1;
    }
}
