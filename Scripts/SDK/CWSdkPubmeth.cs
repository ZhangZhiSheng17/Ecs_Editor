using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
#if CWSDK
static public class CWSdkPubmeth
{
    static ICWSDK _sdk;
    static public ICWSDK GetSdk
    {
        get
        {
            if (_sdk == null)
            {
#if UNITY_ANDROID
                _sdk = new CWAndroid();
#elif UNITY_IOS
                _sdk = new CWIos();
#else
                _sdk = new CWDef();
#endif
            }
            return _sdk;
        }
    }
    static public void InitSDK()
    {

    }
}

public interface ICWSDK
{
    void Init();
    void Login();
    void Pay(PayData data);

    void OpenPhoto();
}

public class CWAndroid : ICWSDK
{
    private AndroidJavaClass m_sdkClass = new AndroidJavaClass("com.example.vr.cameralibrary.man");
    void ICWSDK.Init()
    {
        m_sdkClass.Call("initSDK");
    }

    void ICWSDK.Login()
    {
        m_sdkClass.Call("login");
    }

    void ICWSDK.Pay(PayData data)
    {
        string json = JsonUtility.ToJson(data);
        m_sdkClass.Call("pay", json);
    }

    void ICWSDK.OpenPhoto()
    {
        m_sdkClass.Call("OpenPhoto");
    }
}

public class CWIos : ICWSDK
{
    [DllImport("__Internal")]
    public static extern void _Init();
    [DllImport("__Internal")]
    private static extern void _Login();
    [DllImport("__Internal")]
    private static extern void _Pay(string data);
    void ICWSDK.Init()
    {
        CWIos._Init();
    }

    void ICWSDK.Login()
    {
        CWIos._Login();
    }

    void ICWSDK.OpenPhoto()
    {
    }

    void ICWSDK.Pay(PayData data)
    {
        string json = JsonUtility.ToJson(data);
        CWIos._Pay(json);
    }
}

public class CWDef : ICWSDK
{
    void ICWSDK.Init()
    {
    }

    void ICWSDK.Login()
    {
    }

    void ICWSDK.OpenPhoto()
    {
    }

    void ICWSDK.Pay(PayData data)
    {
    }
}
#endif