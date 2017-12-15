using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET {
    public static class Utility {
        public static string LoadLuaFile(string fileName) {
            string _filePath;
#if UNITY_EDITOR
            _filePath = System.IO.Path.Combine(Application.dataPath + "/Lua/", fileName + ".lua");
            FileInfo _info = new FileInfo(_filePath);
            string _data = "";
            if (_info.Exists) {
                StreamReader r = new StreamReader(_filePath);
                _data = r.ReadToEnd();
                return _data;
            }
#else
            _filePath = "Lua/" + fileName + ".lua";
            TextAsset _s = Resources.Load(_filePath) as TextAsset;
            return _s.text;
#endif
            return null;
        }

        public static string removeExtension(string path) {
            string filename = path;
            int minPos = path.LastIndexOf('/');
            int maxPos = path.LastIndexOf('.');
            if (maxPos > minPos)
                filename = path.Remove(maxPos);
            return filename;
        }

        public static int getSystemState() {
            if (Application.internetReachability == NetworkReachability.NotReachable) {
                return 0;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork) {
                return 1;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork) {
                return 2;
            }
            return 0;
        }

        public static float getBatteryLevel() {
            return SystemInfo.batteryLevel;
        }

        public static int getBatteryState() {
            if (SystemInfo.batteryStatus == BatteryStatus.Charging) { //充电中
                return 1;
            }
            else if (SystemInfo.batteryStatus == BatteryStatus.NotCharging) { //未充电
                return 2;
            }
            else if(SystemInfo.batteryStatus == BatteryStatus.Full) { //满电
                return 3;
            }
            else if(SystemInfo.batteryStatus == BatteryStatus.Discharging) { //放电
                return 4;
            }
            else {
                return 0;
            }
        }
    }
}

