using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CallOtherPlatform : MonoBehaviour {
    private static readonly CallOtherPlatform m_instance = new CallOtherPlatform();
    private AndroidJavaClass m_java;
    private AndroidJavaObject m_javaInstance;

    public string AndroidClassName = "com.text.cn";
    public string AndroidFuncName = "";

    public static CallOtherPlatform getInstance() {
        return m_instance;
    }

    private CallOtherPlatform() {
#if UNITY_ANDROID
        m_java = new AndroidJavaClass(AndroidClassName);
        m_javaInstance = m_java.GetStatic<AndroidJavaObject>("m_instance");
#endif
    }


#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern string iosCall(int commd, string str);
#endif

    public string Call(int commd, string str) {
#if UNITY_ANDROID
        return m_javaInstance.Call<string>(AndroidFuncName);
#elif UNITY_IOS
        return iosCall(commd, str);
#endif
    }
}
