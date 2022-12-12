using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    public GameObject[] DontDestory;

    public List<ETCButton> Attack;

    public ETCJoystick Joystick;

    public GameObject uiroot;

    void Start()
    {
#if CWSDK
        GameObject skdcallback = new GameObject("PhotoCallback");
        skdcallback.AddComponent<CWSdkCallBack>();
#endif


        for (int i = 0; i < DontDestory.Length; i++)
        {
            GameObject.DontDestroyOnLoad(DontDestory[i]);
        }
        GameSceneUtils.LoadSceneAsync("Lobby",()=> {
            JoyStickMgr.Instance.m_joyGO = DontDestory[0];
            JoyStickMgr.Instance.m_joystick = Joystick;
            JoyStickMgr.Instance.m_skillBtn = Attack;
            
            //配置数据解析
            GameData.Instance.InitByRoleName("Teddy");
            //任务配置表解析 C端
            GameData.Instance.InitTaskData();

            World.Instance.Init();
        });

    }

    public void onImagePath(string ret)
    {
        if (!ret.Equals("Fail"))
        {

            return;
        }
        Debug.LogError("打开失败");
    }

    void Update()
    {
        
    }
}
