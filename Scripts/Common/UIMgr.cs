using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr :SingleTon<UIMgr>
{
    public GameObject m_uiroot;
    public GameObject m_hudroot;

    public Dictionary<string, UIbase> m_uiDic;


    public void Init(GameObject root,GameObject hud)
    {
        m_uiroot = root;
        m_hudroot = hud;
        m_uiDic = new Dictionary<string, UIbase>();
        m_uiDic.Add("Lobby", new Lobbysys());
        m_uiDic.Add("battle", new Battlesys());
        m_uiDic.Add("minimap", new MinimapSys());
        m_uiDic.Add("taskPanel", new TaskSys());

        Open("Lobby");
        Open("battle");
        Open("minimap");
        Open("taskPanel");
    }


    public void Open(string key)
    {
        UIbase ui;
        if (m_uiDic.TryGetValue(key,out ui))
        {
            ui.DoCreate(key);
        }
    }

    public void Close(string key)
    {
        UIbase ui;
        if (m_uiDic.TryGetValue(key, out ui))
        {
            ui.Destory();
        }
    }
}
